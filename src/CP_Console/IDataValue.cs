using RSCG_CompositeProvider_Common;

namespace CP_Console;
[CompositeProvider]
public interface IDataValue
{
    public string Connection { get; set; }
    public bool IsValid();

    public string KeyFromValue(string key, bool defaultValue);

    public Task<long> getNumberPersons();
}
