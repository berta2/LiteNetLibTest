using LiteNetLib;
using LiteNetLib.Utils;
using LiteNetLibTest.Core.Data;
using LiteNetLibTest.Core.Packets;

NetPacketProcessor _netPacketProcessor = new NetPacketProcessor();

_netPacketProcessor.RegisterNestedType<Employee>(() => new Employee());
_netPacketProcessor.RegisterNestedType<Attendee>(() => new Attendee());
_netPacketProcessor.RegisterNestedType<Department>(() => new Department());
_netPacketProcessor.RegisterNestedType<Person>(() => new Person());

_netPacketProcessor.SubscribeReusable<LoginRequestPacket, NetPeer>(OnLoginRequestPacketReceived);

EventBasedNetListener listener = new EventBasedNetListener();
NetManager server = new NetManager(listener);

listener.ConnectionRequestEvent += Listener_ConnectionRequestEvent;
listener.NetworkReceiveEvent += Listener_NetworkReceiveEvent;
listener.PeerConnectedEvent += Listener_PeerConnectedEvent;



server.Start(9050);

while (!Console.KeyAvailable)
{
    server.PollEvents();
    Thread.Sleep(15);
}
server.Stop();


void OnLoginRequestPacketReceived(LoginRequestPacket packet, NetPeer peer)
{
    if (packet.Username == "user123" && packet.Password == "pass123")
    {
        var emp = new Employee();
        var person = new Person { FirstName = "Sarah", LastName = "Collin" };
        //var dep = new Department { Name = "Office", Number = "100" };

        emp.Person = person;
        //emp.Department = dep;

        var bytes = _netPacketProcessor.Write(new LoginOkResponsePacket { Token = "XXX9XXX", Employee = emp });
        peer.Send(bytes, DeliveryMethod.ReliableOrdered);
    }
    Console.WriteLine("Login: " + packet.Username);
}

void Listener_NetworkReceiveEvent(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
{
    _netPacketProcessor.ReadAllPackets(reader, peer);
}

void Listener_PeerConnectedEvent(NetPeer peer)
{
    Console.WriteLine(peer.EndPoint + " connected!");
}

void Listener_ConnectionRequestEvent(ConnectionRequest request)
{
    if (server.ConnectedPeersCount < 10)
        request.AcceptIfKey("SomeConnectionKey");
    else
        request.Reject();
}