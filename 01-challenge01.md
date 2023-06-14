# Challenge 1: The Landing Before the Launch

CosmicWorks has big plans for their retail site, but they need to start somewhere; they need a landing zone in Azure for all of their services. It will take a while to prepare their e-Commerce site to migrate to Azure, but they're eager to launch a POC of a simple chat interface where users can interact with a virtual agent to find product and account information. They've created a simplistic Blazor web application for the UI elements and have asked you to to incorporate the backend plumbing to do the following:

- Store the chat messages in an Azure Cosmos DB database, grouped by chat sessions
- Use Azure OpenAI to create vector embeddings and chat completions
- Use a vector database to search for product and account information by the vector embeddings
- Load up existing product and account information into Azure Cosmos DB and the vector database

For this challenge, you will deploy the services into the landing zone in preparation for the launch of the POC.

## Challenge

Your team must:

1. Deploy the Azure services needed to support the chat interface
2. Set up your development environment

### Hints

- You will need to deploy the following Azure services:
  - Azure Cosmos DB
  - Azure OpenAI
  - A vector database of your choosing (e.g. Azure Search, MongoDB vCore, Azure PostegreSQL with pg-vector, etc.)
  - A web service that supports running the front-end web app in a Docker container
  - A web service that supports running the back-end web API in a Docker container

### Success Criteria

To complete this challenge successfully, you must:

- Deploy Azure Cosmos DB with the NoSQL API. It should have a database named `database` and containers named `completions` with a partition key of `/sessionId`, `customer` with a partition key of `/customerId`, `embedding` with a partition key of `/id`, and `product` with a partition key of `/categoryId`. You only need to deploy the account to a single region. Make a best guess at the RU/s for each container. You will adjust these in a later challenge based on performance and cost.
- Deploy Azure OpenAI with the following deployments:
  - `completions` with the `gpt-35-turbo` model
  - `embeddings` with the `text-embedding-ada-002` model
- Deploy a vector database of your choosing. You need to have at least one vector-based index that can retrieve the most relevant fields for all of the document types. You will need to load the data into the vector database in a later challenge.
- Deploy a web service that supports running the front-end web app in a Docker container. You can use any web service that supports Docker containers. You will need to load the front-end web app into the web service in a later challenge.
- Deploy a web service that supports running the back-end web API in a Docker container. You can use any web service that supports Docker containers. You will need to load the back-end web API into the web service in a later challenge.

### Resources

- [Azure Cosmos DB](https://docs.microsoft.com/azure/cosmos-db/)

## Explore Further

TODO: Add relevant information about the technologies deployed in this challenge.
