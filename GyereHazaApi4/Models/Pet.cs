using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GyereHazaApi4.Models
{
    public class Pet
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Owners")]
        public Guid OwnerId { get; set; }
        public Owner Owners { get; set; }
        [Required]
        public string TypeOfAnimal { get; set; }
        public string Breed { get; set; }
        public string Sex { get; set; }
        public string Issues { get; set; }
        public Boolean is_Missing { get; set; }
    }
}
