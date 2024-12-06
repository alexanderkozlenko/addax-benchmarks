using Addax.Benchmarks.Abstractions;
using Addax.Formats.Tabular;

namespace Addax.Benchmarks.BenchmarkEngines;

public sealed class AddaxEngine : BenchmarkEngine, IBenchmarkEngine<Record<string>>, IBenchmarkEngine<Record<double>>, IBenchmarkEngine<Record<DateTime>>
{
    private static readonly TabularDialect s_dialect = new("\r\n", ',', '"');

    private static readonly TabularOptions s_options = new()
    {
        LeaveOpen = true,
        StringFactory = IsSwitchEnabled("ENABLE_STRING_POOLING") ? new(maxLength: 128) : null,
    };

    public void ReadRecords(Stream stream, ICollection<Record<string>> records)
    {
        using var reader = new TabularReader(stream, s_dialect, s_options);

        while (reader.TryPickRecord())
        {
            reader.TryReadField();
            reader.TryGetString(out var field0);
            reader.TryReadField();
            reader.TryGetString(out var field1);
            reader.TryReadField();
            reader.TryGetString(out var field2);
            reader.TryReadField();
            reader.TryGetString(out var field3);

            var record = new Record<string>
            {
                Field0 = field0,
                Field1 = field1,
                Field2 = field2,
                Field3 = field3,
            };

            records.Add(record);
        }
    }

    public void ReadRecords(Stream stream, ICollection<Record<double>> records)
    {
        using var reader = new TabularReader(stream, s_dialect, s_options);

        while (reader.TryPickRecord())
        {
            reader.TryReadField();
            reader.TryGetDouble(out var field0);
            reader.TryReadField();
            reader.TryGetDouble(out var field1);
            reader.TryReadField();
            reader.TryGetDouble(out var field2);
            reader.TryReadField();
            reader.TryGetDouble(out var field3);

            var record = new Record<double>
            {
                Field0 = field0,
                Field1 = field1,
                Field2 = field2,
                Field3 = field3,
            };

            records.Add(record);
        }
    }

    public void ReadRecords(Stream stream, ICollection<Record<DateTime>> records)
    {
        using var reader = new TabularReader(stream, s_dialect, s_options);

        while (reader.TryPickRecord())
        {
            reader.TryReadField();
            reader.TryGetDateTime(out var field0);
            reader.TryReadField();
            reader.TryGetDateTime(out var field1);
            reader.TryReadField();
            reader.TryGetDateTime(out var field2);
            reader.TryReadField();
            reader.TryGetDateTime(out var field3);

            var record = new Record<DateTime>
            {
                Field0 = field0,
                Field1 = field1,
                Field2 = field2,
                Field3 = field3,
            };

            records.Add(record);
        }
    }

    public void WriteRecords(Stream stream, Record<string>[] records)
    {
        using var writer = new TabularWriter(stream, s_dialect, s_options);

        foreach (var record in records)
        {
            writer.WriteString(record.Field0);
            writer.WriteString(record.Field1);
            writer.WriteString(record.Field2);
            writer.WriteString(record.Field3);
            writer.FinishRecord();
        }
    }

    public void WriteRecords(Stream stream, Record<double>[] records)
    {
        using var writer = new TabularWriter(stream, s_dialect, s_options);

        foreach (var record in records)
        {
            writer.WriteDouble(record.Field0);
            writer.WriteDouble(record.Field1);
            writer.WriteDouble(record.Field2);
            writer.WriteDouble(record.Field3);
            writer.FinishRecord();
        }
    }

    public void WriteRecords(Stream stream, Record<DateTime>[] records)
    {
        using var writer = new TabularWriter(stream, s_dialect, s_options);

        foreach (var record in records)
        {
            writer.WriteDateTime(record.Field0);
            writer.WriteDateTime(record.Field1);
            writer.WriteDateTime(record.Field2);
            writer.WriteDateTime(record.Field3);
            writer.FinishRecord();
        }
    }

    protected override string Name
    {
        get
        {
            return "Addax";
        }
    }
}
