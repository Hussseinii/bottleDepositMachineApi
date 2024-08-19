var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logFilePath = builder.Configuration.GetValue<string>("Logging:LogFilePath");
builder.Services.AddSingleton<ICustomLoggerService>(new FileLoggerService(logFilePath));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowReactApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/api/bottleDepositMachine/logs", async (LogEntry logEntry, ICustomLoggerService loggerService) =>
{

    await loggerService.StoreLogAsync(logEntry);

    return Results.Ok();
});

app.Run();



