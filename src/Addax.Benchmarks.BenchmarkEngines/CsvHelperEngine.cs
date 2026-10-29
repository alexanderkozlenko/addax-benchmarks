using Addax.Benchmarks.Abstractions;
using CsvHelper;

namespace Addax.Benchmarks.BenchmarkEngines;

public sealed class CsvHelperEngine : BenchmarkEngine,
    IBenchmarkEngine<Record<string>>,
    IBenchmarkEngine<Record<double>>,
    IBenchmarkEngine<Record<DateTime>>
{
    private static readonly CsvReaderOptions s_readerOptions = new()
    {
        Separator = ',',
        Quote = '"',
        HasHeader = false,
        PoolStrings = IsSwitchEnabled("ENABLE_STRING_POOLING"),
    };

    private static readonly CsvWriterOptions s_writerOptions = new()
    {
        NewLine = "\r\n",
        Separator = ',',
        HasHeader = false,
    };

    private static readonly string[] s_dateTimeFormats = ["o"];

    public void ReadRecords(Stream stream, ICollection<Record<string>> records)
    {
        using var reader = CsvReader.Create(stream, s_readerOptions, leaveOpen: true);

        while (reader.Read())
        {
            var record = new Record<string>
            {
                Field0 = reader.GetString(0),
                Field1 = reader.GetString(1),
                Field2 = reader.GetString(2),
                Field3 = reader.GetString(3),
            };

            records.Add(record);
        }
    }

    public void ReadRecords(Stream stream, ICollection<Record<double>> records)
    {
        using var reader = CsvReader.Create(stream, s_readerOptions, leaveOpen: true);

        while (reader.Read())
        {
            var record = new Record<double>
            {
                Field0 = reader.Parse<double>(0),
                Field1 = reader.Parse<double>(1),
                Field2 = reader.Parse<double>(2),
                Field3 = reader.Parse<double>(3),
            };

            records.Add(record);
        }
    }

    public void ReadRecords(Stream stream, ICollection<Record<DateTime>> records)
    {
        using var reader = CsvReader.Create(stream, s_readerOptions, leaveOpen: true);

        while (reader.Read())
        {
            var record = new Record<DateTime>
            {
                Field0 = reader.ParseExact<DateTime>(0, s_dateTimeFormats),
                Field1 = reader.ParseExact<DateTime>(1, s_dateTimeFormats),
                Field2 = reader.ParseExact<DateTime>(2, s_dateTimeFormats),
                Field3 = reader.ParseExact<DateTime>(3, s_dateTimeFormats),
            };

            records.Add(record);
        }
    }

    public void WriteRecords(Stream stream, IReadOnlyCollection<Record<string>> records)
    {
        using var writer = CsvWriter.Create(stream, s_writerOptions, leaveOpen: true);

        foreach (var record in records)
        {
            writer.WriteField(record.Field0);
            writer.WriteField(record.Field1);
            writer.WriteField(record.Field2);
            writer.WriteField(record.Field3);
            writer.EndRecord();
        }
    }

    public void WriteRecords(Stream stream, IReadOnlyCollection<Record<double>> records)
    {
        using var writer = CsvWriter.Create(stream, s_writerOptions, leaveOpen: true);

        foreach (var record in records)
        {
            writer.WriteField(record.Field0);
            writer.WriteField(record.Field1);
            writer.WriteField(record.Field2);
            writer.WriteField(record.Field3);
            writer.EndRecord();
        }
    }

    public void WriteRecords(Stream stream, IReadOnlyCollection<Record<DateTime>> records)
    {
        using var writer = CsvWriter.Create(stream, s_writerOptions, leaveOpen: true);

        foreach (var record in records)
        {
            writer.WriteField(record.Field0, s_dateTimeFormats[0]);
            writer.WriteField(record.Field1, s_dateTimeFormats[0]);
            writer.WriteField(record.Field2, s_dateTimeFormats[0]);
            writer.WriteField(record.Field3, s_dateTimeFormats[0]);
            writer.EndRecord();
        }
    }

    protected override string Name
    {
        get
        {
            return "CsvHelper";
        }
    }
}
