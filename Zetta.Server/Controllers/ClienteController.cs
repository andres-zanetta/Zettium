using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zetta.BD.DATA;
using Zetta.BD.DATA.ENTITY;
using Zetta.BD.DATA.REPOSITORY;
using Zetta.Shared.DTOS.Cliente;
using Zetta.Shared.DTOS.Historial;

namespace Zetta.Server.Controllers
{
    [ApiController]
    [Route("/api/Cliente")]
    public class ClientesController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClientesController(Context context, IMapper mapper, IClienteRepositorio clienteRepositorio)
        {
            _context = context;
            _mapper = mapper;
            _clienteRepositorio = clienteRepositorio;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<ActionResult<List<GET_ClienteDTO>>> Get()
        {
            var clientes = await _clienteRepositorio.GetAllAsync();
            var clientesDTO = _mapper.Map<List<GET_ClienteDTO>>(clientes);
            return Ok(clientesDTO);
        }

        // GET: api/Cliente/papelera
        [HttpGet("papelera")]
        public async Task<ActionResult<List<GET_ClienteDTO>>> GetPapelera()
        {
            var clientes = await _clienteRepositorio.GetInactivosAsync();
            var dtos = _mapper.Map<List<GET_ClienteDTO>>(clientes);
            return Ok(dtos);
        }

        [HttpGet("{id:int}/historial")]
        public async Task<ActionResult<List<HistorialTecnicoDTO>>> GetHistorial(int id)
        {
            // Presupuestos del cliente
            var presupuestos = await _context.Presupuestos
                .Where(p => p.ClienteId == id)
                .ToListAsync();

            // Obras del cliente (incluye comentarios)
            var obras = await _context.Obras
                .Where(o => o.ClienteId == id)
                .Include(o => o.Comentarios)
                .ToListAsync();

            // Visitas técnicas del cliente
            var visitas = await _context.VisitasTecnicas
                .Where(v => v.ClienteId == id)
                .ToListAsync();

            var lista = new List<HistorialTecnicoDTO>();

            lista.AddRange(presupuestos.Select(p => new HistorialTecnicoDTO
            {
                Fecha = p.FechaCreacion,
                Tipo = "Presupuesto",
                Titulo = $"Presupuesto #{p.Id} - {p.Rubro}",
                Descripcion = $"{(string.IsNullOrWhiteSpace(p.Observacion) ? "" : p.Observacion + " - ")}Total: {p.Total:C}",
                Estado = p.Aceptado ? "Aceptado" : "Pendiente",
                ReferenciaId = p.Id
            }));

            lista.AddRange(obras.Select(o => new HistorialTecnicoDTO
            {
                Fecha = o.FechaInicio,
                Tipo = "Obra",
                Titulo = $"Obra #{o.Id}",
                Descripcion = $"Presupuesto #{o.PresupuestoId}",
                Estado = o.EstadoObra.ToString(),
                ReferenciaId = o.Id
            }));

            lista.AddRange(visitas.Select(v => new HistorialTecnicoDTO
            {
                Fecha = v.FechaVisita,
                Tipo = "Visita",
                Titulo = $"Visita #{v.Id} - {v.Tipo}",
                Descripcion = v.Observaciones ?? "",
                Estado = v.Estado.ToString(),
                ReferenciaId = v.Id
            }));

            // Comentarios asociados a obras
            foreach (var o in obras)
            {
                if (o.Comentarios != null)
                {
                    foreach (var c in o.Comentarios)
                    {
                        lista.Add(new HistorialTecnicoDTO
                        {
                            Fecha = c.Fecha,
                            Tipo = "Comentario",
                            Titulo = $"Comentario en Obra #{o.Id}",
                            Descripcion = c.Texto,
                            Estado = string.Empty,
                            ReferenciaId = o.Id
                        });
                    }
                }
            }

            var ordenada = lista.OrderByDescending(x => x.Fecha).ToList();
            return Ok(ordenada);
        }

        // GET: api/clientes/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GET_ClienteDTO>> GetById(int id)
        {
            var cliente = await _clienteRepositorio.GetByIdAsync(id);

            if (cliente == null)
                return NotFound("Cliente no encontrado.");

            var clienteDTO = _mapper.Map<GET_ClienteDTO>(cliente);
            return Ok(clienteDTO);
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] POST_ClienteDTO dto)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(dto);
                cliente.Activo = true; 
                await _clienteRepositorio.AddAsync(cliente);
                return Ok(cliente.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/clientes/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] PUT_ClienteDTO dto)
        {
            var dbCliente = await _clienteRepositorio.GetByIdAsync(id);
            if (dbCliente == null)
                return NotFound("Cliente no encontrado.");

            _mapper.Map(dto, dbCliente);

            try
            {
                await _clienteRepositorio.UpdateAsync(dbCliente);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Cliente/restaurar/5
        [HttpPut("restaurar/{id:int}")]
        public async Task<ActionResult> Restaurar(int id)
        {
            try
            {
                await _clienteRepositorio.RestaurarAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/clientes/5 (Soft Delete)
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cliente = await _clienteRepositorio.GetByIdAsync(id);
            if (cliente == null)
                return NotFound("Cliente no encontrado.");

            try
            {
                await _clienteRepositorio.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Cliente/definitivo/5
        [HttpDelete("definitivo/{id:int}")]
        public async Task<ActionResult> DeleteDefinitivo(int id)
        {
            try
            {
                await _clienteRepositorio.EliminarDefinitivamenteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}