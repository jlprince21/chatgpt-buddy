using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using OpenAI_API;
using OpenAI_API.Models;
using OpenAI_API.Chat;
using System.Threading.Tasks;

namespace ChatGptBuddy
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Replace with your API key here
            string apiKey = "API KEY GOES HERE";

            DataContext dbContext = new DataContext();

            var client = new OpenAI_API.OpenAIAPI(apiKey);

            await PromptAndResponseChat(client, dbContext);

            // await PerformAsyncOperation(client);
            // Console.ReadLine();
        }

        static async Task<int> PromptAndResponseChat(OpenAIAPI client, DbContext dbContext)
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
                WriteConversationEntry(dbContext, chatId, chatCounter++, Speaker.ChatGPT, response);
            }

            // foreach (ChatMessage msg in chat.Messages)
            // {
            //     Console.WriteLine($"{msg.Role}: {msg.Content}");
            // }

            return 2;
        }

        static void WriteConversationEntry(DbContext dbContext, string chatId, int sequence, Speaker speaker, string text)
        {
            dbContext.Add(new ConversationEntry { ChatId = chatId, Sequence = sequence, EventTime = DateTime.UtcNow, Speaker = speaker, Text = text });
            dbContext.SaveChanges();
        }

        static async Task<ChatResult?> PerformAsyncOperation(OpenAIAPI api)
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
