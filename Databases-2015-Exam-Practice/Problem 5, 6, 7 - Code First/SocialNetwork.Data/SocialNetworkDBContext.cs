using SocialNetwork.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Data
{
    public class SocialNetworkDBContext : DbContext
    {
        public SocialNetworkDBContext()
            : base("SocialNetwork")
        {
        }

        public virtual IDbSet<Friednship> Friendships { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public virtual IDbSet<Message> Messages { get; set; }

        public virtual IDbSet<Post> Posts { get; set; }

        public virtual IDbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
