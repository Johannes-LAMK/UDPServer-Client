using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] buffer = new byte[1000];
            int lengthOfReceivedMessage;
            EndPoint remoteEndPoint = (EndPoint)new IPEndPoint(IPAddress.Any, 12345);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 12345);
            Socket socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(localEndPoint);
            Console.WriteLine("Server started!");
            bool run = true;
            while (run)
            {
                lengthOfReceivedMessage = socket.ReceiveFrom(buffer, ref remoteEndPoint);
                if (lengthOfReceivedMessage == 0)
                {
                    run = false;
                    break;
                }
                socket.SendTo(buffer, buffer.Length, 0, remoteEndPoint);
                Console.WriteLine("Message received and sent back, to client!");
            }
            socket.Close();
            Console.WriteLine("Server stoppped!");
        }
    }
}
