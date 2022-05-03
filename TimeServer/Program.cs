using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;

namespace TimeServer
{
    public static class TCPListenerProgram
    {
        private static void Main(string[] args)
        {
            var anyEndpoint = new IPEndPoint(IPAddress.Any, 44444); // originally used loopback
            var tcpListener = new TcpListener(anyEndpoint);

            tcpListener.Start();

            Console.WriteLine($"Server listening on : {tcpListener.LocalEndpoint}");

            while (true)
            {
                Console.WriteLine("Waiting for connection...");
                // a client will soon hopefully establish and return a connection          
                var tcpClient = tcpListener.AcceptTcpClient();

                //IterationOne(tcpListener, tcpClient);
                StartNewThread(tcpListener, tcpClient);
            }
        }
        
        

        private static void StartNewThread(TcpListener tcpListener, TcpClient tcpClient)
        {
            new Thread(() =>
            {

                var clientID = tcpClient.Client.RemoteEndPoint;
                Console.WriteLine($"Cline {clientID} connected"); // client ID print
                
                GetStreamGoing(tcpListener, tcpClient);
                
                // once while loop has been broken (player client has quit)
                Console.WriteLine($"Closing the connection to {clientID}");
                tcpClient.Dispose(); // end sessions
                
            }).Start();
        }

        
        
        private static void GetStreamGoing(TcpListener tcpListener, TcpClient tcpClient)
        {
            var clientID = tcpClient.Client.RemoteEndPoint;
            Console.WriteLine($"Cline {clientID} connected"); // client ID print
                
            // set up stream and relevant helper classes
            var stream = tcpClient.GetStream(); // so we can read and write data from the stream
            var streamReader = new StreamReader(stream); // this negates using encoding.ASCII; easier writing to stream
            var streamWriter = new StreamWriter(stream);
            var input = streamReader.ReadLine();
            streamWriter.AutoFlush = true; // tool to flush stream buffer after every Write(Char) call
            streamWriter.WriteLine("Input '1' to play Harryokémon or '2' to start the TimeServer ");

            while (true)
            {
                if (input == "Exit")
                    break;

                switch (input)
                {
                    case "1":
                        PlayHarryokémon(streamWriter, input);
                        break;
                    case "2":
                        StartTimeServer(streamWriter, input, tcpListener, tcpClient);
                        break;
                    
                    default:
                        InvalidInput(streamWriter);
                        break;
                }
                
                tcpClient.Dispose();
            }
        }
        
        
        
        [SuppressMessage("ReSharper", "IdentifierTypo")]
        private static void PlayHarryokémon(TextWriter streamWriter, string input)
        {
            // TODO: Add Player and AI name options

            streamWriter.WriteLine("Welcome to Harryokémon! Choose either 'Fire', 'Grass' or 'Water'.");
            streamWriter.WriteLine("If you want to quit though, write 'Exit'");
            
            var playerScore = 0;
            var aiScore = 0;

            while (true)
            {
                if (input == "Exit")
                    break;
                    
                var options = new[] {"Water", "Fire", "Grass"};
                var random = new Random();
                var randomOption = random.Next(0, options.Length);
                var aiRandomChoice = options[randomOption];
                
                
                switch (input)
                {
                    case "Fire":
                    case "Grass":
                    case "Water":
                        EvaluateMove(streamWriter, input, aiRandomChoice, playerScore, aiScore);
                        break;
                        
                    default:
                        InvalidInput(streamWriter);
                        break;
                }
                    
                // TODO: try to catch the client if the server crashes 
                // TODO: try to catch the client + socket variable if the socket is in use
                // TODO: might want to read from the stream asynchronously and/or on a separate thread for realtime scenarios
                    
            }
        }



        private static void StartTimeServer(TextWriter streamWriter, string input, IDisposable tcpClient)
        {
            while (true)
            {
                streamWriter.Write($"The current server time is: {DateTime.Now}.");
                streamWriter.Write("Would you like to check it again? (Y/N/)");

                switch (input)
                {
                    case "Y":                     
                        streamWriter.WriteLine($"The server time has updated to: {DateTime.Now}");
                        break;
                    
                    case "N":
                        streamWriter.WriteLine("Exiting TimeSever...");
                        break;
                    
                    default:
                        InvalidInput(streamWriter);
                        break;
                }
                
                tcpClient.Dispose();
            }
        }



        private static void InvalidInput(TextWriter streamWriter)
        {
            streamWriter.WriteLine("Invalid input. Try again.");
        }
        

        
        private static void EvaluateMove(TextWriter streamWriter, string playerMove, string aiMove, int playerScore,
            int aiScore)
        {
            streamWriter.Write($"You chose {playerMove}. The AI chose {aiMove}...");

            if (playerMove == aiMove)
            {
                streamWriter.WriteLine("You were equally matched. Try again.");
                return;
            }

            switch (playerMove)
            {
                // Fire choice logic

                case "Fire" when aiMove == "Grass":
                    CalculateScore(streamWriter, playerScore, "Player", aiScore);
                    break;
                case "Fire":
                    CalculateScore(streamWriter, aiScore, "AI", playerScore);
                    break;


                // Grass choice logic

                case "Grass" when aiMove == "Water":
                    CalculateScore(streamWriter, playerScore, "Player", aiScore);
                    break;
                case "Grass":
                    CalculateScore(streamWriter, aiScore, "AI", playerScore);
                    break;


                // Water choice logic

                case "Water" when aiMove == "Fire":
                    CalculateScore(streamWriter, playerScore, "Player", aiScore);
                    break;
                case "Water":
                    CalculateScore(streamWriter, aiScore, "AI", playerScore);
                    break;
            }
        }
        
        
        
        private static void CalculateScore(TextWriter streamWriter, int winnerScore, string winnerID, int loserScore)
        {
            winnerScore++;

            streamWriter.WriteLine($"The {winnerID} won");
            streamWriter.WriteLine($"Scores: {winnerScore} - AI: {loserScore}");
        }
    }
}

