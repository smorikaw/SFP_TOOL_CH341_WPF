using SFP_TOOL_CH341;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SFP_TOOL_CH341
{
    /// <summary>
    /// ScriptEdit.xaml の相互作用ロジック
    /// </summary>
    public partial class ScriptEdit : Window
    {

        /// Global
        private UInt32 I2Cmode;
        private CH341 ch341_sfp = new();
        private USBISS usbiss_sfp = new();
        private SC18IM700 im700_sfp = new();

        public ScriptEdit()
        {
            InitializeComponent();
        }

        byte convW(string str, UInt32 format) {
            byte val = 0;
            switch (format)
            {
                case 0: // HEX
                    val = (byte)Int32.Parse(str, System.Globalization.NumberStyles.HexNumber);
                    break;
                case 1: // DEC
                    val = (byte)Int32.Parse(str, System.Globalization.NumberStyles.Integer);
                    break;
                case 2: // ASCII
                        //   val = (byte)Int32.Parse(str, System.Globalization.NumberStyles.ASCIINumber);
                    break;
                default:
                    val = (byte)Int32.Parse(str, System.Globalization.NumberStyles.HexNumber);
                    break;
            }
            return val;
        }
        string convF(byte v, UInt32 format)
        {
            string s = "";
            switch (format)
            {
                case 0: // HEX
                    s = string.Format("{0:X2}", v);
                    break;
                case 1: // DEC
                    s = string.Format("{0:D2}", v);
                    break;
                case 2: // ASCII
                    if ((v >= 0x20) && (v <= 0x7e))  // if ASCII char range 
                    {
                        char c = (char)v;
                        s = c.ToString();
                    }
                    else
                    {
                        s = ".";
                    }
                    break;
                default:
                    s = string.Format("{0:X2}", v);
                    break;
            }
            return s;
        }

        //
        // script action main
        //
        void doScript(UInt32 action, byte address, byte reg, UInt32 format, String valT, TextBox teXt)
        {

            switch (action)
            {
                case 0: break;
                case 1:             // read action
                    switch (I2Cmode)
                    {
                        case 0:
                            teXt.Text = convF(ch341_sfp.readI2CReg8(address, reg), format);
                            break;
                        case 1:
                            teXt.Text = convF(usbiss_sfp.readI2CReg8(address, reg), format);
                            break;
                        case 2:
                            teXt.Text = convF(im700_sfp.readI2CReg8(address, reg), format);
                            break;
                    }
                    break;
                case 2:         // write action
                    byte val = convW(valT, format);
                    switch (I2Cmode)
                    {
                        case 0:
                            ch341_sfp.writeI2CReg8(address, reg, val);
                            break;
                        case 1:
                            usbiss_sfp.writeI2CReg8(address, reg, val);
                            break;
                        case 2:
                            im700_sfp.writeI2CReg8(address, reg, val);
                            break;
                    }
                    teXt.Text = "";
                    break;
            }
        }
        UInt32 I2Cmode_set()
        {
            UInt32 mode=0;
            switch (((MainWindow)this.Owner).COM_MODE)
            {
                case 0://

                    mode = 0; break;
                case 1: // USB-ISS
                    usbiss_sfp.port = ((MainWindow)this.Owner).COM_PORT;
                    mode = 1;  break;
                case 2: // IM700
                    im700_sfp.port = ((MainWindow)this.Owner).COM_PORT;
                    mode = 2; break;
            }

            return mode;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            byte[] addTable;
            addTable = new byte[] { 0xa0, 0xa2, 0xac };

            I2Cmode = I2Cmode_set();
            byte address, reg;
            UInt32 action, format;
            // loop 01
            if (Action01.SelectedIndex >= 0) { 
                action = (uint)Action01.SelectedIndex;
                address = addTable[ADD01.SelectedIndex];
                reg = (byte)Int32.Parse(Reg01.Text, System.Globalization.NumberStyles.HexNumber);
                format = (uint)Form01.SelectedIndex;
                doScript(action, address, reg, format, Val01.Text, Res01);
            }
            if (Action02.SelectedIndex >= 0)
            {
                action = (uint)Action02.SelectedIndex;
                address = addTable[ADD02.SelectedIndex];
                reg = (byte)Int32.Parse(Reg02.Text, System.Globalization.NumberStyles.HexNumber);
                format = (uint)Form02.SelectedIndex;
                doScript(action, address, reg, format, Val02.Text, Res02);
            }
            if (Action03.SelectedIndex >= 0)
            {
                action = (uint)Action03.SelectedIndex;
                address = addTable[ADD03.SelectedIndex];
                reg = (byte)Int32.Parse(Reg03.Text, System.Globalization.NumberStyles.HexNumber);
                format = (uint)Form03.SelectedIndex;
                doScript(action, address, reg, format, Val03.Text, Res03);
            }
            if (Action04.SelectedIndex >= 0)
            {
                action = (uint)Action04.SelectedIndex;
                address = addTable[ADD04.SelectedIndex];
                reg = (byte)Int32.Parse(Reg04.Text, System.Globalization.NumberStyles.HexNumber);
                format = (uint)Form04.SelectedIndex;
                doScript(action, address, reg, format, Val04.Text, Res04);
            }
            if (Action05.SelectedIndex >= 0)
            {
                action = (uint)Action05.SelectedIndex;
                address = addTable[ADD05.SelectedIndex];
                reg = (byte)Int32.Parse(Reg05.Text, System.Globalization.NumberStyles.HexNumber);
                format = (uint)Form05.SelectedIndex;
                doScript(action, address, reg, format, Val05.Text, Res05);
            }
            if (Action06.SelectedIndex >= 0)
            {
                action = (uint)Action06.SelectedIndex;
                address = addTable[ADD06.SelectedIndex];
                reg = (byte)Int32.Parse(Reg06.Text, System.Globalization.NumberStyles.HexNumber);
                format = (uint)Form06.SelectedIndex;
                doScript(action, address, reg, format, Val06.Text, Res06);
            }
            if (Action07.SelectedIndex >= 0)
            {
                action = (uint)Action07.SelectedIndex;
                address = addTable[ADD07.SelectedIndex];
                reg = (byte)Int32.Parse(Reg07.Text, System.Globalization.NumberStyles.HexNumber);
                format = (uint)Form07.SelectedIndex;
                doScript(action, address, reg, format, Val07.Text, Res07);
            }
            if (Action08.SelectedIndex >= 0)
            {
                action = (uint)Action08.SelectedIndex;
                address = addTable[ADD08.SelectedIndex];
                reg = (byte)Int32.Parse(Reg08.Text, System.Globalization.NumberStyles.HexNumber);
                format = (uint)Form08.SelectedIndex;
                doScript(action, address, reg, format, Val08.Text, Res08);
            }
        }
    }
}
