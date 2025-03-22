namespace Gauss.Investment.Domain.Repositories
{
    public interface IUnitOfWork
    {
        public Task Commit();
    }
}
