using MiddlewareAPI.DataContext;
using MiddlewareAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareAPI.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<ResponseModel<TokenModel>> LoginAsync(LoginModel login);
        Task<ResponseModel<TokenModel>> RefreshTokenAsync(TokenModel request);
    }
}

