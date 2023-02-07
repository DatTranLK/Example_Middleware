using Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos
{
    public class ErrorResponseModel
    {
        public ResponseCode ResponseCode { get; set; }
        public string Message { get; set; }
        public ErrorResponseModel(ResponseCode responseCode, string message)
        {
            ResponseCode = responseCode;
            Message = message;
        }
    }
}
