using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

public class FileSender
{
    private const int BufferSize = 8192;
    public void SendFile(string filePath, string ipAddress, int port)
    {
        // Read the file into a byte array
        byte[] fileBytes = File.ReadAllBytes(filePath);

        // Create a TCP client and connect to the server
        using (TcpClient client = new TcpClient())
        {
            client.Connect(ipAddress, port);
            // Get the network stream for sending data
            NetworkStream networkStream = client.GetStream();
            // Send the file size to the server
            byte[] fileSizeBytes = BitConverter.GetBytes(fileBytes.Length);
            networkStream.Write(fileSizeBytes, 0, fileSizeBytes.Length);
            // Send the file contents to the server in chunks
            int bytesRead;
            int totalBytesSent = 0;
            byte[] buffer = new byte[BufferSize];
            using (MemoryStream memoryStream = new MemoryStream(fileBytes))
            {
                while ((bytesRead = memoryStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    networkStream.Write(buffer, 0, bytesRead);
                    totalBytesSent += bytesRead;
                }
            }
            Console.WriteLine("File sent successfully. Total bytes sent: " + totalBytesSent);
        }
    }
}
