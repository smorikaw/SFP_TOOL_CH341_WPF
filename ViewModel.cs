using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SFP_TOOL_CH341
{
    internal class ViewModel
    {

        //DataBindig用変数
        public ReactiveCollection<string> COMPorts { get; set; }
              = new ReactiveCollection<string>();

        public void GetCOMPorts()
        {
            COMPorts.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (var port in ports)
            {
                COMPorts.Add(port);
            }
        }
    }
}
