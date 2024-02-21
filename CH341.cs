
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SFP_TOOL_CH341
{
    internal class CH341
    {

        //  private static extern int MyFuncA(int a);
        //       int ans = MyFuncA(1);
        //
        // HANDLE h = CH341OpenDevice(iIndex);
        // ULONG dllVersion = CH341GetVersion();
        // ULONG driverVersion = CH341GetDrvVersion();
        // PVOID p = CH341GetDeviceName(iIndex);
        // ULONG icVersion = CH341GetVerIC(iIndex);
        // BOOL b = CH341ResetDevice(iIndex);
        //BOOL WINAPI  CH341ReadI2C(  // Read one byte of data from the I2C interface
        //  ULONG iIndex,  // Specify the serial number of the CH341 device
        //   UCHAR iDevice,  // The lower 7 bits specify the I2C device address
        //    UCHAR iAddr,  // Address of specified data unit
        //    PUCHAR oByte);  // Address of specified data unit


        //BOOL WINAPI  CH341WriteI2C(  // Write a byte of data to the I2C interface
        //   ULONG iIndex,  // Specify the serial number of the CH341 device
        //   UCHAR iDevice,  // The lower 7 bits specify the I2C device address
        //   UCHAR iAddr,  // Address of specified data unit
        //   UCHAR iByte);  // Byte data to be written
        [DllImport("./CH341DLLA64.DLL")]
        private static extern long CH341OpenDevice(long i);
        [DllImport("./CH341DLLA64.DLL")]
        private static extern long CH341ResetDevice(long i);
        [DllImport("./CH341DLLA64.DLL")]
        private static extern long CH341GetVersion(long i);
        [DllImport("./CH341DLLA64.DLL")]
        private static extern long CH341GetDrvVersion(long i);
        [DllImport("./CH341DLLA64.DLL")]
        private static extern int CH341GetVerIC(long i);
        [DllImport("./CH341DLLA64.DLL")]
        private static extern byte[] CH341GetDeviceName(long i);
        [DllImport("./CH341DLLA64.DLL")]
        private static extern int CH341ReadI2C(long i,int  addr, byte off,ref byte b);
        [DllImport("./CH341DLLA64.DLL")]
        private static extern int CH341WriteI2C(long i, byte addr, byte off, byte b);
  
        // CH341のドライバーとchipのversionを調べる
       unsafe public int getver(ref long dllVersion, ref long driverVersion, ref byte[] p, ref long icVersion)
        {
            Console.Error.WriteLine("CH341.getver");
            long h = CH341OpenDevice(0);
            Console.Error.WriteLine(h);
            // DLL verison
            dllVersion = CH341GetVersion(0);

            // Driver version
            driverVersion = CH341GetDrvVersion(0);

            // Device Name
            p = CH341GetDeviceName(0);

            // IC verison 0x10=CH341,0x20=CH341A,0x30=CH341A3
            icVersion = CH341GetVerIC(0);

            return (int)h;
        }
        // 指定されたlowr pageをI2経由で読む
        public int read_low(ref byte[] o, byte page)
        {
            byte i;
            byte data=0;

            Console.Error.WriteLine("SFP/QSFP/OSFP EEPROM A0h page dump via CH341");

            CH341OpenDevice(0);
            CH341ResetDevice(0);

            CH341WriteI2C(0, 0x50, 0x7f, page);    // page select

            for (i = 0x80; i < 0xff; i++)
            {               // 128 byteあけて後半だけに埋める
                int b = CH341ReadI2C(0, 0x50, i, ref data);
		        o[i] = data;
            }
            return i;
        }
        // CH341が使えるか確認する
        public int check()
        {
            Console.Error.WriteLine("check CH341 driver");

            // Open Device
            int h = (int)CH341OpenDevice((long)0);

            //	std::cerr << h;
            if (h < 0) { return 0; }
            else { return 1; }
        }
        //
        //初期のpageをI2C経由で読む
        public int read_page00(ref byte[] eprom)
        {
            byte i;
            byte data=0;

            Console.Error.WriteLine("SFP/QSFP/OSFP EEPROM A0h dump via CH341");

            // Open Device
            CH341OpenDevice(0);
            // Reset Device
            CH341ResetDevice(0);
            // I2C speed mode (0=20k / 1 = 100K / 2 =400k)
        //    CH341SetStream(0, 1);

            CH341WriteI2C(0, 0x50, 0x7f, 0x00);        // page select 00

            for (i = 0; i < 0xff; i++)
            {
                CH341ReadI2C(0, 0x50, i, ref data);
                eprom[i] = data;
                //	Sleep(1);		// delat 1 msec
            }
            return 0xff;
        }
        public short readw(byte addr,byte page, byte offset)
        {
            byte[] buf= new byte[2] ;
            CH341WriteI2C(0, 0x50, 0x7f, page);
            CH341ReadI2C(0, addr, offset, ref buf[0]);
            CH341ReadI2C(0, addr, ++offset, ref buf[1]);
            return BitConverter.ToInt16(buf, 0); ;
        }
    }
}
