using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GyereHazaApi4.Models
{
    public class Owner
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Area { get; set; }
    }
}
