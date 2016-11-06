using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

using SocialNetwork.Models.Models;
using SocialNetwork.ConsoleClient.Models;
using SocialNetwork.Data;

namespace SocialNetwork.ConsoleClient.Parsers
{
    public class Importer
    {
        private SocialNetworkDBContext dbContext = new SocialNetworkDBContext();

        public void ImportFriendships()
        {
            var filePath = @"..\..\XmlFiles\Friendships.xml";
            var friendships = this.Deserialize<FriendshipXmlModel>(filePath, "Friendships");

            this.ProceedFriendShip(friendships);
        }

        public void ImportPosts()
        {
            var filePath = @"..\..\XmlFiles\Posts.xml";
            var posts = this.Deserialize<PostXmlModel>(filePath, "Posts");

            this.ProceedPost(posts);
        }

        private IEnumerable<TModel> Deserialize<TModel>(string fileName, string rootElement)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("File not found!", fileName);
            }

            var serializer = new XmlSerializer(typeof(List<TModel>), new XmlRootAttribute(rootElement));
            IEnumerable<TModel> result;
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                result = (IEnumerable<TModel>)serializer.Deserialize(fs);
            }

            return result;
        }

        private void ProceedFriendShip(IEnumerable<FriendshipXmlModel> friendships)
        {
            var addedFriendships = 0;
            var addedUsers = dbContext.UserProfiles.Select(u => u.UserName).ToList();
            foreach (var fr in friendships)
            {
                var firstUser = GetUser(dbContext, fr.FirstUser, addedUsers);
                var secondUser = GetUser(dbContext, fr.SecondUser, addedUsers);

                var friendship = new Friednship()
                {
                    FirstUser = firstUser,
                    SecondUser = secondUser,
                    ApprovingDate = fr.FriendsSince,
                    IsApproved = fr.Approved
                };

                foreach (var message in fr.Messages)
                {
                    friendship.Messages.Add(new Message()
                    {
                        Author = message.Author == firstUser.UserName ? firstUser : secondUser,
                        Content = message.Content,
                        SendingDate = message.SentOn,
                        SeeingDate = message.SentOn
                    });
                }

                dbContext.Friendships.Add(friendship);
                addedFriendships++;

                if (addedFriendships % 10 == 0)
                {
                    Console.Write('.');
                }

                if (addedFriendships % 100 == 0)
                {
                    Console.WriteLine($"Added {addedFriendships} friendships.");
                    dbContext.SaveChanges();
                    dbContext = new SocialNetworkDBContext();
                }
            }
        }

        private UserProfile GetUser(SocialNetworkDBContext db, UserXmlModel userModel, ICollection<string> usernames)
        {
            if (usernames.Contains(userModel.Username))
            {
                return db.UserProfiles.FirstOrDefault(u => u.UserName == userModel.Username);
            }
            else
            {
                usernames.Add(userModel.Username);

                var user = new UserProfile()
                {
                    UserName = userModel.Username,
                    RegistrationDate = userModel.RegisteredOn,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName
                };

                foreach (var image in userModel.Images)
                {
                    user.Images.Add(new Image()
                    {
                        Url = image.ImageUrl,
                        FileExtension = image.FileExtension
                    });
                }

                dbContext.UserProfiles.Add(user);
                dbContext.SaveChanges();

                return user;
            }
            
        }

        private void ProceedPost(IEnumerable<PostXmlModel> posts)
        {
            var addedPosts = 0;
            foreach (var p in posts)
            {
                var usernames = p.Users.Split(',');
                var users = dbContext.UserProfiles.Where(u => usernames.Contains(u.UserName)).ToList();

                var post = new Post()
                {
                    Content = p.Content,
                    PostingDate = p.PostedOn
                };

                foreach (var user in users)
                {
                    post.TaggedUsers.Add(user);
                }

                dbContext.Posts.Add(post);
                addedPosts++;

                if (addedPosts % 10 == 0)
                {
                    Console.Write('.');
                }

                if (addedPosts % 100 == 0)
                {
                    Console.WriteLine($"Added {addedPosts} posts.");
                    dbContext.SaveChanges();
                    dbContext = new SocialNetworkDBContext();
                }
            }
        }
    }
}
