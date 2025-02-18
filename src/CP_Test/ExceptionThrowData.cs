namespace CP_Test;

public class ExceptionThrowData : IDataValue
{
    public string Connection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Task<long> getNumberPersons()
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<string> getPersons(long idDepartment)
    {
        throw new NotImplementedException();
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
