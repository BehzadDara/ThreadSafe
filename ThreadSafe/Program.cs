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