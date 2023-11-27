namespace Addax.Benchmarks.Abstractions;

public abstract class BenchmarkEngine
{
    public sealed override string ToString()
    {
        return Name;
    }

    protected abstract string Name
    {
        get;
    }
}
