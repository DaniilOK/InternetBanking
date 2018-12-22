using System;
using System.Collections.Generic;

namespace IB.Services.Interface.Models
{
    public class ProfileModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Inactive { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public IEnumerable<string> Permissions { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public ProfileModel(Guid id, string firstName, string lastName, bool inactive, string email, bool isEmailConfirmed,
            IEnumerable<string> permissions)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Inactive = inactive;
            Email = email;
            IsEmailConfirmed = isEmailConfirmed;
            Permissions = permissions ?? new List<string>();
        }
    }
}
