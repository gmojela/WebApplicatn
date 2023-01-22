using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicatn.Models
{
    public class User
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        [Required]
        public string CellNumber { get; set; }
    }
}
