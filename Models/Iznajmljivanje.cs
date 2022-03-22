using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Iznajmljivanje
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [JsonIgnore]
        public Korisnik Korisnik { get; set; }

        [Required]
        [JsonIgnore]
        public Firma Firma { get; set; }

        [Required]
        [JsonIgnore]
        public Vozilo Vozilo { get; set; }
    }
}