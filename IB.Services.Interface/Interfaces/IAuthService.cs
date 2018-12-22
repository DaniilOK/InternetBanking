using System;
using IB.Services.Interface.Commands;
using IB.Services.Interface.Models;
using IB.Services.Interface.Models.Enums;

namespace IB.Services.Interface.Interfaces
{
    public interface IAuthService
    {
        LoginModel Login(LoginCommand loginCommand);

        RegistrationModel Registration(RegistrationCommand registrationCommand);



        BanResult BanUser(Guid id, bool isBan);

        EmailConfirmedResult ConfirmEmail(Guid id, bool confirm);
    }
}
