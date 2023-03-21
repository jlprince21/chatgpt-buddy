using System;
using System.Collections.Generic;
using OpenAI_API;
using OpenAI_API.Models;
using OpenAI_API.Chat;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Replace with your API key here
        string apiKey = "API KEY GOES HERE";

        var client = new OpenAI_API.OpenAIAPI(apiKey);

        await PromptAndResponseChat(client);

        // await PerformAsyncOperation(client);
        // Console.ReadLine();
    }

    static async Task<int> PromptAndResponseChat(OpenAIAPI client)
    {
        var chat = client.Chat.CreateConversation();

        for (int x = 0; x < 5; x++)
        {
            Console.WriteLine("Enter a prompt: ");
            var prompt = Console.ReadLine();

            chat.AppendUserInput(prompt);
            string response = await chat.GetResponseFromChatbot();

            Console.WriteLine($"Response: {response}");
        }

        foreach (ChatMessage msg in chat.Messages)
        {
            Console.WriteLine($"{msg.Role}: {msg.Content}");
        }

        return 2;
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
