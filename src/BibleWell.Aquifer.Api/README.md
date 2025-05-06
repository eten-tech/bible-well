# Bible Well API

## Client Generation

The code in the `Clients` folder/namespace is auto-generated from the Aquifer.Well.API OpenAPI spec. To generate the client,
ensure you have the latest code for aquifer-server and run:
```bash
cd aquifer-server
dotnet run --project src/Aquifer.Well.API --generateclients true
```
This command assumes that you have a local clone of the aquifer-server repo that is sitting in a folder next to this repo.