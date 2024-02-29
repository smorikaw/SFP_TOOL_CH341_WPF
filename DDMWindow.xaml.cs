using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

// SFF-8472
//    Table 8-5 Diagnostic Monitoring Type
//      byte 92 bit 6 : Digital diagnostic monitoring implemented
//    Table 8-6 Enhanced Options
//      bit 6 : Optional soft TX_DISABLE control and monitoring implemented
//    9.8 Real Time Diagnostic and Control Registers [Address A2h, Bytes 96-111]
//     byte 96 tmp, byte 98 VCC, byte 100 TX bias, byte 102 TX PWR, 104 RX PWR
// SFF-8636
//   Table 6-8 Free Side Monitoring Values (Page 00h Bytes 22-23
//   Table 6-9 Channel Monitoring Values (Page 00h Bytes 34-81)
//     byte 34 : RX PWR, byte 42 : TX bias, byte 50 : TX PWR
//   Table 6-29 Equalizer, Emphasis, Amplitude and Timing (Page 03h Bytes 224-229)
//   FEC 
// CMIS
//   Table 8-47 Supported Monitors Advertisement (Page 01h)
//     byte 159 bit 1 VccMonSupported., bit 0 TempMonSupported,
//     byte 160 bit 2 RxOpticalPowerMonSupported, bit 1  TxOpticalPowerMonSupported, bit 0 TxBiasMonSupported
//   Table 8-82 Media Lane-Specific Monitors (Page 11h)
//     byte 154
namespace SFP_TOOL_CH341
{
    /// <summary>
    /// DDMWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class DDMWindow : Window
    {
        private List<DataGridItems> ddms;

        public DDMWindow()
        {
            InitializeComponent();

            ddms = new List<DataGridItems>
            {
                new DataGridItems("0", "-40.0000 dBm", "0.0000 mA", "-40.0000 dBm"),
                new DataGridItems("1", "-40.0000 dBm", "0.0000 mA", "-40.0000 dBm"),
                new DataGridItems("2", "-40.0000 dBm", "0.0000 mA", "-40.0000 dBm"),
                new DataGridItems("3", "-40.0000 dBm", "0.0000 mA", "-40.0000 dBm"),
                new DataGridItems("4", "-40.0000 dBm", "0.0000 mA", "-40.0000 dBm"),
                new DataGridItems("5", "-40.0000 dBm", "0.0000 mA", "-40.0000 dBm"),
                new DataGridItems("6", "-40.0000 dBm", "0.0000 mA", "-40.0000 dBm"),
                new DataGridItems("7", "-40.0000 dBm", "0.0000 mA", "-40.0000 dBm"),
            };

            DataGridName.ItemsSource = ddms;
            // LP mode diable check SFF-8472 byte 93 bit 6
        }
        int format_check()
        {
            return 1;
        }
        // byte 50-57 LSB equal to 0.1 μW,
        float SFF8636_ddm_txpwr(int i)
        {
            int v;
            float f;
            CH341 ch341 = new();
          //  v = ch341.readw(0x50, 0, (byte)(50 + (i * 2)));
            v = ch341.readI2CReg8(0xa0, (byte)(50 + (i * 2))) * 0x100 + ch341.readI2CReg8(0xa0, (byte)(51 + (i * 2)));
            f = (float)v * 0.0001F;
            return (float)System.Math.Log10((double)f)*10F;
        }
        float SFF8636_ddm_rxpwr(int i)
        {
            int v;
            float f;
            CH341 ch341 = new();
            //   v = ch341.readw(0x50, 0, (byte)(34 + (i * 2)));
            v = ch341.readI2CReg8(0xa0, (byte)(34 + (i * 2))) * 0x100 + ch341.readI2CReg8(0xa0, (byte)(35 + (i * 2)));
            f = (float)v * 0.0001F;
            return (float)System.Math.Log10((double)f)*10F;
        }
        // equal to 2 μA,
        float SFF8636_ddm_txbias(int i)
        {
            int v;
            float f;
            CH341 ch341 = new();
            //   v = ch341.readw(0x50, 0, (byte)(42 + (i * 2)));
            v = ch341.readI2CReg8(0xa0, (byte)(42 + (i * 2))) * 0x100 + ch341.readI2CReg8(0xa0, (byte)(43 + (i * 2)));
            f = (float)v * 0.002F;
            return f;
        }
        void SFF8472_ddm_update()
        {
            //        ddms[0]._RXpwr = string.Format("{0:F4} dBm", SFF8472_ddm_txpwr());
            //       ddms[0]._TXbias = string.Format("{0:F4} mA", SFF8472_ddm_txpwr());
            //       ddms[0]._TXpwr = string.Format("{0:F4} dBm", SFF8472_ddm_txpwr());
        }
        void SFF8636_temp()
        {
            CH341 ch341 = new();
            float temp = (float)ch341.readI2CReg8(0xa0, 23) / 256.0F + (float)ch341.readI2CReg8(0xa0, 22); ;
            tempText.Text = string.Format("{0:X2}", ch341.readI2CReg8(0xa0, 22)) +
                string.Format("{0:X2} : ", ch341.readI2CReg8(0xa0, 23)) + 
                string.Format("{0:F2} C", temp);
        }
        void SFF8636_vcc()
        {
            CH341 ch341 = new();
            float vcc = ((float)ch341.readI2CReg8(0xa0,27) + (float)ch341.readI2CReg8(0xa0, 26) * 256F) / 10000F;
            vccText.Text = string.Format("{0:X2}", ch341.readI2CReg8(0xa0, 26)) +
                string.Format("{0:X2} : ", ch341.readI2CReg8(0xa0, 27)) +
                string.Format("{0:F2} V", vcc);
        }
        void SFF8636_ddm_update()
        {
            int i;
            // if single lamda
            /*
            // if
            {
                ddms[0]._RXpwr = string.Format("{0:F4} dBm", SFF8636_ddm_rxpwr(0));
                ddms[0]._TXbias = string.Format("{0:F4} mA", SFF8636_ddm_txbias(0));
                ddms[0]._TXpwr = string.Format("{0:F4} dBm", SFF8636_ddm_txpwr(0));
            }
            */
            SFF8636_temp();
            SFF8636_vcc();
            for (i = 0; i < 4; i++)
            {
                ddms[i]._RXpwr = string.Format("{0:F4} dBm", SFF8636_ddm_rxpwr(i));
                ddms[i]._TXbias = string.Format("{0:F4} mA", SFF8636_ddm_txbias(i));
                ddms[i]._TXpwr = string.Format("{0:F4} dBm", SFF8636_ddm_txpwr(i));
            }
            for (i = 4; i < 8; i++)
            {
                ddms[i]._RXpwr = "NA";
                ddms[i]._TXbias = "NA";
                ddms[i]._TXpwr = "NA";
            }
        }
        // update bottun
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (format_check())
            {
                case 0: // SFF-8472
                    break;
                case 1: // SFF-8636
                    SFF8636_ddm_update();
                    break;
                case 2: // CMIS
                    break;
            }
            DataGridName.Items.Refresh();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // byte 99 bit 1 is LP/TXDis select
            // byte 93 bit 0 is Override
            //         bit 3 is class 8 enable
            //         bit 2 is class 5-7 enable
            //         bit 1 is set Lpmode(class 1)
            CH341 ch341 = new CH341();
            ch341.writeI2CReg8(0x0a, 93, 0b_0000_0101);
            txpText.Text = "LP mode on";
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CH341 ch341 = new CH341();
            ch341.writeI2CReg8(0x0a, 93, 0b_0000_0011);
            txpText.Text = "LP mode off";
        }
    }
    public class DataGridItems
    {
        public DataGridItems(string items0, string items1, string items2, string items3)
        {
            this._Lane = items0;
            this._RXpwr = items1;
            this._TXbias = items2;
            this._TXpwr = items3;
        }

        public string _Lane { get; set; }
        public string _RXpwr { get; set; }
        public string _TXbias { get; set; }
        public string _TXpwr { get; set; }
    }
}
