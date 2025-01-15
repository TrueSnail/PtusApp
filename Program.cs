using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddOpenApi("PtusService", opt =>
{
    opt.AddDocumentTransformer((document, context, token) =>
    {
        document.Info.Title = "PtusService";
        document.Info.Version = "0.0.1";
        return Task.CompletedTask;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opt =>
    {
        opt.WithTitle("PTUS");
        opt.Theme = ScalarTheme.BluePlanet;
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();