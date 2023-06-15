# Challenge 2: It's All About the Payload

CosmicWorks has years of product, customer, and sales data that they exported to an Azure storage account. They want to load this data into the Azure Cosmos DB account for a future migration of their e-Commerce site to Azure, but also for the POC of the chat interface. They also want to load the data into the vector database so that they can search for product and account information by the vector embeddings.

One critical component of the magic that makes the CosmicWorks chat interface work is the ability to search for products and accounts by their vector embeddings. When a user types a question into the chat interface, we need to create a vector embedding for the question, then search for the most similar vector embeddings for products and accounts. The vector embeddings for products and accounts are stored in a vector database, allowing us to return relevant documents that get sent to Azure OpenAI's completions endpoint.

## Challenge

Your team must:

1. Find an efficient and repeatable way to load data from the storage account into Cosmos DB. For this exercise, you only need to load the data once, but CosmicWorks wants to be able to repeat the process in the future with new data.
2. Verify that the data was loaded into Cosmos DB.
3. Use the Azure OpenAI service to create vector embeddings for the user prompt that is entered into the chat interface.
4. Create a process that automatically generates vector embeddings for all of the products and accounts in the Cosmos DB database and stores them in the vector database.

### Hints

- Think about how you build the logic for accessing the Azure OpenAI service to perform the vector embeddings. You will use this same logic layer to perform other Azure OpenAI tasks in later challenges.
- Think about how you can use the Cosmos DB Change Feed to trigger the creation of vector embeddings for new/updated products and accounts.

### Success Criteria

To complete this challenge successfully, you must:

- Demonstrate to your coach that you can load the data from the storage account into Cosmos DB using a method that can be repeated in the future.
- Perform a document count on each container in Cosmos DB and verify that the counts match the number of documents in the storage account.
- Generate vector embeddings of a sufficiently high dimensionality that is supported by the Azure OpenAI service as well as your vector database.
- Encapsulate the embedding logic within a service layer that can be used by other components of the CosmicWorks chat interface, as well as a future REST API service.
- Create a process that automatically generates vector embeddings for all of the products and accounts in the Cosmos DB database and stores them in the vector database.
- Demonstrate to your coach a successful search for products and accounts by vector embeddings. This does not necessarily have to originate from the chat interface at this point.

### Resources

- Links to tool options

## Explore Further

Add some context to the challenge and technologies used.
