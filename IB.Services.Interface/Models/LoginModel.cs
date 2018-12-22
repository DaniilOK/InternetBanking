using IB.Services.Interface.Models.Enums;

namespace IB.Services.Interface.Models
{
    public class LoginModel
    {
        public LoginResult LoginResult { get; }
        public string UserId { get; }
        public string FullName { get; }
        public string Permissions { get; }

        public LoginModel(LoginResult loginResult)
        {
            LoginResult = loginResult;
        }

        public LoginModel(LoginResult loginResult, string userId, string fullName, string permissions)
        {
            LoginResult = loginResult;
            UserId = userId;
            FullName = fullName;
            Permissions = permissions;
        }
    }
}
