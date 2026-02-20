var builder = DistributedApplication.CreateBuilder(args);

var pg = builder.AddPostgres("pg").WithImage("postgres:17.5")
    //.WithLifetime(ContainerLifetime.Session) // restart it every time you run the app
    .WithLifetime(ContainerLifetime.Persistent) //keep it around, you know, uhm, persistent?
    .WithDataVolume("questions-api"); // don't technically need this, but I'll talk more aobut it tomorrow.

var qaDb = pg.AddDatabase("qa-db");


var qaApi = builder.AddProject<Projects.Qa_Api>("qa-api")
    .WithReference(qaDb)
    .WaitFor(qaDb);

var bff = builder.AddProject<Projects.BackendForFrontend>("bff")
    .WithReference(qaApi)
    .WaitFor(qaApi);

builder.Build().Run();