using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models.Models
{
    public class Message
    {
        public int Id { get; set; }

        [ForeignKey("Friendship")]
        public int FriendshipId { get; set; }

        public virtual Friednship Friendship { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public virtual UserProfile Author { get; set; }

        [Required]
        public string Content { get; set; }

        [Index]
        public DateTime SendingDate { get; set; }

        public DateTime? SeeingDate { get; set; }
    }
}