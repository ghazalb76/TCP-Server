using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TCP_Server
{
    class Server
    {
        const int PORT_NO = 5000;
        const string SERVER_IP = "172.31.34.255";
        const string message = "Hello!";

        static void Main(string[] args)
        {
            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAdd, PORT_NO);
            Console.WriteLine("Listening...");
            listener.Start();

            //---incoming client connected---
            TcpClient client = listener.AcceptTcpClient();

            //---get the incoming data through a network stream---
            NetworkStream nwStream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];

            while (true)
            {
                //---read incoming stream---
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                //---convert the data received into a string---
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received : " + dataReceived);

                if (dataReceived == "1")
                {
                    //---write back the text to the client---
                    byte[] messageBytes = System.Text.Encoding.ASCII.GetBytes(message);
                    Console.WriteLine("Sending back : " + dataReceived);
                    nwStream.Write(messageBytes, 0, messageBytes.Length);
                    Console.WriteLine("Data has been sent");
                }
            }
            client.Close();
            listener.Stop();
            Console.ReadLine();
        }
    }
}

