using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Authentication.Services
{
    public interface IRefreshTokenService
    {
        string GenerateToken();
    }
}
