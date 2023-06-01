class Program
{
    static void Main(string[] args)
    {
        FileSender fileSender = new FileSender();
        string filePath = "C:\\path\\to\\file.txt";
        string ipAddress = "127.0.0.1";
        int port = 1234;
        fileSender.SendFile(filePath, ipAddress, port);
        FileReceiver fileReceiver = new FileReceiver();
        string savePath = "C:\\path\\to\\save\\received\\file.txt";
        fileReceiver.ReceiveFile(savePath, port);
        Console.ReadLine();
    }
}
