namespace CP_Test;

[TestClass]
public sealed class TestWaitAsync
{
    [DataTestMethod]
    [DataRow(1, 2000)]
    [DataRow(2, 1000)]
    [DataRow(3, 1000)]
    public async Task TestWaitAgain(int secondsToWait, int valueToReturn)
    {
        TimeWaiting tw = new ();
        DataValue_CP provider = new DataValue_CP(new WaitAsyncThenThrow(secondsToWait), new HardValues(valueToReturn));
        var nr = await provider.getNumberPersons();
        Assert.AreEqual(1, nr);
        Assert.AreEqual(1, provider.lastUsedInterface);
        var diff = tw.diffNow.TotalMilliseconds;
        Assert.IsTrue(diff >= secondsToWait*1000);
        tw.Restart();
        nr = await provider.getNumberPersons();
        Assert.AreEqual(1, nr);
        Assert.AreEqual(1, provider.lastUsedInterface);
        Assert.IsTrue(tw.diffNow.TotalMilliseconds > secondsToWait*1000);


    }
    [DataTestMethod]
    [DataRow(2, 1000)]
    [DataRow(3, 2000)]
    public async Task TestUseLastSoNoWait(int secondsToWait, int valueToReturn)
    {
        TimeWaiting tw = new ();
        DataValue_CP provider = new DataValue_CP(new WaitAsyncThenThrow(secondsToWait), new HardValues(valueToReturn));
        provider.UseFirstTheLastOneThatWorks = true;
        var nr = await provider.getNumberPersons();
        Assert.AreEqual(1, nr);
        Assert.AreEqual(1, provider.lastUsedInterface);
        Assert.IsTrue(tw.diffNow.TotalMilliseconds >= secondsToWait * 1000);
        tw.Restart();
        nr = await provider.getNumberPersons();
        Assert.AreEqual(1, nr);
        Assert.AreEqual(1, provider.lastUsedInterface);
        Assert.IsTrue(tw.diffNow.TotalMilliseconds < secondsToWait * 1000);


    }

}
