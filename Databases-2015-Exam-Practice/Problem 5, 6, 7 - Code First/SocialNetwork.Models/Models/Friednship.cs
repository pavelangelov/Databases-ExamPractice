using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Models.Models
{
    public class Friednship
    {
        private ICollection<Message> messages;

        public Friednship()
        {
            this.messages = new List<Message>();
        }

        public int Id { get; set; }

        [ForeignKey("FirstUser")]
        public int FirstUserId { get; set; }

        public virtual UserProfile FirstUser { get; set; }

        [ForeignKey("SecondUser")]
        public int SecondUserId { get; set; }

        public virtual UserProfile SecondUser { get; set; }

        [Index]
        public bool IsApproved { get; set; }

        public DateTime? ApprovingDate { get; set; }

        public virtual ICollection<Message> Messages
        {
            get
            {
                return this.messages;
            }

            set
            {
                this.messages = value;
            }
        }
    }
}
