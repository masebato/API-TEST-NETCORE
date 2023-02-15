
using ApiTest.Interfaces;
using ApiTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            try
            {
                var Clients = await _clientRepository.GetAll();
                return Ok(Clients);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
           
        }


        [HttpPost]
        public async Task<IActionResult> save([FromBody] Client client)
        {
            try
            {
                await _clientRepository.Create(client);
                return Ok(new { message = "ok" });
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> update([FromBody] Client client)
        {
            Client oClient = await _clientRepository.getOne(client.ClientId);

            if(oClient == null)
            {
                return BadRequest("Client not found");
            }

            try
            {
                oClient.State = client.State ?? oClient.State;
                oClient.Password = client.Password ?? oClient.Password;
                oClient.oPerson.Address = client.oPerson.Address ?? oClient.oPerson.Address;
                oClient.oPerson.Identification = client.oPerson.Address ?? oClient.oPerson.Address;
                oClient.oPerson.Name = client.oPerson.Name ?? oClient.oPerson.Name;
                oClient.oPerson.Gender = client.oPerson.Gender ?? oClient.oPerson.Gender;
                oClient.oPerson.Age = client.oPerson.Age ?? oClient.oPerson.Age;

                await _clientRepository.update(oClient);

                return Ok(new { message = "ok" });
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }


        [HttpDelete]
        [Route("delete/{idClient:int}")]
        public async Task<IActionResult> Delete(int idClient)
        {
            Client oClient = await _clientRepository.getOne(idClient);

            if (oClient == null)
            {
                return BadRequest("Client not found");
            }

            try
            {
                await _clientRepository.Delete(oClient);
                return Ok(new { message = "ok" });
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

    }
}
