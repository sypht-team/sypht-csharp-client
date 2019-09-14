# Sypht C# Client
This repository is a C#/.NET sample implementation for working with the Sypht API at https://api.sypht.com.

## About Sypht
[Sypht](https://sypht.com) is a SaaS [API]((https://docs.sypht.com/)) which extracts key fields from documents. For
example, you can upload an image or pdf of a bill or invoice and extract the amount due, due date, invoice number
and biller information.

### Getting started
To get started you'll need API credentials, i.e. a `client_id` and `client_secret`, which can be obtained by registering
for an [account](https://www.sypht.com/signup/developer)

### Prerequisites
* supports .NET Core 2.2

### Usage
Populate system environment variable with the credentials generated above:

```Bash
export SYPHT_API_KEY="$client_id:$client_secret"
```

then invoke the example file under /examples/simple.cs

```Bash
dotnet clean
dotnet build
dotnet run examples/simple.cs
```