using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteNetLibTest.Core.Data;
using LiteNetLibTest.Core.Interfaces;

namespace LiteNetLibTest.Core.Packets
{
    public class EmployeePacket : IPacket
    {
        public string Token { get; set; }
        public string Cmd { get; set; }
        public Employee Employee { get; set; }

    }
}
