using MuddiestMoment.Api.Student;

var builder = WebApplication.CreateBuilder(args);
// configure this application to use the settings that are shared across all APIs in this solution 
builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddValidation(); // opting in to services to handle some stuff for you
// above this is configuration of services (things that own some state and the process around it) that we need in our 
// application
var app = builder.Build();
// everything here is setting up how we actually handle incoming request and write responses.

// add the code I am about to write that allows us to handle POST to /student/moments
app.MapStudentEndpoints();

app.MapDefaultEndpoints(); // the health cehcks and all that.

// the api us not up and running (listening for requests until we hit the next line)
app.Run(); // "blocking loop"