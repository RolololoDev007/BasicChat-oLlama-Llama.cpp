using OllamaSharp;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    private static readonly HttpClient client = new HttpClient();
    static readonly string Host = PedirDireccionIp();

    static async Task Main(string[] args)
    {
        Console.CancelKeyPress += async (sender, e) =>
        {
            e.Cancel = true;
        };
        
        await MainMenu();
        
    }

    static async Task MainMenu()
    {
        Console.Write("Select an IA Model (1-2):\n1 - oLlama\n2 - llama.cpp\nOption Selected: ");
        var opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                await OLlama();
                break;
            case "2":
                await LlamaCPP();
                break;
            default:
                Console.WriteLine("Invalid Option.");
                await MainMenu();
                break;
        }
    }

    private static string PedirDireccionIp()
    {
        Console.Write("Enter your server's IP address: ");
        var ip = Console.ReadLine();
        return ip;
    }

    #region Llama.cpp
    public class LlamaResponse
    {
        public string content { get; set; }
    }
    static async Task<string> EnviarPromptAsync(string prompt)
    {
        var url = $"http://{Host}:6589/completions";

        var payload = new
        {
            prompt = prompt,
            n_predict = 9999,
            temperature = 0.7
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(url, content);
        var responseText = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error {response.StatusCode}: {responseText}");
            throw new HttpRequestException($"Error {response.StatusCode}");
        }

        var llamaResponse = JsonSerializer.Deserialize<LlamaResponse>(responseText);
        return llamaResponse?.content ?? "Error al interpretar la respuesta.";
    }
    static async Task LlamaCPP()
    {
        Console.WriteLine("\nLLaMA.cpp Chat\n");

        while (true)
        {
            Console.Write("User Question: ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Going back to the selection menu...\n");
                await MainMenu();
                return;
            }

            string entrada = "<s>[INST]" + input + "[/INST]";
            string respuesta = await EnviarPromptAsync(entrada);

            Console.WriteLine($"LLaMA Answer: {respuesta}");
        }
    }
    #endregion

    #region oLlama
    public static async Task OLlama()
    {
        var uri = new Uri($"http://{Host}:11434");
        var client = new OllamaApiClient(uri);
        var models = (await client.ListLocalModelsAsync()).ToList();

        Console.WriteLine("\nAvailable models:\n");
        for (int i = 0; i < models.Count; i++)
            Console.WriteLine($"·{models[i].Name}");

        Console.Write("\nWrite the model LLM name: ");
        var modelName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(modelName))
        {
            Console.WriteLine("Model name can't be empty.");
            return;
        }

        client.SelectedModel = modelName;
        var chat = new Chat(client);

        Console.WriteLine($"\noLlama's Chat with model '{modelName}'\n");

        while (true)
        {
            Console.Write("\nUser Question: ");
            var prompt = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(prompt))
            {
                Console.WriteLine("Going back to the selection menu...\n");
                await MainMenu();
                return;
            }

            Console.WriteLine("\noLlama Answer: ");
            await foreach (var token in chat.SendAsync(prompt))
                Console.Write(token);

            Console.WriteLine();
        }
    }
    #endregion
}
