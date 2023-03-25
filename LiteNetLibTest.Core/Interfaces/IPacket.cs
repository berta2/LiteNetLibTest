using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteNetLibTest.Core.Interfaces
{
    public interface IPacket
    {
        string Token { get; set; }
        string Cmd { get; set; }
    }
}
