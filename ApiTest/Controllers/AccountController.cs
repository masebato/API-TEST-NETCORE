using ApiTest.Interfaces;
using ApiTest.Models;
using ApiTest.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
    {

        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> getAccounts()
        {
            try
            {
                var Clients = await _accountRepository.GetAll();
                return Ok(Clients);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }

        }

        [HttpGet]
        [Route("/{idAccount:int}")]
        public async Task<ActionResult<IEnumerable<Account>>> getAccountsById(int idAccount)
        {
            try
            {
                var Clients = await _accountRepository.getAccountById(idAccount);
                return Ok(Clients);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }

        }


        [HttpPost]
        public async Task<IActionResult> save([FromBody] Account account)
        {
            try
            {
                await _accountRepository.Create(account);
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> update([FromBody] Account account)
        {
            Account oAccount = await _accountRepository.getAccountById(account.AccountId);

            if (oAccount == null)
            {
                return BadRequest("account not found");
            }

            try
            {
                oAccount.Number = account.Number ?? oAccount.Number;
                oAccount.Type = account.Type ?? oAccount.Type;
                oAccount.InitialBalance = account.InitialBalance ?? oAccount.InitialBalance;
                oAccount.State = account.State ?? oAccount.State;

                await _accountRepository.update(oAccount);

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }


        [HttpDelete]
        [Route("delete/{idAccount:int}")]
        public async Task<IActionResult> Delete(int idAccount)
        {
            Account oClient = await _accountRepository.getAccountById(idAccount);

            if (oClient == null)
            {
                return BadRequest("Account not found");
            }

            try
            {
                await _accountRepository.Delete(oClient);
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }



    }
}
