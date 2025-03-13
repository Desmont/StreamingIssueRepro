# Description
On .NET 9 the ProblemDetails object is prefixed with "[" which breaks the parsing of the response. On .NET 8 this works as expected

## How to reproduce
Run the main project or the tests to reproduce the issue
```shell
dotnet test
```
