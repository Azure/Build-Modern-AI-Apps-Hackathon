# Challenge 1: The Landing Before the Launch

CosmicWorks has big plans for their retail site, but they need to start somewhere; they need a landing zone in Azure for all of their services. It will take a while to prepare their e-Commerce site to migrate to Azure, but they're eager to launch a POC of a simple chat interface where users can interact with a virtual agent to find product and account information. 

They've created a simplistic Blazor web application for the UI elements and have asked you to to incorporate the backend plumbing to do the following:

- Store the chat history in an Azure Cosmos DB database
  - They expect the following types of messages: Session (for the chat session), Message (the user and assistant message).
  - A message should have a sender (Assistant or User), tokens (that indicates how many tokens were used), text (the text from the assistant or the user), rating (thumbs up or down) and vector (the vector embedding of the user's text).
- Source the customer and product data from the Azure Comos DB database
- Use Azure OpenAI to create vector embeddings and chat completions
- Use a Azure Cognitive Search to search for relevant product and account information by the vector embeddings
- Encapsulate the orchestration of interactions with OpenAI behind a back-end web service

For this challenge, you will deploy the services into the landing zone in preparation for the launch of the POC.

## Challenge

Your team must:

1. Deploy the Azure services needed to support the chat interface
2. Clone the repo with the Blazor web application and starter artifacts
3. Set up your development environment

### Hints

- You will need to deploy the following Azure services within a new Resource Group:
  - Azure Cosmos DB
  - Azure OpenAI
  - Azure Cognitive Search
  - Azure Kubernetes Service that will host:
    - A web service that supports running the front-end Blazor web app in a Docker container
    - A web service that supports running the back-end web API in a Docker container
- You will load data and deploy code into these services in a later challenge. 
### Success Criteria

To complete this challenge successfully, you must:

- Deploy Azure Cosmos DB with the NoSQL API. It should have a database named `database` and containers named `completions` with a partition key of `/sessionId`, `customer` with a partition key of `/customerId`, `embedding` with a partition key of `/id`, and `product` with a partition key of `/categoryId`. You only need to deploy the account to a single region. Make a best guess at the RU/s for each container. You will adjust these in a later challenge based on performance and cost.
- Deploy Azure OpenAI with the following deployments:
  - `completions` with the `gpt-35-turbo` model
  - `embeddings` with the `text-embedding-ada-002` model
- Deploy Azure Cognitive Search in the basic tier. 
- Deploy Azure Kubernetes Service in the basic tier.


### Resources

- [Azure Cosmos DB](https://learn.microsoft.com/en-us/azure/cosmos-db/)
- [Azure OpenAI](https://learn.microsoft.com/en-us/azure/cognitive-services/openai/overview)
- [Azure Cognitive Search](https://learn.microsoft.com/en-us/azure/search/)
- [Azure Kubernetes Service](https://learn.microsoft.com/en-us/azure/aks/intro-kubernetes)

## Explore Further

- [Understanding embeddings](https://learn.microsoft.com/en-us/azure/cognitive-services/openai/concepts/understand-embeddings)
