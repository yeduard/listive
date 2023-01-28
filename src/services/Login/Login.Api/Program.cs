using Login.Api;
using Login.Api.Services;
using Login.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRabbitMqEventBus(builder.Configuration);

builder.Services.AddLoginDbContext(builder.Configuration);

builder.Services.AddIdentity(builder.Configuration);

builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

app.MigrateDatabase();

app.SeedIdentityService();
    
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
