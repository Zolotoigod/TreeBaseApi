using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TreeBase.ExceptionHandling;
using TreeBase.Repositories;
using TreeBase.Repositories.Context;
using TreeBase.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var s = builder.Configuration.GetConnectionString("TreeBase");
builder.Services.AddDbContext<TreeBaseContext>(opt =>
opt.UseNpgsql(builder.Configuration.GetConnectionString("TreeBase")), ServiceLifetime.Singleton);

builder.Services.AddSingleton(_ => new IdGenerator())
    .AddSingleton<ITreeService, TreeService>()
    .AddSingleton<IJournalService, JournalService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => 
{
    opt.EnableAnnotations();
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "TreeBaseApi", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionLoggingMiddleware>();
app.MapControllers();

app.Run();
