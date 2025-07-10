using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Setup DB Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new NpgsqlDataSourceBuilder(connectionString).Build());

// Setup API Controllers
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//localhost:PORT/swagger to see all endpoints
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();