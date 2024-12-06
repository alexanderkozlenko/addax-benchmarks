using System.Reflection;
using Addax.Benchmarks.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Addax.Benchmarks.BenchmarkEngines.UnitTests;

[TestClass]
public sealed class BenchmarkEnginesTests
{
    private static readonly BenchmarkEngine[] s_engines = CreateEngines();

    [TestMethod]
    [DynamicData(nameof(EnginesForString))]
    public void HandleString(IBenchmarkEngine<Record<string>> provider)
    {
        var recordsW = new Record<string>[]
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

        var recordsR = new List<Record<string>>();

        provider.ReadRecords(stream, recordsR);

        Assert.AreEqual(1, recordsR.Count);
        Assert.AreEqual("________________", recordsR[0].Field0);
        Assert.AreEqual("________________", recordsR[0].Field1);
        Assert.AreEqual("____\"______\"____", recordsR[0].Field2);
        Assert.AreEqual("____\"______\"____", recordsR[0].Field3);
    }

    [TestMethod]
    [DynamicData(nameof(EnginesForDouble))]
    public void HandleDouble(IBenchmarkEngine<Record<double>> provider)
    {
        var recordsW = new Record<double>[]
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

        var recordsR = new List<Record<double>>();

        provider.ReadRecords(stream, recordsR);

        Assert.AreEqual(1, recordsR.Count);
        Assert.AreEqual(Math.E, recordsR[0].Field0);
        Assert.AreEqual(Math.E, recordsR[0].Field1);
        Assert.AreEqual(Math.PI, recordsR[0].Field2);
        Assert.AreEqual(Math.PI, recordsR[0].Field3);
    }

    [TestMethod]
    [DynamicData(nameof(EnginesForDateTime))]
    public void HandleDateTime(IBenchmarkEngine<Record<DateTime>> provider)
    {
        var recordsW = new Record<DateTime>[]
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

        var recordsR = new List<Record<DateTime>>();

        provider.ReadRecords(stream, recordsR);

        Assert.AreEqual(1, recordsR.Count);
        Assert.AreEqual(new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc), recordsR[0].Field0);
        Assert.AreEqual(new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc), recordsR[0].Field1);
        Assert.AreEqual(new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc), recordsR[0].Field2);
        Assert.AreEqual(new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc), recordsR[0].Field3);
    }

    public static IEnumerable<object[]> EnginesForString
    {
        get
        {
            return s_engines.OfType<IBenchmarkEngine<Record<string>>>().Select(static x => new[] { x });
        }
    }

    public static IEnumerable<object[]> EnginesForDouble
    {
        get
        {
            return s_engines.OfType<IBenchmarkEngine<Record<double>>>().Select(static x => new[] { x });
        }
    }

    public static IEnumerable<object[]> EnginesForDateTime
    {
        get
        {
            return s_engines.OfType<IBenchmarkEngine<Record<DateTime>>>().Select(static x => new[] { x });
        }
    }

    private static BenchmarkEngine[] CreateEngines()
    {
        return Assembly.Load("Addax.Benchmarks.BenchmarkEngines").GetExportedTypes()
            .Where(static x => x.BaseType == typeof(BenchmarkEngine))
            .Select(static x => (BenchmarkEngine)Activator.CreateInstance(x)!)
            .OrderBy(static x => x.ToString())
            .ToArray();
    }
}
