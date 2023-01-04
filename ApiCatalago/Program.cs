using ApiCatalago.ApiEndpoints;
using ApiCatalago.AppServicesExtensions;
using ApiCatalago.Context;
using ApiCatalago.Models;
using ApiCatalago.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAutenticationJwt();

var app = builder.Build();

app.MapCategoriasEndpoints();
app.MapAutenticacaoEndpoints();
app.MapProdutosEndpoints();


var environment = app.Environment;
app.UseExceptionHandling(environment)
    .UseSwaggerMiddleweare()
    .UseAppCors();

app.UseAuthentication();
app.UseAuthorization();

app.Run();