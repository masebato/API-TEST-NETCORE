using ApiTest.Models;

namespace ApiTest.Interfaces
{
    public interface ITransactionRepository
    {

        Task<IEnumerable<Trasnsaction>> GetAll();
        Task<Trasnsaction> getById(int id);
        Task Create(Trasnsaction transaction);
        Task update(Trasnsaction transaction);
        Task Delete(Trasnsaction transaction);
        Task<Trasnsaction> getByAccountId(int id);
        Task<IEnumerable<dynamic>> getTransactionDate(DateTime startDate, DateTime endDate);
    }
}
