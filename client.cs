using VncSharp;
using System.Drawing;
class Program
{
    static void Main(string[] args)
    {
        VncServer vncServer = new VncServer();
        vncServer.StartServer();
        Console.ReadLine();
    }
}
