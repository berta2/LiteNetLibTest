using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteNetLibTest.Core.Data
{
    public class Department : INetSerializable
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            DepartmentId = reader.GetInt();
            Name = reader.GetString();
            Number = reader.GetString();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(DepartmentId);
            writer.Put(Name);
            writer.Put(Number);
        }
    }
}
