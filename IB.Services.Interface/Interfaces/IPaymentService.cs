using IB.Services.Interface.Commands;
using IB.Services.Interface.Models.Enums;

namespace IB.Services.Interface.Interfaces
{
    public interface IPaymentService
    {
        TransferResult MakeTransfer(TransferCommand command);

        PaymentResult MakePayment(CardPaymentCommand command);

        PaymentResult MakePayment(BankAccountPaymentCommand command);
    }
}
