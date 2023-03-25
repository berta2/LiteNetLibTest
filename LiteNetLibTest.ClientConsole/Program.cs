using LiteNetLib;
using LiteNetLib.Utils;
using LiteNetLibTest.Core.Data;
using LiteNetLibTest.Core.Interfaces;
using LiteNetLibTest.Core.Packets;

NetPacketProcessor _netPacketProcessor = new NetPacketProcessor();
EventBasedNetListener listener = new EventBasedNetListener();
NetManager client;

NetPeer server = null;

_netPacketProcessor.RegisterNestedType<Employee>(() => new Employee());
_netPacketProcessor.RegisterNestedType<Attendee>(() => new Attendee());
_netPacketProcessor.RegisterNestedType<Department>(() => new Department());
_netPacketProcessor.RegisterNestedType<Person>(() => new Person());

_netPacketProcessor.SubscribeReusable<EmployeePacket, NetPeer>(OnEmployeePacketReceived);
_netPacketProcessor.SubscribeReusable<LoginOkResponsePacket, NetPeer>(OnLoginOkResponsePacketReceived);

listener.PeerDisconnectedEvent += Listener_PeerDisconnectedEvent;
listener.PeerConnectedEvent += Listener_PeerConnectedEvent;
listener.NetworkReceiveEvent += Listener_NetworkReceiveEvent;

client = new NetManager(listener);

//Thread.Sleep(1000);
client.Start();

client.Connect("localhost", 9050, "SomeConnectionKey");

while (true)
{
    client.PollEvents();
    Thread.Sleep(15);
}

void Send(IPacket packet)
{
    byte[] bytes = new byte[0];
    if (packet.GetType() == typeof(LoginRequestPacket))
    {
        bytes = _netPacketProcessor.Write((LoginRequestPacket)packet);

        server?.Send(bytes, DeliveryMethod.ReliableOrdered);
    }
}

void Login(string username, string password)
{
    var loginRequest = new LoginRequestPacket { Username = username, Password = password };
    Send(loginRequest);
}

#region PacketProcessor

void OnLoginOkResponsePacketReceived(LoginOkResponsePacket packet, NetPeer peer)
{

    Console.WriteLine(packet.Token + " " + packet.Employee.Person?.FirstName + " " + packet.Employee.Department?.Name);
    Console.WriteLine("try again...");
    Console.ReadLine();
    Login("user123", "pass123");
}

void OnEmployeePacketReceived(EmployeePacket packet, NetPeer peer)
{
    Console.WriteLine(packet.Cmd + ": Employee:" + packet.Employee.Person.FirstName);
}

#endregion

#region Listener Events

void Listener_PeerConnectedEvent(NetPeer peer)
{
    server = peer;
    Console.WriteLine("connected");

    Console.WriteLine("press any key for login...");
    Console.ReadKey();
    Login("user123", "pass123");

}

void Listener_PeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
{
    server = null;
    Console.WriteLine("disconnected");
}

void Listener_NetworkReceiveEvent(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
{
    _netPacketProcessor.ReadAllPackets(reader, peer);
}

#endregion