using NetVet.Domain.Repositories;
using NetVet.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers(); // Add support for MVC controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection configuration
builder.Services.AddScoped<IAppointmentRepository, MockAppointmentRepository>();
builder.Services.AddScoped<IAppoinmentNotificationService, AppointmentNotificationService>();
builder.Services.AddScoped<INotificationServiceRegistry, NotificationServiceRegistry>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin() 
               .AllowAnyMethod() 
               .AllowAnyHeader(); 
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // Automatically map controller endpoints

app.Run();