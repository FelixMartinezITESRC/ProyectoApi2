using System;
using System.Collections.Generic;

namespace ApiAeropuerto.Models
{
    public partial class Vuelos
    {
        public int Id { get; set; }
        public string CodigoVuelo { get; set; } = null!;
        public string Destino { get; set; } = null!;
        public DateTime HorarioSalida { get; set; }
        public string PuertaSalida { get; set; } = null!;
        public string Estado { get; set; } = null!;
    }
}
