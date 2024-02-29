using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace SFP_TOOL_CH341
{
    /// <summary>
    /// COMSET.xaml の相互作用ロジック
    /// </summary>
    public partial class COMSET : Window
    {
        private SerialPort? serialPort;
        private UInt32 COM_MODE;
        private String COM_PORT="COM5";
        public String[]? COMPorts;
        //       private Binding COMPorts = new Binding("COMPorts");
        ViewModel viewModel = new ViewModel();

        public COMSET()
        {
            InitializeComponent();
            DataContext = viewModel;        // for binding
        }

        private void ComboBoxDropDownOpened_COMPort(object sender, EventArgs e)
        {

            viewModel.GetCOMPorts();
        }
        public void SerialOpen()
        {
            try
            {
                serialPort.PortName = COM_PORT;        //選択したport名
       //       serialPort.BaudRate = baudrate;    //選択したbaudrate
                serialPort.BaudRate = 9600;    //選択したbaudrate
                serialPort.DataBits = 8;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.WriteTimeout = 1000;
                serialPort.ReadTimeout = 1000;
                serialPort.Encoding = Encoding.UTF8;
                serialPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SerialClose()
        {
            try
            {
                serialPort.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void mode_update()
        {
            string s = COM_PORT + "\r\n"; 
            switch (COM_MODE)
            {
                case 0: s += "not sel"; break;
                case 1: s += "USB-ISS"; break;
                case 2: s += "IM700";   break;
            }
            ((MainWindow)this.Owner).COM_MODE = COM_MODE;
            ((MainWindow)this.Owner).COM_PORT = COM_PORT;
            ((MainWindow)this.Owner).modeText.Text = s;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            COM_MODE = 1;
            mode_update();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            COM_MODE = 2;
            mode_update();
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            COM_MODE = 0;
            mode_update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (COM_MODE)
            {
                case 0:
                    comText.Text = "port select first";
                    break;
                case 1:
                    USBISS port1 = new USBISS();
                    port1.COM_PORT = COM_PORT;
                    comText.Text = port1.check();
                    break;
                case 2:
                    SC18IM700 port2 = new SC18IM700();
                    port2.port = COM_PORT;
                    comText.Text = port2.check();
                    break;
            }
        }

        private void COM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            COM_PORT = (string)COM.SelectedItem;
            mode_update();
        }
    }
}
