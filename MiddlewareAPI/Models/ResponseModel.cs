using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareAPI.DataContext
{
    public class ResponseModel<TokenModel>
    {
        public ResponseModel()
        {
            IsSuccess = true;
            Message = "This is your token";
        }
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public TokenModel Data { get; set; }
    }
}
