using BarDoDG.Application.Constants;
using BarDoDG.Application.DTOs;
using BarDoDG.Application.Interfaces;
using BarDoDG.Application.Interfaces.Mappers;
using BarDoDG.Application.Validation;
using BarDoDG.Domain.Entities;
using BarDoDG.Domain.Interfaces.Services;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarDoDG.Application
{
    public class ApplicationServiceItemComprado : IApplicationServiceItemComprado
    {
        private readonly IServiceItemComprado _serviceItemComprado;
        private readonly IMapperItemComprado _mapperItemComprado;
        private readonly IServiceComanda _serviceComanda;
        private readonly IServiceItem _serviceItem;
        private readonly IApplicationServiceComanda _applicationServiceComanda;

        public ApplicationServiceItemComprado(IServiceItemComprado serviceItemComprado, IMapperItemComprado mapperItemComprado,
                                              IServiceComanda serviceComanda, IServiceItem serviceItem,
                                              IApplicationServiceComanda applicationServiceComanda)
        {
            _serviceItemComprado = serviceItemComprado;
            _mapperItemComprado = mapperItemComprado;
            _serviceComanda = serviceComanda;
            _serviceItem = serviceItem;
            _applicationServiceComanda = applicationServiceComanda;
        }

        public int Insert(ItemCompradoDTO itemCompradoDTO)
        {
            int id = 0;
            if (itemCompradoDTO == null)
                throw new BadRequestException("Objeto não informado.");
            else
            {
                var comanda = _serviceComanda.GetById(itemCompradoDTO.IdComanda);
                var item = _serviceItem.GetById(itemCompradoDTO.IdItem);
                if (comanda == null || item == null)
                    throw new ObjectNotFoundException("Comanda e item são obrigatórios.");

                if (comanda.DataFechamento.HasValue)
                    throw new BadRequestException("Essa comanda já foi encerrada! Não é possível inserir mais itens.");

                //Só é permitido 3 sucos por comanda.
                List<ItemCompradoDTO> itemCompradoDTOs = this.GetAllByComanda(itemCompradoDTO.IdComanda);
                if (itemCompradoDTOs?.Count > 0)
                {
                    if (itemCompradoDTOs.Where(p => p.IdItem == Consts.Item.Suco)?.ToList()?.Count == 3 && itemCompradoDTO.IdItem == Consts.Item.Suco)
                        throw new BadRequestException("Só é permitido 3 sucos por comanda.");
                }

                var itemComprado = _mapperItemComprado.MapperDTOToEntity(itemCompradoDTO);
                id = _serviceItemComprado.Insert(itemComprado);
                //Atualizar os valores da comanda
                _applicationServiceComanda.AtualizarComanda(itemComprado.IdComanda);

                return id;
            }
        }

        public List<ItemCompradoDTO> GetAllByComanda(int idComanda)
        {
            List<ItemCompradoDTO> itemCompradoDTOs = new List<ItemCompradoDTO>();
            List<ItemComprado> itemComprados = _serviceItemComprado.GetAllByComanda(idComanda);

            itemCompradoDTOs = (from c in itemComprados
                                join item in _serviceItem.GetAll() on c.IdItem equals item.IdItem
                                select new ItemCompradoDTO
                                {
                                    IdItemComprado = c.IdItemComprado,
                                    IdComanda = c.IdComanda,
                                    IdItem = c.IdItem,
                                    Descricao = item.Descricao,
                                    Valor = item.Valor
                                }).ToList();

            return itemCompradoDTOs;
        }

        public IEnumerable<ItemCompradoDTO> GetAll()
        {
            IEnumerable<ItemComprado> itemComprado = _serviceItemComprado.GetAll();
            return _mapperItemComprado.MapperListItemCompradosDTO(itemComprado);
        }

        public void Delete(int id)
        {
            var comanda = _serviceItemComprado.GetById(id);
            if (comanda == null)
                throw new ObjectNotFoundException("Objeto não encontrado.");
            else
            {
                _serviceItemComprado.Delete(id);
                //Atualizar os valores da comanda
                _applicationServiceComanda.AtualizarComanda(comanda.IdComanda);
            }
        }
    }
}
