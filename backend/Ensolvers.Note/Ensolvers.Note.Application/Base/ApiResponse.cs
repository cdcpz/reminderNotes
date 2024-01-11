using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Application.Base
{
    public class ApiResponse<T>
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        /// <summary>
        /// This method make a response without data but with personalized status and message
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        public ApiResponse(HttpStatusCode status, string message)
        {
            Status = status;
            Message = message;
            Data = default;
        }

        /// <summary>
        /// This method make a successful response with a personalized message and data
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public ApiResponse(string message, T data)
        {
            Status = HttpStatusCode.OK;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// This method make a fully personalized response
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public ApiResponse(HttpStatusCode status, string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
