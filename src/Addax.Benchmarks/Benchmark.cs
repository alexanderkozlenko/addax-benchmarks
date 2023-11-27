using System.Reflection;
using Addax.Benchmarks.Abstractions;

namespace Addax.Benchmarks;

public abstract class Benchmark
{
    protected static readonly BenchmarkEngine[] Engines = DiscoverEngines();

    private static BenchmarkEngine[] DiscoverEngines()
    {
        return Assembly
            .Load("Addax.Benchmarks.BenchmarkEngines")
            .GetTypes()
            .Where(static x => x.BaseType == typeof(BenchmarkEngine))
            .Select(static x => (BenchmarkEngine)Activator.CreateInstance(x)!)
            .ToArray();
    }
}
