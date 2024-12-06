namespace Addax.Benchmarks.Abstractions;

public abstract class BenchmarkEngine
{
    public sealed override string ToString()
    {
        return Name;
    }

    protected static bool IsSwitchEnabled(string name)
    {
        return Environment.GetEnvironmentVariable(name)?.ToUpperInvariant() is "1" or "TRUE";
    }

    protected abstract string Name
    {
        get;
    }
}
