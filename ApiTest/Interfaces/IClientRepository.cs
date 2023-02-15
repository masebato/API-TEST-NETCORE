using ApiTest.Models;

namespace ApiTest.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAll();
        Task Create(Client client);
        Task<Client> getOne(int id);
        Task update(Client client);
        Task Delete(Client client);
    }
}
