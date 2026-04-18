using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.dto.Login;
using AhmadHRMSBackend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AhmadHRMSBackend.DataAccessLayer.Login
{
    public class LoginRepository:ILogin
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;
        public LoginRepository(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<string> CheckUser(CheckUserDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.email) || string.IsNullOrWhiteSpace(dto.password))
                return null;

            // 🔍 Find user by email
            var user = await _context.Users
             .Include(x => x.Role)
             .FirstOrDefaultAsync(x =>
                 x.Email == dto.email &&
                 !x.IsDeleted);

            if (user == null)
                return null;

            // 🔒 Check active / locked
            if (!user.IsActive || user.IsLocked)
                return null;

            // 🔐 Verify password
            bool isPasswordValid = VerifyPassword(
                dto.password,
                user.PasswordHash,
                user.PasswordSalt
            );

            if (!isPasswordValid)
            {
                user.FailedLoginAttempts += 1;

                // 🔒 Lock after 3 failed attempts
                if (user.FailedLoginAttempts >= 3)
                {
                    user.IsLocked = true;
                }

                await _context.SaveChangesAsync();
                return null;
            }

            // ✅ Login success
            user.FailedLoginAttempts = 0;
            user.LastLoginDate = DateTime.Now;

            await _context.SaveChangesAsync();

            // 🔐 JWT generate
            var token = _jwtService.GenerateToken(user);

            return token;
        }

        private bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            //using (var hmac = new System.Security.Cryptography.HMACSHA512(
            //    Convert.FromBase64String(storedSalt)))
            //{
            //    var computedHash = Convert.ToBase64String(
            //        hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
            //    );

            //    return computedHash == storedHash;
            //}

            if(password==storedHash)
                return true;
            else
            {
                return false;
            }
        }
    }
}
