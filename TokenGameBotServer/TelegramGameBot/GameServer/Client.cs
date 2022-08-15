using System.Net.Sockets;
using System.Text;

namespace TelegramBotGame.GameServer
{
    class Client
    {
        public Client(TcpClient Client)
        {
            NetworkStream stream = Client.GetStream();
            
            var data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);
            
            var message = builder.ToString();
            Console.WriteLine(message);
            Client.Close();
        }
    }
}

