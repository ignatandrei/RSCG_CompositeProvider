using RSCG_CompositeProvider_Common;

namespace CP_Test;

[CompositeProvider]
public interface IDataValue
{
    public string Connection { get; set; }
    public bool IsValid();

    public string KeyFromValue(string key, bool defaultValue);

    public Task<long> getNumberPersons();

    public IAsyncEnumerable<string> getPersons(long idDepartment);
}
