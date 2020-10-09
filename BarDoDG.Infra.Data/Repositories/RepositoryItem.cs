using BarDoDG.Domain.Interfaces.Repositories;
using BarDoDG.Infra.Data.Context;

namespace BarDoDG.Infra.Data.Repositories
{
    public class RepositoryItem : RepositoryBase<Domain.Entities.Item>, IRepositoryItem
    {
        private readonly BARDGContext _dbContext;

        public RepositoryItem(BARDGContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
