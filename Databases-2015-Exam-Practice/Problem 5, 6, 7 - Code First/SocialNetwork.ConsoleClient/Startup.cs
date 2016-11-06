using SocialNetwork.ConsoleClient.Parsers;

namespace SocialNetwork.ConsoleClient
{
    public class Startup
    {
        public static void Main()
        {
            var importer = new Importer();

            importer.ImportFriendships();
            importer.ImportPosts();
        }
    }
}
