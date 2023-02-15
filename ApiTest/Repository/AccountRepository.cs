using ApiTest.Data;
using ApiTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Interfaces
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankContext _dbContext;
        private readonly DbSet<Account> _dbSet;

        public AccountRepository(BankContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Account>();
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _dbSet.Include(p => p.oClient).ToListAsync();

        }

        public async Task<Account> getAccountById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Create(Account account)
        {
            await _dbSet.AddAsync(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task update(Account account)
        {
            _dbSet.Attach(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Account account)
        {
            _dbSet.Remove(account);
            await _dbContext.SaveChangesAsync();
        }

    }
}
