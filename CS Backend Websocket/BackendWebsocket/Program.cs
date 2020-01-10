using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackendWebsocket
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri url = new Uri("ws://echo.websocket.org:80");
            VortexWebsocketClient client = new VortexWebsocketClient(url);
            client.Send("123");
            Thread.Sleep(1000);
            client.Send("This is another test");

            Thread.Sleep(2000);
            client.Dispose();

            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
