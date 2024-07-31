using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventosServer.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public DateTime FechaEvento { get; set; }
        [MaxLength (50, ErrorMessage = "El campo {0} debe de tener máximo {1} caracteres")]
        public string Lugar {  get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe de tener máximo {1} caracteres")]
        public string Descripcion { get; set; }
        [Column(TypeName ="decimal(12,2)")]
        [DisplayFormat(DataFormatString ="{0:C2}")]
        public decimal Precio { get; set; }
        [MaxLength(10, ErrorMessage = "El campo {0} debe de tener máximo {1} caracteres")]
        public string Estado { get; set; }
    }
}
