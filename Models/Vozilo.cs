using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    public class Vozilo
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Marka { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [Required]
        [MaxLength(7)]
        public string Tablice { get; set; }

        [Required]
        public int Cena { get; set; }

        [Required]
        public int BrojOcenjivanja { get; set; }

        [Required]
        [Range(0.00, 5.00)]
        public double Ocena { get; set; }

        [Required]
        public bool Iznajmnjeno { get; set; }

        public List<Iznajmljivanje> Iznajmljivanja { get; set; }
        
        public Firma Firme { get; set; }

        [NotMapped]
        public string ZaPrikaz
        {
            get { return Marka + " " + Model + " " + Tablice; }
        }
    }
}