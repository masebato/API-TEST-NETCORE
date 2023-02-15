using ApiTest.Interfaces;
using ApiTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionController(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trasnsaction>>> getAccounts()
        {
            try
            {
                var trasnsactions = await _transactionRepository.GetAll();
                return Ok(trasnsactions);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }

        }


        [HttpGet]
        [Route("/reportes")]
        public async Task<ActionResult<IEnumerable<dynamic>>> getTransactionBydate([FromQuery]string startDate, [FromQuery]string endDate )
        {
            try
            {
                DateTime start = DateTime.Parse(startDate);
                DateTime end = DateTime.Parse(endDate);

                var trasnsactions = await _transactionRepository.getTransactionDate(start,end);
                return Ok(trasnsactions);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }

        }

        [HttpGet]
        [Route("/{idTransaction:int}")]
        public async Task<ActionResult<IEnumerable<Trasnsaction>>> getTransactionsById(int idTransaction)
        {
            try
            {
                var trasnsactions = await _transactionRepository.getById(idTransaction);
                return Ok(trasnsactions);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }

        }


        [HttpPost]
        public async Task<IActionResult> save([FromBody] Trasnsaction transaction)
        {
            try
            {
                Trasnsaction oTransaction = await _transactionRepository.getByAccountId(transaction.AccountIdFk);
                int currentBalance = 0;
                int substractValue = 0;

                Account oAccount = await _accountRepository.getAccountById(transaction.AccountIdFk);
                if (oAccount == null)
                {
                    return BadRequest("account not found");
                }

                if (oTransaction== null)
                {
                    currentBalance = int.Parse(oAccount.InitialBalance);
                    substractValue = int.Parse(transaction.Value);
                }
                else
                {
                    currentBalance = int.Parse(oTransaction.Balance);
                    substractValue = int.Parse(transaction.Value);

                }
              
                if (transaction.Type == "debit")
                {

                    int haveBalance = (currentBalance - substractValue);

                    if (haveBalance < 0)
                    {
                        return BadRequest("saldo no disponible");
                    }
                    transaction.Balance = haveBalance.ToString();
                }
                else if(transaction.Type == "credit")
                {
                    int haveBalance = (currentBalance + substractValue);
                    transaction.Balance = haveBalance.ToString();
                }
              
                transaction.DateTransaction = DateTime.Now;

                await _transactionRepository.Create(transaction);
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> update([FromBody] Trasnsaction transaction)
        {
            Trasnsaction oTransaction = await _transactionRepository.getById(transaction.TransactionId);

            if (oTransaction == null)
            {
                return BadRequest("transaction not found");
            }

            try
            {
                oTransaction.Balance = transaction.Balance ?? oTransaction.Balance;
                oTransaction.Type = transaction.Type ?? oTransaction.Type;
                oTransaction.Value = transaction.Value ?? oTransaction.Value;
              
                await _transactionRepository.update(oTransaction);

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }


        [HttpDelete]
        [Route("delete/{idTransaction:int}")]
        public async Task<IActionResult> Delete(int idTransaction)
        {
            Trasnsaction oTransaction = await _transactionRepository.getById(idTransaction);

            if (oTransaction == null)
            {
                return BadRequest("transaction not found");
            }

            try
            {
                await _transactionRepository.Delete(oTransaction);
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }




    }
}
