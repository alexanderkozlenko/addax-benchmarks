using System.Runtime.CompilerServices;
using Addax.Benchmarks.Abstractions;
using Addax.Formats.Tabular;

namespace Addax.Benchmarks;

internal static class Factory
{
    private static readonly TabularDialect s_dialect = new("\r\n", ',', '"');
    private static readonly TabularOptions s_options = new() { LeaveOpen = true };

    public static List<Record<T>> CreateRecordCollection<T>(int count)
    {
        if (typeof(T) == typeof(string))
        {
            return Unsafe.As<List<Record<T>>>(CreateRecordCollectionForString(count));
        }

        if (typeof(T) == typeof(double))
        {
            return Unsafe.As<List<Record<T>>>(CreateRecordCollectionForDouble(count));
        }

        if (typeof(T) == typeof(DateTime))
        {
            return Unsafe.As<List<Record<T>>>(CreateRecordCollectionForDateTime(count));
        }

        throw new NotSupportedException();
    }

    public static MemoryStream CreateRecordStream<T>(int count)
    {
        if (typeof(T) == typeof(string))
        {
            return CreateRecordStreamForString(count);
        }

        if (typeof(T) == typeof(double))
        {
            return CreateRecordStreamForDouble(count);
        }

        if (typeof(T) == typeof(DateTime))
        {
            return CreateRecordStreamForDateTime(count);
        }

        throw new NotSupportedException();
    }

    private static List<Record<string>> CreateRecordCollectionForString(int count)
    {
        var records = new List<Record<string>>();

        for (var i = 0; i < count; i++)
        {
            var record = new Record<string>
            {
                Field0 = "________________",
                Field1 = "________________",
                Field2 = "____\"______\"____",
                Field3 = "____\"______\"____",
            };

            records.Add(record);
        }

        return records;
    }

    private static List<Record<double>> CreateRecordCollectionForDouble(int count)
    {
        var records = new List<Record<double>>();

        for (var i = 0; i < count; i++)
        {
            var record = new Record<double>
            {
                Field0 = Math.E,
                Field1 = Math.E,
                Field2 = Math.PI,
                Field3 = Math.PI,
            };

            records.Add(record);
        }

        return records;
    }

    private static List<Record<DateTime>> CreateRecordCollectionForDateTime(int count)
    {
        var records = new List<Record<DateTime>>();

        for (var i = 0; i < count; i++)
        {
            var record = new Record<DateTime>
            {
                Field0 = new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc),
                Field1 = new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc),
                Field2 = new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc),
                Field3 = new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc),
            };

            records.Add(record);
        }

        return records;
    }

    public static MemoryStream CreateRecordStreamForString(int count)
    {
        var records = CreateRecordCollectionForString(count);
        var stream = new MemoryStream();

        using var writer = new TabularWriter(stream, s_dialect, s_options);

        foreach (var record in records)
        {
            writer.WriteString(record.Field0);
            writer.WriteString(record.Field1);
            writer.WriteString(record.Field2);
            writer.WriteString(record.Field3);
            writer.FinishRecord();
        }

        return stream;
    }

    public static MemoryStream CreateRecordStreamForDouble(int count)
    {
        var records = CreateRecordCollectionForDouble(count);
        var stream = new MemoryStream();

        using var writer = new TabularWriter(stream, s_dialect, s_options);

        foreach (var record in records)
        {
            writer.WriteDouble(record.Field0);
            writer.WriteDouble(record.Field1);
            writer.WriteDouble(record.Field2);
            writer.WriteDouble(record.Field3);
            writer.FinishRecord();
        }

        return stream;
    }

    public static MemoryStream CreateRecordStreamForDateTime(int count)
    {
        var records = CreateRecordCollectionForDateTime(count);
        var stream = new MemoryStream();

        using var writer = new TabularWriter(stream, s_dialect, s_options);

        foreach (var record in records)
        {
            writer.WriteDateTime(record.Field0);
            writer.WriteDateTime(record.Field1);
            writer.WriteDateTime(record.Field2);
            writer.WriteDateTime(record.Field3);
            writer.FinishRecord();
        }

        return stream;
    }
}
