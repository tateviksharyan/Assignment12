using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Assignment12
{
    class Program
    {
        const int PORT = 3000;
        const string SERVER_IP = "127.0.0.1";

        static void Main(string[] args)
        {
            IPAddress localAddress = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAddress, PORT);
            TcpClient tcpClient = null;
            UdpClient udpClient = null;
            listener.Start();

            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, PORT);

            while(true)
            {
                tcpClient = listener.AcceptTcpClient();
                udpClient = new UdpClient(PORT);

                int size = tcpClient.ReceiveBufferSize;
                if (size != 0)
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] buffer = new byte[size];

                    int bytesRead = networkStream.Read(buffer, 0, size);

                    string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    double result = ProcessOperation(receivedData);

                    Console.WriteLine("The result of {0} operation: {1}", receivedData, result);

                    byte[] bytesToSend = Encoding.ASCII.GetBytes(result.ToString());

                    networkStream.Write(bytesToSend, 0, bytesToSend.Length);

                    tcpClient.Close();
                }
                else
                {
                    byte[] buffer = udpClient.Receive(ref endpoint);

                    string receivedData = Encoding.ASCII.GetString(buffer, 0, buffer.Length);

                    double result = ProcessOperation(receivedData);

                    Console.WriteLine("The result of {0} operation: {1}", receivedData, result);

                    byte[] bytesToSend = Encoding.ASCII.GetBytes(result.ToString());

                    udpClient.Send(bytesToSend, bytesToSend.Length, endpoint);

                    listener.Stop();
                }
                Console.ReadLine();
            }
        }

        private static double ProcessOperation(string operationString)
        {
            string[] operands = operationString.Split(':');

            IMathServiceImpl operation = new IMathServiceImpl();

            switch (operands[0])
            {
                case "+":
                    return operation.Add(double.Parse(operands[1]), double.Parse(operands[2]));
                case "-":
                    return operation.Sub(double.Parse(operands[1]), double.Parse(operands[2]));
                case "*":
                    return operation.Mult(double.Parse(operands[1]), double.Parse(operands[2]));
                case "/":
                    return operation.Div(double.Parse(operands[1]), double.Parse(operands[2]));
                default:
                    throw new Exception("Unsupported operation!");
            }
        }
    }
}
