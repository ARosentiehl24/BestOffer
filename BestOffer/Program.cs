using BestDeal.Business.Offer;
using BestDeal.Business.Offer.Contract;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options =>
{
    options.FormatterMappings.SetMediaTypeMappingForFormat
        ("xml", MediaTypeHeaderValue.Parse("application/xml"));
    options.FormatterMappings.SetMediaTypeMappingForFormat
        ("config", MediaTypeHeaderValue.Parse("application/xml"));
    options.FormatterMappings.SetMediaTypeMappingForFormat
        ("js", MediaTypeHeaderValue.Parse("application/json"));
}).AddXmlSerializerFormatters();

builder.Services.AddControllers(options =>
{
    //options.InputFormatters.Insert(0, new Microsoft.AspNetCore.Mvc.Formatters.XmlSerializerInputFormatter(new Microsoft.AspNetCore.Mvc.MvcOptions { }));
    options.OutputFormatters.Add(new Microsoft.AspNetCore.Mvc.Formatters.XmlSerializerOutputFormatter());
}).AddXmlDataContractSerializerFormatters(); 

//Dependency Injection
builder.Services.AddScoped<IOfferBusiness, OfferBusiness>();

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