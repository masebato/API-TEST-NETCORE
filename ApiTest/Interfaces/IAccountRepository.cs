using ApiTest.Models;

namespace ApiTest.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAll();
        Task<Account> getAccountById(int id);
        Task Create(Account account);
        Task update(Account account);
        Task Delete(Account account);
    }
}
