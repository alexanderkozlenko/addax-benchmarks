namespace Addax.Benchmarks.Abstractions;

public interface IBenchmarkEngine<T>
{
    void ReadRecords(Stream stream, ICollection<T> records);

    void WriteRecords(Stream stream, T[] records);
}
