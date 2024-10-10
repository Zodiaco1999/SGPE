using Microsoft.AspNetCore.Mvc;
using SGPE.Comun.Models;

namespace SGPE.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        public ServiceResponse response = new();

        [NonAction]
        public void SetDataResponse(object Data)
        {
            response.Data = Data;
        }

        [NonAction]
        public void SetMsgErrorResponse(Exception ex)
        {
            response.Message = GetExceptionMessage(ex);
            response.Success = false;
        }

        [NonAction]
        public void SetMsgErrorResponse(string msgError)
        {
            response.Message = msgError;
            response.Success = false;
        }

        [NonAction]
        public void SetMessageResponse(string msg)
        {
            response.Message = msg;
        }

        [NonAction]
        public void SetErrorResponse(bool success)
        {
            response.Success = success;
        }

        [NonAction]
        public string GetExceptionMessage(Exception ex)
        {
            return ex.Message + $"{(ex.InnerException != null ? " => " + GetExceptionMessage(ex.InnerException) : "")}";
        }

    }
}
