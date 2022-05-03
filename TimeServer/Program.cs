using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TimeServer
{
    public static class TCPListenerProgram
    {
        private static void Main(string[] args)
        {
            const bool any = false;
            
            var anyEndpoint = new IPEndPoint(IPAddress.Any, 44444); // listen on any given socket
            var loopbackEndpoint = new IPEndPoint(IPAddress.Loopback, 44444); // listen on loopback... server IP = 127.0.0.1
            
            var tcpListener = new TcpListener(loopbackEndpoint);
            if (any) // toggle
                tcpListener = new TcpListener(anyEndpoint);
            tcpListener.Start();

            Console.WriteLine($"Server listening on : {tcpListener.LocalEndpoint}");
            
            while (true)
            {
                Console.WriteLine("Waiting for connection..."); 
                // a client will soon hopefully establish and return a connection          
                var tcpClient = tcpListener.AcceptTcpClient();

                //IterationOne(tcpListener, tcpClient);
                SimplePokémonGame(tcpListener, tcpClient);
            }
        }
        

        private static void SimplePokémonGame(TcpListener tcpListener, TcpClient tcpClient)
        {
            new Thread(() =>
            {

                var clientID = tcpClient.Client.RemoteEndPoint;
                Console.WriteLine($"Cline {clientID} connected"); // client ID print
                
                // set up stream and relevant helper classes
                var stream = tcpClient.GetStream(); // so we can read and write data from the stream
                var streamReader = new StreamReader(stream); // this negates using encoding.ASCII; easier writing to stream
                var streamWriter = new StreamWriter(stream);
                var input = streamReader.ReadLine();

                streamWriter.AutoFlush = true; // tool to flush stream buffer after every Write(Char) call
                
                var options = new[] {"Water, Fire, Grass"};
                streamWriter.WriteLine("Welcome to the elemental game! Choose either 'Fire', 'Grass' or 'Water'.");
                streamWriter.WriteLine("If you want to quit though, write 'Exit'");
                var random = new Random();
                int playerScore = 0;
                int aiScore = 0;

                while (true)
                {
                    if (input == "Exit")
                        break;
                    
                    var randomOption = random.Next(0, options.Length);
                    var aiRandomChoice = options[randomOption];
                    switch (input)
                    {
                        case "Fire":
                        case "Grass":
                        case "Water":
                            EvaluateMove(input, aiRandomChoice);
                            break;
                        
                        default:
                            streamWriter.WriteLine("Invalid input. Try again.");
                            break;
                    }
                }

                void EvaluateMove(string playerMove, string aiMove)
                {
                    streamWriter.Write($"You chose {playerMove}. The AI chose {aiMove}...");
                    if (playerMove == aiMove)
                    {
                        streamWriter.WriteLine("You were equally matched. Try again."); return;
                    }

                    switch (playerMove)
                    {
                        case "Fire" when aiMove == "Grass":
                            CalculateScore(playerScore, "Player");
                            break;
                        case "Fire":
                            CalculateScore(aiScore, "AI");
                            break;
                        
                        
                        case "Grass" when aiMove == "Water":
                            CalculateScore(playerScore, "Player");

                            break;
                        case "Grass":
                            CalculateScore(aiScore, "AI");
                            break;
                        
                        
                        case "Water" when aiMove == "Fire":
                            CalculateScore(playerScore, "Player");
                            break;
                        case "Water":
                            CalculateScore(aiScore, "AI");
                            break;
                    }
                }

                void CalculateScore(int winner, string winnerID)
                {
                    if (winner == playerScore)
                        playerScore++;
                    else
                        aiScore++;

                    streamWriter.WriteLine($"The {winnerID} won");
                    streamWriter.WriteLine($"Scores: {playerScore} - AI: {aiScore}");
                    
                }
                
                Console.WriteLine($"Closing the connection to {clientID}");
                tcpClient.Dispose();
                
            }).Start();
        }
        

        private static void SimpleConnectStreamProgram(TcpListener tcpListener, TcpClient tcpClient)
        {
            var bufferSize = new byte[100];
            tcpClient.GetStream().Read(bufferSize, 0, 100);
                
            var comment = Encoding.ASCII.GetString(bufferSize);
            Console.WriteLine($"New comment from Client: {comment}");
                
            var currentTime = Encoding.ASCII.GetBytes(DateTime.Now.ToString(CultureInfo.CurrentCulture));
            tcpClient.GetStream().Write(currentTime, 0, currentTime.Length);
            tcpClient.Close();
        }
    }
}

