namespace CP_Test;

class TimeWaiting 
{
    public TimeSpan diffNow
    {
        get
        {
            return DateTime.Now - start;
        }
    }
    private DateTime start;
    public TimeWaiting()
    {
        Restart();
    }
    public void Restart()
    {
        start = DateTime.Now;
    }
    
}   
