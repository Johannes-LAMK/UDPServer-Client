using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(Dns.GetHostAddresses("127.0.0.1")[0], 12345);
            EndPoint endPoint = (EndPoint)new IPEndPoint(IPAddress.Any, 12345);
            byte[] bytesToSend;
            byte[] buffer = new byte[1000];
            string message;
            Socket socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            Console.WriteLine("Client started!");
            bool run = true;
            while (run)
            {
                Console.WriteLine("Give a message: ");
                message = Console.ReadLine();
                if (message == "")
                {
                    run = false;
                    socket.SendTo(System.Text.Encoding.UTF8.GetBytes(""), remoteEndPoint);
                    break;
                }
                bytesToSend = System.Text.Encoding.UTF8.GetBytes(message);
                socket.SendTo(bytesToSend, remoteEndPoint);
                socket.ReceiveFrom(buffer, ref endPoint);
                Console.WriteLine("Got: " + System.Text.Encoding.UTF8.GetString(buffer, 0, message.Length));
            }
            socket.Close();
            Console.WriteLine("Client stoppped!");
        }
    }
}
