using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO.Ports;
// need nugets System.IO.Ports
namespace SFP_TOOL_CH341
{
    internal class USBISS
    {
        static SerialPort? serialPort;
        public String? COM_PORT;

        public byte readI2CReg8(byte add, byte reg)
        {
            byte[] buf = new byte[8];
            serialPort = new SerialPort();
            // Allow the user to set the appropriate properties.
            serialPort.PortName = COM_PORT;
            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;

            // Set the read/write timeouts
            serialPort.ReadTimeout = 500;

            serialPort.Open();

            buf[0] = 0x55;     // get I2C device count
                               //  buf[0] = (byte)addr<<1;     //
            buf[1] = ++add;
            buf[2] = reg;
            buf[3] = 0x01;      // number of write data
            serialPort.Write(buf, 0, 4);    // buffer,offset,bytes
            serialPort.Read(buf, 0, 1);

            serialPort.Close();

            return buf[0];
        }
        public byte writeI2CReg8(byte add, byte reg,byte data)
        {
            byte[] buf = new byte[8];
            serialPort = new SerialPort();
            // Allow the user to set the appropriate properties.
            serialPort.PortName = COM_PORT;
            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;

            // Set the read/write timeouts
            serialPort.ReadTimeout = 500;

            serialPort.Open();

            buf[0] = 0x55;     // get I2C device count
                               //  buf[0] = (byte)addr<<1;     //
            buf[1] = add;
            buf[2] = reg;
            buf[3] = 0x01;      // number of write data
            buf[4] = data;
            serialPort.Write(buf, 0, 5);    // buffer,offset,bytes
            serialPort.Read(buf, 0, 1);

            serialPort.Close();

            return buf[0];
        }
        public bool USBISSwriteI2C(string port,byte addr,byte reg, byte data)
        {
            byte[] buf = new byte[8];
            serialPort = new SerialPort();
            // Allow the user to set the appropriate properties.
            serialPort.PortName = port;
            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;

            // Set the read/write timeouts
            serialPort.ReadTimeout = 500;

            serialPort.Open();

            buf[0] = 0x55;     // get I2C device count
                               //  buf[0] = (byte)addr<<1;     //
            buf[1] = 0xa0;
            buf[2] = 0x01;      // nu,ber of write data
            buf[3] = data;
            serialPort.Write(buf, 0, 4);    // buffer,offset,bytes
            Thread.Sleep(1);  // 1 ms delay
            serialPort.Read(buf, 0, 1);

            serialPort.Close();

            return true;
        }
        public string getdevc(String port)
        {
            byte[] buf = new byte[8];
            serialPort = new SerialPort();
            // Allow the user to set the appropriate properties.
            serialPort.PortName = "COM5";
            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;

            // Set the read/write timeouts
            serialPort.ReadTimeout = 500;

            serialPort.Open();

            buf[0] = 0x58;     // get I2C device count
            buf[0] = 0xa0;     //
            serialPort.Write(buf, 0, 2);    // buffer,offset,bytes
            Thread.Sleep(1);  // 1 ms delay
            serialPort.Read(buf, 0, 1);

            serialPort.Close();
            return string.Format("{0:X2}:", buf[0]);
        }
        // get version 
        // return module ID = 0x07, FW revision , current mode 
        public string getver(String port)
        {
            byte[] buf = new byte[8];
            serialPort = new SerialPort();
            // Allow the user to set the appropriate properties.
            serialPort.PortName = "COM5";
            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;

            // Set the read/write timeouts
            serialPort.ReadTimeout = 500;

            serialPort.Open();

                buf[0] = 0x5a;     // ISSS verion
                buf[1] = 0x01;     //

                serialPort.Write(buf, 0, 2);    // buffer,offset,bytes
                Thread.Sleep(1);  // 1 ms delay
                serialPort.Read(buf, 0, 3);

            serialPort.Close();
            return string.Format("ID={0:X2}:", buf[0]) + string.Format(" FW REV={0:X2}:", buf[1]) + string.Format("MODE={0:X2}", buf[2]);
        }
        // USB-ISS FW version check
        public String check()
        {
            byte[] sb = new byte[5];

            SerialPort serialPort = new SerialPort(COM_PORT, 9600, Parity.None, 8, StopBits.One);
            serialPort.Open();

            sb[0] = 0x5A; // USB-ISS version
            sb[1] = 0x01; // target I2C address with write mode


            serialPort.Write(sb, 0, 2);
            serialPort.Read(sb, 0, 3);

            return string.Format("ID(normary 07)={0:X2}", sb[0]) +
                string.Format("/FW={0:X2}", sb[1]) +
                string.Format("/MODE={0:X2}", sb[2]);
        }
        public void read_upper(ref byte[] data, byte page,string port)
        {
            int i;
            byte[] buf = new byte[8];
            serialPort = new SerialPort();
            // Allow the user to set the appropriate properties.
            serialPort.PortName = port;
            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;

            // Set the read/write timeouts
            serialPort.ReadTimeout = 500;

            serialPort.Open();
            for (i = 0x80; i < 0x100; i++)
            {
                buf[0] = 0x55;     // 1 byte read
                buf[1] = 0xa1;     // I2C arrdess
                buf[2] = (byte)i;     // register
                buf[3] = 0x01;     // number of read
                serialPort.Write(buf, 0, 4);    // buffer,offset,bytes
                Thread.Sleep(1);  // 1 ms delay
                serialPort.Read(buf, 0, 1);
                data[i] = buf[0];
            }

            serialPort.Close();
        }
        public void read_page00(ref byte[] data, string port)
        {
            int i;
            byte[] buf = new byte[8];
            serialPort = new SerialPort();
            // Allow the user to set the appropriate properties.
            serialPort.PortName = port;
            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;

            // Set the read/write timeouts
            serialPort.ReadTimeout = 500;
            
            serialPort.Open();
            for (i=0;i<0x100; i++)
            {
                buf[0] = 0x55;     // 1 byte read
                buf[1] = 0xa1;     // I2C arrdess
                buf[2] = (byte)i;     // register
                buf[3] = 0x01;     // number of read
                serialPort.Write(buf, 0, 4);    // buffer,offset,bytes
                Thread.Sleep(1);  // 1 ms delay
                serialPort.Read(buf, 0, 1);
                data[i] = buf[0];
            }

            serialPort.Close();
        }
    }
}
