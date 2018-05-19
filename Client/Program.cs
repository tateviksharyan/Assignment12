using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        const int PORT = 3000;
        const string SERVER_IP = "127.0.0.1";

        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("Choose connection type: TCP or UDP \n Or type exit for finish.");
                string connectionType = Console.ReadLine();
                if (connectionType.Equals("TCP"))
                {
                    Console.WriteLine("Enter math operation.");

                    string operation = Console.ReadLine();

                    TcpClient tcpClient = new TcpClient(SERVER_IP, PORT);
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytesToSend = Encoding.ASCII.GetBytes(operation);

                    networkStream.Write(bytesToSend, 0, bytesToSend.Length);

                    byte[] bytesToRead = new byte[tcpClient.ReceiveBufferSize];
                    int readBytes = networkStream.Read(bytesToRead, 0, tcpClient.ReceiveBufferSize);
                    Console.WriteLine("Result of {0} operation: {1}", operation, Encoding.ASCII.GetString(bytesToRead, 0, readBytes));
                    Console.ReadLine();
                    tcpClient.Close();
                }
                else
                if (connectionType.Equals("UDP"))
                {
                    Console.WriteLine("Enter math operation.");

                    string operation = Console.ReadLine();

                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                    IPAddress serverIp = IPAddress.Parse(SERVER_IP);
                    byte[] bytesToSend = Encoding.ASCII.GetBytes(operation);

                    IPEndPoint serverEndPoint = new IPEndPoint(serverIp, PORT);

                    socket.SendTo(bytesToSend, serverEndPoint);
                    byte[] bytesToRead = new byte[socket.ReceiveBufferSize];
                    int readedBytesCount = socket.Receive(bytesToRead);
                    Console.WriteLine("Result of {0} operation: {1}", operation, Encoding.ASCII.GetString(bytesToRead, 0, readedBytesCount));
                    Console.ReadLine();
                    socket.Close();
                }
                else
                if (connectionType.Equals("exit"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Enter valid protocol name: TCP or UDP.");
                }
            }
        }
    }
}
