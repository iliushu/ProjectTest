using System.Diagnostics;
using System.Reflection;
using System.Text;
#if false
Type type = typeof(Person);
object person = Activator.CreateInstance(type);

PropertyInfo name = type.GetProperty("Name");
name.SetValue(person, "Alice");
PropertyInfo age = type.GetProperty("Age");
age.SetValue(person, 30);

MethodInfo method = type.GetMethod("Introduce");
method.Invoke(person, null);

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public void Introduce()
    {
        Console.WriteLine($"Hello, my name is {Name} and I am {Age} years old.");
    }
}
#endif
#if false


// 获取 Type 对象
Type utilityType = typeof(Utility);

// 创建 Utility 实例
object utilityInstance = Activator.CreateInstance(utilityType);

// 定义要传递的参数数组
string[] names = { "Alice", "Bob", "Charlie" };

// 获取 PrintItems 方法的信息
MethodInfo printItemsMethod = utilityType.GetMethod("PrintItems");

// 构建泛型方法
MethodInfo genericMethod = printItemsMethod.MakeGenericMethod(typeof(string));

// 调用 PrintItems 方法
genericMethod.Invoke(utilityInstance, new object[] { names });
public class Utility
{
    public void PrintItems<T>(T[] items)
    {
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
}
#endif
#if false
// 获取 Type 对象
Type secretClassType = typeof(SecretClass);

// 创建 SecretClass 实例
object secretInstance = Activator.CreateInstance(secretClassType);

// 获取私有字段 _secretNumber
FieldInfo secretNumberField = secretClassType.GetField("_secretNumber", BindingFlags.NonPublic | BindingFlags.Instance);
if (secretNumberField != null)
{
    int secretNumber = (int)secretNumberField.GetValue(secretInstance);
    Console.WriteLine($"Accessed private field value: {secretNumber}");
}

// 获取私有方法 RevealSecret
MethodInfo revealSecretMethod = secretClassType.GetMethod("RevealSecret", BindingFlags.NonPublic | BindingFlags.Instance);
if (revealSecretMethod != null)
{
    revealSecretMethod.Invoke(secretInstance, null);
}
public class SecretClass
{
    private int _secretNumber = 42;

    private void RevealSecret()
    {
        Console.WriteLine($"The secret number is {_secretNumber}");
    }
}
#endif
#if false
// 创建发布者实例
Publisher publisher = new Publisher();

// 创建订阅者实例
Subscriber subscriber = new Subscriber();

// 订阅事件
publisher.OnSomethingHappened += subscriber.HandleEvent;

// 执行工作
publisher.DoWork();
// 定义委托类型
public delegate void MyEventHandler(string message);

// 发布者类
public class Publisher
{
    // 定义一个事件
    public event MyEventHandler OnSomethingHappened;

    // 触发事件的方法
    protected virtual void RaiseEvent(string message)
    {
        OnSomethingHappened?.Invoke(message);
    }

    public void DoWork()
    {
        // 模拟工作完成
        Console.WriteLine("Doing work...");
        // 触发事件
        RaiseEvent("Work completed!");
    }
}

// 订阅者类
public class Subscriber
{
    // 定义一个符合委托签名的方法
    public void HandleEvent(string message)
    {
        Console.WriteLine($"Received message: {message}");
    }
}
#endif
#if false
// 创建发布者实例
MessagePublisher publisher = new MessagePublisher();

// 创建订阅者实例
SubscriberA subscriberA = new SubscriberA();
SubscriberB subscriberB = new SubscriberB();

// 创建中间件实例，将消息转发给订阅者 A
Middleware middlewareA = new Middleware(publisher, subscriberA.HandleMessage);

// 创建另一个中间件实例，将消息转发给订阅者 B
Middleware middlewareB = new Middleware(publisher, subscriberB.HandleMessage);

// 发布消息
publisher.PublishMessage("This is an important message.");
publisher.PublishMessage("This is a regular message.");
// 定义委托类型
public delegate void MessageHandler(string message);

// 发布者类
public class MessagePublisher
{
    // 定义一个事件
    public event MessageHandler OnMessagePublished;

    // 触发事件的方法
    protected virtual void RaiseEvent(string message)
    {
        OnMessagePublished?.Invoke(message);
    }

    public void PublishMessage(string message)
    {
        Console.WriteLine($"Publishing message: {message}");
        RaiseEvent(message);
    }
}

// 订阅者 A 类
public class SubscriberA
{
    public void HandleMessage(string message)
    {
        Console.WriteLine($"Subscriber A received message: {message}");
    }
}

// 订阅者 B 类
public class SubscriberB
{
    public void HandleMessage(string message)
    {
        Console.WriteLine($"Subscriber B received message: {message}");
    }
}

// 中间件类
public class Middleware
{
    private readonly MessagePublisher _publisher;
    private readonly MessageHandler _handler;

    public Middleware(MessagePublisher publisher, MessageHandler handler)
    {
        _publisher = publisher;
        _handler = handler;
        _publisher.OnMessagePublished += ProcessMessage;
    }

    private void ProcessMessage(string message)
    {
        if (ShouldForwardMessage(message))
        {
            Console.WriteLine($"Middleware forwarding message: {message}");
            _handler(message);
        }
        else
        {
            Console.WriteLine($"Middleware filtered out message: {message}");
        }
    }

    private bool ShouldForwardMessage(string message)
    {
        // 示例过滤条件：只转发包含 "important" 的消息
        return message.Contains("important");
    }
}
#endif
#if false
// 创建发布者实例
MessagePublisher publisher = new MessagePublisher();

// 创建订阅者实例
SubscriberA subscriberA = new SubscriberA();
SubscriberB subscriberB = new SubscriberB();
SubscriberC subscriberC = new SubscriberC();

// 订阅事件
publisher.OnMessagePublished += subscriberA.HandleInfoMessage;
publisher.OnMessagePublished += subscriberB.HandleWarningMessage;
publisher.OnMessagePublished += subscriberC.HandleErrorMessage;

// 发布不同类型的消息
publisher.PublishMessage("This is an info message.");
publisher.PublishMessage("This is a warning message.");
publisher.PublishMessage("This is an error message.");
publisher.PublishMessage("This is a regular message.");

// 定义委托类型
public delegate void MessageHandler(string message);

// 发布者类
public class MessagePublisher
{
    // 定义一个事件
    public event MessageHandler OnMessagePublished;

    // 触发事件的方法
    protected virtual void RaiseEvent(string message)
    {
        OnMessagePublished?.Invoke(message);
    }

    public void PublishMessage(string message)
    {
        Console.WriteLine($"Publishing message: {message}");
        RaiseEvent(message);
    }
}

// 订阅者 A 类
public class SubscriberA
{
    public void HandleInfoMessage(string message)
    {
        if (message.Contains("info"))
        {
            Console.WriteLine($"Subscriber A received info message: {message}");
        }
    }
}

// 订阅者 B 类
public class SubscriberB
{
    public void HandleWarningMessage(string message)
    {
        if (message.Contains("warning"))
        {
            Console.WriteLine($"Subscriber B received warning message: {message}");
        }
    }
}

// 订阅者 C 类
public class SubscriberC
{
    public void HandleErrorMessage(string message)
    {
        if (message.Contains("error"))
        {
            Console.WriteLine($"Subscriber C received error message: {message}");
        }
    }
}
#endif
#if false
// 创建发布者实例
MessagePublisher publisher = new MessagePublisher();

// 创建订阅者实例
SubscriberA subscriberA = new SubscriberA();
SubscriberB subscriberB = new SubscriberB();
SubscriberC subscriberC = new SubscriberC();

// 创建中间件实例，将消息转发给订阅者 A
Middleware middlewareA = new Middleware(publisher, subscriberA.HandleInfoMessage);

// 创建另一个中间件实例，将消息转发给订阅者 B
Middleware middlewareB = new Middleware(publisher, subscriberB.HandleWarningMessage);

// 创建另一个中间件实例，将消息转发给订阅者 C
Middleware middlewareC = new Middleware(publisher, subscriberC.HandleErrorMessage);

// 发布不同类型的消息
publisher.PublishMessage("This is an important info message.");
publisher.PublishMessage("This is a urgent warning message.");
publisher.PublishMessage("This is an error message.");
publisher.PublishMessage("This is a regular message.");
// 定义消息处理委托类型
public delegate void MessageHandler(string message, Action<string> callback);

// 定义回调委托类型
//public delegate void CallbackHandler(string result);

// 发布者类
public class MessagePublisher
{
    // 定义一个事件
    public event MessageHandler OnMessagePublished;

    // 触发事件的方法
    protected virtual void RaiseEvent(string message)
    {
        OnMessagePublished?.Invoke(message, HandleCallback);
    }

    private void HandleCallback(string result)
    {
        Console.WriteLine($"Callback received: {result}");
    }

    public void PublishMessage(string message)
    {
        Console.WriteLine($"Publishing message: {message}");
        RaiseEvent(message);
    }
}

// 订阅者 A 类
public class SubscriberA
{
    public void HandleInfoMessage(string message, Action<string> callback)
    {
        if (message.Contains("info"))
        {
            Console.WriteLine($"Subscriber A received info message: {message}");
            callback($"Processed by Subscriber A: {message}");
        }
    }
}

// 订阅者 B 类
public class SubscriberB
{
    public void HandleWarningMessage(string message, Action<string> callback)
    {
        if (message.Contains("warning"))
        {
            Console.WriteLine($"Subscriber B received warning message: {message}");
            callback($"Processed by Subscriber B: {message}");
        }
    }
}

// 订阅者 C 类
public class SubscriberC
{
    public void HandleErrorMessage(string message, Action<string> callback)
    {
        if (message.Contains("error"))
        {
            Console.WriteLine($"Subscriber C received error message: {message}");
            callback($"Processed by Subscriber C: {message}");
        }
    }
}

// 中间件类
public class Middleware
{
    private readonly MessagePublisher _publisher;
    private readonly MessageHandler _handler;

    public Middleware(MessagePublisher publisher, MessageHandler handler)
    {
        _publisher = publisher;
        _handler = handler;
        _publisher.OnMessagePublished += ProcessMessage;
    }

    private void ProcessMessage(string message, Action<string> callback)
    {
        if (ShouldForwardMessage(message))
        {
            Console.WriteLine($"Middleware forwarding message: {message}");
            _handler(message, callback);
        }
        else
        {
            Console.WriteLine($"Middleware filtered out message: {message}");
        }
    }

    private bool ShouldForwardMessage(string message)
    {
        // 示例过滤条件：只转发包含 "important" 或 "urgent" 的消息
        return message.Contains("important") || message.Contains("urgent");
    }
}
#endif
#if false
BankAccount account = new BankAccount();

Thread t1 = new Thread(() => account.Deposit(100));
Thread t2 = new Thread(() => account.Withdraw(50));
Thread t3 = new Thread(() => account.Withdraw(70));
Thread t4 = new Thread(() => account.Deposit(200));
Thread t5 = new Thread(() => account.Withdraw(100));

t1.Start();
t2.Start();
t3.Start();
t4.Start();
t5.Start();

t1.Join();
t2.Join();
t3.Join();
t4.Join();
t5.Join();

Console.WriteLine($"Final balance: {account.GetBalance():C}");
public class BankAccount
{
    private decimal balance = 0;
    private readonly Semaphore semaphore = new Semaphore(2, 2); // Allow up to 2 threads

    public void Deposit(decimal amount)
    {
        semaphore.WaitOne();
        try
        {
            Console.WriteLine($"Depositing {amount:C}...");
            Thread.Sleep(50); // Simulate some processing time
            balance += amount;
            Console.WriteLine($"Deposit complete. New balance: {balance:C}");
        }
        finally
        {
            semaphore.Release();
        }
    }

    public void Withdraw(decimal amount)
    {
        semaphore.WaitOne();
        try
        {
            Console.WriteLine($"Withdrawing {amount:C}...");
            Thread.Sleep(50); // Simulate some processing time
            if (balance >= amount)
            {
                balance -= amount;
                Console.WriteLine($"Withdrawal complete. New balance: {balance:C}");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }
        finally
        {
            semaphore.Release();
        }
    }

    public decimal GetBalance()
    {
        return balance;
    }
}
#endif
#if false
BankAccount account = new BankAccount();

Thread t1 = new Thread(() => account.Deposit(100));
Thread t2 = new Thread(() => account.Withdraw(50));
Thread t3 = new Thread(() => account.Withdraw(70));

t1.Start();
t2.Start();
t3.Start();

t1.Join();
t2.Join();
t3.Join();

Console.WriteLine($"Final balance: {account.GetBalance():C}");
public class BankAccount
{
    private decimal balance = 0;
    private readonly Mutex mutex = new Mutex(true);

    public void Deposit(decimal amount)
    {
        mutex.WaitOne();
        try
        {
            Console.WriteLine($"Depositing {amount:C}...");
            Thread.Sleep(50); // Simulate some processing time
            balance += amount;
            Console.WriteLine($"Deposit complete. New balance: {balance:C}");
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }

    public void Withdraw(decimal amount)
    {
        mutex.WaitOne();
        try
        {
            Console.WriteLine($"Withdrawing {amount:C}...");
            Thread.Sleep(50); // Simulate some processing time
            if (balance >= amount)
            {
                balance -= amount;
                Console.WriteLine($"Withdrawal complete. New balance: {balance:C}");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }

    public decimal GetBalance()
    {
        return balance;
    }
}
#endif
#if false
DataFetcher fetcher = new DataFetcher();
// URLs to fetch data from
string[] urls = {
            "https://jsonplaceholder.typicode.com/posts/1",
            "https://jsonplaceholder.typicode.com/comments?postId=1",
            "https://jsonplaceholder.typicode.com/photos/1"
        };

// Create tasks for each URL
Task<string>[] tasks = new Task<string>[urls.Length];
for (int i = 0; i < urls.Length; i++)
{
    tasks[i] = fetcher.FetchDataAsync(urls[i]);
}

// Wait for all tasks to complete
string[] results = await Task.WhenAll(tasks);

// Process the results
for (int i = 0; i < results.Length; i++)
{
    Debug.WriteLine($"Result from {urls[i]}:");
    Debug.WriteLine(results[i]);
}
public class DataFetcher
{
    private readonly HttpClient _httpClient;

    public DataFetcher()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> FetchDataAsync(string url)
    {
        Console.WriteLine($"Fetching data from {url}...");
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string data = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Data fetched from {url}");
        return data;
    }
}
#endif
#if false
FileManager fileManager = new FileManager();
string filePath = "example.txt";
string contentToWrite = "Hello, this is an example of asynchronous file operations.";

// Write to file asynchronously
await fileManager.WriteToFileAsync(filePath, contentToWrite);

// Read from file asynchronously
string contentRead = await fileManager.ReadFromFileAsync(filePath);

// Output the read content
Console.WriteLine("Content read from file:");
Console.WriteLine(contentRead);
public class FileManager
{
    public async Task WriteToFileAsync(string filePath, string content)
    {
        Console.WriteLine($"Writing to file {filePath}...");
        byte[] encodedText = Encoding.Unicode.GetBytes(content);
        using (FileStream sourceStream = new FileStream(filePath,
            FileMode.Create, FileAccess.Write, FileShare.None,
            bufferSize: 4096, useAsync: true))
        {
            await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
        }
        Console.WriteLine($"Write to file {filePath} completed.");
    }

    public async Task<string> ReadFromFileAsync(string filePath)
    {
        Console.WriteLine($"Reading from file {filePath}...");
        StringBuilder sb = new StringBuilder();
        byte[] buffer = new byte[4096];
        int bytesRead;
        using (FileStream sourceStream = new FileStream(filePath,
            FileMode.Open, FileAccess.Read, FileShare.Read,
            bufferSize: 4096, useAsync: true))
        {
            while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                sb.Append(Encoding.Unicode.GetString(buffer, 0, bytesRead));
            }
        }
        Console.WriteLine($"Read from file {filePath} completed.");
        return sb.ToString();
    }
}
#endif
FileManager fileManager = new FileManager();
string filePath = "example.txt";
string contentToWrite = "Hello, this is an example of asynchronous file operations.";

// Write to file asynchronously
await fileManager.WriteToFileAsync(filePath, contentToWrite);

// Read from file asynchronously
string contentRead = await fileManager.ReadFromFileAsync(filePath);

// Output the read content
Console.WriteLine("Content read from file:");
Console.WriteLine(contentRead);
public class FileManager
{
    public async Task WriteToFileAsync(string filePath, string content)
    {
        Console.WriteLine($"Writing to file {filePath}...");
        byte[] encodedText = Encoding.Unicode.GetBytes(content);
        using (FileStream sourceStream = new FileStream(filePath,
            FileMode.Create, FileAccess.Write, FileShare.None,
            bufferSize: 4096, useAsync: true))
        {
            await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
        }
        Console.WriteLine($"Write to file {filePath} completed.");
    }

    public async Task<string> ReadFromFileAsync(string filePath)
    {
        Console.WriteLine($"Reading from file {filePath}...");
        StringBuilder sb = new StringBuilder();
        byte[] buffer = new byte[4096];
        int bytesRead;
        using (FileStream sourceStream = new FileStream(filePath,
            FileMode.Open, FileAccess.Read, FileShare.Read,
            bufferSize: 4096, useAsync: true))
        {
            while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                sb.Append(Encoding.Unicode.GetString(buffer, 0, bytesRead));
            }
        }
        Console.WriteLine($"Read from file {filePath} completed.");
        return sb.ToString();
    }
}