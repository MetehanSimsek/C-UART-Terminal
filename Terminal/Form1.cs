using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace Terminal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonDisconnect.Enabled = false;
            buttonSend.Enabled = false;
            foreach (var seriPort in SerialPort.GetPortNames())
            {
                comboBoxPorts.Items.Add(seriPort);
            }
            comboBoxPorts.SelectedIndex = 0;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBoxPorts.Text;
            try
            {
                serialPort1.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection Failed \n Hata:{ex.Message}", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (serialPort1.IsOpen)
            {
                buttonConnect.Enabled = false;
                buttonDisconnect.Enabled = true;
                buttonSend.Enabled = true;
            }



        }
        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            // serialPort1.PortName = comboBoxPorts.Text;
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                buttonConnect.Enabled = true;
                buttonDisconnect.Enabled = false;
                buttonSend.Enabled = false;

            }
        }
        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write(textBoxSend.Text);
                textBoxSend.Clear();
            }
        }

        public delegate void veriGoster(String s);

        public void textBoxYaz(String s)
        {
            textBoxReceiver.Text += s;
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            String gelenVeri = serialPort1.ReadExisting();
            //textBoxReceiver.Text += gelenVeri;
            textBoxReceiver.Invoke(new veriGoster(textBoxYaz), gelenVeri);


        }

       

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            serialPort1.PortName = comboBoxPorts.Text;
        }



        private void textBoxReceiver_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSend_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            int baud = Int32.Parse(baudrate.Text);
            serialPort1.BaudRate = baud;
            comboBoxPorts.SelectedIndex = 0;
        }

        private void parity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (parity.Text == "Even")
            {
                serialPort1.Parity = Parity.Even;
            }
            if (parity.Text == "Mark")
            {
                serialPort1.Parity = Parity.Mark;
            }
            if (parity.Text == "None")
            {
                serialPort1.Parity = Parity.None;
            }
            if (parity.Text == "Odd")
            {
                serialPort1.Parity = Parity.Odd;
            }
            if (parity.Text == "Space")
            {
                serialPort1.Parity = Parity.Space;
            }
        }

        private void dataBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            int dataBit = Int32.Parse(dataBits.Text);
            serialPort1.DataBits = dataBit;
           
        }

        private void stopBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(stopBits.Text == "1")
            {
                serialPort1.StopBits = StopBits.One;
            }
            if (stopBits.Text == "1.5")
            {
                serialPort1.StopBits = StopBits.OnePointFive;
            }
                if (stopBits.Text == "2")
            {
                serialPort1.StopBits = StopBits.Two;
            }
            
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
        public  void dosyadan_oku()
        {
 
        }

        public static void dosyayaz()
        {
            string dosya_yolu = @"C:\Users\meteh\OneDrive\Masaüstü\ReadData.txt";
            FileStream fs = new FileStream(dosya_yolu, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            //sw.WriteLine("Merhaba");
            //sw.WriteLine("Dunya");
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string dosya_yolu = @"SC:\Users\meteh\OneDrive\Masaüstü\ReadData.txt";
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open,FileAccess.Read  );
            StreamReader sw = new StreamReader(fs);
            fileReadBox.Text = sw.ReadToEnd();

            sw.Close();
            fs.Close();
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine(fileReadBox.Text);
               
            }
        }

        private void fileReadBox_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBoxPorts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
