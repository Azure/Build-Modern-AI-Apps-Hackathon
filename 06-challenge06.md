# Challenge 6: The Colonel Needs a Promotion

In this challenge, you and your team will add new capability by creating a new plugin for Semantic Kernel.

In the previous challenge where you exprimented with prompt engineering, you may have observed that there are some things the large language model does not do well consistently. One example of this is dealing with numbers and doing math operations. Sometimes the model will do it correctly. Other times, such as when the numbers differ by an order of magnitude it may suggest things like the smaller number is bigger than the larger one. 

Your challenge is to pick a numeric or math problem and instead of relying the on the large language model to perform the calculation, you will make the model smart enough to call out to code that will perform the calculations with precision and then return the result back to the model. 
## Challenge

Your team must:

1. Create a new Semantic Function implemented in C# in the VectorSearchAiAssistant.SemanticKernel project that takes the numeric inputs, applies the desired computation and returns the results in string format that a skill will understand.
2. Create a new skill (plugin) for the Semantic Kernel that is purpose built to take the prompt input, invoke the new Semantic Function when it encounters prompts that need to use it.  
3. Register the new Semantic Function in the SemanticKernelRAGService by updating the constructor. 
4. Find a way to replace the call in the SemanticKernalRAGService.cs GetResponse method which directly gets the completion, with one that will first make a plan, decide if your function should be used and then execute the completion request accordingly.

### Hints

- You might want to try building this first in a simple console project.
- You should use the SequentialPlanner from Semantic Kernel to create and execute a plan around the prompt, so that it can choose when to invoke your new Semantic Function. 
- You will have to update how you handle the completion response from the SequentialPlanner. Take a look at the value of ModelResults.First(). 

### Success Criteria

To complete this challenge successfully, you must:

- Show your coach an example chat where your new Semantic Function was applied.

### Resources

- [Semantic Kernel auto create plans with planner](https://learn.microsoft.com/semantic-kernel/ai-orchestration/planner?tabs=Csharp)
- [Semantic Kernel creating native functions](https://learn.microsoft.com/en-us/semantic-kernel/ai-orchestration/native-functions?tabs=Csharp)


## Explore Further

[Microsoft Semantic Kernel on Github](https://github.com/microsoft/semantic-kernel)

