using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SGPE.Comun.Excepcion;
using SGPE.Comun.Models;
using System.Data.SqlClient;
using System.Net;

namespace SGPE.Comun.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<HttpContext> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context) 
        {
            try 
            {
                try
                {
                    var body = await new StreamReader(context.Request.Body).ReadToEndAsync();

                    context.Request.Body.Position = 0;

                    var json = JsonConvert.SerializeObject(new
                    {
                        path = context.Request.Path.Value,
                        userId = context.User.Identity?.Name,
                        method = context.Request.Method,
                        remoteIp = $"{context.Connection.RemoteIpAddress}:{context.Connection.RemotePort}",
                        body
                    });

                    _logger.LogInformation("Invoke {0}", json);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex, "Error leyendo 'Request.Body'");
                }

                await _next.Invoke(context);
            }
            catch(Exception ex)
            {
                int idError = new Random().Next(100000, 999999);

                try
                {
                    var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "CustomErrorId: {@CustomErrorId}. Message: {@Message}. Description: {@description}. Host: {@Host}. Path: {@Path}. UserId: {@UserId}. Method: {@Method}. RemoteIpAddress: {@RemoteIpAddress}", idError, ex.Message, "Error leyendo 'Request.Body'", context.Request.Host.Value, context.Request.Path.Value, context.User.Identity?.Name, context.Request.Method, context.Connection.RemoteIpAddress.ToString());
                }

                await HandleExceptionAsync(context, ex, idError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, int idError)
        {
            var response = context.Response;

            var json = JsonConvert.SerializeObject(new { path = context.Request.Path.Value, userid = context.User.Identity?.Name, method = context.Request.Method, remoteIp = $"{context.Connection.RemoteIpAddress}:{context.Connection.RemotePort}" });

            CustomErrorResponse customError = new CustomErrorResponse();
            customError.Id = idError;

            if (exception is DbUpdateException dbUpdateException && dbUpdateException.InnerException is SqlException sqlException && sqlException.Number == 547)
            {
                customError.StatusCode = (int)HttpStatusCode.BadRequest;
                customError.TypeException = TypeException.Error;
                customError.Message = $"Se generó un conflicto con una restricción asociada a la entidad. No se pudo finalizar la transacción";
                customError.IsWarning = true;

                _logger.LogError(exception, "CustomErrorId: {@CustomErrorId}. Message: {@Message}. Description: {@description}. Host: {@Host}. Path: {@Path}. UserId: {@UserId}. Method: {@Method}. RemoteIpAddress: {@RemoteIpAddress}", customError.Id, customError.TypeException, customError.Message, context.Request.Host.Value, context.Request.Path.Value, context.User.Identity?.Name, context.Request.Method, context.Connection.RemoteIpAddress.ToString());
            }
            else if (exception is BadRequestException badRequestException)
            {
                customError.StatusCode = badRequestException.Code;
                customError.TypeException = TypeException.Error;
                customError.Message = badRequestException.Message;

                if (badRequestException.IsWarning)
                {
                    customError.TypeException = TypeException.Warning;
                    _logger.LogWarning(badRequestException, $"{badRequestException.Description}. {json}");
                }
                else
                {
                    _logger.LogError(exception, "CustomErrorId: {@CustomErrorId}. Message: {@Message}. Description: {@description}. Host: {@Host}. Path: {@Path}. UserId: {@UserId}. Method: {@Method}. RemoteIpAddress: {@RemoteIpAddress}", customError.Id, customError.TypeException, $"{(string.IsNullOrEmpty(customError.Message) ? "" : customError.Message + ", ")}{exception.InnerException?.Message}", context.Request.Host.Value, context.Request.Path.Value, context.User.Identity?.Name, context.Request.Method, context.Connection.RemoteIpAddress.ToString());
                }
            }
            else if (exception is NotFoundException notFoundException)
            {
                customError.StatusCode = notFoundException.Code;
                customError.TypeException = TypeException.Error;
                customError.Message = notFoundException.Message;

                _logger.LogError(exception, "CustomErrorId: {@CustomErrorId}. Message: {@Message}. Description: {@description}. Host: {@Host}. Path: {@Path}. UserId: {@UserId}. Method: {@Method}. RemoteIpAddress: {@RemoteIpAddress}", customError.Id, customError.TypeException, customError.Message, context.Request.Host.Value, context.Request.Path.Value, context.User.Identity?.Name, context.Request.Method, context.Connection.RemoteIpAddress.ToString());
            }
            else if (exception is ValidationException validationException)
            {
                customError.StatusCode = (int)HttpStatusCode.NotAcceptable;
                customError.TypeException = TypeException.Warning;
                customError.Message = validationException.Message;
                customError.IsWarning = true;

                _logger.LogWarning(exception, "CustomErrorId: {@CustomErrorId}. Message: {@Message}. Description: {@description}. Host: {@Host}. Path: {@Path}. UserId: {@UserId}. Method: {@Method}. RemoteIpAddress: {@RemoteIpAddress}", idError, customError.TypeException, customError.Message, context.Request.Host.Value, context.Request.Path.Value, context.User.Identity?.Name, context.Request.Method, context.Connection.RemoteIpAddress.ToString());
            }
            else
            {
                customError.StatusCode = (int)HttpStatusCode.InternalServerError;
                customError.TypeException = TypeException.Error;
                customError.Message = "Intente de nuevo, si el problema persiste contacte con soporte";

                _logger.LogError(exception, "CustomErrorId: {@CustomErrorId}. Message: {@Message}. Description: {@description}. Host: {@Host}. Path: {@Path}. UserId: {@UserId}. Method: {@Method}. RemoteIpAddress: {@RemoteIpAddress}", customError.Id, exception.Message, exception.InnerException?.Message, context.Request.Host.Value, context.Request.Path.Value, context.User.Identity?.Name, context.Request.Method, context.Connection.RemoteIpAddress.ToString());
            }

            response.ContentType = "application/json";
            response.StatusCode = customError.StatusCode;

            var serviceResponse = new ServiceResponse
            {
                Data = new 
                {
                   customError.Id,
                   customError.IsWarning,
                   customError.StatusCode,
                   customError.Detail
                },
                Message = customError.Message,
                Success = false
            };

            await response.WriteAsync(JsonConvert.SerializeObject(serviceResponse, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }

    }
}
