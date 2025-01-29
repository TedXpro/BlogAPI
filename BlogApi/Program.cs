using BlogApi.Services;
using BLOGAPI.Services;
using BLOGAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register The Service class to the dependecny Injection Container
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<ICommentService, CommentService>();
// builder.Services.AddSingleton<IBlogService, BlogService>();
builder.Services.AddSingleton<ILikeService, LikeService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapControllers(); 

app.UseAuthorization();

app.Run();
