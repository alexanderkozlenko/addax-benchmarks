using System.Globalization;
using Addax.Benchmarks.Abstractions;
using Addax.Benchmarks.Contracts;
using CsvHelper;
using CsvHelper.Configuration;

namespace Addax.Benchmarks.BenchmarkEngines;

public sealed class CsvHelperEngine : BenchmarkEngine, IBenchmarkEngine<RecordS>, IBenchmarkEngine<RecordN>, IBenchmarkEngine<RecordD>, IBenchmarkEngine<RecordM>
{
    private static readonly CsvConfiguration s_configuration = new(CultureInfo.InvariantCulture)
    {
        NewLine = "\r\n",
        Delimiter = ",",
        Quote = '"',
        HasHeaderRecord = false,
    };

    private static readonly string[] s_dateTimeFormats = ["o"];

    public void ReadRecords(Stream stream, ICollection<RecordS> records)
    {
        using var reader = new CsvReader(new StreamReader(stream, leaveOpen: true), s_configuration);

        while (reader.Read())
        {
            reader.TryGetField<string>(0, out var field0);
            reader.TryGetField<string>(1, out var field1);
            reader.TryGetField<string>(2, out var field2);
            reader.TryGetField<string>(3, out var field3);

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
        using var reader = new CsvReader(new StreamReader(stream, leaveOpen: true), s_configuration);

        while (reader.Read())
        {
            reader.TryGetField<double>(0, out var field0);
            reader.TryGetField<double>(1, out var field1);
            reader.TryGetField<double>(2, out var field2);
            reader.TryGetField<double>(3, out var field3);

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
        using var reader = new CsvReader(new StreamReader(stream, leaveOpen: true), s_configuration);

        reader.Context.TypeConverterOptionsCache.GetOptions<DateTime>().Formats = s_dateTimeFormats;
        reader.Context.TypeConverterOptionsCache.GetOptions<DateTime>().DateTimeStyle = DateTimeStyles.AdjustToUniversal;

        while (reader.Read())
        {
            reader.TryGetField<DateTime>(0, out var field0);
            reader.TryGetField<DateTime>(1, out var field1);
            reader.TryGetField<DateTime>(2, out var field2);
            reader.TryGetField<DateTime>(3, out var field3);

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
        using var reader = new CsvReader(new StreamReader(stream, leaveOpen: true), s_configuration);

        reader.Context.TypeConverterOptionsCache.GetOptions<DateTime>().Formats = s_dateTimeFormats;
        reader.Context.TypeConverterOptionsCache.GetOptions<DateTime>().DateTimeStyle = DateTimeStyles.AdjustToUniversal;

        while (reader.Read())
        {
            reader.TryGetField<string>(0, out var field0);
            reader.TryGetField<bool>(1, out var field1);
            reader.TryGetField<double>(2, out var field2);
            reader.TryGetField<DateTime>(3, out var field3);

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
        using var writer = new CsvWriter(new StreamWriter(stream, leaveOpen: true), s_configuration);

        foreach (var record in records)
        {
            writer.WriteField(record.Field0);
            writer.WriteField(record.Field1);
            writer.WriteField(record.Field2);
            writer.WriteField(record.Field3);
            writer.NextRecord();
        }
    }

    public void WriteRecords(Stream stream, RecordN[] records)
    {
        using var writer = new CsvWriter(new StreamWriter(stream, leaveOpen: true), s_configuration);

        foreach (var record in records)
        {
            writer.WriteField(record.Field0);
            writer.WriteField(record.Field1);
            writer.WriteField(record.Field2);
            writer.WriteField(record.Field3);
            writer.NextRecord();
        }
    }

    public void WriteRecords(Stream stream, RecordD[] records)
    {
        using var writer = new CsvWriter(new StreamWriter(stream, leaveOpen: true), s_configuration);

        writer.Context.TypeConverterOptionsCache.GetOptions<DateTime>().Formats = s_dateTimeFormats;
        writer.Context.TypeConverterOptionsCache.GetOptions<DateTime>().DateTimeStyle = DateTimeStyles.AdjustToUniversal;

        foreach (var record in records)
        {
            writer.WriteField(record.Field0);
            writer.WriteField(record.Field1);
            writer.WriteField(record.Field2);
            writer.WriteField(record.Field3);
            writer.NextRecord();
        }
    }

    public void WriteRecords(Stream stream, RecordM[] records)
    {
        using var writer = new CsvWriter(new StreamWriter(stream, leaveOpen: true), s_configuration);

        writer.Context.TypeConverterOptionsCache.GetOptions<DateTime>().Formats = s_dateTimeFormats;
        writer.Context.TypeConverterOptionsCache.GetOptions<DateTime>().DateTimeStyle = DateTimeStyles.AdjustToUniversal;

        foreach (var record in records)
        {
            writer.WriteField(record.Field0);
            writer.WriteField(record.Field1);
            writer.WriteField(record.Field2);
            writer.WriteField(record.Field3);
            writer.NextRecord();
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
