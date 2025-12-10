using Portfolio.Services; 

var builder = WebApplication.CreateBuilder(args);




builder.Services.Configure<GitHubSettings>(
    builder.Configuration.GetSection("GitHubSettings"));

builder.Services.AddMemoryCache();

builder.Services.AddScoped<IGitHubService, GitHubService>();

builder.Services.Decorate<IGitHubService, CachedGitHubService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();