#pragma warning disable CA1001
#pragma warning disable IDE1006

using Addax.Benchmarks.Abstractions;
using Addax.Benchmarks.Contracts;
using BenchmarkDotNet.Attributes;

namespace Addax.Benchmarks;

[GenericTypeArguments(typeof(RecordS))]
[GenericTypeArguments(typeof(RecordN))]
[GenericTypeArguments(typeof(RecordD))]
[GenericTypeArguments(typeof(RecordM))]
public class Benchmark<T> : Benchmark
{
    private readonly MemoryStream _streamR = Factory.CreateRecordStream<T>();
    private readonly MemoryStream _streamW;
    private readonly List<T> _recordsR;
    private readonly T[] _recordsW = Factory.CreateRecordArray<T>();

    public Benchmark()
    {
        _streamW = new(_streamR.Capacity);
        _recordsR = new(_recordsW.Length);
    }

    [Benchmark(Description = "read records")]
    [ArgumentsSource(nameof(EnginesT))]
    public void ReadRecords(IBenchmarkEngine<T> Engine)
    {
        _streamR.Seek(0, SeekOrigin.Begin);
        _recordsR.Clear();

        Engine.ReadRecords(_streamR, _recordsR);
    }

    [Benchmark(Description = "write records")]
    [ArgumentsSource(nameof(EnginesT))]
    public void WriteRecords(IBenchmarkEngine<T> Engine)
    {
        _streamW.Seek(0, SeekOrigin.Begin);

        Engine.WriteRecords(_streamW, _recordsW);
    }

    public IEnumerable<IBenchmarkEngine<T>> EnginesT
    {
        get
        {
            return Engines.OfType<IBenchmarkEngine<T>>();
        }
    }
}
