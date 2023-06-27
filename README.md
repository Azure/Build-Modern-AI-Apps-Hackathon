# Azure Cosmos DB & OpenAI: Bring Your Own Data to ChatGPT Hackathon

CosmicWorks has big plans for their retail site. They're eager to launch a POC of a simple chat interface where users can interact with a virtual agent to find product and account information.

The scenario centers around a consumer retail "Intelligent Agent" that allows users to ask questions on vectorized product, customer and sales order data stored in a database. The data in this solution is the Cosmic Works sample for Azure Cosmos DB. This data is an adapted subset of the Adventure Works 2017 dataset for a retail Bike Shop that sells bicycles, biking accessories, components and clothing.

> BUT you can bring your own data instead.

This hackathon will challenge you and your team to launch a POC of a simple chat interface where users can interact with a virtual agent to find product and account information. Through the course of the hackathon, you will modify an existing application to do the following:

- Store the chat messages in an Azure Cosmos DB database, grouped by chat sessions
- Use Azure OpenAI to create vector embeddings and chat completions
- Use a vector database to search for product and account information by the vector embeddings
- Load up existing product and account information into Azure Cosmos DB and the vector database
- Use prompt engineering to create a pipeline that manages the conversation flow, vectorization, search, data handling, and response generation
