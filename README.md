# Build and Modernize AI Applications Hackathon

CosmicWorks has big plans for their retail site. They're eager to launch a POC of a simple chat interface where users can interact with a virtual agent to find product and account information.

The scenario centers around a consumer retail "Intelligent Agent" that allows users to ask questions on vectorized product, customer and sales order data stored in a database. The data in this solution is the Cosmic Works sample for Azure Cosmos DB. This data is an adapted subset of the Adventure Works 2017 dataset for a retail Bike Shop that sells bicycles, biking accessories, components and clothing.

> BUT you can bring your own data instead.

This hackathon will challenge you and your team to launch a POC of a chat interface where users can interact with a virtual agent to find product and account information. Through the course of the hackathon, you will modify an existing application to do the following:

- Store the chat messages in an Azure Cosmos DB database, grouped by chat sessions
- Use Azure OpenAI Service to create vector embeddings and chat completions
- Use Azure Cognitive Search as a vector database to search for product and account information by the vector embeddings
- Load up existing product and account information into Azure Cosmos DB and the Azure Cognitive Search vector index
- Create a process that manages the conversation flow, vectorization, search, data handling, and response generation
- Externally manage system prompts

## Prerequisites

- Azure Subscription
- Visual Studio
- .NET 7 SDK
- Docker Desktop
- Azure CLI 2.49.0
- Helm v3.11.1 or greater - https://helm.sh/
- Subscription with access to the Azure OpenAI Service. Start here to [Request Access to Azure OpenAI Service](https://customervoice.microsoft.com/Pages/ResponsePage.aspx?id=v4j5cvGGr0GRqy180BHbR7en2Ais5pxKtso_Pz4b1_xUOFA5Qk1UWDRBMjg0WFhPMkIzTzhKQ1dWNyQlQCN0PWcu)

## Setting up your development environment

The following steps will guide you through the process needed to being the hackathon.

### Clone this repo

Clone this repository and change to the `main` branch

```pwsh
git clone https://github.com/Azure/Build-Modern-AI-Apps-Hackathon
git checkout main
```

### Deploy to Azure the core services

1. Open the PowerShell command line and navigate to the directory where you cloned the repo.
2. Navigate into the `starter-artifacts\code\VectorSearchAiAssistant` folder.
3. Run the following PowerShell script to provision the infrastructure and deploy the API and frontend. Provide the name of a NEW resource group that will be created. This will provision all of the required infrastructure, deploy the API and web app services into AKS, and import data into Cosmos.

```pwsh
./scripts/Starter-Deploy.ps1  -resourceGroup <resource-group-name> -location <location> -subscription <subscription-id>
```

>**NOTE**:
>
>If `<resource-group-name>` already exists, your user must have `Owner` permissions on the resource group.
>If `<resource-group-name>` does not exist exists, the deployment script will create it. In this case, your user must have `Owner` permissions on the subscription in which the resource group will be created.

### Verify initial deployment


1. After the command completes, navigate to resource group and obtain the name of the AKS service.
2. Execute the following command to obtain the website's endpoint

  ```pwsh
  az aks show -n <aks-name> -g <resource-group-name> -o tsv --query addonProfiles.httpApplicationRouting.config.HTTPApplicationRoutingZoneName
  ```

```pwsh
az deployment group show -g $resourceGroup -n cosmosdb-openai-azuredeploy -o json --query properties.outputs.webFqdn.value
```

3. Browse to the website with the returned hostname.

If the website loads, you are ready to continue with the hackathon challenges. Don't worry if the website is not fully operational yet - you will get it there!

## Run the solution locally using Visual Studio

You can run the website and the REST API that supports it locally. You need to first update your local configuration and then you can run the solution in the debugger using Visual Studio.

#### Configure local settings

- In the `Search` project, make sure the content of the `appsettings.json` file is similar to this:

    ```json
    {
        "DetailedErrors": true,
        "Logging": {
            "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
        },
        "AllowedHosts": "*",
        "MSCosmosDBOpenAI": {
            "ChatManager": {
                "APIUrl": "https://localhost:63279",
                "APIRoutePrefix": ""
            }
        }
    }
    ```

- In the `ChatServiceWebApi` project, make sure the content of the `appsettings.json` file is similar to this:

    ```json
    {
        "Logging": {
            "LogLevel": {
                "Default": "Information",
                "Microsoft.AspNetCore": "Warning"
            }
        },
        "AllowedHosts": "*",
        "MSCosmosDBOpenAI": {
            "CognitiveSearch": {
                "IndexName": "vector-index",
                "MaxVectorSearchResults": 10
            },
            "OpenAI": {
                "CompletionsDeployment": "completions",
                "CompletionsDeploymentMaxTokens": 4096,
                "EmbeddingsDeployment": "embeddings",
                "EmbeddingsDeploymentMaxTokens": 8191,
                "ChatCompletionPromptName": "RetailAssistant.Default",
                "ShortSummaryPromptName": "Summarizer.TwoWords",
                "PromptOptimization": {
                    "CompletionsMinTokens": 50,
                    "CompletionsMaxTokens": 300,
                    "SystemMaxTokens": 1500,
                    "MemoryMinTokens": 500,
                    "MemoryMaxTokens": 2500,
                    "MessagesMinTokens": 1000,
                    "MessagesMaxTokens": 3000
                }
            },
            "CosmosDB": {
                "Containers": "completions, customer, product",
                "Database": "database",
                "ChangeFeedLeaseContainer": "leases"
            },
            "DurableSystemPrompt": {
                "BlobStorageContainer": "system-prompt"
            }
        }
    }
    ```

- In the `ChatServiceWebApi` project, create an `appsettings.Development.json` file with the following content (replace all `<...>` placeholders with the values from your deployment):

    ```json
    {
        "MSCosmosDBOpenAI": {
            "CognitiveSearch": {
                "Endpoint": "https://<...>.search.windows.net",
                "Key": "<...>"
            },
            "OpenAI": {
                "Endpoint": "https://<...>.openai.azure.com/",
                "Key": "<...>"
            },
            "CosmosDB": {
                "Endpoint": "https://<...>.documents.azure.com:443/",
                "Key": "<...>"
            },
            "DurableSystemPrompt": {
                "BlobStorageConnection": "<...>"
            }
        }
    }
    ```

    >**NOTE**: THe `BlobStorageConnection` value can be found in the Azure Portal by navigating to the Storage Account created by the deployment (the one that has a container named `system-prompt`) and selecting the `Access keys` blade. The value is the `Connection string` for the `key1` key.

### Running in debug 

To run locally and debug using Visual Studio, open the solution file to load the projects and prepare for debugging.

Before you can start debugging, you need to set the startup projects. To do this, right-click on the solution in the Solution Explorer and select `Set Startup Projects...`. In the dialog that opens, select `Multiple startup projects` and set the `Action` for the `ChatServiceWebApi` and `Search` projects to `Start`.

Also, make sure the newly created `appsettings.Development.json` file is copied to the output directory. To do this, right-click on the file in the Solution Explorer and select `Properties`. In the properties window, set the `Copy to Output Directory` property to `Copy always`..

You are now ready to start debugging the solution locally. To do this, press `F5` or select `Debug > Start Debugging` from the menu.

**NOTE**: With Visual Studio, you can also use alternate ways to manage the secrets and configuration. For example, you can use the `Manage User Secrets` option from the context menu of the `ChatWebServiceApi` project to open the `secrets.json` file and add the configuration values there.


## Teardown

When you have finished with the hackathon, simply delete the resource group that was created. 
