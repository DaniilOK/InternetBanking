using IB.Common;
using IB.Services.Interface.Commands;
using IB.Services.Interface.Interfaces;
using IB.Services.Interface.Models.Enums;
using InternetBanking.Filters;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPut("api/transfer")]
        [CommitRequired]
        public IActionResult MakeTransfer([FromBody]TransferCommand command)
        {
            var result = _paymentService.MakeTransfer(command);

            if (result != TransferResult.Success)
            {
                return BadRequest(result.GetStringValue());
            }

            return Ok(result.GetStringValue());
        }

        [HttpPut("api/card/payment")]
        [CommitRequired]
        public IActionResult MakeCardPayment([FromBody]CardPaymentCommand command)
        {
            var result = _paymentService.MakePayment(command);

            if (result != PaymentResult.Success)
            {
                return BadRequest(result.GetStringValue());
            }

            return Ok(result.GetStringValue());
        }

        [HttpPut("api/bankaccount/payment")]
        [CommitRequired]
        public IActionResult MakeBankAccountPayment([FromBody]BankAccountPaymentCommand command)
        {
            var result = _paymentService.MakePayment(command);

            if (result != PaymentResult.Success)
            {
                return BadRequest(result.GetStringValue());
            }

            return Ok(result.GetStringValue());
        }
    }
}
