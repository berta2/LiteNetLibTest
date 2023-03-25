using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteNetLibTest.Core.Data
{
    public class Person : INetSerializable
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            PersonId = reader.GetInt();
            FirstName = reader.GetString();
            LastName = reader.GetString();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(PersonId);
            writer.Put(FirstName);
            writer.Put(LastName);
        }
    }
}
