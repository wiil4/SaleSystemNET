using Microsoft.EntityFrameworkCore;
using WSSale.Models;

var builder = WebApplication.CreateBuilder(args);

var MyCORS = "MyCors";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding ContextConnection
builder.Services.AddDbContext<RealSaleContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
});

//ADDING CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyCORS, build =>
    {
        build.WithHeaders("*"); //POST
        build.WithOrigins("*"); //GET
        build.WithMethods("*"); //PUT AND DELETE
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//USING CORS
app.UseCors(MyCORS);

app.UseAuthorization();

app.MapControllers();

app.Run();
