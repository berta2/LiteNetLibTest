using LiteNetLibTest.Core.Data;
using LiteNetLibTest.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteNetLibTest.Core.Packets
{
    public class LoginOkResponsePacket : IPacket
    {
        public string? Token { get; set; }
        public string Cmd { get; set; } = String.Empty;

        public string? Message { get; set; }
        public Employee Employee { get; set; }
    }
}
