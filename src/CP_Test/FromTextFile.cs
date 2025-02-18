namespace CP_Test;
public class FromTextFile : IDataValue
{
    private readonly int waitAsync;

    public FromTextFile() : this(10)
    {

    }
    public FromTextFile(int waitAsync) 
    {
        this.waitAsync = waitAsync;
    }
    public string Connection { get; set; }= "FromTextFile";
    public bool IsValid()
    {
        return true;
    }
    public string KeyFromValue(string key, bool defaultValue)
    {
        return $"key {key} from text file {defaultValue}";
    }
    
    public async Task<long> getNumberPersons()
    {
        if (waitAsync > 0)
            await Task.Delay(waitAsync); 

         return await Task.FromResult(2L);
    }
    public IAsyncEnumerable<string> getPersons(long idDepartment)
    {
        return AsyncEnumerable.Empty<string>();
    }
}