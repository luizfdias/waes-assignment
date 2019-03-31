# Waes Diff API

This is an API developed in ASP.NET Core with diff functionality that analyzes two data (left and right) and shows their differences.

## Getting Started

To get started in a development environment, it's only necessary to clone this repository, open the solution and start the project.

### Prerequisites

ASP.NET CORE 2.2 or above

### Configuration

No configurations are required, since this API is not using any external components.

## How to use the Diff Api

The API has 3 entry points as following and they are self documented by swagger in the path "HOST/swagger/index.html":

![Swagger API documentation](https://i.ibb.co/bvv4pBr/swagger.png)

The provided {CorrelationId} must be the same between all callings. It will be used as the correlation identification of the whole process to check the diff and obtain the results.

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
    "data": {
        "id": "aae658bc-5692-4feb-aee5-d95e543be5a9",
        "correlationId": "123456789",
        "content": "YWJjIDEyMw0KIDE1OSA5NTE="
    }
}

  HTTP 201 Created
  {
    "data": {
        "id": "aae658bc-5692-4feb-aee5-d95e543be5a9",
        "correlationId": "123456789",
        "content": "YWJjIDEyMw0KIDE1OSA5NTE="
    }
}
```

To get the result of the diff, the third endpoint must be called, passing the same identification as before.

Here is a sample request to accomplish this:

```
  GET HOST/v1/diff/123456789
```

Here is a sample response when differences are not found:

```
HTTP 200 OK
{
    "data": {
        "result": "Equal"
    }
}
```

Here is a sample response when differences are found:

```
HTTP 200 OK
{
    "data": {
        "result": "NotEqual",
        "info": [
            {
                "startIndex": 0,
                "length": 3
            },
            {
                "startIndex": 3,
                "length": 2
            },
            {
                "startIndex": 5,
                "length": 1
            }
        ]
    }
}
```

Here is a sample response when the data in each set is not the same sized:

```
HTTP 200 OK
{
    "data": {
        "result": "NotOfEqualSize"
    }
}
```

The "result" field indicates the diff result (Equal | NotEqual | NotOfEqualSize).

## Design of the API

![API Design](https://i.ibb.co/mR7jJPP/architecture.png)

## Running the tests

### Automated tests

The tests of this project are divided in two categories: Unit tests and integration tests. 

Each component has its own tests. They are easy to recognize, by its name. The unit tests projects have the suffix "UnitTests" while the integration tests project has the suffix "IntegrationTests".

The tests can be executed in Visual Studio or any similar tool.

## Future improvements (NICE TO HAVE)

- The analyze of the diff could be asynchronous so the post request would not get stuck in case of the file is too big.

## Built With
API:
* [ASP.NET CORE](https://www.asp.net/core/overview/aspnet-vnext) 
* [MediatR](https://github.com/jbogard/MediatR) 
* [AutoMapper](https://automapper.org/) 
* [Serilog-AspnetCore](https://github.com/serilog/serilog-aspnetcore) 
* [Swagger](https://swagger.io/) 

Tests:
* [AutoFixture](https://github.com/AutoFixture/AutoFixture) 
* [NSubstitute](https://github.com/nsubstitute/NSubstitute) 
* [XUnit](https://github.com/xunit/xunit) 
* [FluentAssertions](https://github.com/fluentassertions/fluentassertions) 

## Authors

* **Luiz Fernando Dias Rezende** 
