using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Threading;

namespace BackendWebsocket
{
    public class VortexWebsocketClient : IDisposable
    {
        private int timeout = 2000; // timeout in milliseconds
        private ClientWebSocket client; // websocket client
        private Uri url; // URL+port for websocket server
        private Thread wsListener; // thread for listening service
        private CancellationTokenSource cancelSource;
        private CancellationToken cancelToken;

        public VortexWebsocketClient(Uri url)
        {
            cancelSource = new CancellationTokenSource();
            cancelToken = cancelSource.Token;
            client = new ClientWebSocket();
            this.url = url;

            using (var cts = new CancellationTokenSource(timeout))
            {
                client.ConnectAsync(url, cts.Token).Wait(); // creating and connecting socket client
            }
            wsListener = new Thread(new ThreadStart(Receive)); // starting listening service
            wsListener.Start();
        }

        public void Send(String txt) // sending to server
        {
            byte[] byteData = Encoding.ASCII.GetBytes(txt);
            ArraySegment<byte> segment = new ArraySegment<byte>(byteData);
            CancellationToken ct = new CancellationToken();
            client.SendAsync(segment, WebSocketMessageType.Text, false, ct).Wait();
        }

        public void Receive() // receiving from server
        {
            byte[] buffer = new byte[65536];
            var segment = new ArraySegment<byte>(buffer);
            var rcvBytes = new byte[128];
            var rcvBuffer = new ArraySegment<byte>(rcvBytes);
            while (true)
            {
                try
                {
                    WebSocketReceiveResult rcvResult = client.ReceiveAsync(rcvBuffer, cancelToken).GetAwaiter().GetResult();
                    byte[] msgBytes = rcvBuffer.Skip(rcvBuffer.Offset).Take(rcvResult.Count).ToArray();
                    string rcvMsg = Encoding.UTF8.GetString(msgBytes);
                    Console.WriteLine("Received: {0}", rcvMsg);
                }
                catch (Exception e) // handling cancel message
                {
                    break;
                }
            }

        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    cancelSource.Cancel(); // cancling listening
                    wsListener.Join(); // closing listening thread
                    CancellationToken ct = new CancellationToken();
                    client.CloseAsync(WebSocketCloseStatus.NormalClosure, "", ct).Wait();
                    client.Dispose(); // cleaning up the socket client
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~VortexWebsocketClient()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
