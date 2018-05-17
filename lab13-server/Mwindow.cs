using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab13_server
{
    public partial class Mwindow : Form
    {
        public Mwindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {                   
            Listener l = new Listener();
            button1.Enabled = false;
        }
    }
}
