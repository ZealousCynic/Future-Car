package zbc.vortex.vortex;

import java.net.URI;

import tech.gusavila92.websocketclient.WebSocketClient;


public class SocketClient implements IVortexControllerListener
{
    private URI endpoint;
    private WebSocketClient client;
    int timeout = 5000;

    public SocketClient(URI endpoint)
    {
        this.endpoint = endpoint;
        client = new WebSocketClient (endpoint)
        {
            @Override
            public void onOpen()
            {
            }

            @Override
            public void onTextReceived(String s)
            {
                System.out.println(s);
            }
            @Override
            public void onBinaryReceived(byte[] data)
            {
            }
            @Override
            public void onPingReceived(byte[] data)
            {
            }
            @Override
            public void onPongReceived(byte[] data) {
            }
            @Override
            public void onException(Exception e)
            {
            }
            @Override
            public void onCloseReceived()
            {
            }
        };

        client.setConnectTimeout(10000);
        client.setReadTimeout(60000);
        client.enableAutomaticReconnection(5000);
        client.connect();
    }

    @Override
    public void ChangeSpeed(int speed)
    {
        String command;
        if(speed > 0)
        {
            command = "21";
        }
        else if(speed < 0)
        {
            command = "22";
        }
        else
        {
            command = "20";
        }
        client.send(command);
    }

    @Override
    public void ChangeDirection(int direction)
    {
        String command;
        if(direction > 0)
        {
            command = "32";
        }
        else if(direction < 0)
        {
            command = "31";
        }
        else
        {
            command = "30";
        }
        client.send(command.getBytes());
    }
}
