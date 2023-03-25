using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteNetLibTest.Core.Data
{
    public class Attendee : INetSerializable
    {
        public int AttendeeId { get; set; }
        public Person Person { get; set; }
        public Department Department { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            AttendeeId = reader.GetInt();
            Person = reader.Get<Person>();
            Department = reader.Get<Department>();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(AttendeeId);
            writer.Put(Person);
            writer.Put(Department);
        }
    }
}
