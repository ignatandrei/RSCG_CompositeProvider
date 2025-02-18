namespace CP_Test;

[TestClass]
public sealed class TestObtainSecondAsync
{
    [DataTestMethod]
    [DataRow(1000, 2000)]
    [DataRow(2000, 1000)]
    public async Task TestDirectFunc1(int first, int second)
    {
        DataValue_CP provider = new DataValue_CP(new ExceptionThrowData(), new FromTextFile(first), new HardValues(second));
        var nr = await provider.getNumberPersons();
        Assert.AreEqual(2, nr);
        Assert.AreEqual(1, provider.lastUsedInterface);
    }
    [DataTestMethod]
    [DataRow(1000, 2000)]
    [DataRow(2000, 1000)]
    public async Task TestDirectFunc2(int first, int second)
    {
        DataValue_CP provider = new DataValue_CP(new ExceptionThrowData(), new HardValues(second), new FromTextFile(first));
        var nr = await provider.getNumberPersons();
        Assert.AreEqual(1, nr);
        Assert.AreEqual(1, provider.lastUsedInterface);
    }

}
