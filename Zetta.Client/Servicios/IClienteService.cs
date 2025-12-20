using Zetta.Shared.DTOS.Cliente;
using Zetta.Shared.DTOS.Historial;

namespace Zetta.Client.Servicios
{
    public interface IClienteService
    {
        Task Create(POST_ClienteDTO cliente);
        Task Delete(int id);
        Task<List<GET_ClienteDTO>?> GetAll();
        Task<GET_ClienteDTO?> GetById(int id);
        Task Update(int id, PUT_ClienteDTO cliente);
        Task<List<GET_ClienteDTO>?> GetInactivos();
        Task Restaurar(int id);
        Task EliminarDefinitivamente(int id);
        Task<List<HistorialTecnicoDTO>?> GetHistorialAsync(int clienteId);
    }
}
