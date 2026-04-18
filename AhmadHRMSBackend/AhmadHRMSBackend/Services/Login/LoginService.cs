using AhmadHRMSBackend.dto.Login;
using AhmadHRMSBackend.UnitofWork;
using Microsoft.AspNetCore.Mvc;

namespace AhmadHRMSBackend.Services.Login
{
    public class LoginService
    {
        private readonly IUnitofWork _unitOfWork;

        public LoginService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> CheckUser(CheckUserDto dto)
        {
            var result = await _unitOfWork.Login.CheckUser(dto);
            return result;
        }
    }
}
