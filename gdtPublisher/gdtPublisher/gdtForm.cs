using gdtPublisher.Processes;
using gdtPublisher.Utils;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace gdtPublisher
{
    public partial class gdtForm : Form
    {
        public gdtForm()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            bool msgCopyData = m.Msg == WinApi.WM_COPYDATA;
            if (msgCopyData)
            {
                WinApi.Copydata copy = (WinApi.Copydata)Marshal.PtrToStructure(m.LParam, typeof(WinApi.Copydata));
                string message = Marshal.PtrToStringAnsi(copy.lpData);

                Transactions.Create(message);
            }
            base.WndProc(ref m);
        }
        private void gdtForm_Load(object sender, EventArgs e)
        {

        }
    }
}
