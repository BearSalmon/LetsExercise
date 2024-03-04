using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPSend : MonoBehaviour
{
    public string ipAddress = "127.0.0.1"; // IP address of the receiver
    public int port = 1234; // Port number

    UdpClient client;

    void Start()
    {
        client = new UdpClient();
    }

    public void SendData(string data)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        client.Send(bytes, bytes.Length, ipAddress, port);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendData("Hello from Unity!");
        }
    }
}
