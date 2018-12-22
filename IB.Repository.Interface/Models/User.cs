using System;
using IB.Common.Helpers;

namespace IB.Repository.Interface.Models
{
    public class User : Entity<Guid>
    {
        public const int LoginLength = 128;
        public const int FirstNameLength = 128;
        public const int LastNameLength = 128;
        public const int EmailLength = 128;
        public const int SaltLength = 100;

        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Inactive { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public static User CreateUser(string login, string firstName, string lastName, string email, string password)
        {
            var salt = PasswordHelper.GenerateSalt(SaltLength);
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Login = login,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Inactive = false,
                PasswordSalt = salt,
                PasswordHash = PasswordHelper.ComputeHash(password, salt)
            };
            return user;
        }

        public bool VerifyPassword(string password)
        {
            return !string.IsNullOrEmpty(password) &&
                   PasswordHelper.ComputeHash(password, PasswordSalt) == PasswordHash;
        }
    }
}
