package zbc.vortex.vortex;

import java.util.ArrayList;

public class VortexController
{
    private int speed;
    private int direction;
    private ArrayList<IVortexControllerListener> listeners;

    public int getSpeed()
    {
        return speed;
    }

    public int getDirection()
    {
        return direction;
    }

    public VortexController()
    {
        speed = 0;
        direction = 0;
        listeners = new ArrayList<IVortexControllerListener>();
    }

    public void addListener(IVortexControllerListener listener)
    {
        listeners.add(listener);
    }

    public void removeListener(IVortexControllerListener listener)
    {
        listeners.remove(listener);
    }
    
    public void ChangeSpeed(int speed)
    {
        this.speed = speed;
        for (IVortexControllerListener l : listeners)
        {
            l.ChangeSpeed(speed);
        }
    }

    public void ChangeDirection(int direction)
    {
        this.direction = direction;
        for (IVortexControllerListener l : listeners)
        {
            l.ChangeDirection(direction);
        }
    }


}
