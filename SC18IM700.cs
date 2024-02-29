using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SFP_TOOL_CH341
{
    internal class SC18IM700
    {
        public String port = "COM6";

        public byte readI2CReg8(byte ad, byte reg)
        {
            byte[] sb = new byte[5];

            SerialPort serialPort = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
            serialPort.Open();

                sb[0] = 0x53; // 's' start
                sb[1] = ad; // target I2C address with write mode
                sb[2] = 0x01; // number of bytes
                sb[3] = reg;    // target register address
                sb[4] = 0x50;   // 'P' end of command
                serialPort.Write(sb, 0, 5);
                sb[0] = 0x53;
                sb[1] = 0xa1;   // target I2C addres with read mode
                sb[2] = 0x01;
                sb[3] = 0x50;
                serialPort.Write(sb, 0, 4);
                serialPort.Read(sb, 0, 1);
            
            serialPort.Close();
            return sb[0];
        }
        public void writeI2CReg8(byte ad, byte reg,byte data)
        {
            byte[] sb = new byte[6];


            SerialPort serialPort = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
            serialPort.Open();

                sb[0] = 0x53; // 's' start
                sb[1] = (byte)(ad + 1); // target I2C address with write mode
                                        //Note that the second byte sent is the I2C-bus device slave address.
                                        //The least significantbit(R) of this byte must be set to 1 to indicate
                                        //this is an I2C - bus write command.
                sb[2] = 0x01; // number of bytes
                sb[3] = reg;    // target register address
                sb[4] = data;
                sb[5] = 0x50;   // 'P' end of command
                serialPort.Write(sb, 0, 6);

            serialPort.Close();

        }
        public void readI2C(byte start, byte len, ref byte[] buf)
        {
            byte[] sb = new byte[5];
            int i;

            SerialPort serialPort = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
            serialPort.Open();
            for (i = start; i < (start + len); i++)
            {
                sb[0] = 0x53; // 's' start
                sb[1] = 0xa0; // target I2C address with write mode
                sb[2] = 0x01; // number of bytes
                sb[3] = (byte)i;    // target register address
                sb[4] = 0x50;   // 'P' end of command
                serialPort.Write(sb, 0, 5);
                sb[0] = 0x53;
                sb[1] = 0xa1;   // target I2C addres with read mode
                sb[2] = 0x01;
                sb[3] = 0x50;
                serialPort.Write(sb, 0, 4);
                serialPort.Read(sb, 0, 1);
                buf[i] = sb[0];
            }
            serialPort.Close();
        }
        public String check()
        {
            byte[] sb = new byte[5];
            SerialPort serialPort = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
            serialPort.Open();

            sb[0] = 0x52; // 'R' read internal register
            sb[1] = 0x07; // I2CClkL
            sb[2] = 0x08; // I2CClkH
            sb[3] = 0x0a; // I2Cstat reg
            sb[4] = 0x50;   // 'P' end of command
            serialPort.Write(sb, 0, 5);

            serialPort.Read(sb, 0, 3);
            serialPort.Close();

            //           return string.Format("ID(normary 07)={0:X2}", sb[0]) +
            //               string.Format("/FW={0:X2}", sb[1]) +
            //               string.Format("/MODE={0:X2}", sb[2]);
            return string.Format("{0:X2}:", sb[0]) +
                        string.Format("{0:X2}:", sb[1]) +
                        string.Format("{0:X2}", sb[2]);
        }
    }
}
