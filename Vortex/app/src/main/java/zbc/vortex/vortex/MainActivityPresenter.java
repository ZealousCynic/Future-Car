package zbc.vortex.vortex;

public class MainActivityPresenter implements IVortexControllerListener
{
    VortexController vortexController;
    View view;

    public MainActivityPresenter(View view)
    {
        this.view = view;
        vortexController = new VortexController();
    }

    @Override
    public void ChangeSpeed(int speed)
    {
        view.SpeedChange(speed);
    }

    @Override
    public void ChangeDirection(int direction)
    {
        view.DirectionChange(direction);
    }

    public interface View
    {
        public void SpeedChange(int speed);
        public void DirectionChange(int direction);

    }



}
