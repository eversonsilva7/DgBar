using BarDoDG.Application.DTOs;
using System.Collections.Generic;

namespace BarDoDG.Application.Interfaces
{
    public interface IApplicationServiceComanda
    {
        void Add(ComandaDTO comandaDTO);
        int Insert(ComandaInsertDTO comandaDTO);
        void Update(ComandaDTO comandaDTO);
        void Delete(int id);
        IEnumerable<ComandaDTO> GetAll();
        List<ComandaDTO> GetAllPersonalized();
        ComandaDTO GetPersonalizedById(int id);        
        void AtualizarComanda(int idComanda, bool fecharComanda = false);
    }
}
