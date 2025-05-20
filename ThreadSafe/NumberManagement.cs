namespace ThreadSafe;

public class NumberManagement
{
    private int _number = 0; 
    private readonly object _lock = new(); 
    private readonly SemaphoreSlim _semaphore = new(1);

    public void Add(int number)
    {
        _number += number;
        Console.WriteLine($"After add {number} result is {_number}");
    }

    public void Subtract(int number)
    {
        _number -= number;
        Console.WriteLine($"After subtract {number} result is {_number}");
    }

    public void AddLock(int number)
    {
        lock (_lock)
        {
            _number += number;
            Console.WriteLine($"After add lock {number} result is {_number}");
        }
    }

    public void SubtractLock(int number)
    {
        lock (_lock)
        {
            _number -= number;
            Console.WriteLine($"After subtract lock {number} result is {_number}");
        }
    }

    public async Task AddSemaphore(int number)
    {
        await _semaphore.WaitAsync();
        try
        {
            _number += number;
            Console.WriteLine($"After add semaphore {number} result is {_number}");
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task SubtractSemaphore(int number)
    {
        await _semaphore.WaitAsync();
        try
        {
            _number -= number;
            Console.WriteLine($"After subtract semaphore {number} result is {_number}");
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void AddInterLocked(int number)
    {
        Interlocked.Add(ref _number, number);
    }

    public void SubtractInterLocked(int number)
    {
        Interlocked.Add(ref _number, -1 * number);
    }
}
