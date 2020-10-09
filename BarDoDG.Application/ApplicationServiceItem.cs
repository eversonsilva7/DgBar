using BarDoDG.Application.DTOs;
using BarDoDG.Application.Interfaces;
using BarDoDG.Application.Interfaces.Mappers;
using BarDoDG.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace BarDoDG.Application
{
    public class ApplicationServiceItem : IApplicationServiceItem
    {
        private readonly IServiceItem _serviceItem;
        private readonly IMapperItem _mapperItem;

        public ApplicationServiceItem(IServiceItem serviceItem, IMapperItem mapperItem)
        {
            _serviceItem = serviceItem;
            _mapperItem = mapperItem;
        }

        public void Add(ItemDTO itemDTO)
        {
            var item = _mapperItem.MapperDTOToEntity(itemDTO);            
            _serviceItem.Add(item);
        }

        public IEnumerable<ItemDTO> GetAll()
        {
            //recupera a lista de Itens e retorna uma lista em DTO
            var itens = _serviceItem.GetAll();
            return _mapperItem.MapperListItemsDTO(itens);
        }

        public ItemDTO GetById(int id)
        {
            var item = _serviceItem.GetById(id);
            return _mapperItem.MapperEntityToDTO(item);
        }

        public void Delete(int id)
        {            
            _serviceItem.Delete(id);
        }

        public void Update(ItemDTO itemDTO)
        {
            var item = _mapperItem.MapperDTOToEntity(itemDTO);
            _serviceItem.Update(item);
        }
    }
}
