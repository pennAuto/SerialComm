using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Management;


namespace pennSerial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            getAvailablePorts();
        }
        void getAvailablePorts()
        {            
            string[] ports = 
                SerialPort.GetPortNames();
            cmbPortNames.Items.AddRange(ports);   
        }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPortNames.Text == "" || cmbBaudRate.Text == "")
                {
                    txtReceive.Text = "Please select port settings";
                }
                else
                {
                    txtReceive.Text = "";
                    serialPort1.PortName = cmbPortNames.Text;
                    serialPort1.BaudRate = Convert.ToInt32(cmbBaudRate.Text);
                    serialPort1.RtsEnable = true;
                    serialPort1.DtrEnable = true;
                    serialPort1.Handshake = Handshake.None;
                    serialPort1.Open();
                    progressBar1.Value = 100;
                    btnSend.Enabled = true;
                    btnReceive.Enabled = true;
                    txtSend.Enabled = true;
                    btnOpenPort.Enabled = false;
                    btnClosePort.Enabled = true;
                 
                }
            }
            catch(UnauthorizedAccessException)
            {
                txtReceive.Text = "Unauthorised Access";
            }
        }

        private void btnClosePort_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            progressBar1.Value = 0;
            btnSend.Enabled = false;
            btnReceive.Enabled = false;
            txtSend.Enabled = false ;
            btnOpenPort.Enabled = true;
            btnClosePort.Enabled = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            txtReceive.Text = "";
            //int sendVal = Convert.ToInt32(txtSend.Text);
            byte[] buffer = new byte[1];
            buffer[0] = 2;
            serialPort1.Write(buffer, 0, 1);
            txtSend.Text = "";
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            txtReceive.Text = "";
            serialPort1.ReadTimeout = 3000;
            try
            {
                txtReceive.Text = serialPort1.ReadLine();
                //txtReceive.Text = serialPort1.ReadExisting();
            }
            catch (TimeoutException)
            {
                txtReceive.Text = "Timeout exception...";
            }

        }
    }
}
