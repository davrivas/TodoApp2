using Microsoft.EntityFrameworkCore;
using TodoApp.DataAccess.Contexts;
using TodoApp.DataAccess.Repositories;
using TodoApp.Services;
using TodoApp.Services.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("TodoAppDb");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, opts => opts.EnableRetryOnFailure()));

builder.Services.AddScoped<ITodoItemService, TodoItemService>();
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

await app.RunAsync();
