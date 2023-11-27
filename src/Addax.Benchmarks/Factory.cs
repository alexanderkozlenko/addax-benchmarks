using System.Runtime.CompilerServices;
using Addax.Benchmarks.Contracts;
using Addax.Formats.Tabular;

namespace Addax.Benchmarks;

internal static class Factory
{
    private const int s_count = 1024 * 1024;

    private static readonly TabularDialect s_dialect = new("\r\n", ',', '"');
    private static readonly TabularOptions s_options = new() { LeaveOpen = true };

    public static T[] CreateRecordArray<T>()
    {
        if (typeof(T) == typeof(RecordS))
        {
            return Unsafe.As<T[]>(CreateRecordSArray());
        }

        if (typeof(T) == typeof(RecordN))
        {
            return Unsafe.As<T[]>(CreateRecordNArray());
        }

        if (typeof(T) == typeof(RecordD))
        {
            return Unsafe.As<T[]>(CreateRecordDArray());
        }

        if (typeof(T) == typeof(RecordM))
        {
            return Unsafe.As<T[]>(CreateRecordMArray());
        }

        throw new NotSupportedException();
    }

    public static MemoryStream CreateRecordStream<T>()
    {
        if (typeof(T) == typeof(RecordS))
        {
            return CreateRecordSStream();
        }

        if (typeof(T) == typeof(RecordN))
        {
            return CreateRecordNStream();
        }

        if (typeof(T) == typeof(RecordD))
        {
            return CreateRecordDStream();
        }

        if (typeof(T) == typeof(RecordM))
        {
            return CreateRecordMStream();
        }

        throw new NotSupportedException();
    }

    private static RecordS[] CreateRecordSArray()
    {
        var records = new RecordS[s_count];

        for (var i = 0; i < records.Length; i++)
        {
            records[i].Field0 = "________________";
            records[i].Field1 = "________________";
            records[i].Field2 = "____\"______\"____";
            records[i].Field3 = "____\"______\"____";
        }

        return records;
    }

    private static RecordN[] CreateRecordNArray()
    {
        var records = new RecordN[s_count];

        for (var i = 0; i < records.Length; i++)
        {
            records[i].Field0 = Math.E;
            records[i].Field1 = Math.E;
            records[i].Field2 = Math.PI;
            records[i].Field3 = Math.PI;
        }

        return records;
    }

    private static RecordD[] CreateRecordDArray()
    {
        var records = new RecordD[s_count];

        for (var i = 0; i < records.Length; i++)
        {
            records[i].Field0 = new(1969, 07, 24, 16, 50, 35, TimeSpan.Zero);
            records[i].Field1 = new(1969, 07, 24, 16, 50, 35, TimeSpan.Zero);
            records[i].Field2 = new(1969, 07, 24, 06, 50, 35, TimeSpan.FromHours(-10));
            records[i].Field3 = new(1969, 07, 24, 06, 50, 35, TimeSpan.FromHours(-10));
        }

        return records;
    }

    private static RecordM[] CreateRecordMArray()
    {
        var records = new RecordM[s_count];

        for (var i = 0; i < records.Length; i++)
        {
            records[i].Field0 = "____\"______\"____";
            records[i].Field1 = true;
            records[i].Field2 = Math.PI;
            records[i].Field3 = new(1969, 07, 24, 06, 50, 35, TimeSpan.FromHours(-10));
        }

        return records;
    }

    public static MemoryStream CreateRecordSStream()
    {
        var records = CreateRecordSArray();
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

    public static MemoryStream CreateRecordNStream()
    {
        var records = CreateRecordNArray();
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

    public static MemoryStream CreateRecordDStream()
    {
        var records = CreateRecordDArray();
        var stream = new MemoryStream();

        using (var writer = new TabularWriter(stream, s_dialect, s_options))
        {
            foreach (var record in records)
            {
                writer.WriteDateTimeOffset(record.Field0);
                writer.WriteDateTimeOffset(record.Field1);
                writer.WriteDateTimeOffset(record.Field2);
                writer.WriteDateTimeOffset(record.Field3);
                writer.FinishRecord();
            }
        }

        stream.Seek(0, SeekOrigin.Begin);

        return stream;
    }

    public static MemoryStream CreateRecordMStream()
    {
        var records = CreateRecordMArray();
        var stream = new MemoryStream();

        using (var writer = new TabularWriter(stream, s_dialect, s_options))
        {
            foreach (var record in records)
            {
                writer.WriteString(record.Field0);
                writer.WriteBoolean(record.Field1);
                writer.WriteDouble(record.Field2);
                writer.WriteDateTimeOffset(record.Field3);
                writer.FinishRecord();
            }
        }

        stream.Seek(0, SeekOrigin.Begin);

        return stream;
    }
}
