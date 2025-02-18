namespace CP_Test;
class HardValues : IDataValue
{
    private readonly int waitAsync;

    public HardValues():this(10)
    {

    }
    public HardValues(int waitAsync) 
    {
        this.waitAsync = waitAsync;
    }
    public string Connection { get; set; } = "HardValues";
    public bool IsValid()
    {
        return true;
    }
    public string KeyFromValue(string key, bool defaultValue)
    {
        return $"key {key} from hardvalues {defaultValue}";
    }
    public async Task<long> getNumberPersons()
    {
        if (waitAsync > 0)
            await Task.Delay(waitAsync);
        
         return await Task.FromResult(1L);
    }
    public async IAsyncEnumerable<string> getPersons(long idDepartment)
    {
        await Task.Yield();
        yield return idDepartment + "andrei1";
        yield return idDepartment + "andrei2";
    }
}