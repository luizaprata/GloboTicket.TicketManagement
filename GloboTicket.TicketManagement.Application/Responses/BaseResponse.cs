using System.Collections.Generic;

namespace GloboTicket.TicketManagement.Application.Responses
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
        }

        public BaseResponse(string message = null)
        {
            Success = true;
            Message = message;
        }

        public BaseResponse(string message = null, bool success)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; internal set; }
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}
