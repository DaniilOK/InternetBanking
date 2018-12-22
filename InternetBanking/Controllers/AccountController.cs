using System;
using IB.Common;
using IB.Services.Interface.Commands;
using IB.Services.Interface.Interfaces;
using IB.Services.Interface.Models.Enums;
using InternetBanking.Filters;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("api/account")]
        [CommitRequired]
        public IActionResult CreateBankAccount([FromBody]CreateBankAccountCommand command)
        {
            var result = _accountService.CreateAccount(command.UserId);

            if (result != CreateAccountResult.Success)
            {
                return BadRequest(result.GetStringValue());
            }

            return Ok(result.GetStringValue());
        }

        [HttpPost("api/card")]
        [CommitRequired]
        public IActionResult CreateBankCard([FromBody]CreateBankCardCommand command)
        {
            var result = _accountService.CreateCard(command.UserId, command.BankAccountId);

            if (result != CreateBankCardResult.Success)
            {
                return BadRequest(result.GetStringValue());
            }

            return Ok(result.GetStringValue());
        }

        [HttpGet("api/account/{id:guid}")]
        public IActionResult GetAccount(Guid id)
        {
            var account = _accountService.GetAccount(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpGet("api/account/all/{userId:guid}")]
        public IActionResult GetAccounts(Guid userId)
        {
            var account = _accountService.GetAccounts(userId);
            return Ok(account);
        }

        [HttpGet("api/card/{id:guid}")]
        public IActionResult GetCard(Guid id)
        {
            var card = _accountService.GetCard(id);

            if (card == null)
            {
                return NotFound();
            }

            return Ok(card);
        }

        [HttpGet("api/card/all/{userId:guid}")]
        public IActionResult GetCards(Guid userId)
        {
            var card = _accountService.GetCards(userId);
            return Ok(card);
        }
    }
}
