namespace Agario_Server
{
    class Program
    {
        public class MainServer
        {
            static void Main(string[] args)
            {
                Console.Title = "Agar.io Game Server";
                AgarioServer.ServerStart(100, 5757);
                Console.ReadKey();
            }
        }
    }
}