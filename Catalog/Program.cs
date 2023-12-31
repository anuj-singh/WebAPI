using WebAPI.Catalog.Repositories.Interfaces;
using WebAPI.Catalog.Repositories.Implemntations;
using MongoDB.Driver;
using WebAPI.Catalog.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
var builder = WebApplication.CreateBuilder(args);

//public configuration= builder.IConfiguration;
// Add services to the container.
BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IInMemProductRepo,MongoDBProductRepo>();
builder.Services.AddSingleton<IMongoClient>(ServiceProvider=>{
    var settings=builder.Configuration
    .GetSection(nameof(MongoDbSettings))
    .Get<MongoDbSettings>();
    return new MongoClient(settings.ConnectionString);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
