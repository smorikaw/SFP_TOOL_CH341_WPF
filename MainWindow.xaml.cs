using System;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;



namespace SFP_TOOL_CH341
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public byte[] EEPROM = new byte[256];
        public byte[] PAGE00 = new byte[256];
        public byte[] PAGE01 = new byte[256];
        public byte[] PAGE02 = new byte[256];
        public byte[] PAGE03 = new byte[256];

        public UInt32? COM_MODE;
        public String? COM_PORT;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            // Configure load file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "EEPROM"; // Default file name
            dialog.DefaultExt = ".bin"; // Default file extension
            dialog.Filter = "bin documents (.bin)|*.bin"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Load document
                string filename = dialog.FileName;
                int len = LoadFromBinaryFile(filename, this);
                //
                // HEX dump
                //
                textBox.Text = HEX_lower();
                textBox.Text += HEX_page00();
                if (len > 0x100) { 
                textBox.Text += HEX_page01();
                textBox.Text += HEX_page02();
                textBox.Text += HEX_page03();
                }
                // decode
                SFP sfp = new();
                textBox.Text += sfp.sff_eth(this);

            }
        }
        private void ReadI2C_Click(object sender, RoutedEventArgs e)
        {
            // SFF8472
            //    A2h Diagnostics DDM
            // SFF8636 Table 6-22 Option Values (Page 00h Bytes 193-195)
            //    byte 195 7 : Memory Page 02 provided. 1 if implemented, else 0.
            //             6 : Memory Page 01h provided. 1 if implemented, else 0.
            //             0 : Pages 20-21h implemented. Default = 0 (not implemented).
            // CMIS
            //     Page 01h (Advertising)
            //      
            //     Page 03h (User EEPROM)
            statusLabel.Content = "start CH341 read";
            CH341 ch341 = new CH341();
            statusLabel.Content = "read I2C lower";
            ch341.read_page00(ref EEPROM);
            textBox.Text = HEX_lower();

            statusLabel.Content = "read I2C page00";
            ch341.read_upper(ref PAGE00, 0);
            textBox.Text += HEX_page00();
            //   statusLabel.Content = "read I2C page01";
            //   ch341.read_upper(ref PAGE01, 1);
            //  textBox.Text += HEX_page01();
            //   statusLabel.Content = "read I2C page02";
            //   ch341.read_upper(ref PAGE02, 2);

            statusLabel.Content = "read I2C page03";
            ch341.read_upper(ref PAGE03, 3);
            textBox.Text += HEX_page03();
            //
            SFP sfp = new();
            textBox.Text += sfp.sff_eth(this);      // decode details
        }
        private void checkCH341_Click(object sender, RoutedEventArgs e)
        {
            statusLabel.Content = "check CH341 driver";
            long dllV=0L, driverV=0L, icV=0L;
            string name="";
            CH341 ch341 = new();
            ch341.getver(ref dllV, ref driverV, ref name, ref icV);
            textBox.Text  = string.Format("dllVersion    : {0:D2}", dllV);
            textBox.Text += string.Format("driverVersion : {0:D2}", driverV);
            textBox.Text += string.Format("driverName    : {0:D2}", name);
            textBox.Text += string.Format("icVersion     : {0:D2}", icV);

        }
        private void COMsel_Click(object sender, RoutedEventArgs e)
        {
            statusLabel.Content = "open Serial setting";
            COMSET comset = new COMSET();
            comset.Owner = this;
            comset.Show();
        }
        private void script_Click(object sender, RoutedEventArgs e)
        {
            statusLabel.Content = "open Script Edit window";
            ScriptEdit w = new ScriptEdit();
            w.Owner = this;
            w.Show();
        }
        private void ISS_Click(object sender, RoutedEventArgs e)
        {
            // SFF8472
            //    A2h Diagnostics DDM
            // SFF8636 Table 6-22 Option Values (Page 00h Bytes 193-195)
            //    byte 195 7 : Memory Page 02 provided. 1 if implemented, else 0.
            //             6 : Memory Page 01h provided. 1 if implemented, else 0.
            //             0 : Pages 20-21h implemented. Default = 0 (not implemented).
            // CMIS
            //     Page 01h (Advertising)
            //      
            //     Page 03h (User EEPROM)
            switch (COM_MODE)
            {
                case 0:
                    MessageBox.Show("Please select I2C chip type","Serial I2C type not set",
                        MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Yes);
                    break;
                case 1:
                    statusLabel.Content = "read ISS upper";
                    USBISS usbiss = new();
                    usbiss.read_page00(ref EEPROM, COM_PORT);
                    usbiss.read_upper(ref PAGE00, 0, COM_PORT);
                    break;
                case 2:
                    statusLabel.Content = "read IM700 upper";
                    SC18IM700 im700 = new();
                    im700.readI2C(0x00, 0x80, ref EEPROM);
                    im700.readI2C(0x80, 0x80, ref PAGE00);
                    break;
            }


            //
            // HEX dump
            //
            textBox.Text = HEX_lower();
            textBox.Text += HEX_page00();

            SFP sfp = new();
            textBox.Text += sfp.sff_eth(this);
        }
        //==========================================================
        //
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "EEPROM"; // Default file name
            dialog.DefaultExt = ".bin"; // Default file extension
            dialog.Filter = "bin documents (.bin)|*.bin"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dialog.FileName;
                SaveToJsonFile(filename,this);
            }
        }
        private void Save_json_Click(object sender, RoutedEventArgs e) {
            // Configure save file dialog box
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "EEPROM"; // Default file name
            dialog.DefaultExt = ".bin"; // Default file extension
            dialog.Filter = "bin documents (.json)|*.json"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dialog.FileName;

                SaveToBinaryFile(filename, this);
            }
        }
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            textBox.Copy();
        }
        private void OnCtrlC(object sender, RoutedEventArgs e)
                        {
                            //なんか
                        }
        private void Quit_Click(object sender, RoutedEventArgs e) {
                            this.Close();
                        }
        private void I2CConf_Click(object sender, RoutedEventArgs e)
        {
            USBISS usbiss = new();
            MessageBox.Show(usbiss.getver(COM_PORT), "USB-ISS get version",
                MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
        }
        private void DDM_Click(object sender, RoutedEventArgs e)
        {
            DDMWindow ddmwin = new DDMWindow();
            ddmwin.Owner = this;
            ddmwin.Show();
        }
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            INFOWindow infwin = new INFOWindow();
            infwin.Owner = this;
            infwin.Show();
        }
        //
        //=============================================
        //
        public String HEX_lower() {
            int i, j;
            String s = "";
            for (i = 0; i< 0x08; i++)
            {
                s += System.String.Format("{0:X2} : ", i << 4);
                for (j = 0x00; j< 0x10; j++) {
                    s += System.String.Format("{0:X2} ", EEPROM[i * 0x10 + j]);
                }
                s += System.Environment.NewLine;
            }
            return s;
        }
        public String HEX_page00()
        {
            int i, j;
            String s = "[page 00]" + System.Environment.NewLine;
            for (i = 0x08; i < 0x10; i++)
            {
                s += System.String.Format("{0:X2} : ", i << 4);
                for (j = 0x00; j < 0x10; j++)
                {
                    s += System.String.Format("{0:X2} ", PAGE00[i * 0x10 + j]);
                }
                s += System.Environment.NewLine;
            }
            return s;
        }
        public String HEX_page01()
        {
            int i, j;
            String s = "[page 01]" + System.Environment.NewLine;
            for (i = 0x08; i < 0x10; i++)
            {
                s += System.String.Format("{0:X2} : ", i << 4);
                for (j = 0x00; j < 0x10; j++)
                {
                    s += System.String.Format("{0:X2} ", PAGE01[i * 0x10 + j]);
                }
                s += System.Environment.NewLine;
            }
            return s;
        }
        public String HEX_page02()
        {
            int i, j;
            String s = "[page 02]" + System.Environment.NewLine;
            for (i = 0x08; i < 0x10; i++)
            {
                s += System.String.Format("{0:X2} : ", i << 4);
                for (j = 0x00; j < 0x10; j++)
                {
                    s += System.String.Format("{0:X2} ", PAGE02[i * 0x10 + j]);
                }
                s += System.Environment.NewLine;
            }
            return s;
        }
        public String HEX_page03() {
            int i, j;
            String s = "[page 03]" + System.Environment.NewLine;
            for (i = 0x08; i < 0x10; i++)
            {
                s += System.String.Format("{0:X2} : ", i << 4);
                for (j = 0x00; j < 0x10; j++)
                {
                    s += System.String.Format("{0:X2} ", PAGE03[i * 0x10 + j]);
                }
                s += System.Environment.NewLine;
            }
            return s;
        }
        //=================================================================================================
        public static void SaveToJsonFile(string path, MainWindow w)
        {
            FileStream fs = new FileStream(path,
                FileMode.Create,
                FileAccess.Write);
            var writer = new BinaryWriter(fs);
            //public virtual void Write (byte[] buffer, int index, int count);
            writer.Write(w.EEPROM, 0x00, 0x80);
            writer.Write(w.PAGE00, 0x80, 0x80);
            writer.Write(w.PAGE01, 0x80, 0x80);
            writer.Write(w.PAGE02, 0x80, 0x80);
            writer.Write(w.PAGE03, 0x80, 0x80);
            fs.Close();
        }
        public static void SaveToBinaryFile(string path, MainWindow w)
        {
            var data = new SFP_EEPROM
            {
                upper = w.EEPROM,
                page00 = w.PAGE00,
                page01 = w.PAGE01,
                page02 = w.PAGE02,
                page03 = w.PAGE03,
            };
  //      var json_str = System.Text.Json.Serialization(data);

        FileStream fs = new FileStream(path,
                FileMode.Create,
                FileAccess.Write);
            var writer = new BinaryWriter(fs);
            //public virtual void Write (byte[] buffer, int index, int count);
    //        writer.Write(w.EEPROM, 0x00, 0x80);
    //        writer.Write(w.PAGE00, 0x80, 0x80);
     //       writer.Write(w.PAGE01, 0x80, 0x80);
      //      writer.Write(w.PAGE02, 0x80, 0x80);
       //     writer.Write(w.PAGE03, 0x80, 0x80);
       //     writer.Write(json_str);
            fs.Close();
        }
        public static int LoadFromBinaryFile(string path, MainWindow w)
        {
            FileInfo file = new FileInfo(path);
            int len = (int)file.Length;
            FileStream fs = new FileStream(path,
                FileMode.Open,
                 FileAccess.Read);
            var reader = new BinaryReader(fs);
            if (0x80 < (len-1))
            {
                ;

                //public virtual void Write (byte[] buffer, int index, int count);
                reader.Read(w.EEPROM, 0x00, 0x80);
            }
            if (0x80 < len) reader.Read(w.PAGE00, 0x80, 0x80);
//            if (0x100 < (len)) reader.Read(w.PAGE00, 0x80, 0x80);
            if (0x180 < (len)) reader.Read(w.PAGE01, 0x80, 0x80);
            if (0x200 < (len)) reader.Read(w.PAGE02, 0x80, 0x80);
            if (0x280 < (len)) reader.Read(w.PAGE03, 0x80, 0x80);
            fs.Close();
            // 前半の128byteエリアは使わない
            /*
            int i;
            for(i =0; i< 0x80; i++)
            {
                w.PAGE00[0x80 + i] = w.PAGE00[i];
                w.PAGE01[0x80 + i] = w.PAGE01[i];
                w.PAGE02[0x80 + i] = w.PAGE02[i];
                w.PAGE03[0x80 + i] = w.PAGE03[i];
            }
            */

            return len;
        }

        private void FontLarge_Click(object sender, RoutedEventArgs e)
        {
            textBox.FontSize += 2;
        }
        private void FontSmall_Click (object sender, RoutedEventArgs e)
        {
            textBox.FontSize -= 2;
        }
    }//// end of class define
    class SFP_EEPROM
    {
        public byte[] EEPROM = new byte[256];
        public byte[] PAGE00 = new byte[256];
        public byte[] PAGE01 = new byte[256];
        public byte[] PAGE02 = new byte[256];
        public byte[] PAGE03 = new byte[256];
        public byte[]? upper { get; set; }
        public byte[]? page00 { get; set; }
        public byte[]? page01 { get; set; }
        public byte[]? page02 { get; set; }
        public byte[]? page03 { get; set; }
    }
}/// end of name space