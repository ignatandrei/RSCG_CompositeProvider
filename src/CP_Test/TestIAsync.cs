namespace CP_Test;
[TestClass]
public class TestIAsync
{
    [TestMethod]
    public async Task TestAsyncEmpty()
    {
        DataValue_CP provider = new DataValue_CP(new FromTextFile(), new HardValues());
        int nr = 0, nrDep = 10;
        await foreach(var item in provider.getPersons(nrDep))
        {
            //from text file gives empty list
            nr++;
        }
        Assert.AreEqual(0, nr);
        Assert.IsNotNull(provider.lastUsedInterface);
        Assert.AreEqual(0, provider.lastUsedInterface.Value);
    }

    [TestMethod]
    public async Task TestAsync_Empty_Error_Value()
    {
        DataValue_CP provider = new DataValue_CP(new ExceptionThrowData(), new FromTextFile(), new HardValues());
        int nr = 0, nrDep = 10; 
        await foreach (var item in provider.getPersons(nrDep))
        {
            //from text file gives empty list
            nr++;
        }
        Assert.AreEqual(0, nr);
        Assert.IsNotNull(provider.lastUsedInterface);
        Assert.AreEqual(1, provider.lastUsedInterface.Value);
    }
    [TestMethod]
    public async Task TestAsync_Error_Empty_Value()
    {
        DataValue_CP provider = new DataValue_CP(new ExceptionThrowData(),  new HardValues(), new FromTextFile());
        int nr = 0, nrDep = 10;
        await foreach (var item in provider.getPersons(nrDep))
        {
            nr++;
            Assert.AreEqual(nrDep + "andrei" + nr, item);
        }
        Assert.AreEqual(2, nr);
        Assert.IsNotNull(provider.lastUsedInterface);
        Assert.AreEqual(1, provider.lastUsedInterface.Value);
    }
    [TestMethod]
    public async Task TestAsync_Value_Value()
    {
        DataValue_CP provider = new DataValue_CP(new HardValues(), new HardValues());
        int nr = 0, nrDep = 10;
        await foreach (var item in provider.getPersons(nrDep))
        {
            nr++;
            Assert.AreEqual(nrDep + "andrei" + nr, item);
        }
        Assert.AreEqual(2, nr);
        Assert.IsNotNull(provider.lastUsedInterface);
        Assert.AreEqual(0, provider.lastUsedInterface.Value);
    }
}
