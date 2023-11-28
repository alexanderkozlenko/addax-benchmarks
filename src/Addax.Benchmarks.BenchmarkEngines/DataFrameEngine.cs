using Addax.Benchmarks.Abstractions;
using Addax.Benchmarks.Contracts;
using Microsoft.Data.Analysis;

namespace Addax.Benchmarks.BenchmarkEngines;

public sealed class DataFrameEngine : BenchmarkEngine, IBenchmarkEngine<RecordS>, IBenchmarkEngine<RecordN>, IBenchmarkEngine<RecordD>, IBenchmarkEngine<RecordM>
{
    private static readonly Type[] s_dataTypesS =
    [
        typeof(string),
        typeof(string),
        typeof(string),
        typeof(string),
    ];

    private static readonly Type[] s_dataTypesN =
    [
        typeof(double),
        typeof(double),
        typeof(double),
        typeof(double),
    ];

    private static readonly Type[] s_dataTypesD =
    [
        typeof(DateTime),
        typeof(DateTime),
        typeof(DateTime),
        typeof(DateTime),
    ];

    private static readonly Type[] s_dataTypesM =
    [
        typeof(string),
        typeof(bool),
        typeof(double),
        typeof(DateTime),
    ];

    public void ReadRecords(Stream stream, ICollection<RecordS> records)
    {
        var frame = DataFrame.LoadCsv(stream, header: false, dataTypes: s_dataTypesS);

        foreach (var row in frame.Rows)
        {
            var record = new RecordS
            {
                Field0 = (string)row[0],
                Field1 = (string)row[1],
                Field2 = (string)row[2],
                Field3 = (string)row[3],
            };

            records.Add(record);
        }
    }

    public void ReadRecords(Stream stream, ICollection<RecordN> records)
    {
        var frame = DataFrame.LoadCsv(stream, header: false, dataTypes: s_dataTypesN);

        foreach (var row in frame.Rows)
        {
            var record = new RecordN
            {
                Field0 = (double)row[0],
                Field1 = (double)row[1],
                Field2 = (double)row[2],
                Field3 = (double)row[3],
            };

            records.Add(record);
        }
    }

    public void ReadRecords(Stream stream, ICollection<RecordD> records)
    {
        var frame = DataFrame.LoadCsv(stream, header: false, dataTypes: s_dataTypesD);

        foreach (var row in frame.Rows)
        {
            var record = new RecordD
            {
                Field0 = (DateTime)row[0],
                Field1 = (DateTime)row[1],
                Field2 = (DateTime)row[2],
                Field3 = (DateTime)row[3],
            };

            records.Add(record);
        }
    }

    public void ReadRecords(Stream stream, ICollection<RecordM> records)
    {
        var frame = DataFrame.LoadCsv(stream, header: false, dataTypes: s_dataTypesM);

        foreach (var row in frame.Rows)
        {
            var record = new RecordM
            {
                Field0 = (string)row[0],
                Field1 = (bool)row[1],
                Field2 = (double)row[2],
                Field3 = (DateTime)row[3],
            };

            records.Add(record);
        }
    }

    public void WriteRecords(Stream stream, RecordS[] records)
    {
        var columns = (DataFrameColumn[])
        [
            new StringDataFrameColumn("0"),
            new StringDataFrameColumn("1"),
            new StringDataFrameColumn("2"),
            new StringDataFrameColumn("3"),
        ];

        var frame = new DataFrame(columns);
        var row = new object?[4];

        foreach (var record in records)
        {
            row[0] = record.Field0;
            row[1] = record.Field1;
            row[2] = record.Field2;
            row[3] = record.Field3;

            frame.Append(row, inPlace: true);
        }

        DataFrame.SaveCsv(frame, stream, header: false);
    }

    public void WriteRecords(Stream stream, RecordN[] records)
    {
        var columns = (DataFrameColumn[])
        [
            new DoubleDataFrameColumn("0"),
            new DoubleDataFrameColumn("1"),
            new DoubleDataFrameColumn("2"),
            new DoubleDataFrameColumn("3"),
        ];

        var frame = new DataFrame(columns);
        var row = new object?[4];

        foreach (var record in records)
        {
            row[0] = record.Field0;
            row[1] = record.Field1;
            row[2] = record.Field2;
            row[3] = record.Field3;

            frame.Append(row, inPlace: true);
        }

        DataFrame.SaveCsv(frame, stream, header: false);
    }

    public void WriteRecords(Stream stream, RecordD[] records)
    {
        var columns = (DataFrameColumn[])
        [
            new DateTimeDataFrameColumn("0"),
            new DateTimeDataFrameColumn("1"),
            new DateTimeDataFrameColumn("2"),
            new DateTimeDataFrameColumn("3"),
        ];

        var frame = new DataFrame(columns);
        var row = new object?[4];

        foreach (var record in records)
        {
            row[0] = record.Field0;
            row[1] = record.Field1;
            row[2] = record.Field2;
            row[3] = record.Field3;

            frame.Append(row, inPlace: true);
        }

        DataFrame.SaveCsv(frame, stream, header: false);
    }

    public void WriteRecords(Stream stream, RecordM[] records)
    {
        var columns = (DataFrameColumn[])
        [
            new StringDataFrameColumn("0"),
            new BooleanDataFrameColumn("1"),
            new DoubleDataFrameColumn("2"),
            new DateTimeDataFrameColumn("3"),
        ];

        var frame = new DataFrame(columns);
        var row = new object?[4];

        foreach (var record in records)
        {
            row[0] = record.Field0;
            row[1] = record.Field1;
            row[2] = record.Field2;
            row[3] = record.Field3;

            frame.Append(row, inPlace: true);
        }

        DataFrame.SaveCsv(frame, stream, header: false);
    }

    protected override string Name
    {
        get
        {
            return "DataFrame";
        }
    }
}
