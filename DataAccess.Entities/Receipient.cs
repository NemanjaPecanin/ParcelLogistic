using System.ComponentModel.DataAnnotations;

namespace ParcelLogistics.SKS.Package.DataAccess.Entities
{
    public class Receipient
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
