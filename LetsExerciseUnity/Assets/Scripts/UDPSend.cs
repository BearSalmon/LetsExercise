using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPSend : MonoBehaviour
{
    public string ipAddress = "127.0.0.1"; // IP address of the receiver
    public int port_for_counter = 1234; // Port number
    public int port_for_poseset = 1235; // Port number

    UdpClient client;

    void Start()
    {
        client = new UdpClient();
    }

    public void SendDataForCounter(string data)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        client.Send(bytes, bytes.Length, ipAddress, port_for_counter);
    }
    public void SendDataForPoseset(string data)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        client.Send(bytes, bytes.Length, ipAddress, port_for_poseset);
    }

    void Update()
    {
        // test send
        //SendDataForPoseset("Hello from Unity!");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendDataForPoseset("Hello from Unity!");
        }
    }
}
