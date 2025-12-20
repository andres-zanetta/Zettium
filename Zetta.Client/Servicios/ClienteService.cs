using System.Net.Http.Json;
using Zetta.Shared.DTOS.Cliente;
using Zetta.Shared.DTOS.Historial;

namespace Zetta.Client.Servicios
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "/api/Cliente";

        public ClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GET_ClienteDTO>?> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<GET_ClienteDTO>>(BaseUrl);
        }

        public async Task<GET_ClienteDTO?> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<GET_ClienteDTO>($"{BaseUrl}/{id}");
        }

        public async Task<List<HistorialTecnicoDTO>?> GetHistorialAsync(int clienteId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<HistorialTecnicoDTO>>($"{BaseUrl}/{clienteId}/historial");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener historial: {ex.Message}");
                return new List<HistorialTecnicoDTO>();
            }
        }
        public async Task Create(POST_ClienteDTO cliente)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, cliente);
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(int id, PUT_ClienteDTO cliente)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", cliente);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<GET_ClienteDTO>?> GetInactivos()
        {
            // GET: /api/Cliente/papelera
            return await _httpClient.GetFromJsonAsync<List<GET_ClienteDTO>>($"{BaseUrl}/papelera");
        }

        public async Task Restaurar(int id)
        {
            // PUT: /api/Cliente/restaurar/{id}
            var response = await _httpClient.PutAsync($"{BaseUrl}/restaurar/{id}", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task EliminarDefinitivamente(int id)
        {
            // DELETE: /api/Cliente/definitivo/{id}
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/definitivo/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}