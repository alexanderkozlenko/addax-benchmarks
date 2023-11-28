using Addax.Benchmarks.Abstractions;
using Addax.Benchmarks.Contracts;
using Addax.Formats.Tabular;

namespace Addax.Benchmarks.BenchmarkEngines;

public sealed class AddaxEngine : BenchmarkEngine, IBenchmarkEngine<RecordS>, IBenchmarkEngine<RecordN>, IBenchmarkEngine<RecordD>, IBenchmarkEngine<RecordM>
{
    private static readonly TabularDialect s_dialect = new("\r\n", ',', '"');
    private static readonly TabularOptions s_options = new() { LeaveOpen = true };

    public void ReadRecords(Stream stream, ICollection<RecordS> records)
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

            var record = new RecordS
            {
                Field0 = field0,
                Field1 = field1,
                Field2 = field2,
                Field3 = field3,
            };

            records.Add(record);
        }
    }

    public void ReadRecords(Stream stream, ICollection<RecordN> records)
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

            var record = new RecordN
            {
                Field0 = field0,
                Field1 = field1,
                Field2 = field2,
                Field3 = field3,
            };

            records.Add(record);
        }
    }

    public void ReadRecords(Stream stream, ICollection<RecordD> records)
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

            var record = new RecordD
            {
                Field0 = field0,
                Field1 = field1,
                Field2 = field2,
                Field3 = field3,
            };

            records.Add(record);
        }
    }

    public void ReadRecords(Stream stream, ICollection<RecordM> records)
    {
        using var reader = new TabularReader(stream, s_dialect, s_options);

        while (reader.TryPickRecord())
        {
            reader.TryReadField();
            reader.TryGetString(out var field0);
            reader.TryReadField();
            reader.TryGetBoolean(out var field1);
            reader.TryReadField();
            reader.TryGetDouble(out var field2);
            reader.TryReadField();
            reader.TryGetDateTime(out var field3);

            var record = new RecordM
            {
                Field0 = field0,
                Field1 = field1,
                Field2 = field2,
                Field3 = field3,
            };

            records.Add(record);
        }
    }

    public void WriteRecords(Stream stream, RecordS[] records)
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

    public void WriteRecords(Stream stream, RecordN[] records)
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

    public void WriteRecords(Stream stream, RecordD[] records)
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

    public void WriteRecords(Stream stream, RecordM[] records)
    {
        using var writer = new TabularWriter(stream, s_dialect, s_options);

        foreach (var record in records)
        {
            writer.WriteString(record.Field0);
            writer.WriteBoolean(record.Field1);
            writer.WriteDouble(record.Field2);
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
