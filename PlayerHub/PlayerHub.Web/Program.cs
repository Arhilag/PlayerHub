using PlayerHub.Infrastructure;
using PlayerHub.Application.Auth;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddScoped<IAuthService, AuthService>();
var app = builder.Build();
app.MapControllers();

app.Run();
