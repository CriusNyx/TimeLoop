public class InputBuffer
{
    public bool jump = false;
    public bool grappelHook = false;
    public bool airDashPressed = false;
    public bool doAirDash;
    public float airDashPressedTime = 0;

    public InputBuffer(InputBuffer previousFrame)
    {
        if (previousFrame != null)
        {
            if (previousFrame.airDashPressed)
            {
                this.airDashPressedTime = previousFrame.airDashPressedTime + 1;
            }
        }
    }
}
