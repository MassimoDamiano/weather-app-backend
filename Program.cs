using WeatherApi.Configuration;
using WeatherApi.Clients;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<OpenWeatherClient>(
    (serviceProvider, httpClient) =>
    {
        var options = serviceProvider
            .GetRequiredService<IOptions<OpenWeatherOptions>>()
            .Value;

        httpClient.BaseAddress = new Uri(options.BaseUrl);
    });

builder.Services
    .AddOptions<OpenWeatherOptions>()
    .Bind(builder.Configuration.GetSection(OpenWeatherOptions.SectionName))
    .Validate(
        options => Uri.TryCreate(
            options.BaseUrl,
            UriKind.Absolute,
            out _),
        "OpenWeather:BaseUrl debe ser una URL absoluta válida.")
    .Validate(
        options => !string.IsNullOrWhiteSpace(options.ApiKey),
        "OpenWeather:ApiKey es obligatoria.")
    .ValidateOnStart();

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

