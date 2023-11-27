using System.Reflection;
using Addax.Benchmarks.Abstractions;
using Addax.Benchmarks.Contracts;
using Addax.Formats.Tabular;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Addax.Benchmarks.BenchmarkEngines.UnitTests;

[TestClass]
public sealed class BenchmarkEnginesTests
{
    private static readonly BenchmarkEngine[] s_engines = DiscoverEngines();
    private static readonly TabularDialect s_dialect = new("\r\n", ',', '"');
    private static readonly TabularOptions s_options = new() { LeaveOpen = true };

    [TestMethod]
    [DynamicData(nameof(EnginesS))]
    public void ReadRecordsS(IBenchmarkEngine<RecordS> provider)
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

        var recordsR = new List<RecordS>();

        using var stream = new MemoryStream();
        using var writer = new TabularWriter(stream, s_dialect, s_options);

        foreach (var record in recordsW)
        {
            writer.WriteString(record.Field0);
            writer.WriteString(record.Field1);
            writer.WriteString(record.Field2);
            writer.WriteString(record.Field3);
            writer.FinishRecord();
        }

        writer.Flush();
        stream.Seek(0, SeekOrigin.Begin);
        provider.ReadRecords(stream, recordsR);

        Assert.AreEqual(1, recordsR.Count);
        Assert.AreEqual("________________", recordsR[0].Field0);
        Assert.AreEqual("________________", recordsR[0].Field1);
        Assert.AreEqual("____\"______\"____", recordsR[0].Field2);
        Assert.AreEqual("____\"______\"____", recordsR[0].Field3);
    }

    [TestMethod]
    [DynamicData(nameof(EnginesN))]
    public void ReadRecordsN(IBenchmarkEngine<RecordN> provider)
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

        var recordsR = new List<RecordN>();

        using var stream = new MemoryStream();
        using var writer = new TabularWriter(stream, s_dialect, s_options);

        foreach (var record in recordsW)
        {
            writer.WriteDouble(record.Field0);
            writer.WriteDouble(record.Field1);
            writer.WriteDouble(record.Field2);
            writer.WriteDouble(record.Field3);
            writer.FinishRecord();
        }

        writer.Flush();
        stream.Seek(0, SeekOrigin.Begin);
        provider.ReadRecords(stream, recordsR);

        Assert.AreEqual(1, recordsR.Count);
        Assert.AreEqual(Math.E, recordsR[0].Field0);
        Assert.AreEqual(Math.E, recordsR[0].Field1);
        Assert.AreEqual(Math.PI, recordsR[0].Field2);
        Assert.AreEqual(Math.PI, recordsR[0].Field3);
    }

    [TestMethod]
    [DynamicData(nameof(EnginesD))]
    public void ReadRecordsD(IBenchmarkEngine<RecordD> provider)
    {
        var recordsW = new RecordD[]
        {
            new()
            {
                Field0 = new(1969, 07, 24, 16, 50, 35, TimeSpan.Zero),
                Field1 = new(1969, 07, 24, 16, 50, 35, TimeSpan.Zero),
                Field2 = new(1969, 07, 24, 06, 50, 35, TimeSpan.FromHours(-10)),
                Field3 = new(1969, 07, 24, 06, 50, 35, TimeSpan.FromHours(-10)),
            }
        };

        var recordsR = new List<RecordD>();

        using var stream = new MemoryStream();
        using var writer = new TabularWriter(stream, s_dialect, s_options);

        foreach (var record in recordsW)
        {
            writer.WriteDateTimeOffset(record.Field0);
            writer.WriteDateTimeOffset(record.Field1);
            writer.WriteDateTimeOffset(record.Field2);
            writer.WriteDateTimeOffset(record.Field3);
            writer.FinishRecord();
        }

        writer.Flush();
        stream.Seek(0, SeekOrigin.Begin);
        provider.ReadRecords(stream, recordsR);

        Assert.AreEqual(1, recordsR.Count);
        Assert.AreEqual(new(1969, 07, 24, 16, 50, 35, TimeSpan.Zero), recordsR[0].Field0);
        Assert.AreEqual(new(1969, 07, 24, 16, 50, 35, TimeSpan.Zero), recordsR[0].Field1);
        Assert.AreEqual(new(1969, 07, 24, 06, 50, 35, TimeSpan.FromHours(-10)), recordsR[0].Field2);
        Assert.AreEqual(new(1969, 07, 24, 06, 50, 35, TimeSpan.FromHours(-10)), recordsR[0].Field3);
    }

    [TestMethod]
    [DynamicData(nameof(EnginesM))]
    public void ReadRecordsM(IBenchmarkEngine<RecordM> provider)
    {
        var recordsW = new RecordM[]
        {
            new()
            {
                Field0 = "____\"______\"____",
                Field1 = true,
                Field2 = Math.PI,
                Field3 = new(1969, 07, 24, 06, 50, 35, TimeSpan.FromHours(-10)),
            }
        };

        var recordsR = new List<RecordM>();

        using var stream = new MemoryStream();
        using var writer = new TabularWriter(stream, s_dialect, s_options);

        foreach (var record in recordsW)
        {
            writer.WriteString(record.Field0);
            writer.WriteBoolean(record.Field1);
            writer.WriteDouble(record.Field2);
            writer.WriteDateTimeOffset(record.Field3);
            writer.FinishRecord();
        }

        writer.Flush();
        stream.Seek(0, SeekOrigin.Begin);
        provider.ReadRecords(stream, recordsR);

        Assert.AreEqual(1, recordsR.Count);
        Assert.AreEqual("____\"______\"____", recordsR[0].Field0);
        Assert.AreEqual(true, recordsR[0].Field1);
        Assert.AreEqual(Math.PI, recordsR[0].Field2);
        Assert.AreEqual(new(1969, 07, 24, 06, 50, 35, TimeSpan.FromHours(-10)), recordsR[0].Field3);
    }

    [TestMethod]
    [DynamicData(nameof(EnginesS))]
    public void WriteRecordsS(IBenchmarkEngine<RecordS> provider)
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

        var recordsR = new List<RecordS>();

        using var stream = new MemoryStream();

        provider.WriteRecords(stream, recordsW);
        stream.Seek(0, SeekOrigin.Begin);

        using var reader = new TabularReader(stream, s_dialect, s_options);

        while (reader.TryPickRecord())
        {
            reader.TryReadField();

            if (reader.CurrentPositionType == TabularPositionType.EndOfStream)
            {
                continue;
            }

            reader.TryGetString(out var field0);
            reader.TryReadField();
            reader.TryGetString(out var field1);
            reader.TryReadField();
            reader.TryGetString(out var field2);
            reader.TryReadField();
            reader.TryGetString(out var field3);

            var record = new RecordS
            {
                Field0 = field0,
                Field1 = field1,
                Field2 = field2,
                Field3 = field3,
            };

            recordsR.Add(record);
        }

        Assert.AreEqual(1, recordsR.Count);
        Assert.AreEqual("________________", recordsR[0].Field0);
        Assert.AreEqual("________________", recordsR[0].Field1);
        Assert.AreEqual("____\"______\"____", recordsR[0].Field2);
        Assert.AreEqual("____\"______\"____", recordsR[0].Field3);
    }

    [TestMethod]
    [DynamicData(nameof(EnginesS))]
    public void WriteRecordsN(IBenchmarkEngine<RecordN> provider)
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

        var recordsR = new List<RecordN>();

        using var stream = new MemoryStream();

        provider.WriteRecords(stream, recordsW);
        stream.Seek(0, SeekOrigin.Begin);

        using var reader = new TabularReader(stream, s_dialect, s_options);

        while (reader.TryPickRecord())
        {
            reader.TryReadField();

            if (reader.CurrentPositionType == TabularPositionType.EndOfStream)
            {
                continue;
            }

            reader.TryGetDouble(out var field0);
            reader.TryReadField();
            reader.TryGetDouble(out var field1);
            reader.TryReadField();
            reader.TryGetDouble(out var field2);
            reader.TryReadField();
            reader.TryGetDouble(out var field3);

            var record = new RecordN
            {
                Field0 = field0,
                Field1 = field1,
                Field2 = field2,
                Field3 = field3,
            };

            recordsR.Add(record);
        }

        Assert.AreEqual(1, recordsR.Count);
        Assert.AreEqual(Math.E, recordsR[0].Field0);
        Assert.AreEqual(Math.E, recordsR[0].Field1);
        Assert.AreEqual(Math.PI, recordsR[0].Field2);
        Assert.AreEqual(Math.PI, recordsR[0].Field3);
    }

    [TestMethod]
    [DynamicData(nameof(EnginesD))]
    public void WriteRecordsD(IBenchmarkEngine<RecordD> provider)
    {
        var recordsW = new RecordD[]
        {
            new()
            {
                Field0 = new(1969, 07, 24, 16, 50, 35, TimeSpan.Zero),
                Field1 = new(1969, 07, 24, 16, 50, 35, TimeSpan.Zero),
                Field2 = new(1969, 07, 24, 06, 50, 35, TimeSpan.FromHours(-10)),
                Field3 = new(1969, 07, 24, 06, 50, 35, TimeSpan.FromHours(-10)),
            }
        };

        var recordsR = new List<RecordD>();

        using var stream = new MemoryStream();

        provider.WriteRecords(stream, recordsW);
        stream.Seek(0, SeekOrigin.Begin);

        using var reader = new TabularReader(stream, s_dialect, s_options);

        while (reader.TryPickRecord())
        {
            reader.TryReadField();

            if (reader.CurrentPositionType == TabularPositionType.EndOfStream)
            {
                continue;
            }

            reader.TryGetDateTimeOffset(out var field0);
            reader.TryReadField();
            reader.TryGetDateTimeOffset(out var field1);
            reader.TryReadField();
            reader.TryGetDateTimeOffset(out var field2);
            reader.TryReadField();
            reader.TryGetDateTimeOffset(out var field3);

            var record = new RecordD
            {
                Field0 = field0,
                Field1 = field1,
                Field2 = field2,
                Field3 = field3,
            };

            recordsR.Add(record);
        }

        Assert.AreEqual(1, recordsR.Count);
        Assert.AreEqual(new(1969, 07, 24, 16, 50, 35, TimeSpan.Zero), recordsR[0].Field0);
        Assert.AreEqual(new(1969, 07, 24, 16, 50, 35, TimeSpan.Zero), recordsR[0].Field1);
        Assert.AreEqual(new(1969, 07, 24, 06, 50, 35, TimeSpan.FromHours(-10)), recordsR[0].Field2);
        Assert.AreEqual(new(1969, 07, 24, 06, 50, 35, TimeSpan.FromHours(-10)), recordsR[0].Field3);
    }

    [TestMethod]
    [DynamicData(nameof(EnginesM))]
    public void WriteRecordsM(IBenchmarkEngine<RecordM> provider)
    {
        var recordsW = new RecordM[]
        {
            new()
            {
                Field0 = "____\"______\"____",
                Field1 = true,
                Field2 = Math.PI,
                Field3 = new(1969, 07, 24, 06, 50, 35, TimeSpan.FromHours(-10)),
            }
        };

        var recordsR = new List<RecordM>();

        using var stream = new MemoryStream();

        provider.WriteRecords(stream, recordsW);
        stream.Seek(0, SeekOrigin.Begin);

        using var reader = new TabularReader(stream, s_dialect, s_options);

        while (reader.TryPickRecord())
        {
            reader.TryReadField();

            if (reader.CurrentPositionType == TabularPositionType.EndOfStream)
            {
                continue;
            }

            reader.TryGetString(out var field0);
            reader.TryReadField();
            reader.TryGetBoolean(out var field1);
            reader.TryReadField();
            reader.TryGetDouble(out var field2);
            reader.TryReadField();
            reader.TryGetDateTimeOffset(out var field3);

            var record = new RecordM
            {
                Field0 = field0,
                Field1 = field1,
                Field2 = field2,
                Field3 = field3,
            };

            recordsR.Add(record);
        }

        Assert.AreEqual(1, recordsR.Count);
        Assert.AreEqual("____\"______\"____", recordsR[0].Field0);
        Assert.AreEqual(true, recordsR[0].Field1);
        Assert.AreEqual(Math.PI, recordsR[0].Field2);
        Assert.AreEqual(new(1969, 07, 24, 06, 50, 35, TimeSpan.FromHours(-10)), recordsR[0].Field3);
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
