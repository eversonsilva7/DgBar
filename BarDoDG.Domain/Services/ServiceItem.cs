using BarDoDG.Domain.Entities;
using BarDoDG.Domain.Interfaces.Repositories;
using BarDoDG.Domain.Interfaces.Services;

namespace BarDoDG.Domain.Services
{
    public class ServiceItem : ServiceBase<Item>, IServiceItem
    {
        private readonly IRepositoryItem _repositoryItem;

        public ServiceItem(IRepositoryItem repositoryItem) : base(repositoryItem)
        {
            this._repositoryItem = repositoryItem;
        }
    }
}
