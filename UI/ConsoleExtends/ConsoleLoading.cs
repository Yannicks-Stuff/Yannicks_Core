using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yannick.Extensions.IListExtensions;
using Yannick.Extensions.ObjectExtensions;

namespace Yannick.UI
{
    public partial class Console
    {
        public static class LoadingScreen
        {
            /// <summary>
            /// Status of the task.
            /// </summary>
            public enum TaskStatus
            {
                Pending,
                Running,
                Completed,
                Faulted
            }

            private static bool _isLoading = false;
            private static Thread _loadingThread;
            private static object _lock = new object();

            private static List<LoadingTask> _tasks = new List<LoadingTask>();
            private static int _totalTasks = 0;
            private static int _completedTasks = 0;

            private static ConcurrentQueue<LoadingTask> _taskQueue = new ConcurrentQueue<LoadingTask>();
            private static List<Task> _runningTasks = new List<Task>();
            private static SemaphoreSlim _semaphore;
            private static Task _processingTask;

            /// <summary>
            /// Sets the maximum number of concurrent tasks.
            /// Use -1 or less for unlimited concurrency.
            /// </summary>
            public static int MaxConcurrency { get; set; } = -1; // Default is unlimited

            // Events
            public static event Action Started;
            public static event Action Stopped;
            public static event Action<LoadingTask> TaskAdded;
            public static event Action<LoadingTask> TaskStarted;
            public static event Action<LoadingTask> TaskCompleted;

            /// <summary>
            /// Starts the loading screen and begins processing tasks.
            /// </summary>
            /// <param name="initialTasks">Optional list of initial tasks to add.</param>
            public static void Start(List<LoadingTask> initialTasks = null)
            {
                if (_isLoading)
                    return;

                // Initialize the semaphore based on MaxConcurrency
                if (MaxConcurrency <= 0)
                {
                    _semaphore = new SemaphoreSlim(int.MaxValue); // Unlimited concurrency
                }
                else
                {
                    _semaphore = new SemaphoreSlim(MaxConcurrency);
                }

                if (initialTasks != null)
                {
                    foreach (var task in initialTasks)
                    {
                        AddTask(task);
                    }
                }

                _isLoading = true;
                Started?.Invoke();

                // Start processing tasks
                _processingTask = Task.Run(() => ProcessTasks());

                // Start the loading screen display update
                _loadingThread = new Thread(() =>
                {
                    while (_isLoading)
                    {
                        lock (_lock)
                        {
                            UpdateDisplay();
                        }

                        Thread.Sleep(500); // Update every 500ms
                    }
                });

                _loadingThread.Start();
            }

            /// <summary>
            /// Stops the loading screen and waits for all tasks to complete.
            /// </summary>
            public static void Stop()
            {
                if (!_isLoading)
                    return;

                _isLoading = false;

                // Wait for the processing tasks to finish
                _processingTask.Wait();

                _loadingThread.Join();

                lock (_lock)
                {
                    ClearDisplay();
                    Stopped?.Invoke();
                }
            }

            /// <summary>
            /// Adds a new task to the queue.
            /// </summary>
            /// <param name="task">The task to add.</param>
            public static void AddTask(LoadingTask task)
            {
                lock (_lock)
                {
                    _tasks.Add(task);
                    _totalTasks++;
                    _taskQueue.Enqueue(task);
                    TaskAdded?.Invoke(task);
                }
            }

            private static async Task ProcessTasks()
            {
                while (_isLoading || !_taskQueue.IsEmpty)
                {
                    if (_taskQueue.TryDequeue(out var task))
                    {
                        await _semaphore.WaitAsync();

                        lock (_lock)
                        {
                            task.Status = TaskStatus.Running;
                            task.StartTime = DateTime.Now;
                            TaskStarted?.Invoke(task);
                        }

                        var taskRunner = Task.Run(async () =>
                        {
                            try
                            {
                                await task.Work();
                            }
                            catch (Exception)
                            {
                                lock (_lock)
                                {
                                    task.Status = TaskStatus.Faulted;
                                }
                            }
                            finally
                            {
                                lock (_lock)
                                {
                                    task.Status = TaskStatus.Completed;
                                    task.EndTime = DateTime.Now;
                                    _completedTasks++;
                                    TaskCompleted?.Invoke(task);
                                }

                                _semaphore.Release();
                            }
                        });

                        lock (_runningTasks)
                            _runningTasks.Add(taskRunner);

                        lock (_runningTasks)
                            _runningTasks.RemoveAll(t => t.IsCompleted);
                    }
                    else
                    {
                        await Task.Delay(100);
                    }
                }

                await Task.WhenAll(_runningTasks.ApplyTo<Task[], List<Task>>(e =>
                {
                    lock (_runningTasks)
                        return e.ToArray(_runningTasks);
                }));
            }

            private static void UpdateDisplay()
            {
                var eta = CalculateETA();

                var sb = new System.Text.StringBuilder();
                sb.AppendLine($"Tasks: {_completedTasks}/{_totalTasks} Completed - ETA: {FormatTimeSpan(eta)}");
                foreach (var task in _tasks)
                    sb.AppendLine($"{GetStatusSymbol(task.Status)} {task.Name}");

                ClearDisplay();

                System.Console.Write(sb.ToString());
            }

            private static void ClearDisplay()
            {
                var linesToClear = _tasks.Count + 1;
                for (var i = 0; i < linesToClear; i++)
                {
                    System.Console.SetCursorPosition(0, System.Console.CursorTop - 1);
                    System.Console.Write(new string(' ', System.Console.BufferWidth));
                }

                System.Console.SetCursorPosition(0, System.Console.CursorTop - linesToClear + 1);
            }

            private static TimeSpan CalculateETA()
            {
                var completedTasks = _tasks.FindAll(t => t.Status == TaskStatus.Completed);
                if (completedTasks.Count == 0)
                {
                    return TimeSpan.Zero;
                }

                double totalTime = 0;
                foreach (var task in completedTasks)
                {
                    if (task is { StartTime: not null, EndTime: not null })
                    {
                        totalTime += (task.EndTime.Value - task.StartTime.Value).TotalSeconds;
                    }
                }

                var averageTimePerTask = totalTime / completedTasks.Count;

                var remainingTasks = _totalTasks - _completedTasks;
                var estimatedTotalSeconds = averageTimePerTask * remainingTasks;

                return TimeSpan.FromSeconds(estimatedTotalSeconds);
            }

            private static string FormatTimeSpan(TimeSpan timeSpan)
            {
                return timeSpan == TimeSpan.Zero ? "--:--" : $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            }

            private static string GetStatusSymbol(TaskStatus status)
            {
                return status switch
                {
                    TaskStatus.Pending => "[ ]",
                    TaskStatus.Running => "[>]",
                    TaskStatus.Completed => "[✓]",
                    TaskStatus.Faulted => "[X]",
                    _ => "[ ]"
                };
            }

            /// <summary>
            /// Represents a task in the loading screen.
            /// </summary>
            public class LoadingTask
            {
                public LoadingTask(string name, Func<Task> work)
                {
                    Name = name;
                    Work = work;
                    Status = TaskStatus.Pending;
                }

                public string Name { get; }
                public DateTime? StartTime { get; set; }
                public DateTime? EndTime { get; set; }
                public TaskStatus Status { get; set; }
                public Func<Task> Work { get; }
            }
        }
    }
}