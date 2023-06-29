using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.AI.Embeddings;
using VectorSearchAiAssistant.Service.Models.ConfigurationOptions;
using VectorSearchAiAssistant.Service.Interfaces;
using Microsoft.Extensions.Logging;
using VectorSearchAiAssistant.SemanticKernel.Connectors.Memory.AzureCognitiveSearch;
using VectorSearchAiAssistant.SemanticKernel.Skills.Core;
using VectorSearchAiAssistant.Service.Models.Search;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.SemanticKernel.AI.TextCompletion;
using VectorSearchAiAssistant.Service.Models.Chat;
using Newtonsoft.Json;
using VectorSearchAiAssistant.SemanticKernel.Chat;
using VectorSearchAiAssistant.SemanticKernel.Text;

namespace VectorSearchAiAssistant.Service.Services;

public class SemanticKernelRAGService : IRAGService
{
    readonly SemanticKernelRAGServiceSettings _settings;
    readonly IKernel _semanticKernel;
    readonly ILogger<SemanticKernelRAGService> _logger;
    readonly ISystemPromptService _systemPromptService;
    readonly IChatCompletion _chat;
    readonly AzureCognitiveSearchVectorMemory _memory;

    bool _memoryInitialized = false;

    public bool IsInitialized => _memoryInitialized;

    public SemanticKernelRAGService(
        ISystemPromptService systemPromptService,
        IOptions<SemanticKernelRAGServiceSettings> options,
        ILogger<SemanticKernelRAGService> logger)
    {
        _systemPromptService = systemPromptService;
        _settings = options.Value;
        _logger = logger;

        var builder = new KernelBuilder();

        builder.WithLogger(_logger);

        builder.WithAzureTextEmbeddingGenerationService(
            _settings.OpenAI.EmbeddingsDeployment,
            _settings.OpenAI.Endpoint,
            _settings.OpenAI.Key);

        builder.WithAzureChatCompletionService(
            _settings.OpenAI.CompletionsDeployment,
            _settings.OpenAI.Endpoint,
            _settings.OpenAI.Key);

        _semanticKernel = builder.Build();

        _memory = new AzureCognitiveSearchVectorMemory(
            _settings.CognitiveSearch.Endpoint,
            _settings.CognitiveSearch.Key,
            _settings.CognitiveSearch.IndexName,
            _semanticKernel.GetService<ITextEmbeddingGeneration>(),
            _logger);
        Task.Run(() =>  InitializeMemory());

        _semanticKernel.RegisterMemory(_memory);

        _chat = _semanticKernel.GetService<IChatCompletion>();
    }

    private async Task InitializeMemory()
    {
        await _memory.Initialize(new List<Type>
        {
            typeof(Customer),
            typeof(Product),
            typeof(SalesOrder)
        });

        _memoryInitialized = true;
    }

    public async Task<(string Completion, int UserPromptTokens, int ResponseTokens, float[]? UserPromptEmbedding)> GetResponse(string userPrompt, List<Message> messageHistory)
    {
        /* TODO: Challenge 3. 
         * Complete the todo tasks as instructed by the comments
         */
        var memorySkill = new TextEmbeddingObjectMemorySkill();

        // Create a new context for this interaction with the Semantic Kernel
        var skContext = _semanticKernel.CreateNewContext();

        /* Use the recall memory skill to query the Cognitive Search vector index 
         * for data documents that are similar to the query
         */
        var memories = await memorySkill.RecallAsync(
            userPrompt,
            _settings.CognitiveSearch.IndexName,
            null,
            _settings.CognitiveSearch.MaxVectorSearchResults,
            skContext);

        // Read the resulting user prompt embedding so it can be persisted later on  
        var userPromptEmbedding = memorySkill.LastInputTextEmbedding?.ToArray();

        // The memoryCollection stores the documents retrieved from Cognitive Search
        List<object?> memoryCollection;
        if (string.IsNullOrEmpty(memories))
            memoryCollection = new List<object?>();
        else
        {
            var memoryCollectionRaw = JsonConvert.DeserializeObject<List<string>>(memories);
            memoryCollection = memoryCollectionRaw.Select(m => JsonConvert.DeserializeObject(m)).ToList();
        }

        var chatHistory = new ChatBuilder(
                _semanticKernel,
                _settings.OpenAI.CompletionsDeploymentMaxTokens,
                promptOptimizationSettings: _settings.OpenAI.PromptOptimization);

        /* TODO: 
         * Uncomment and complete the following chain to add the
         * SystemPrompt, Memories, and MessageHistory in the correct order.
         */
        //chatHistory
        //    .With_____(
        //        await _systemPromptService.GetPrompt(_settings.OpenAI.ChatCompletionPromptName))
        //    .With_____(
        //        memoryCollection)
        //    .With_____(
        //        messageHistory.Select(m => (new AuthorRole(m.Sender), m.Text.NormalizeLineEndings())).ToList())
        //    .Build();

        //chatHistory.AddUserMessage(userPrompt);

        /* TODO: 
         * Get the ChatCompletionService
         * Invoke the GetChatCompletions method asynchronously 
         */
        //var chat = _semanticKernel.GetService<IChatCompletion>();
        //var completionResults = await chat.GetChatCompletionsAsync(chatHistory);

        // TODO: Get the first completionResults and retrieve the ChatMessage from that
        //var reply = await ______[0]._______();

        // TODO: Extract the OpenAIChatResult to get to the prompt and completion token counts
        //var rawResult = (completionResults[0] as ITextResult).ModelResult.GetOpenAIChatResult();

        //TODO: Replace the following return value with the correct values according to function signature
        return new("", 0, 0, null);
        
    }

    public async Task<string> Summarize(string sessionId, string userPrompt)
    {
        var summarizerSkill = new GenericSummarizerSkill(
            await _systemPromptService.GetPrompt(_settings.OpenAI.ShortSummaryPromptName),
            500,
            _semanticKernel);

        var updatedContext = await summarizerSkill.SummarizeConversationAsync(
            userPrompt,
            _semanticKernel.CreateNewContext());

        //Remove all non-alpha numeric characters (Turbo has a habit of putting things in quotes even when you tell it not to
        var summary = Regex.Replace(updatedContext.Result, @"[^a-zA-Z0-9\s]", "");

        return summary;
    }

    public async Task AddMemory<T>(T item, string itemName, Action<T, float[]> vectorizer) where T : EmbeddedEntity
    {
        item.entityType__ = item.GetType().Name;
        await _memory.AddMemory<T>(item, itemName, vectorizer);
    }

    public async Task RemoveMemory<T>(T item)
    {
        await _memory.RemoveMemory<T>(item);
    }
}
