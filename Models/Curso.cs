using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Parcial.Models
{
    public class Curso
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Codigo { get; set; } = null!;   // inicializador para evitar warning

        [Required]
        public string Nombre { get; set; } = null!;   // inicializador

        [Range(1, int.MaxValue, ErrorMessage = "Los créditos deben ser mayores a 0")]
        public int Creditos { get; set; }

        [Range(0, int.MaxValue)]
        public int CupoMaximo { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan HorarioInicio { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan HorarioFin { get; set; }

        public bool Activo { get; set; } = true;

        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>(); // inicializada
    }
}
