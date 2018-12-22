namespace IB.Services.Interface.Commands
{
    public class LoginCommand : BaseCommand
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
