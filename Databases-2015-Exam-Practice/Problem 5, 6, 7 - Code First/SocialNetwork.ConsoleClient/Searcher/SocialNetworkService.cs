using SocialNetwork.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.ConsoleClient.Searcher
{
    public class SocialNetworkService : ISocialNetworkService
    {
        private SocialNetworkDBContext context;

        public SocialNetworkService(SocialNetworkDBContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("The database context cannot be null!");
            }

            this.context = context;
        }

        public IEnumerable GetChatUsers(string username)
        {
            return null;
        }

        public IEnumerable GetFriendships(int page = 1, int pageSize = 25)
        {
            var friendships = context.Friendships
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .Select(f => new
                                        {
                                            FirstUserUsername = f.FirstUser.UserName,
                                            FirstUserImage = f.FirstUser.Images.Select(i => i.Url).FirstOrDefault(),
                                            SecondUserUsername = f.SecondUser.UserName,
                                            SecondUserImage = f.SecondUser.Images.Select(i => i.Url).FirstOrDefault()
                                        })
                                        .ToList();

            return friendships;
        }

        public IEnumerable GetPostsByUser(string username)
        {
            var posts = context.Posts.Where(p => p.TaggedUsers.Any(u => u.UserName == username))
                                        .Select(p => new
                                        {
                                            p.PostingDate,
                                            p.Content,
                                            Users = p.TaggedUsers.Select(u => u.UserName)
                                        })
                                        .ToList();

            return posts;
        }

        public IEnumerable GetUsersAfterCertainDate(int year)
        {
            var users = context.UserProfiles.Where(u => u.RegistrationDate.Year >= year)
                                            .Select(u => new
                                            {
                                                Username = u.UserName,
                                                FirstName = u.FirstName,
                                                LastName = u.LastName,
                                                Images = u.Images.Count
                                            });

            return users;
        }
    }
}
