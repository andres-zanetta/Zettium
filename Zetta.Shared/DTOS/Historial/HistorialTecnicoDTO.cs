using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta.Shared.DTOS.Historial
{
    public class HistorialTecnicoDTO
    {
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } = "";      // Presupuesto, Obra, Visita, Comentario
        public string Titulo { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Estado { get; set; } = "";
        public int? ReferenciaId { get; set; }      // Id del Presupuesto/Obra/Visita/Comentario
    }
}
