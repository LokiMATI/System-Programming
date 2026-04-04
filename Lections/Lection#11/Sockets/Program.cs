using System.Net.Sockets;

var tcpSocket = new Socket(
    SocketType.Stream, 
    ProtocolType.Tcp);

var udpSocket = new Socket(
    SocketType.Dgram,
    ProtocolType.Udp);
