using System;

namespace Aplicacion.DTO
{
    public class SiniestroQueryParams
    {
        public int? DepartamentoId { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        
        private int _pagina = 1;
        private int _tamanoPagina = 10;
        private const int MaxTamanoPagina = 100;
        
        public int Pagina
        {
            get => _pagina;
            set => _pagina = value < 1 ? 1 : value;
        }
        
        public int TamanoPagina
        {
            get => _tamanoPagina;
            set => _tamanoPagina = Math.Min(value, MaxTamanoPagina);
        }
        
        public bool FechasValidas()
        {
            if (!FechaInicio.HasValue || !FechaFin.HasValue) return true;
            return FechaInicio <= FechaFin;
        }
    }
}