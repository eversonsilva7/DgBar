using BarDoDG.Application.Constants;
using BarDoDG.Application.DTOs;
using BarDoDG.Application.Interfaces;
using BarDoDG.Application.Interfaces.Mappers;
using BarDoDG.Application.Validation;
using BarDoDG.Domain.Entities;
using BarDoDG.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarDoDG.Application
{
    public class ApplicationServiceComanda : IApplicationServiceComanda
    {
        private readonly IServiceComanda _serviceComanda;
        private readonly IMapperComanda _mapperComanda;
        private readonly IServiceCliente _serviceCliente;
        private readonly IMapperCliente _mapperCliente;
        private readonly IServiceItemComprado _serviceItemComprado;
        private readonly IServiceItem _serviceItem;
        //private readonly IApplicationServiceItemComprado _applicationServiceItemComprado; //Utilizando ele dá referência circular

        public ApplicationServiceComanda(IServiceComanda serviceComanda, IMapperComanda mapperComanda,
                                         IServiceCliente serviceCliente, IMapperCliente mapperCliente,
                                         IServiceItemComprado serviceItemComprado, IServiceItem serviceItem)
        {
            _serviceComanda = serviceComanda;
            _mapperComanda = mapperComanda;
            _serviceCliente = serviceCliente;
            _mapperCliente = mapperCliente;            
            _serviceItemComprado = serviceItemComprado;
            _serviceItem = serviceItem;
        }

        public void Add(ComandaDTO comandaDTO)
        {
            if (comandaDTO == null)
            {
                throw new ObjectNotFoundException("Objeto não informado.");
            }
            else
            {
                var comanda = _mapperComanda.MapperDTOToEntity(comandaDTO);
                _serviceComanda.Add(comanda);
            }
        }

        /// <summary>
        /// Se o idCliente não estiver cadastrado, mas passar o nome do cliente, será criado um cliente para a comanda
        /// </summary>
        /// <param name="comandaInsertDTO"></param>
        /// <returns></returns>
        public int Insert(ComandaInsertDTO comandaInsertDTO)
        {
            if (comandaInsertDTO == null)
                throw new BadRequestException("Objeto não informado.");
            else
            {
                Comanda comandaEntity = new Comanda(); 
                var cliente = _serviceCliente.GetById(comandaInsertDTO.IdCliente ?? 0);
                if (cliente == null && string.IsNullOrEmpty(comandaInsertDTO.NomeCliente))
                    throw new ObjectNotFoundException("Cliente não encontrado.");
                else
                {
                    //Se o cliente já existe, só adiciona na comanda
                    if (cliente?.IdCliente > 0)
                    {
                        comandaEntity.IdCliente = cliente.IdCliente;
                    }
                    else
                    {
                        ClienteDTO clienteDTO = new ClienteDTO()
                        {
                            IdCliente = 0,
                            Nome = comandaInsertDTO.NomeCliente
                        };
                        comandaEntity.IdCliente = _serviceCliente.Insert(_mapperCliente.MapperDTOToEntity(clienteDTO));
                        if (comandaEntity.IdCliente <= 0)
                            throw new ObjectNotFoundException("Não foi possível criar o cliente.");
                    }
                }

                comandaEntity.DataAbertura = DateTime.Now;
                comandaEntity.DataFechamento = null;
                comandaEntity.Desconto = null;
                
                return _serviceComanda.Insert(comandaEntity);
            }
        }

        public List<ComandaDTO> GetAllPersonalized()
        {
            List<ComandaDTO> comandaDTOs = new List<ComandaDTO>();
            comandaDTOs = (from c in _serviceComanda.GetAll()
                           join cli in _serviceCliente.GetAll() on c.IdClienteNavigation.IdCliente equals cli.IdCliente
                           select new ComandaDTO
                           {
                               IdComanda = c.IdComanda,
                               DataAbertura = c.DataAbertura,
                               DataFechamento = c.DataFechamento,
                               Desconto = c.Desconto,
                               ValorTotal = c.ValorTotal,
                               ValorTotalComDesconto = c.ValorTotalComDesconto,
                               IdCliente = c.IdClienteNavigation.IdCliente,
                               NomeCliente = cli.Nome
                           }).ToList();

            return comandaDTOs;
        }

        public IEnumerable<ComandaDTO> GetAll()
        {
            //recupera a lista de comandas e retorna uma lista em DTO
            var comandas = _serviceComanda.GetAll();
            return _mapperComanda.MapperListComandasDTO(comandas);
        }

        /// <summary>
        /// Retorna a comanda completa com todos os itens comprados
        /// </summary>
        /// <returns></returns>
        public ComandaDTO GetPersonalizedById(int idComanda)
        {
            ComandaDTO comandaDTO = this.GetAllPersonalized().Where(p => p.IdComanda == idComanda).FirstOrDefault();
            if (comandaDTO == null)
                throw new ObjectNotFoundException("Comanda não encontrada.");

            return comandaDTO;
        }

        public void Delete(int id)
        {
            var comanda = _serviceComanda.GetById(id);
            if (comanda == null)
                throw new ObjectNotFoundException("Objeto não encontrado.");

            _serviceComanda.Delete(id);
        }

        public void Update(ComandaDTO comandaDTO)
        {
            var comanda = _serviceComanda.GetById(comandaDTO.IdComanda);
            if (comanda == null)
                throw new ObjectNotFoundException("Objeto não encontrado.");

            comanda = _mapperComanda.MapperDTOToEntity(comandaDTO);
            _serviceComanda.Update(comanda);
        }

        public void AtualizarComanda(int idComanda, bool fecharComanda = false)
        {
            Comanda comanda = _serviceComanda.GetById(idComanda);

            if (comanda == null)
                throw new ObjectNotFoundException("Objeto não encontrado.");

            ApplicationServiceItemComprado applicationServiceItemComprado = new ApplicationServiceItemComprado(_serviceItemComprado, null, _serviceComanda, _serviceItem, this);

            List<ItemCompradoDTO> itemCompradoDTOs = applicationServiceItemComprado.GetAllByComanda(idComanda);

            decimal desconto = decimal.Zero;
            decimal valorTotal = decimal.Zero;
            if (itemCompradoDTOs?.Count > 0)
            {
                valorTotal = itemCompradoDTOs.Sum(p => p.Valor);

                var itemCompradoAgrupados = itemCompradoDTOs.Where(p => p.IdItem == Consts.Item.Cerveja || p.IdItem == Consts.Item.Suco)?.ToList()?.GroupBy(info => info.IdItem)
                                                           .Select(group => new
                                                           {
                                                               IdItem = group.Key,
                                                               Count = group.Count()
                                                           })
                                                           .OrderBy(x => x.IdItem).ToList();
                if (itemCompradoAgrupados?.Count > 0)
                {
                    int qtdCerveja = itemCompradoAgrupados.Where(p => p.IdItem == Consts.Item.Cerveja).Select(s => s.Count).FirstOrDefault();
                    int qtdSuco = itemCompradoAgrupados.Where(p => p.IdItem == Consts.Item.Suco).Select(s => s.Count).FirstOrDefault();
                    int qtdDesconto = 0;

                    //Na compra de 1 cerveja e 1 suco, essa cerveja sai por 3 reais
                    if (qtdCerveja > 0 && qtdSuco > 0)
                    {
                        if (qtdCerveja > qtdSuco)
                            qtdDesconto = qtdSuco;
                        else if (qtdCerveja < qtdSuco)
                            qtdDesconto = qtdCerveja;
                        else//qualquer um
                            qtdDesconto = qtdCerveja;
                        //2 é o valor de desconto para cada cerveja
                        desconto = qtdDesconto * 2;
                    }
                }
            }

            if (fecharComanda)
                comanda.DataFechamento = DateTime.Now;
            comanda.Desconto = desconto;
            comanda.ValorTotal = valorTotal;
            comanda.ValorTotalComDesconto = valorTotal - desconto;
            _serviceComanda.Update(comanda);
        }
    }
}
