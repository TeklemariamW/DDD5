using AccountOwnerServer.Extensions;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureRepositoryWrapper();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure persistence
//builder.Services.ConfigureLogMessageRepository(builder.Environment, builder.Configuration);
//var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:CosmosDb:AccountKey");
//var dbName = builder.Configuration.GetValue<string>("ConnectionStrings:CosmosDb:DbName");
//builder.Services.AddDbContext<RepositoryContext>(options =>
//                options.UseCosmos(
//                    connectionString, dbName));
builder.Services.ConfigureCosmosDB(builder.Environment, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
    app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
