using LifeCicle.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddKeyedTransient<ILifeTimeService, LifeTimeService>("Transient");
builder.Services.AddKeyedScoped<ILifeTimeService, LifeTimeService>("Scoped");
builder.Services.AddKeyedSingleton<ILifeTimeService, LifeTimeService>("Singleton");

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

app.MapGet("/", (
    [FromKeyedServices("Transient")] ILifeTimeService transientService,
    [FromKeyedServices("Scoped")] ILifeTimeService scopedService,
    [FromKeyedServices("Singleton")] ILifeTimeService singletonService,

    [FromKeyedServices("Transient")] ILifeTimeService secondTransientService,
    [FromKeyedServices("Scoped")] ILifeTimeService secondScopedService,
    [FromKeyedServices("Singleton")] ILifeTimeService secondSingletonService
) =>
{
    string Bold(object value) => $"<b>{value}</b>";
    var output = new List<string>
    {
        "--- Requerimento HTTP Atual ---",
        
        // Substituindo **{value}** por <b>{value}</b>
        $"Transient (1ª Injeção): ID: {Bold(transientService.Id)}, Operação: {Bold(transientService.GetOperationCount())}",
        $"Scoped (1ª Injeção): ID: {Bold(scopedService.Id)}, Operação: {Bold(scopedService.GetOperationCount())}",
        $"Singleton (1ª Injeção): ID: {Bold(singletonService.Id)}, Operação: {Bold(singletonService.GetOperationCount())}",

        "", // Linha em branco para separação
        
        // Substituindo **{value}** por <b>{value}</b>
        $"Transient (2ª Injeção): ID: {Bold(secondTransientService.Id)}, Operação: {Bold(secondTransientService.GetOperationCount())}",
        $"Scoped (2ª Injeção): ID: {Bold(secondScopedService.Id)}, Operação: {Bold(secondScopedService.GetOperationCount())}",
        $"Singleton (2ª Injeção): ID: {Bold(secondSingletonService.Id)}, Operação: {Bold(secondSingletonService.GetOperationCount())}",

        "", // Linha em branco para separação
        
        // Demonstração adicional (opcional): chamando um método de operação novamente na primeira injeção
        $"Scoped (1ª Injeção) Operação 2: Operação: {Bold(scopedService.GetOperationCount())}",

    };

    return Results.Text(string.Join("\n", output).Replace("\n", "<br/>"), "text/html", Encoding.UTF8);
});

app.Run();
