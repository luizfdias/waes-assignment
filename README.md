# Waes Diff API

This is an API developed in ASP.NET Core with diff functionality that analyzes two data (left and right) and respond the differences.

## Getting Started

An example of this API is hosted in https://waesdiffapi.azurewebsites.net/ for tests purposes.

To get started in a development environment, it's only necessary to clone this repository, open the solution and start the project.

### Prerequisites

ASP.NET CORE 2.1 or above

```
Give examples
```

### Configuration

Before to get a development env running, it's necessary to setup some configurations in "appsettings.json".

This API works with two storage types to store the data that will be analyzed (Memory cache and Azure Blob Storage). To setup this settings, the AppSettings:StorageType must be set as following:

```
  "AppSettings": {
    "StorageType": "Memory" // Possible values: Memory | AzureBlob
  }
```

If Memory is chosen, the MemoryStorage:DataExpirationInSeconds must be provided:

```
  "MemoryStorage": {
    "DataExpirationInSeconds" : 60
  }
```

if AzureBlob is chosen, the BlobStorage:ConnectionString and BlobStorage:ContainerName must be set:

```
  "BlobStorage": {
    "ConnectionString": "YOUR_CONNECTION_STRING_TO_AZURE_BLOB_STORAGE", 
    "ContainerName": "YOUR_CONTAINER_NAME"
  }
```

This is an example of an appsettings.json:

```
  "BlobStorage": {
    "ConnectionString": "YOUR_CONNECTION_STRING_TO_AZURE_BLOB_STORAGE", 
    "ContainerName": "YOUR_CONTAINER_NAME"
  },
  "MemoryStorage": {
    "DataExpirationInSeconds" : 60
  },
  "AppSettings": {
    "StorageType": "AzureBlob" // or Memory
  }
```

## Running the tests

### Functionality tests

The API has 3 entry points. 

First it's need to provide the data to be Analyzed in the following endpoints:

```
  POST HOST/v1/diff/{id}/left
  POST HOST/v1/diff/{id}/right
```

The {Id} must be the same between the data used to be compared, because it will be used as the identification to get the diff between the them. 

Here’s a sample request to left and right resources:

```
  POST HOST/v1/diff/1/left
  BODY 1st_data  

  POST HOST/v1/diff/1/right
  BODY 2nd_data  
```

Here's a sample response to both endpoints:

```
201 Created
```

To get the result of the diff, the third endpoint must be called, passing the same identification as before.

Here's a sample request to accomplish this:

```
  GET HOST/v1/diff/1
```

Here's a sample response:

```
  {
    "equalsSize": true, // Its says if the data have same sizes
    "dataInfo": [ // It contains the data info that was analyzed
        {
            "id": "left_abc123", // Side and identification of the data. This id is generate by the application for internal control
            "length": 9 // Length of the data analyzed
        },
        {
            "id": "right_abc123",
            "length": 9
        }
    ],
    "differences": [ // this is where the differences are showed. It contains the start position of the difference and its length
        {
            "startOffSet": 3,
            "length": 1
        },
        {
            "startOffSet": 5,
            "length": 3
        }
    ]
}
```

### Automated tests

The tests of this project is divided in two categories: Unit tests and integration tests. 

Each component has its own project for their unit tests and there is just one project for the integration tests. They are easy to recognize, by its name. The unit tests projects has the suffix "UnitTests" while the integration tests project has the suffix "IntegrationTests".

The tests can be executed in Visual Studio or any similar tool.

```
Give an example
```

### And coding style tests

Explain what these tests test and why

```
Give an example
```

## Deployment

Add additional notes about how to deploy this on a live system

## Built With

Test:
* [AutoFixture](https://github.com/AutoFixture/AutoFixture) 
* [NSubstitute](https://github.com/nsubstitute/NSubstitute) 
* [XUnit](https://github.com/xunit/xunit) 
* [XUnit](https://github.com/fluentassertions/fluentassertions) 

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Billie Thompson** - *Initial work* - [PurpleBooth](https://github.com/PurpleBooth)

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
