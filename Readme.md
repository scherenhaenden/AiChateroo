# AIChateroo

AIChateroo is a local AI assistant and automation orchestration platform built with **.NET 8 (C#)**. It enables running Large Language Models (LLMs) offline on your local machine, utilizing semantic search (RAG) and browser automation tools.

## Key Features

- **Offline LLM Execution**: Run `.gguf` model formats locally using CPU or GPU via [LLamaSharp](https://github.com/SciSharp/LLamaSharp) without external API dependencies.
- **RAG & Semantic Memory**: Integrated with **Microsoft Kernel Memory** for document indexing, chunking, and Retrieval-Augmented Generation (RAG) semantically.
- **Agent Orchestration**: Utilizes **Microsoft Semantic Kernel** for managing cognitive flows and AI agent interactions.
- **Browser Automation**: Includes built-in support for **Playwright** and **Selenium WebDriver** (with ChromeDriver) to allow automated web scraping, data collection, and action orchestration.
- **Cross-Platform UI**: Supports deployment across Desktop (macOS/Windows/Linux), mobile (Android/iOS), and WebAssembly (Browser) using [Avalonia UI](https://github.com/AvaloniaUI/Avalonia).

---

## Project Structure

- **`AIChateroo`**: Core engine project containing the LLM runner (`LlModelEngine`), model definitions, mapping, and integrations.
- **`AIChateroo.Cli`**: A terminal-based chat client wrapper to run and test local models in a standard input-output loop.
- **`AiChateroo.Avalonia.Cross`**: Multi-platform user interface project powered by Avalonia UI.
- **`AIChateroo.Tests`**: Unit and integration test suite.

---

## Prerequisites

- **.NET 8 SDK**
- A compatible GGUF model (e.g., `mistral-7b-instruct-v0.1.Q4_0.gguf` or similar, compatible with LLamaSharp).

---

## Getting Started

### 1. Configure Git Post Buffer (Recommended for large files/models)
```bash
git config http.postBuffer 524288000
```

### 2. Configure Model Path
Open `Example.cs` in the `AIChateroo` project and configure the local path to your GGUF model:
```csharp
string modelPath = "/path/to/your/model.gguf";
```

### 3. Run the CLI Chat Application
To start chatting with your model in the terminal:
```bash
dotnet run --project AIChateroo.Cli
```

### 4. Run the Desktop Application
To launch the Avalonia UI desktop application:
```bash
dotnet run --project AiChateroo.Avalonia.Cross/AiChateroo.Avalonia.Cross.Desktop
```