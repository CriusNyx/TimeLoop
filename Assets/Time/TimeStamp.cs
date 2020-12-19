public class TimeStamp
{
    public readonly float minutes;
    public readonly float seconds;

    public TimeStamp(float minutes, float seconds)
    {
        this.minutes = minutes;
        this.seconds = seconds;
    }

    public float GetTime()
    {
        return minutes * 60f + seconds;
    }
}
