using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        [MaxLength(4)]
        public string FileExtension { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual UserProfile User { get; set; }
    }
}