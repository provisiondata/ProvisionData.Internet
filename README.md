# ProvisionData.Internet
Provides .NETStandard 2.0 compatible Internet types like `DomainName`, `Label`, `Serial`\* and `TTL`\*.



\*Coming soon(ish)!

## Build and Publish

```powershell
PS> .\build.ps1 PushToPdsi    # Beta
PS> .\build.ps1 PushToNuget   # RC, Release
PS> dotnet nuget delete -k $env:PdsiApiKey -s https://baget.pdsint.net/v3/index.json ProvisionData.Internet <1.1.0-beta0001>  # Oops
```
