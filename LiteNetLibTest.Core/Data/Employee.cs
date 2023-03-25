using LiteNetLib.Utils;

namespace LiteNetLibTest.Core.Data
{
    public class Employee : INetSerializable
    {
        public int EmployeeId { get; set; }
        public Person? Person { get; set; }
        public Department? Department { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            EmployeeId = reader.GetInt();
            Person = reader.Get<Person>();
            Department = reader.Get<Department>();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(EmployeeId);
            if(Person != null)
                writer.Put(Person);
            if(Department != null)
                writer.Put(Department);
        }
    }
}
