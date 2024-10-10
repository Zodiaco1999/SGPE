using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SGPE.Comun.JWT;
using SGPE.WebApi.Services;
using SGPE.WebApi.Services.AuthService;
using SGPE.WebApi.Services.CategoriaProductoService;
using SGPE.WebApi.Services.DetallePedidoService;
using SGPE.WebApi.Services.EmpresaService;
using SGPE.WebApi.Services.EstadoPedidoService;
using SGPE.WebApi.Services.EstadoProductoService;
using SGPE.WebApi.Services.MenuService;
using SGPE.WebApi.Services.ModuloService;
using SGPE.WebApi.Services.PedidoService;
using SGPE.WebApi.Services.PerfilService;
using SGPE.WebApi.Services.ProductoService;
using SGPE.WebApi.Services.UsuarioService;
using System.Text;

namespace SGPE.WebApi;

public static class ContenedorDependencias
{
    public static IServiceCollection AddAplicacionesServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Adición Contexto de base de datos
        services.AddDbContext<SGPEContext>(options => options.UseSqlServer(configuration.GetConnectionString("SGPEConnStr")));
        // Adición y configuracion de Swagger
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Por favor inserte el jwt despues de la palabra bearer de esta forma \"<strong>bearer {JWT}</strong>\"",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        // Adición de AutoMapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        // Adicion de CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin",
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
        });
        // Añadir autentificación el Aplicación por JWT
        var jwtOptions = configuration.GetSection(nameof(JWTOptions));

        services.Configure<JWTOptions>(options =>
        {
            options.Issuer = jwtOptions[nameof(JWTOptions.Issuer)]!;
            options.Audience = jwtOptions[nameof(JWTOptions.Audience)]!;
            options.ValidForMinutes = int.Parse(jwtOptions[nameof(JWTOptions.ValidForMinutes)]!);
            options.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["SecretKeyJWT"]!)), SecurityAlgorithms.Aes128CbcHmacSha256);
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions[nameof(JWTOptions.Issuer)],

                    ValidateAudience = true,
                    ValidAudience = jwtOptions[nameof(JWTOptions.Audience)],

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKeyJWT"]!)),

                    ClockSkew = TimeSpan.FromMinutes(configuration.GetValue<int>("JwtOptions:ValidForMinutes"))
                };
            });

        services.AddScoped<IJWTFactory, JWTFactory>();
        services.AddScoped<ICorreoService, CorreoService>();

        // services de entidades
        services.AddScoped<IProductoService, ProductoService>();
        services.AddScoped<IEstadoProductoService, EstadoProductoService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IPedidoService, PedidoService>();
        services.AddScoped<IEstadoPedidoService, EstadoPedidoService>();
        services.AddScoped<IDetallePedidoService, DetallePedidoService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICategoriaProductoService, CategoriaProductoService>();
        services.AddScoped<IEmpresaService, EmpresaService>();
        services.AddScoped<IModuloService, ModuloService>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IPerfilService, PerfilService>();

        return services;
    }
}
