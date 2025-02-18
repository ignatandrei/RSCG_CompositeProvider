namespace CP_Test;

[TestClass]
public sealed class TestObtainFirstValue
{
    [TestMethod]
    public void TestDirectProp1()
    {
        DataValue_CP provider= new DataValue_CP(new FromTextFile(), new HardValues());
        Assert.AreEqual("FromTextFile", provider.Connection);
        Assert.AreEqual(0, provider.lastUsedInterface);
    }
    [TestMethod]
    public void TestDirectProp2()
    {
        DataValue_CP provider = new DataValue_CP(new HardValues(), new FromTextFile());
        Assert.AreEqual("HardValues", provider.Connection);
        Assert.AreEqual(0,provider.lastUsedInterface);
    }

    [DataTestMethod]
    [DataRow("Andrei",false)]
    [DataRow("Andrei", true)]
    public void TestDirectFunc1(string key, bool defaultValue)
    {
        DataValue_CP provider = new DataValue_CP(new FromTextFile(), new HardValues());
        Assert.AreEqual($"key {key} from text file {defaultValue}", provider.KeyFromValue(key,defaultValue));
        Assert.AreEqual(0, provider.lastUsedInterface);
    }
    [DataTestMethod]
    [DataRow("Andrei", false)]
    [DataRow("Andrei", false)]
    public void TestDirectFunc2(string key, bool defaultValue)
    {
        DataValue_CP provider = new DataValue_CP(new HardValues(), new FromTextFile());
        Assert.AreEqual($"key {key} from hardvalues {defaultValue}", provider.KeyFromValue(key,defaultValue));
        Assert.AreEqual(0, provider.lastUsedInterface);
    }
}
