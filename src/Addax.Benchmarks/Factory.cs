using System.Runtime.CompilerServices;
using Addax.Benchmarks.Abstractions;
using Addax.Formats.Tabular;

namespace Addax.Benchmarks;

internal static class Factory
{
    private static readonly TabularDialect s_dialect = new("\r\n", ',', '"');
    private static readonly TabularOptions s_options = new() { LeaveOpen = true };

    public static T[] CreateRecordArray<T>(int count)
    {
        if (typeof(T) == typeof(Record<string>))
        {
            return Unsafe.As<T[]>(CreateRecordSArray(count));
        }

        if (typeof(T) == typeof(Record<double>))
        {
            return Unsafe.As<T[]>(CreateRecordNArray(count));
        }

        if (typeof(T) == typeof(Record<DateTime>))
        {
            return Unsafe.As<T[]>(CreateRecordDArray(count));
        }

        throw new NotSupportedException();
    }

    public static MemoryStream CreateRecordStream<T>(int count)
    {
        if (typeof(T) == typeof(Record<string>))
        {
            return CreateRecordSStream(count);
        }

        if (typeof(T) == typeof(Record<double>))
        {
            return CreateRecordNStream(count);
        }

        if (typeof(T) == typeof(Record<DateTime>))
        {
            return CreateRecordDStream(count);
        }

        throw new NotSupportedException();
    }

    private static Record<string>[] CreateRecordSArray(int count)
    {
        var records = new Record<string>[count];

        for (var i = 0; i < records.Length; i++)
        {
            records[i].Field0 = "________________";
            records[i].Field1 = "________________";
            records[i].Field2 = "____\"______\"____";
            records[i].Field3 = "____\"______\"____";
        }

        return records;
    }

    private static Record<double>[] CreateRecordNArray(int count)
    {
        var records = new Record<double>[count];

        for (var i = 0; i < records.Length; i++)
        {
            records[i].Field0 = Math.E;
            records[i].Field1 = Math.E;
            records[i].Field2 = Math.PI;
            records[i].Field3 = Math.PI;
        }

        return records;
    }

    private static Record<DateTime>[] CreateRecordDArray(int count)
    {
        var records = new Record<DateTime>[count];

        for (var i = 0; i < records.Length; i++)
        {
            records[i].Field0 = new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc);
            records[i].Field1 = new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc);
            records[i].Field2 = new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc);
            records[i].Field3 = new(1969, 07, 24, 16, 50, 35, DateTimeKind.Utc);
        }

        return records;
    }

    public static MemoryStream CreateRecordSStream(int count)
    {
        var records = CreateRecordSArray(count);
        var stream = new MemoryStream();

        using (var writer = new TabularWriter(stream, s_dialect, s_options))
        {
            foreach (var record in records)
            {
                writer.WriteString(record.Field0);
                writer.WriteString(record.Field1);
                writer.WriteString(record.Field2);
                writer.WriteString(record.Field3);
                writer.FinishRecord();
            }
        }

        stream.Seek(0, SeekOrigin.Begin);

        return stream;
    }

    public static MemoryStream CreateRecordNStream(int count)
    {
        var records = CreateRecordNArray(count);
        var stream = new MemoryStream();

        using (var writer = new TabularWriter(stream, s_dialect, s_options))
        {
            foreach (var record in records)
            {
                writer.WriteDouble(record.Field0);
                writer.WriteDouble(record.Field1);
                writer.WriteDouble(record.Field2);
                writer.WriteDouble(record.Field3);
                writer.FinishRecord();
            }
        }

        stream.Seek(0, SeekOrigin.Begin);

        return stream;
    }

    public static MemoryStream CreateRecordDStream(int count)
    {
        var records = CreateRecordDArray(count);
        var stream = new MemoryStream();

        using (var writer = new TabularWriter(stream, s_dialect, s_options))
        {
            foreach (var record in records)
            {
                writer.WriteDateTime(record.Field0);
                writer.WriteDateTime(record.Field1);
                writer.WriteDateTime(record.Field2);
                writer.WriteDateTime(record.Field3);
                writer.FinishRecord();
            }
        }

        stream.Seek(0, SeekOrigin.Begin);

        return stream;
    }
}
