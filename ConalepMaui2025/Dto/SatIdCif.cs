using System.ComponentModel.DataAnnotations;

namespace ConalepMaui2025.Dto
{
    public class SatIdCif
    {
        public string? IdCif { get; set; }
        [StringLength(13)]
        public string Rfc { get; set; }
       
    }
}
