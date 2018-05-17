using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab13_server
{
    class Listener
    {
        public Listener()
        {
            var th = new Thread(Listen);
            th.Start();
        }

        private void Handler(TcpClient client)
        {
            //Buffer for reading data
            Byte[] bytes = new Byte[256];
            String data = null;
            data = null;

            try
            {
                NetworkStream stream = client.GetStream();

                int i;
                while (true)
                {
                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Debug.Print("Received: {0}", data);
                        if (data.Contains("Enviando Procesos"))
                        {
                            Application.Run(new Procesos(client,stream));
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
            }
        }

        private void Listen()
        {
            TcpListener server = null;
            try
            {
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("192.168.0.16");
                server = new TcpListener(localAddr, port);
                server.Start();
               
                while (true)
                {
                    Debug.Print("Waiting for a connection... ");
                    TcpClient client = server.AcceptTcpClient();                
                    Debug.Print("Connected!");

                    var th = new Thread(() => Handler(client));
                    th.Start();
                    
                    Thread.Sleep(300);
                    //client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }
        }

        
    }
}
