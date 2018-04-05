Please use some editor for architecture view





## Architecture



![alt text](https://github.com/uneidel/BigDataPipelineSample/blob/master/Architecture.PNG "Architecture")

#Azure Components
Azure Service Bus is a generic, cloud-based messaging system for connecting just about anything – applications, services, and devices – wherever they are.
Azure Event Hubs is a highly scalable data ingress service that can ingest millions of events per second so that you can process and analyze the massive amounts of data produced by your connected devices and applications.
Azure Blob Storage is REST-based object storage for unstructured data in the cloud.
## Components
WebApp - Asp.Net Core/Angular2 Docker enabled. 
Eventhub with enabled Blob Archiving 
Azure Data Lake Store with Analytics 
Custom Outputters to Search / recommendation api

# brief explanation of current dataflow
Customer interacts with website either the quiz or infinite scroller of movies. Please note that the website is small sample with should simulate 
customer site (and to learn asp.core with angular2). data will be send via apicalls (todo) to an Eventhub on azure. 
Ingress data will be archived every 30 sec in avro format. Blob is attached as Datasource to Azure Data Lake.  


## TODO
API calls to WebRTC (SignalR preAlpha currently)
Angular2 Directives for Search / RecoAPI

##Links
https://blogs.msdn.microsoft.com/azuredatalake/2016/08/26/how-to-register-u-sql-assemblies-in-your-u-sql-catalog/
https://docs.microsoft.com/de-de/rest/api/searchservice/AddUpdate-or-Delete-Documents
https://docs.microsoft.com/de-de/rest/api/searchservice/create-index
https://azure.github.io/AzureDataLake/
https://grouplens.org/datasets/movielens/10m/
https://github.com/Hacklone/angular2-cool-infinite-grid
https://github.com/Valve/fingerprintjs2
https://recommendations-portal.azurewebsites.net
https://github.com/microsoft/Cognitive-Recommendations-Windows
https://channel9.msdn.com/Events/Visual-Studio/Visual-Studio-2017-Launch/WEB-103
http://anthonychu.ca/post/event-hubs-archive-azure-data-lake-analytics-usql/
https://blogs.msdn.microsoft.com/azuredatalake/2016/08/26/how-to-register-u-sql-assemblies-in-your-u-sql-catalog/
https://stackoverflow.com/questions/42768847/how-to-enable-parallelism-for-a-custom-u-sql-extractor


#register data lake 
https://github.com/Microsoft/azure-docs/blob/master/articles/data-lake-store/data-lake-store-authenticate-using-active-directory.md

#Register custom ADA Component
https://github.com/Azure/usql/tree/master/Examples/DataFormats/Microsoft.Analytics.Samples.Formats

##Thinks to consider
- Creating an index establishes the schema and metadata. Populating the index is a separate operation. 
For this step, you can use an indexer (see Indexer operations (Azure Search Service REST API), 
available for supported data sources) or an Add, Update or Delete Documents (Azure Search Service REST API). 
The inverted index is generated when the documents are posted.
(https://docs.microsoft.com/de-de/rest/api/searchservice/create-index)


- using Data Factory for transfering Data from blob to ADL instead of data sources.


##Data Factory
A .Net Activity is basically just a .dll which implements a specific Interface (IDotNetActivity)
and is then executed by the Azure Data Factory. To be more precise here, the .dll (and all dependencies)
are copied to an Azure Batch Node which then executes the code when the .Net Activity is scheduled by ADF.
So far so good, but the tricky part is to actually develop the .Net code, test, and debug it.

## Data Preparation

Please see Readme.html for more detailed information
ratings.dat - All ratings are contained in the file - UserID::MovieID::Rating::Timestamp
tags.dat - file represents one tag applied to one movie by one user -  UserID::MovieID::Tag::Timestamp
movies.dat - Each line of this file represents one movie - MovieID::Title::Genres

1) Replace integerId with Guid
