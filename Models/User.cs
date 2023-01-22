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
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Cell Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[ . ]?([0-9]{3})[ . ]?([0-9]{4})$",
                   ErrorMessage = "Format: 000 000 0000")]
        public string CellNumber { get; set; }
    }
}
