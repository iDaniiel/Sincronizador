using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gdtPublisherStress
{
    public partial class Form1 : Form
    {
        LoadStress stress;
        public Form1()
        {
            InitializeComponent();
            stress = new LoadStress();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            stress.Send(textBox1.Text);   
        }
    }
}
