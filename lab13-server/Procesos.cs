using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab13_server
{
    public partial class Procesos : Form
    {
        NetworkStream stream;
        TcpClient client;
        private byte[] data;

        public Procesos(TcpClient client,NetworkStream stream)
        {
            InitializeComponent();
            this.client = client;
            this.stream = stream;
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            //Add column header
            listView1.Columns.Add("Proceso", 500);

            Send("Manda los procesos");
            while(true)
            {
                String temp = Listen();

                if (temp.Contains("FIN")){
                    break;
                }
                else
                {
                    Send("recibido");
                    string[] arr = new string[4];
                    ListViewItem itm;                
                    //Add first item
                    arr[0] = temp;
                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
                }
            }
        }

        public String Listen()
        {
            data = new Byte[256];
            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Debug.Print("Received h: {0}", responseData);
            return responseData;
        }

        public void Send(String message)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
            Debug.Print("Sent: {0}", message);
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = listView1.SelectedItems[0];
            Debug.Print(item.SubItems[0].Text);
            Send(item.SubItems[0].Text);
            item.Remove();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Send("FIN");
            this.Dispose();
        }
    }
}
