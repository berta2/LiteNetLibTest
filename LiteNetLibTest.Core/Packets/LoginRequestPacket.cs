using LiteNetLibTest.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteNetLibTest.Core.Packets
{
    public class LoginRequestPacket : IPacket
    {
        public string Token { get; set; } = String.Empty;
        public string Cmd { get; set; } = String.Empty;

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
