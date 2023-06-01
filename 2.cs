using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

public class FileReceiver
{
    private const int BufferSize = 8192; // 8KB buffer size, can be adjusted as needed

    public void ReceiveFile(string savePath, int port)
    {
        // Create a TCP listener and start listening for incoming connections
        using (TcpListener listener = new TcpListener(IPAddress.Any, port))
        {
            listener.Start();
            Console.WriteLine("Waiting for incoming connection...");

            // Accept a client connection
            using (TcpClient client = listener.AcceptTcpClient())
            {
                // Get the network stream for receiving data
                NetworkStream networkStream = client.GetStream();

                // Receive the file size from the sender
                byte[] fileSizeBytes = new byte[4];
                networkStream.Read(fileSizeBytes, 0, fileSizeBytes.Length);
                int fileSize = BitConverter.ToInt32(fileSizeBytes, 0);

                // Create a file stream for writing the received file
                using (FileStream fileStream = new FileStream(savePath, FileMode.Create))
                {
                    // Receive the file contents in chunks
                    int bytesRead;
                    int totalBytesReceived = 0;
                    byte[] buffer = new byte[BufferSize];
                    while ((bytesRead = networkStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                        totalBytesReceived += bytesRead;
                    }

                    Console.WriteLine("File received successfully. Total bytes received: " + totalBytesReceived);
                }
            }
        }
    }
}
