namespace AwesomeGICBank.InMemoryPersistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly Bank _bank;
        protected BaseRepository(Bank bank)
        {
            _bank = bank;
        }
    }
}
