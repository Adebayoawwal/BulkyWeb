using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Catergory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name{ get; set; }
        [Range(1,100)]
        public int DiplayOrder { get; set; }
    }
}
