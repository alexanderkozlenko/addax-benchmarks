using System.Reflection;
using Addax.Benchmarks.Abstractions;
using Addax.Benchmarks.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Addax.Benchmarks.BenchmarkEngines.UnitTests;

[TestClass]
public sealed class BenchmarkEnginesTests
{
    private static readonly BenchmarkEngine[] s_engines = DiscoverEngines();

    [TestMethod]
    [DynamicData(nameof(EnginesS))]
    public void HandleRecordsS(IBenchmarkEngine<RecordS> provider)
    {
        var recordsW = new RecordS[]
        {
            new()
            {
                Field0 = "________________",
                Field1 = "________________",
                Field2 = "____\"______\"____",
                Field3 = "____\"______\"____",
            }
        };

        using var stream = new MemoryStream();

        provider.WriteRecords(stream, recordsW);

        stream.Seek(0, SeekOrigin.Begin);

        var recordsR = new List<RecordS>();

        provider.ReadRecords(stream, recordsR);

        Assert.AreEqual(1, recordsR.Count);
        Assert.AreEqual("________________", recordsR[0].Field0);
        Assert.AreEqual("________________", recordsR[0].Field1);
        Assert.AreEqual("____\"______\"____", recordsR[0].Field2);
        Assert.AreEqual("____\"______\"____", recordsR[0].Field3);
    }

    [TestMethod]
    [DynamicData(nameof(EnginesN))]
    public void HandleRecordsN(IBenchmarkEngine<RecordN> provider)
    {
        var recordsW = new RecordN[]
        {
            new()
            {
                Field0 = Math.E,
                Field1 = Math.E,
                Field2 = Math.PI,
                Field3 = Math.PI,
            }
        };

        using var stream = new MemoryStream();

        provider.WriteRecords(stream, recordsW);

        stream.Seek(0, SeekOrigin.Begin);

        var recordsR = new List<RecordN>();

        provider.ReadRecords(stream, recordsR);

        Assert.AreEqual(1, recordsR.Count);
        Assert.AreEqual(Math.E, recordsR[0].Field0);
        Assert.AreEqual(Math.E, recordsR[0].Field1);
        Assert.AreEqual(Math.PI, recordsR[0].Field2);
        Assert.AreEqual(Math.PI, recordsR[0].Field3);
    }

    [TestMethod]
    [DynamicData(nameof(EnginesD))]
    public void HandleRecordsD(IBenchmarkEngine<RecordD> provider)
    {
        var recordsW = new RecordD[]
        {
            new()
            {
                Field0 = new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc),
                Field1 = new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc),
                Field2 = new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc),
                Field3 = new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc),
            }
        };

        using var stream = new MemoryStream();

        provider.WriteRecords(stream, recordsW);

        stream.Seek(0, SeekOrigin.Begin);

        var recordsR = new List<RecordD>();

        provider.ReadRecords(stream, recordsR);

        Assert.AreEqual(1, recordsR.Count);
        Assert.AreEqual(new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc), recordsR[0].Field0);
        Assert.AreEqual(new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc), recordsR[0].Field1);
        Assert.AreEqual(new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc), recordsR[0].Field2);
        Assert.AreEqual(new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc), recordsR[0].Field3);
    }

    [TestMethod]
    [DynamicData(nameof(EnginesM))]
    public void HandleRecordsD(IBenchmarkEngine<RecordM> provider)
    {
        var recordsW = new RecordM[]
        {
            new()
            {
                Field0 = "____\"______\"____",
                Field1 = true,
                Field2 = Math.PI,
                Field3 = new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc),
            }
        };

        using var stream = new MemoryStream();

        provider.WriteRecords(stream, recordsW);

        stream.Seek(0, SeekOrigin.Begin);

        var recordsR = new List<RecordM>();

        provider.ReadRecords(stream, recordsR);

        Assert.AreEqual(1, recordsR.Count);
        Assert.AreEqual("____\"______\"____", recordsR[0].Field0);
        Assert.AreEqual(true, recordsR[0].Field1);
        Assert.AreEqual(Math.PI, recordsR[0].Field2);
        Assert.AreEqual(new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc), recordsR[0].Field3);
    }

    private static BenchmarkEngine[] DiscoverEngines()
    {
        return Assembly
            .Load("Addax.Benchmarks.BenchmarkEngines")
            .GetTypes()
            .Where(static x => x.BaseType == typeof(BenchmarkEngine))
            .Select(static x => (BenchmarkEngine)Activator.CreateInstance(x)!)
            .ToArray();
    }

    public static IEnumerable<object[]> EnginesS
    {
        get
        {
            return s_engines.OfType<IBenchmarkEngine<RecordS>>().Select(static x => new[] { x });
        }
    }

    public static IEnumerable<object[]> EnginesN
    {
        get
        {
            return s_engines.OfType<IBenchmarkEngine<RecordN>>().Select(static x => new[] { x });
        }
    }

    public static IEnumerable<object[]> EnginesD
    {
        get
        {
            return s_engines.OfType<IBenchmarkEngine<RecordD>>().Select(static x => new[] { x });
        }
    }

    public static IEnumerable<object[]> EnginesM
    {
        get
        {
            return s_engines.OfType<IBenchmarkEngine<RecordM>>().Select(static x => new[] { x });
        }
    }
}
