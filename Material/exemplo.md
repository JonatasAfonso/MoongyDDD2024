[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=jonatasafonso_company-documentprocessor&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=jonatasafonso_company-documentprocessor)

# Introduction 

Introducing our streamlined PDF generation process, where raw data meets precision engineering. Seamlessly transforming diverse payloads into polished PDF documents, this process combines efficiency with creativity. Harnessing cutting-edge technology, it ensures flawless formatting, cohesive designs, and data accuracy. From complex reports to elegant presentations, our process crafts visually captivating PDFs tailored to your unique needs. Experience the power of digital transformation, where information evolves into impactful documents at your fingertips.

# High-level architecture

## Overview

The Document Processor is a solution that receives raw data from multiple sources and transforms it into PDF documents. The solution is composed of several components, as shown in Table 1. The following sections describe each component in detail.

| Resource          | Description |
| ----------------  | ------- |
| APIM              | This component meet security and compliance requirements while enjoying a unified management experience and full observability across all internal and external APIs. |
| FunctionApp - F1  | This component receives the initial payload, validates it, and routes it to the correct workflow. |
| FunctionApp - F2  | This component generates documents in PDF format asynchronously and independently of FunctionApp - F1. The language runtime is decoupled from FunctionApp - F1. |
| Service Bus - SB1 | Responsible for queuing documents to be processed asynchronously. |
| BlobStorage - BS1 | Temporarily stores the documents to be processed. |
| BlobStorage - BS2 | Stores the processed documents. |

**Table 1**. High Level Architecture Components

## Overview of the solution

![Diagram showing the resources used in Document Processor.](./docs/img/Diagram.png)

**Figure 1**. Document Processor high-level architecture

| Step | Description |
| -------- | ------- |
| 1 | Login process belongs do Client Application|
| 2 | Client Application sends user authentication request with ClientId, ClientSecret and ApplicationId|
| 3 | If AzureAD is able to validate the credentials, the AppRegistration service will provide JWT Bearer token|
| 4 | Once Client Application has a valid JWT Token, Client Application will send a valid request to APIM|
| 5 | APIM is responsible to route the request for  FunctionApp - F1|
| 6 | FunctionApp - F1 once has validate the request it creates a response with an 204 - Accepted HttpResult.|
| 7 | APIM  respond to Client Application 204 - Accepted.|
| 8 | Once FunctionApp - F1 has some valid request to work, it will split the document payloads into diferent messages.|
| 9 | Each document payload generates an entry in BlobStorage - BS1|
| 10 | Each message in Service Bus - SB1, trigger FunctionApp - F2 who is reponsible to transform the document into expected PDF format|
| 11 | FunctionApp - F2 retrieve the payload from BS1 to transform to expected format|
| 12 | Once document is generated FunctionApp - F2 send the final document to BlobStorage - BS2|

**Table 2**. High Level architecture explanation


**_TODO:_** 

**Multitenancy Mechanism:** Implement a multitenancy mechanism to handle multiple clients and their respective data securely.

**Deadletter Mechanism for BlobStorage:** Implement a deadletter mechanism for BlobStorage - BS1 to handle failed document processing scenarios and manage error handling effectively.

**Notify Pattern for Client Apps:** Implement a notify pattern for client applications, possibly using callback mechanisms, to notify them of the processing status or completion of their requests.


**_NOTE:_** 
Ensure proper error handling, logging, and security measures are in place throughout the system. Consider implementing retry mechanisms for failed processing steps and monitoring tools to track the system's performance and health. Additionally, document the APIs and communication protocols for future reference and integration purposes.


## Project Components in DEV/SIT environment

TODO: Describe the components in DEV/SIT environment



## Monitoring

TODO: Describe how to monitor these resources

# Additional Information

## Build and Test
This solution was developed using .NET Framework version 7.

For local testing, it is recommended to use Visual Studio 2022+. However, since it is integrated into a Continuous Integration process in Azure DevOps, every commit made to the Develop or Main branches will automatically initiate the compilation process, run automated tests, perform static code analysis using SonarCloud, and then deploy to Azure Cloud using the predefined release pipeline.

For more detailed information, please consult the azure-pipelines.yml file.
## Naming convention and standards

Following the best practices in Cloud Adoption Framework (CAF), explained in https://learn.microsoft.com/en-us/azure/cloud-adoption-framework/ready/azure-best-practices/resource-naming, the solution was developed using the following naming convention and standards:

"A good name for a resource helps you to quickly identify its type, its associated workload, its environment, and the Azure region where it runs. To do so, names should follow a consistent format-a naming convention-that is composed of important information about each resource. The information in the names ideally includes whatever you need to identify specific instances of resources. For example, a public IP address (PIP) for a production SharePoint workload in the West US region might be pip-sharepoint-prod-westus-001."

![Diagram 1: Components of an Azure resource name](./docs/img/resource-naming.png)
**Figure 6**. Components of an Azure resource name



| Naming component  | Description |
| -------- | ------- |
| Resource type | An abbreviation that represents the type of Azure resource or asset. This component is often a prefix or suffix in the name. For more information, see Recommended abbreviations for Azure resource types https://learn.microsoft.com/en-us/azure/cloud-adoption-framework/ready/azure-best-practices/resource-abbreviations. Examples: rg, vm. |
| Project, application, or service name | Name of a project, application, or service that the resource is a part of. Examples: navigator, emissions, sharepoint, hadoop, near real time, payments, business apps |
| Environment | The stage of the development lifecycle for the workload that the resource supports. Examples: dev, sit, qua, prod |
| Region | The Azure region where the resource is deployed. Examples: westus, eastus2, northeurope |
| Instance | The instance count for a specific resource, to differentiate it from other resources that have the same naming convention and naming components. Examples, 01, 001 |

**Table 4**. CAF naming convention explanation 


## Magic strings

**Exports Azure Resource Group to ARM Template**
```bash
az group export --name <resource-group-name> --resource-ids <resource-id> --include-comments
```

**Converts from ARM JSON Template to Bicep templates**
```bash
az group export --name "your_resource_group_name" > main.json
az bicep decompile --file main.json
```

**Generates an unique string value:**
```bicep
param storageAccountName string = 'toylaunch${uniqueString(resourceGroup().id)}'
```




***
 AJ Company, 2023
 All rights reserved

