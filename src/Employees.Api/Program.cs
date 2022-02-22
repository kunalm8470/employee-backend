using Employees.Api.Middlewares;
using Employees.Application.Handlers.v1.Commands.Employees;
using Employees.Application.Handlers.v1.Queries.Employees;
using Employees.Domain.Interfaces;
using Employees.Domain.Models;
using Employees.Infrastructure.Data;
using Employees.Infrastructure.Data.Repositories.v1;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

builder.Services.AddDbContext<EmployeesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMvc()
    .AddFluentValidation(fv => 
    {
        fv.RegisterValidatorsFromAssemblyContaining<Program>();
        fv.DisableDataAnnotationsValidation = true;
    });

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
  
builder.Services.AddTransient<IRequestHandler<GetEmployeesQuery, (List<Employee>, int)>, GetEmployeesQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetEmployeeByIdQuery, Employee?>, GetEmployeeByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<CreateEmployeeCommand, Employee>, CreateEmployeeCommandHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateEmployeeCommand, Unit>, UpdateEmployeeCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteEmployeeCommand, Unit>, DeleteEmployeeCommandHandler>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee backend",
        Description = "Backend to manage employees",
        Contact = new OpenApiContact
        {
            Name = "Kunal Mukherjee"
        }
    });

    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddControllers();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(o =>
    {
        o.AddPolicy("CorsPolicy",
            builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
    });
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("CorsPolicy");
}

app.UseMiddleware<UnhandledExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation("Seeding Database");
using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider provider = scope.ServiceProvider;
    try
    {
        EmployeesContext context = provider.GetRequiredService<EmployeesContext>();
        await EmployeeContextSeed.SeedAsync(context, app.Logger).ConfigureAwait(false);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Run();
