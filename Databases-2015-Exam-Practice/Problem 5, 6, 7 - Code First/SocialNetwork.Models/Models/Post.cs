using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Models.Models
{
    public class Post
    {
        private ICollection<UserProfile> taggedUsers;

        public Post()
        {
            this.PostingDate = DateTime.Now;
            this.taggedUsers = new HashSet<UserProfile>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(10)]
        public string Content { get; set; }

        public DateTime PostingDate { get; set; }

        public virtual ICollection<UserProfile> TaggedUsers
        {
            get
            {
                return this.taggedUsers;
            }

            set
            {
                this.taggedUsers = value;
            }
        }
    }
}
