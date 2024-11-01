// TCP SERVER

using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

TcpListener server = null;

try
{
    var port = 8080;
    IPAddress localAddress = IPAddress.Parse("127.0.0.1");

    server = new TcpListener(localAddress, port);

    server.Start();

    

    while(true) 
    {
        Console.WriteLine("waiting for request...");
        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Connected!");

        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];

        int bytes = stream.Read(buffer, 0, buffer.Length);
        string httpRequest = Encoding.UTF8.GetString(buffer, 0, bytes);
        Console.WriteLine("Message recived: " + httpRequest);

        //Response
         string httpResponse = "HTTP/1.1 200 OK \r\nContent-Type: text/html; charset = UTF-8 \r\n\r\n <html><body><h1> Hi, I am a server. </h1></body></html>";
         byte[] responseBytes = Encoding.UTF8.GetBytes(httpResponse);
         stream.Write(responseBytes, 0, responseBytes.Length);


        client.Close();
    }


}
catch (Exception e)
{

    Console.WriteLine("Error: {0}", e);
}
finally 
{

    server.Stop();

}