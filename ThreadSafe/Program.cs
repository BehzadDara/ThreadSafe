using ThreadSafe;

// not threadsafe
var numberManagement1 = new NumberManagement();
numberManagement1.Add(5);
numberManagement1.Add(6);
numberManagement1.Subtract(7);
Console.WriteLine("-----------------------------------");

// lock (1 thread - in process scope - no async await support - lightweight - simple)
var numberManagement2 = new NumberManagement();
numberManagement2.AddLock(5);
numberManagement2.AddLock(6);
numberManagement2.SubtractLock(7);
Console.WriteLine("-----------------------------------");

// semaphore slim (n thread - in process scope - with async await support - lightweight compared to semaphore - not simple)
var numberManagement3 = new NumberManagement();
await numberManagement3.AddSemaphore(5);
await numberManagement3.AddSemaphore(6);
await numberManagement3.SubtractSemaphore(7);
Console.WriteLine("-----------------------------------");

// inter locked (low overhead - non block - only numeric updates)
var numberManagement4 = new NumberManagement();
numberManagement4.AddInterLocked(5);
numberManagement4.AddInterLocked(6);
numberManagement4.SubtractInterLocked(7);
Console.WriteLine("-----------------------------------");

/*

class Program
{
    /*
     initialCount is the number of resource accesses that will be allowed immediately. 
    Or, in other words, it is the number of times Wait can be called 
    without blocking immediately after the semaphore was instantiated.

    maximumCount is the highest count the semaphore can obtain. 
    It is the number of times Release can be called 
    without throwing an exception assuming initialCount count was zero. 
    If initialCount is set to the same value as maximumCount 
    then calling Release immediately after the semaphore was instantiated will throw an exception.
     */

    public static int initialCount = 3;
    public static int maximumCount = 5;
    public static Semaphore semaphore = new(initialCount, maximumCount);

    public static void Main(string[] args)
    {
        for (int i = 0; i < 10; i++)
        {
            var threadObject = new Thread(Process)
            {
                Name = $"Thread: {i}"
            };

            threadObject.Start();
        }
    }

    private static void Process()
    {
        var threadName = Thread.CurrentThread.Name;

        Console.WriteLine($"{threadName} is waiting to enter the critical section.");
        semaphore.WaitOne();

        Console.WriteLine($"{threadName} is inside the critical section now.");
        Thread.Sleep(1000);

        Console.WriteLine($"{threadName} is releasing the critical section.");
        semaphore.Release();
    }

}

*/
