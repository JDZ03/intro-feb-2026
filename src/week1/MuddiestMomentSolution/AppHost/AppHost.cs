using Scalar.Aspire;

var builder = DistributedApplication.CreateBuilder(args);
var scalar = builder.AddScalarApiReference();

// Infastructure - "backing services" - what is my app going to need in the anviornment in which it runs.
// - database - postgres (great support for relational data (rows and columns) and for documents (like mongodb))
// - identity provider (later)

var postGres = builder.AddPostgres("db-server")
    .WithLifetime(ContainerLifetime.Persistent); // We have in prod a postgres server

// We are going to need a database on that server for the API
var mmDb = postGres.AddDatabase("db-mm");



var mmApi = builder.AddProject<Projects.MuddiestMoment_Api>("mm-api")
    .WithReference(mmDb)
    .WaitFor(mmDb);

scalar.WithApiReference(mmApi);

var gateway = builder.AddProject<Projects.Gateway_Api>("gateway")
    .WithReference(mmApi)
    .WaitFor(mmApi);

builder.Build().Run();
