# Waes Diff API

This is an API developed in ASP.NET Core with diff functionality that analyzes two data (left and right) and shows their differences.

## Getting Started

To get started in a development environment, it's only necessary to clone this repository, open the solution and start the project.

### Prerequisites

ASP.NET CORE 2.1 or above

### Configuration

This ASP.NET CORE API is using MongoDB. In a development environment, Docker is configured to run these services together in two containers. In order to get it running, it is first necessary to set up the configurations in 'appsettings.development.json'.

Here is a sample of that:

```
  "MongoDB": {
    "ConnectionString": "mongodb://localhost:27017",
    "Container": "mongodb://mongo:27017", 
    "Database": "WaesAssignment"
  }
```

## How to use the Diff Api

The API has 3 entry points as following:

```
  POST HOST/v1/diff/{id}/left
  POST HOST/v1/diff/{id}/right
  GET HOST/v1/diff/{id}
```

The provided {Id} must be the same between all callings. It will be used as the correlation identification of the whole process to check the diff and obtain the results.

First step is provide the data to be Analyzed. The field 'content' must be filled with a Base64 encoded value.

Here is a sample request to left and right resources:

```
  POST HOST/v1/diff/1/left    
  {
    "data":{
      "content":"YWJjIDEyMw0KIDE1OSA5NTE="
    }
  }  
```
```
  POST HOST/v1/diff/1/right    
  {
    "data":{
      "content":"YWJjIDEyMw0KIDE1OSA5NTE="
    }
  }    
```

Here is a sample success response to both endpoints:

```
  HTTP 201 Created
  {
    "success": true,
    "result": {
        "id": "aae658bc-5692-4feb-aee5-d95e543be5a9",
        "correlationId": "123456789",
        "content": "YWJjIDEyMw0KIDE1OSA5NTE=",
        "side": "Left"
    }
}

  HTTP 201 Created
  {
    "success": true,
    "result": {
        "id": "aae658bc-5692-4feb-aee5-d95e543be5a9",
        "correlationId": "123456789",
        "content": "YWJjIDEyMw0KIDE1OSA5NTE=",
        "side": "Right"
    }
}
```

To get the result of the diff, the third endpoint must be called, passing the same identification as before.

Here is a sample request to accomplish this:

```
  GET HOST/v1/diff/1
```

Here is a sample response when differences are not found:

```
HTTP 200 OK

{
    "success": true,
    "result": {
        "status": "Equal",
        "dataInfo": [
            {
                "id": "aae658bc-5692-4feb-aee5-d95e543be5a9",
                "correlationId": "123456789",
                "length": 17,
                "side": "Left"
            },
            {
                "id": "84165f5f-ab01-42ba-be1c-5d0887411ed0",
                "correlationId": "123456789",
                "length": 17,
                "side": "Right"
            }
        ]
    }
}
```

Here is a sample response when differences are found:

```
HTTP 200 OK

{
    "success": true,
    "result": {
        "status": "NotEqual",
        "dataInfo": [
            {
                "id": "c6f2df66-ce51-4e92-8404-98a74298164a",
                "correlationId": "123456788",
                "length": 17,
                "side": "Right"
            },
            {
                "id": "99b2b19c-786b-4955-b82a-0f72b04f6004",
                "correlationId": "123456788",
                "length": 17,
                "side": "Left"
            }
        ],
        "differences": [
            {
                "startOffSet": 2,
                "length": 1
            },
            {
                "startOffSet": 14,
                "length": 2
            }
        ]
    }
}
```

Here is a sample response when the data in each set is not the same sized:

```
HTTP 200 OK
{
    "success": true,
    "result": {
        "status": "NotOfEqualSize",
        "dataInfo": [
            {
                "id": "0a4f5c18-c8d0-4492-9881-8ae753722288",
                "correlationId": "123456787",
                "length": 17,
                "side": "Left"
            },
            {
                "id": "52024c73-f5bc-4515-adc8-1c702e4748ec",
                "correlationId": "123456787",
                "length": 20,
                "side": "Right"
            }
        ]
    }
}
```

The "status" field indicate the diff result (Equal | NotEqual | NotOfEqualSize).

## Running the tests

### Automated tests

The tests of this project are divided in two categories: Unit tests and integration tests. 

Each component has its own project for their unit tests and there is just one project for the integration tests. They are easy to recognize, by its name. The unit tests projects have the suffix "UnitTests" while the integration tests project has the suffix "IntegrationTests".

The tests can be executed in Visual Studio or any similar tool.

The integration tests are using a fake repository.

## Future improvements

- Create a persistence model.
- Find a way to test the MongoDB repository in its unity.
- See if worth to make the dependencies of the classes private, in order to encapsulate it. The down side will be hard to write tests.

## Built With
API:
* [ASP.NET CORE](https://www.asp.net/core/overview/aspnet-vnext) 
* [MongoDB](https://www.mongodb.com/) 
* [Docker](https://www.docker.com/) 
* [Serilog-AspnetCore](https://github.com/serilog/serilog-aspnetcore) 

Tests:
* [AutoFixture](https://github.com/AutoFixture/AutoFixture) 
* [NSubstitute](https://github.com/nsubstitute/NSubstitute) 
* [XUnit](https://github.com/xunit/xunit) 
* [FluentAssertions](https://github.com/fluentassertions/fluentassertions) 

## Authors

* **Luiz Fernando Dias Rezende** 
