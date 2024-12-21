using AwesomeGICBank.Domain.Entities;

namespace AwesomeGICBank.InMemoryPersistence.Repositories
{
    public class Bank
    {
        public List<Account> Accounts { get; set; } = new();

    }
}
