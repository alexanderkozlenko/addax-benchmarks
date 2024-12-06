#pragma warning disable CA1000
#pragma warning disable CA1001
#pragma warning disable IDE1006

using System.Reflection;
using Addax.Benchmarks.Abstractions;
using BenchmarkDotNet.Attributes;

namespace Addax.Benchmarks;

[GenericTypeArguments(typeof(Record<string>))]
[GenericTypeArguments(typeof(Record<double>))]
[GenericTypeArguments(typeof(Record<DateTime>))]
public class Benchmark<T>
{
    private const int s_count = 1024 * 1024;

    private static readonly BenchmarkEngine[] s_engines = CreateEngines();

    private readonly MemoryStream _streamR = Factory.CreateRecordStream<T>(s_count);
    private readonly MemoryStream _streamW = new(128 * s_count);
    private readonly List<T> _recordsR = new(s_count);
    private readonly T[] _recordsW = Factory.CreateRecordArray<T>(s_count);

    [Benchmark(Description = "read")]
    [ArgumentsSource(nameof(EnginesT))]
    public void ReadRecords(IBenchmarkEngine<T> Engine)
    {
        _streamR.Seek(0, SeekOrigin.Begin);
        _recordsR.Clear();

        Engine.ReadRecords(_streamR, _recordsR);
    }

    [Benchmark(Description = "write")]
    [ArgumentsSource(nameof(EnginesT))]
    public void WriteRecords(IBenchmarkEngine<T> Engine)
    {
        _streamW.Seek(0, SeekOrigin.Begin);

        Engine.WriteRecords(_streamW, _recordsW);
    }

    public static IEnumerable<IBenchmarkEngine<T>> EnginesT
    {
        get
        {
            return s_engines.OfType<IBenchmarkEngine<T>>();
        }
    }

    private static BenchmarkEngine[] CreateEngines()
    {
        return Assembly.Load("Addax.Benchmarks.BenchmarkEngines").GetExportedTypes()
            .Where(static x => x.BaseType == typeof(BenchmarkEngine))
            .Select(static x => (BenchmarkEngine)Activator.CreateInstance(x)!)
            .OrderBy(static x => x.ToString())
            .ToArray();
    }
}
