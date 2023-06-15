# Challenge 3: Now We're Flying

With the critical components in place, we're ready to tie everything into the chat interface. When a user types a question into the chat interface, we need to create a vector embedding for the question, then search for the most similar vector embeddings for products and accounts. The vector embeddings for products and accounts are stored in a vector database, allowing us to return relevant documents that get sent to Azure OpenAI's completions endpoint. In order to return a human-friendly response to the user, we need to use the completions endpoint to generate a response based on the most relevant documents and some instructional system-level prompts. Furthermore, we need to keep a history of the user's questions and the responses that were generated so that we can use this data to train the model in the future and also allow users to review their past conversations.

## Challenge

Your team must:

1. Update the chat interface to initiate the chat-based workflow you and your team have designed.
2. Invoke the completions endpoint to generate a response based on the most relevant documents and some instructional system-level prompts. The system prompt should instruct the model to do the following:
   1. Tell it that it is an intelligent assistant for a bike company.
   2. Tell it that it is responding to user questions about products, product categories, customers, and sales order information provided in JSON format embedded below.
   3. Only answer questions related to the information provided.
   4. Not to "make up" information and to respond that it does not know the answer to suggest to the user to search for it themselves.
3. Store the user's questions and the responses that were generated so that we can use this data to train the model in the future and also allow users to review their past conversations.

### Hints

- List any hints here.

### Success Criteria

To complete this challenge successfully, you must:

- List success criteria here

### Resources

- Add links here

## Explore Further

Add relevant information here.

