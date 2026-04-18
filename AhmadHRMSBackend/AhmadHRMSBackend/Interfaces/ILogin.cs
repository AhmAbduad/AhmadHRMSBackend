using AhmadHRMSBackend.dto.Login;

namespace AhmadHRMSBackend.Interfaces
{
    public interface ILogin
    {
        Task<string> CheckUser(CheckUserDto dto);
    }
}
