# Challenge 3: Now We're Flying

With the critical components in place, we're ready to tie everything into the chat interface. When a user types a question into the chat interface, we need to create a vector embedding for the question, then search for the most similar vector embeddings for products and accounts, and return the relevant documents that get sent to Azure OpenAI's completions endpoint. 

In order to return a human-friendly response to the user, we need to use the completions endpoint to generate a response based on the most relevant documents and an instructional system-level prompt. Furthermore, we need to keep a history of the user's questions and the responses that were generated so that reload the chat in the future.

There are several approaches to generating prompts for the Azure OpenAI service. One of the most popular approaches is to use a technique called *prompt engineering* to author prompts that are used to generate completions. Prompt engineering is an iterative process that involves authoring prompts, generating completions, and evaluating the results. The results of the evaluation are used to generate new prompts, and the process repeats until the desired results are achieved. 

The starter solution uses Semantic Kernel for prompt engineering. This challenge is about experimenting with system prompts to impact how the completions model works.

## Challenge

Your team must:
 
1. Create the system prompt that defines the assistant's behavior. The system prompt should instruct the model to do the following:
   1. Tell it that it is an intelligent assistant for a bike company.
   2. Tell it that it is responding to user questions about products, product categories, customers, and sales order information provided in JSON format embedded below.
   3. Only answer questions related to the information provided.
   4. Not to "make up" information and to respond that it does not know the answer to suggest to the user to search for it themselves.
2. Upload the system prompt to the Storage Account.
3. Update the chat interface to initiate the chat-based workflow you and your team have implemented.
4. Invoke the completions endpoint to generate a response based on the most relevant documents and some instructional system-level prompts. The system prompt should be included with every completions call, but not repeated in the chat history.
5. Store the user's questions and the responses that were generated so we can reload them in the future.

### Hints

- Think carefully about the system prompt, about how it should respond, what knowledge it is allowed to use when reasoning to create a response, what subjects it is allowed to respond to and importantly what it should not respond to.
- Experiment with eliciting different response formats from the completions model:
   - Respond with a single number
   - Respond with a bulleted lists
   - Respond using simpler syntax (e.g. explain it like I'm five)
- Have the agent reject off topic prompts from the user.

### Success Criteria

To complete this challenge successfully, you must:

- Demonstrate to your coach that you can load the system prompt from the storage account.
- Interact with the assistant thru the web based chat interface.
- View the chat messages saved to the container in Cosmos DB and verify that your new User and Assistant messages are appearing.
- Try a variety of user prompts to see how the assistant responds.

### Resources

- [Intro to prompt engineering](https://learn.microsoft.com/en-us/azure/cognitive-services/openai/concepts/prompt-engineering)

## Explore Further

- [Writing Effective System Prompts](https://learn.microsoft.com/azure/cognitive-services/openai/concepts/system-message)

