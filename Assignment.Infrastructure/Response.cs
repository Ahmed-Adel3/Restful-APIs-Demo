using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Infrastructure
{
    /// <summary> General Response object </summary>
    /// <typeparam name="T"> Generic Parameter [ return Type ] </typeparam>
    public class Response<T> : IActionResult
    {
        /// <summary> Boolean value determining if the request is Success or not</summary>
        public bool Success { get; set; }

        /// <summary> List of error messages if the request failed</summary>
        public string[] Messages { get; set; }

        /// <summary> Result object if result is successful</summary>
        public T Result { get; set; }

        private int StatusCode { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            //var StatusCode = Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
            var objectResult = new ObjectResult(this) { StatusCode = StatusCode };
            await objectResult.ExecuteResultAsync(context);
        }

        /// <summary> Used in case of model state validation failure (400 => Bad request ) </summary>
        /// <param name="modelstate"> Model State </param>
        /// <param name="statusCode"> Status Code </param>
        public Response(ModelStateDictionary modelstate, int statusCode)
        {
            StatusCode = statusCode;
            Success = false;
            Messages = modelstate.Values.FirstOrDefault(a => a.Errors.Any())?.Errors?.Select(e => e.ErrorMessage).ToArray();
            Result = default;
        }

        /// <summary> Used in case of model state validation failure (400 => Bad request ) </summary>
        /// <param name="statusCode"> Status Code </param>
        /// <param name="error"> Error Message </param>
        public Response(string error, int statusCode)
        {
            StatusCode = statusCode;
            Success = (statusCode >= 200) && (statusCode <= 299);
            Messages = new string[] { error };
            Result = default;
        }

        /// <summary> Used in case of success results (200 , 201, ...) </summary>
        /// <param name="data"> Data if Success </param>
        /// <param name="statusCode"> Status Code </param>
        public Response(T data, int statusCode)
        {
            StatusCode = statusCode;
            Success = true;
            Messages = null;
            Result = data;
        }
    }
}
