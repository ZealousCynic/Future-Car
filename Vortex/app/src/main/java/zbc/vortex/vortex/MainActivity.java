package zbc.vortex.vortex;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;

import java.net.URI;
import java.net.URISyntaxException;

public class MainActivity extends AppCompatActivity implements MainActivityPresenter.View, View.OnClickListener
{
    MainActivityPresenter presenter;
    Button buttonForward, buttonStop, buttonReverse, buttonLeft, buttonStraight, buttonRight;
    TextView textSpeed, textDirection, textOutput;
    VortexController vortexController;
    SocketClient socket;
    URI endpoint;


    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        presenter = new MainActivityPresenter(this);
        vortexController = new VortexController();
        try
        {
            endpoint = new URI( "ws://192.168.1.124:81" );
        } catch (URISyntaxException e)
        {
            e.printStackTrace();
        }

        SocketClient client = new SocketClient(endpoint);
        vortexController.addListener(client);
        vortexController.addListener(presenter);



        buttonForward = (Button) findViewById(R.id.buttonForward);
        buttonStop = (Button) findViewById(R.id.buttonStop);
        buttonReverse = (Button) findViewById(R.id.buttonReverse);
        buttonLeft = (Button) findViewById(R.id.buttonLeft);
        buttonStraight = (Button) findViewById(R.id.buttonStraight);
        buttonRight = (Button) findViewById(R.id.buttonRight);
        textSpeed = (TextView) findViewById(R.id.textSpeed);
        textDirection = (TextView) findViewById(R.id.textDirection);


    }

    @Override
    public void SpeedChange(int speed)
    {
        if(speed > 0)
        {
            textSpeed.setText("Forward");
        }
        else if(speed < 0)
        {
            textSpeed.setText("Reverse");
        }
        else
        {
            textSpeed.setText("Stopped");
        }
    }

    @Override
    public void DirectionChange(int direction)
    {
        if(direction > 0)
        {
            textDirection.setText("Right");
        }
        else if(direction < 0)
        {
            textDirection.setText("Left");
        }
        else
        {
            textDirection.setText("Straight");
        }
    }

    @Override
    public void onClick(View v)
    {
        switch(v.getId())
        {
            case R.id.buttonForward:
                vortexController.ChangeSpeed(1);
                break;
            case R.id.buttonReverse:
                vortexController.ChangeSpeed(-1);
                break;
            case R.id.buttonStop:
                vortexController.ChangeSpeed(0);
                break;
            case R.id.buttonRight:
                vortexController.ChangeDirection(1);
                break;
            case R.id.buttonStraight:
                vortexController.ChangeDirection(0);
                break;
            case R.id.buttonLeft:
                vortexController.ChangeDirection(-1);
                break;
        }

    }
}
