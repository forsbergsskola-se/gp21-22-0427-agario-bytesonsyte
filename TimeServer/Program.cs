namespace TimeServer
{
    public class TcpListener
    {
        static void Main(string[] args)
        {

        }
    
    
        System.Net.Sockets.TcpListener TCPListener0 = new System.Net.Sockets.TcpListener(0)
        {
            ExclusiveAddressUse = false
        };

        public TCPClient AcceptTCPClient()
        {
            
            return new TCPClient();

        }

        private void Start()
        {
            TCPListener0.Start();
        }

        public void Stop()
        {
            TCPListener0.Stop();
        
        }
    }

    public class TCPClient
    {
    
    }

    public class Stream
    {
    
    } 
}

