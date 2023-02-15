using ApiTest.Data;
using ApiTest.Interfaces;
using ApiTest.Models;

using Microsoft.EntityFrameworkCore;

namespace ApiTest.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankContext _dbContext;
        private readonly DbSet<Trasnsaction> _dbSet;

        public TransactionRepository(BankContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Trasnsaction>();
        }

        public async Task<IEnumerable<Trasnsaction>> GetAll()
        {
            return await _dbSet.Include(p => p.oAccount).ToListAsync();

        }

        public async Task<Trasnsaction>getById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Trasnsaction> getByAccountId(int id)
        {
            return await _dbSet.Where(p => p.AccountIdFk == id).OrderByDescending(p => p.TransactionId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<dynamic>> getTransactionDate(DateTime startDate, DateTime endDate)
        {
            var query = from transaction in _dbContext.Trasnsactions
                        join account in _dbContext.Accounts on transaction.AccountIdFk equals account.AccountId
                        join client in _dbContext.Clients on account.ClientIdFk equals client.ClientId
                        join person in _dbContext.People on client.PersonIdFk equals person.PersonId
                        where transaction.DateTransaction >= startDate && transaction.DateTransaction <= endDate
                        select new
                        {
                            transaction.DateTransaction,
                            person.Name,
                            account.Number,
                            account.Type,
                            account.InitialBalance,
                            account.State,
                            transaction.Value,
                            transaction.Balance
                        };

            return await query.ToListAsync();
        }

        public async Task Create(Trasnsaction transaction)
        {
            await _dbSet.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task update(Trasnsaction transaction)
        {
            _dbSet.Attach(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Trasnsaction transaction)
        {
            _dbSet.Remove(transaction);
            await _dbContext.SaveChangesAsync();
        }

    }
}
