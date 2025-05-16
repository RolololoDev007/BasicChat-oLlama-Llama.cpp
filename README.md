> 🧠 Make use of your own LLM chatbot locally quickly and easily with just .NET and Ollama!

# BasicChat-oLlama-Call.cpp

BasicChat-oLlama-Llama.cpp is a simple console application that integrates the LLaMA language models via the [`llama.cpp`](https://github.com/ggerganov/llama.cpp) or [`Ollama`](https://ollama.com) library. This tool allows a basic conversation with an LLM model to be held locally, without the need for an internet connection or external services.

## 🧠 What is LLaMA?

LLaMA (Large Language Model Meta AI) is a family of language models developed by Meta. Thanks to projects like [`llama.cpp`](https://github.com/ggerganov/llama.cpp) or [`Ollama`](https://ollama.com), it is possible to run them locally using only the CPU.

---

## 🚀 Features

- Console application written in **C# (.NET)**.
- Connection to Ollama via **HTTP REST API**.
- Allows interaction with local models such as `llama3`, `mistral`, `gemma`, etc.
- Compatible with Windows and .NET 6 or higher.
- Visual Studio based project (`.sln`).

---

## 🛠 Requirements

- .NET SDK 6.0 or higher](https://dotnet.microsoft.com/download)
- Ollama installed and running](https://ollama.com)
- Llama.cpp installed and running](https://github.com/ggml-org/llama.cpp)
- Visual Studio 2022 or equivalent

---

> If you have questions on how to install and run Ollama or llama.cpp, please refer to the official documentation for [Ollama](https://github.com/ollama/ollama) or [llama.cpp](https://github.com/ggerganov/llama.cpp).

---

## 📦 Installation

1. **Clone the repository:**

````bash
git clone https://github.com/RolololoDev007/BasicChat-oLlama-Llama.cpp.git
cd BasicChat-oLlama-Llama.cpp
````

2. **Open the repository in Visual Studio**.

- Launch Visual Studio.
- Select "Open Folder" and choose the repository folder you just cloned.

3. **Compile and start the application**.

- Use the Visual Studio build options to compile the project.
- Run the application from the Visual Studio environment.

4. **Input the local service you want to use**.

- When prompted by the application, choose the backend of your choice: `ollama` or `llama.cpp`.

5. **Enter the host address**.

- Specify the address (e.g. `http://localhost:11434` for Ollama or the port configured for llama.cpp) where you have the selected service running.

6. **Enter the LLM model you want to use**.

- Enter the exact name of the model you have available in the service (e.g. `llama3`, `mistral`, `phi4`, etc. for Ollama; or the name/location of the model in llama.cpp).

That's it! Now you can start chatting with your favourite LLM model locally.
