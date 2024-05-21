using System;
using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;
using UnityEngine.TestTools;

public class AsyncsTests
{
    [UnityTest]
    public IEnumerator TestDelay3000()
    {
        var before = Time.time;
        yield return PoTask.Delay(3000).AsCoroutine();
        Assert.IsTrue(Time.time - before > 3f);
    }

    [UnityTest]
    public IEnumerator TestPoTaskDelay3000()
    {
        var before = Time.time;
        yield return PoTaskDelay3000().AsCoroutine();
        Assert.IsTrue(Time.time - before > 3f);
    }
    
    [UnityTest]
    public IEnumerator TestYield()
    {
        var before = Time.frameCount;
        yield return PoTask.Yield().AsCoroutine();
        var frameCount = Time.frameCount; 
        var frame      = frameCount - before;
        Assert.IsTrue(frame == 1, $"frameCount - before = {frameCount} - {before} = {frame}");
    }
    
    [UnityTest]
    public IEnumerator TestPoTaskYield50Frame()
    {
        var before = Time.frameCount;
        yield return PoTaskYield50Frame().AsCoroutine();
        var frameCount = Time.frameCount; 
        var frame      = frameCount - before;
        Assert.IsTrue(frame == 50, $"frameCount - before = {frameCount} - {before} = {frame}");
    }
    
    [UnityTest]
    public IEnumerator TestPoTaskYield100Frame()
    {
        var before = Time.frameCount;
        yield return PoTaskYield100Frame().AsCoroutine();
        var frameCount = Time.frameCount; 
        var frame      = frameCount - before;
        Assert.IsTrue(frame == 100, $"frameCount - before = {frameCount} - {before} = {frame}");
    }

    [UnityTest]
    public IEnumerator TestPoTaskException()
    {
        Exception ex = null;
        yield return PoTaskException()
           .Catch(exception => ex = exception)
           .AsCoroutine();
        Assert.NotNull(ex);
    }

    public async PoTask PoTaskDelay3000()
    {
        await PoTask.Delay(3000);
    }

    public async PoTask PoTaskYield50Frame()
    {
        for (int i = 0; i < 50; i++) await PoTask.Yield();
    }

    public async PoTask PoTaskYield100Frame()
    {
        await PoTaskYield50Frame();
        await PoTaskYield50Frame();
    }

    public async PoTask PoTaskException()
    {
        await PoTask.Yield();
        await PoTaskInnerException();
    }

    private async PoTask PoTaskInnerException()
    {
        throw new Exception();
    }
}
