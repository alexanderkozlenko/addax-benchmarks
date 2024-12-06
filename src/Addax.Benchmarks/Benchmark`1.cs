#pragma warning disable CA1000
#pragma warning disable CA1001
#pragma warning disable IDE1006

using System.Reflection;
using Addax.Benchmarks.Abstractions;
using BenchmarkDotNet.Attributes;

namespace Addax.Benchmarks;

[GenericTypeArguments(typeof(string))]
[GenericTypeArguments(typeof(double))]
[GenericTypeArguments(typeof(DateTime))]
public class Benchmark<T>
{
    private const int s_count = 1024 * 1024;

    private static readonly BenchmarkEngine[] s_engines = CreateEngines();

    private readonly MemoryStream _streamR = Factory.CreateRecordStream<T>(s_count);
    private readonly MemoryStream _streamW = new(128 * s_count);
    private readonly List<Record<T>> _recordsR = new(s_count);
    private readonly List<Record<T>> _recordsW = Factory.CreateRecordCollection<T>(s_count);

    [Benchmark]
    [ArgumentsSource(nameof(Engines))]
    public void Reading(IBenchmarkEngine<Record<T>> Engine)
    {
        _recordsR.Clear();
        _streamR.Seek(0, SeekOrigin.Begin);

        Engine.ReadRecords(_streamR, _recordsR);
    }

    [Benchmark]
    [ArgumentsSource(nameof(Engines))]
    public void Writing(IBenchmarkEngine<Record<T>> Engine)
    {
        _streamW.Seek(0, SeekOrigin.Begin);

        Engine.WriteRecords(_streamW, _recordsW);
    }

    public static IEnumerable<IBenchmarkEngine<Record<T>>> Engines
    {
        get
        {
            return s_engines.OfType<IBenchmarkEngine<Record<T>>>();
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
