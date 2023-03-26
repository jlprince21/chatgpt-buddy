using Microsoft.EntityFrameworkCore;
using OpenAI_API;
using OpenAI_API.Models;
using OpenAI_API.Chat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatGptBuddy
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var apiKey = Environment.GetEnvironmentVariable("CGB_API_KEY");
            var dbConnectionString = Environment.GetEnvironmentVariable("CGB_DB_CONNECTION");

            if (String.IsNullOrEmpty(apiKey) || String.IsNullOrEmpty(dbConnectionString))
            {
                Console.WriteLine("One or more environment variables aren't set. Exiting.");
                System.Environment.Exit(1);
            }

            DataContext dbContext = new DataContext();
            var client = new OpenAI_API.OpenAIAPI(apiKey);

            var chatId = await PromptAndResponseChat(client, dbContext);

            Console.Write("Enter tags with spaces or nothing to exit: ");
            var tags = Console.ReadLine();

            if (String.IsNullOrEmpty(tags) == false)
            {
                await ApplyTags(dbContext, chatId, tags);
            }

            Console.WriteLine("Goodbye");
        }

        static async Task<string> PromptAndResponseChat(OpenAIAPI client, DbContext dbContext)
        {
            var chat = client.Chat.CreateConversation();
            var chatId = Guid.NewGuid().ToString();
            var chatCounter = 0;

            for (int x = 0; x < 50; x++)
            {
                Console.Write($"\nEnter a prompt ({x}): ");
                var prompt = Console.ReadLine();

                if (String.IsNullOrEmpty(prompt) || prompt.ToUpper() == "EXIT")
                {
                    break;
                }

                WriteConversationEntry(dbContext, chatId, chatCounter++, Speaker.Human, prompt);
                chat.AppendUserInput(prompt);
                string response = await chat.GetResponseFromChatbot();

                Console.WriteLine($"Response ({x}): {response}");
                WriteConversationEntry(dbContext, chatId, chatCounter++, Speaker.ChatGPT, response.TrimStart());
            }

            return chatId;
        }

        static async Task<int> ApplyTags(DataContext dbContext, string chatId, string tags)
        {
            var tagsTokenized = tags.Split(' ').ToList();

            if (tagsTokenized == null || tagsTokenized.Count() <= 0)
            {
                return 2;
            }
            else
            {
                foreach(string candidate in tagsTokenized)
                {
                    var tagFindResult = (from tag in dbContext.Tags
                                    where tag.Text == candidate
                                    select tag).FirstOrDefault<Tag>();

                    if (tagFindResult == null)
                    {
                        var id = Guid.NewGuid().ToString();

                        // create tag
                        dbContext.Tags.Add(new Tag{ Id = id, Text = candidate});
                        dbContext.AppliedTags.Add(new AppliedTag {Id = Guid.NewGuid().ToString(), ChatId = chatId, TagId = id});
                        await dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        // tag is already in database
                        // search AppliedTags and only apply if not present

                        var appliedTagFindResult = (from appliedTag in dbContext.AppliedTags
                                    where appliedTag.TagId == tagFindResult.Id && appliedTag.ChatId == chatId
                                    select appliedTag).FirstOrDefault<AppliedTag>();

                        if (appliedTagFindResult == null)
                        {
                            Console.WriteLine("Applying tag");
                            dbContext.AppliedTags.Add(new AppliedTag{Id = Guid.NewGuid().ToString(), ChatId = chatId, TagId = tagFindResult.Id});
                            await dbContext.SaveChangesAsync();
                        }
                    }
                }
            }

            return 2;
        }

        static void WriteConversationEntry(DbContext dbContext, string chatId, int sequence, Speaker speaker, string text)
        {
            dbContext.Add(new ConversationEntry { ChatId = chatId, Sequence = sequence, EventTime = DateTime.UtcNow, Speaker = speaker, Text = text });
            dbContext.SaveChanges();
        }

        static async Task<ChatResult?> ChatCompletionDemo(OpenAIAPI api)
        {
            var result = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
            {
                Model = Model.ChatGPTTurbo,
                Temperature = 0.1,
                MaxTokens = 1000,
                Messages = new ChatMessage[] {
                    new ChatMessage(ChatMessageRole.User, "Provide a list of what you think are the 10 best songs of all time."),
                    new ChatMessage(ChatMessageRole.User, "Make the list of songs only from 2010 or newer and list each year they were released."),
                }
            });

            Console.WriteLine(result);

            return result; // Return some result
        }
    }
}
