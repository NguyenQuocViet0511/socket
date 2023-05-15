using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class Program
    {
        private const int BUFFER_SIZE = 1024;
        private const int PORT_NUMBER = 9999;
        static TcpListener listener;
        static IPAddress address;
        static byte[] data;
        static Stream stream;
        static Socket socket;
        static ASCIIEncoding encoding = new ASCIIEncoding();
        static void Main(string[] args)
        {

            Connect();
            

        }
        static void Connect()
        {
            address = IPAddress.Parse("127.0.0.1");
            listener = new TcpListener(address, PORT_NUMBER);
            try
            {
                Console.WriteLine("Server started on " + listener.LocalEndpoint);
                Console.WriteLine("Waiting for a connection...");
               
                Thread Listen = new Thread(() =>
                {
                   
                    while (true)
                    {
                        listener.Start();
                        socket = listener.AcceptSocket();
                        Console.WriteLine("Connection received from " + socket.RemoteEndPoint);
                        Thread receive = new Thread(Receive);
                        receive.IsBackground = true;
                        receive.Start();
                    }

                });
                Listen.IsBackground = true;
                Listen.Start();
            }
            catch (Exception)
            {

                address = IPAddress.Parse("127.0.0.1");
                listener = new TcpListener(address, PORT_NUMBER);

            }

            Console.ReadLine();
        }
       
        static void send(string Text)
        {
            socket.Send(encoding.GetBytes(Text));

        }
        static void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[BUFFER_SIZE];
                    socket.Receive(data);
                    string str = encoding.GetString(data);

                    Console.WriteLine(str);
                    send("OKE");

                }

            }
            catch (Exception)
            {

                close();
            }

        }
        static string Text(string text)
        {
            return text;
        }
        static void close()
        {
            socket.Close();
        }
    }
}