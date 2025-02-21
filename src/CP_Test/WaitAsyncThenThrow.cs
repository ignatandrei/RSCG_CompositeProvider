namespace CP_Test;

public class WaitAsyncThenThrow : IDataValue
{
    private readonly int timeSeconds;

    public WaitAsyncThenThrow(int timeSeconds)
    {
        this.timeSeconds = timeSeconds;
    }
    public string Connection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public async Task<long> getNumberPersons()
    {
        await Task.Delay(timeSeconds * 1000);
        throw new NotImplementedException();
    }

    public async IAsyncEnumerable<string> getPersons(long idDepartment)
    {
        await Task.Delay(timeSeconds*1000);
        int x = 1;x--;
        if (x != 0)
        {
            yield return "Person 1";
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    public string KeyFromValue(string key, bool defaultValue)
    {
        throw new NotImplementedException();
    }
}
