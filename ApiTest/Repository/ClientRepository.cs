using ApiTest.Data;
using ApiTest.Interfaces;
using ApiTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly BankContext _dbContext;
        private readonly DbSet<Client> _dbSet;

        public ClientRepository(BankContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Client>();
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _dbSet.Include(p => p.oPerson).ToListAsync();

        }
        public async Task Create(Client client)
        {
            var setPerson =  _dbContext.Set<Person>();
            await setPerson.AddAsync(client.oPerson);

            await _dbSet.AddAsync(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Client> getOne(int id)
        {
            Client currentClient = await _dbSet.FindAsync(id);

            currentClient = _dbSet.Include(p => p.oPerson).Where(p => p.ClientId == id).FirstOrDefault();

            return currentClient;
        }

        public async Task update(Client client)
        {
            _dbSet.Attach(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Client client)
        {
           
            var setPerson = _dbContext.Set<Person>();
            setPerson.Remove(client.oPerson);
            _dbSet.Remove(client);
            await _dbContext.SaveChangesAsync();
        }

    }
}
