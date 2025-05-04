using System;

namespace Yannick.Lang
{
    /// <summary>
    /// Provides functionality for executing actions under different try-catch-finally patterns.
    /// </summary>
    public static class AS
    {
        /// <summary>
        /// Provides methods for running actions in a try block, optionally ignoring exceptions.
        /// </summary>
        public static class Try
        {
            /// <summary>
            /// Executes the specified <paramref name="method"/> in a try block, ignoring any exceptions that occur.
            /// </summary>
            /// <param name="method">The action to execute.</param>
            public static void Run(Action method)
            {
                try
                {
                    method?.Invoke();
                }
                catch
                {
                    // Exception ignored or logged as needed
                }
            }

            /// <summary>
            /// Executes the specified <paramref name="method"/> with parameter <paramref name="p1"/> 
            /// in a try block, ignoring any exceptions that occur.
            /// </summary>
            /// <typeparam name="T">The type of the parameter passed to <paramref name="method"/>.</typeparam>
            /// <param name="method">The action to execute that takes one parameter of type <typeparamref name="T"/>.</param>
            /// <param name="p1">The parameter to pass into <paramref name="method"/>.</param>
            public static void Run<T>(Action<T> method, T p1)
            {
                try
                {
                    method?.Invoke(p1);
                }
                catch
                {
                    // Exception ignored or logged as needed
                }
            }

            /// <summary>
            /// Creates an <see cref="Action"/> that wraps the specified <paramref name="method"/> 
            /// in a try block, ignoring any exceptions that occur when invoked.
            /// </summary>
            /// <param name="method">The action to wrap in a try block.</param>
            /// <returns>An <see cref="Action"/> that executes <paramref name="method"/> in a try block.</returns>
            public static Action Block(Action method)
            {
                return () =>
                {
                    try
                    {
                        method?.Invoke();
                    }
                    catch
                    {
                        // Exception ignored or logged as needed
                    }
                };
            }

            /// <summary>
            /// Executes the specified <paramref name="func"/> in a try block and returns its result.
            /// If an exception occurs, the default value of <typeparamref name="TResult"/> is returned.
            /// </summary>
            /// <typeparam name="TResult">The return type of the function.</typeparam>
            /// <param name="func">The function to execute.</param>
            /// <param name="defaultValue">the default value where exeption is throw default is default</param>
            /// <returns>
            /// The result of <paramref name="func"/>, or the default value of <typeparamref name="TResult"/> if an exception occurs.
            /// </returns>
            public static TResult? Run<TResult>(Func<TResult> func, TResult? defaultValue = default)
            {
                try
                {
                    return func();
                }
                catch
                {
                    return defaultValue;
                }
            }

            /// <summary>
            /// Executes the specified <paramref name="func"/> with parameter <paramref name="p1"/> in a try block 
            /// and returns its result. If an exception occurs, the default value of <typeparamref name="TResult"/> is returned.
            /// </summary>
            /// <typeparam name="T">The type of the parameter passed to <paramref name="func"/>.</typeparam>
            /// <typeparam name="TResult">The return type of the function.</typeparam>
            /// <param name="func">The function to execute that takes a parameter of type <typeparamref name="T"/>.</param>
            /// <param name="p1">The parameter passed into <paramref name="func"/>.</param>
            /// <returns>
            /// The result of <paramref name="func"/> with <paramref name="p1"/>, or the default value of 
            /// <typeparamref name="TResult"/> if an exception occurs.
            /// </returns>
            public static TResult Run<T, TResult>(Func<T, TResult> func, T p1)
            {
                try
                {
                    return func != null ? func(p1) : default;
                }
                catch
                {
                    // Exception ignored or logged as needed
                    return default;
                }
            }

            /// <summary>
            /// Creates a <see cref="Func{TResult}"/> that, when invoked, runs <paramref name="func"/> in a try block 
            /// and returns its result or a specified fallback if an exception occurs.
            /// </summary>
            /// <typeparam name="TResult">The return type of the function.</typeparam>
            /// <param name="func">The function to wrap in a try block.</param>
            /// <param name="fallback">The fallback value to return in case of an exception.</param>
            /// <returns>A function that returns the result of <paramref name="func"/> or <paramref name="fallback"/> on exception.</returns>
            public static Func<TResult> Block<TResult>(Func<TResult> func, TResult fallback = default)
            {
                return () =>
                {
                    try
                    {
                        return func != null ? func() : fallback;
                    }
                    catch
                    {
                        // Exception ignored or logged as needed
                        return fallback;
                    }
                };
            }
        }

        /// <summary>
        /// Provides methods for running actions in a try-catch block.
        /// </summary>
        public static class TryAndCatch
        {
            /// <summary>
            /// Executes the specified <paramref name="tryAction"/> in a try block. 
            /// If an exception of type <typeparamref name="TException"/> occurs, 
            /// the <paramref name="catchAction"/> is invoked.
            /// </summary>
            /// <typeparam name="TException">The type of exception to catch.</typeparam>
            /// <param name="tryAction">The action to attempt in the try block.</param>
            /// <param name="catchAction">
            /// The action invoked if an exception of type <typeparamref name="TException"/> is thrown.
            /// </param>
            public static void Run<TException>(Action tryAction, Action<TException> catchAction)
                where TException : Exception
            {
                try
                {
                    tryAction?.Invoke();
                }
                catch (TException ex)
                {
                    catchAction?.Invoke(ex);
                }
            }

            /// <summary>
            /// Executes the specified <paramref name="tryAction"/> in a try block. 
            /// If any exception occurs, the <paramref name="catchAction"/> is invoked.
            /// </summary>
            /// <param name="tryAction">The action to attempt in the try block.</param>
            /// <param name="catchAction">The action invoked if any exception is thrown.</param>
            public static void Run(Action tryAction, Action<Exception> catchAction)
            {
                try
                {
                    tryAction?.Invoke();
                }
                catch (Exception ex)
                {
                    catchAction?.Invoke(ex);
                }
            }

            /// <summary>
            /// Executes the specified <paramref name="tryFunc"/> in a try block and returns its result.
            /// If an exception of type <typeparamref name="TException"/> occurs, 
            /// <paramref name="catchFunc"/> is invoked to produce a fallback result.
            /// </summary>
            /// <typeparam name="TException">The type of exception to catch.</typeparam>
            /// <typeparam name="TResult">The return type of the function.</typeparam>
            /// <param name="tryFunc">The function to attempt in the try block.</param>
            /// <param name="catchFunc">The function to produce a fallback value when <typeparamref name="TException"/> is thrown.</param>
            /// <returns>The result of <paramref name="tryFunc"/>, or the fallback from <paramref name="catchFunc"/>.</returns>
            public static TResult Run<TException, TResult>(Func<TResult> tryFunc, Func<TException, TResult> catchFunc)
                where TException : Exception
            {
                try
                {
                    return tryFunc != null ? tryFunc() : default;
                }
                catch (TException ex)
                {
                    return catchFunc != null ? catchFunc(ex) : default;
                }
            }

            /// <summary>
            /// Executes the specified <paramref name="tryFunc"/> in a try block and returns its result.
            /// If any exception occurs, <paramref name="catchFunc"/> is invoked to produce a fallback result.
            /// </summary>
            /// <typeparam name="TResult">The return type of the function.</typeparam>
            /// <param name="tryFunc">The function to attempt in the try block.</param>
            /// <param name="catchFunc">The function to produce a fallback value when any exception is thrown.</param>
            /// <returns>The result of <paramref name="tryFunc"/>, or the fallback from <paramref name="catchFunc"/>.</returns>
            public static TResult Run<TResult>(Func<TResult> tryFunc, Func<Exception, TResult> catchFunc)
            {
                try
                {
                    return tryFunc != null ? tryFunc() : default;
                }
                catch (Exception ex)
                {
                    return catchFunc != null ? catchFunc(ex) : default;
                }
            }

            /// <summary>
            /// Example of supporting multiple catch blocks. Tries <paramref name="tryAction"/>, 
            /// catches either <typeparamref name="TException1"/> or <typeparamref name="TException2"/>, 
            /// or rethrows other exceptions.
            /// </summary>
            /// <typeparam name="TException1">The first type of exception to catch.</typeparam>
            /// <typeparam name="TException2">The second type of exception to catch.</typeparam>
            /// <param name="tryAction">The action to attempt in the try block.</param>
            /// <param name="catchAction1">Action to invoke if a <typeparamref name="TException1"/> occurs.</param>
            /// <param name="catchAction2">Action to invoke if a <typeparamref name="TException2"/> occurs.</param>
            public static void Run<TException1, TException2>(
                Action tryAction,
                Action<TException1> catchAction1,
                Action<TException2> catchAction2
            )
                where TException1 : Exception
                where TException2 : Exception
            {
                try
                {
                    tryAction?.Invoke();
                }
                catch (TException1 ex)
                {
                    catchAction1?.Invoke(ex);
                }
                catch (TException2 ex)
                {
                    catchAction2?.Invoke(ex);
                }
            }
        }

        /// <summary>
        /// Provides methods for running actions in a try-finally block.
        /// </summary>
        public static class TryAndFinally
        {
            /// <summary>
            /// Executes the specified <paramref name="tryAction"/> in a try block, 
            /// and always executes the <paramref name="finallyAction"/> afterward, 
            /// regardless of whether an exception is thrown.
            /// </summary>
            /// <param name="tryAction">The action to attempt in the try block.</param>
            /// <param name="finallyAction">The action to perform after <paramref name="tryAction"/> completes or fails.</param>
            public static void Run(Action tryAction, Action finallyAction)
            {
                try
                {
                    tryAction?.Invoke();
                }
                finally
                {
                    finallyAction?.Invoke();
                }
            }


            /// <summary>
            /// Executes the specified <paramref name="tryFunc"/> in a try block, returns its result, 
            /// and always executes the <paramref name="finallyAction"/> afterward, 
            /// regardless of whether an exception is thrown. 
            /// The exception (if any) is not caught here; it is propagated.
            /// </summary>
            /// <typeparam name="TResult">The return type of the function.</typeparam>
            /// <param name="tryFunc">The function to attempt in the try block.</param>
            /// <param name="finallyAction">The action to perform after the try block, regardless of success or exception.</param>
            /// <returns>The result of the <paramref name="tryFunc"/>.</returns>
            public static TResult Run<TResult>(Func<TResult> tryFunc, Action finallyAction)
            {
                try
                {
                    return tryFunc != null ? tryFunc() : default;
                }
                finally
                {
                    finallyAction?.Invoke();
                }
            }

            /// <summary>
            /// Asynchronously executes the specified <paramref name="tryAction"/> in a try block,
            /// and always executes the synchronous <paramref name="finallyAction"/> afterward,
            /// regardless of whether an exception is thrown.
            /// </summary>
            /// <param name="tryAction">The asynchronous action to attempt in the try block.</param>
            /// <param name="finallyAction">The synchronous action to perform after <paramref name="tryAction"/> completes or fails.</param>
            /// <returns>A task that represents the asynchronous operation.</returns>
            public static async Task RunAsync(Func<Task> tryAction, Action finallyAction)
            {
                ArgumentNullException.ThrowIfNull(tryAction);

                try
                {
                    await tryAction().ConfigureAwait(false);
                }
                finally
                {
                    finallyAction?.Invoke();
                }
            }

            /// <summary>
            /// Creates an <see cref="Action"/> that, when invoked, executes the specified <paramref name="tryAction"/>
            /// in a try block, and always executes the <paramref name="finallyAction"/> afterward, 
            /// regardless of whether an exception is thrown.
            /// </summary>
            /// <param name="tryAction">The action to attempt in the try block.</param>
            /// <param name="finallyAction">The action to perform after <paramref name="tryAction"/> completes or fails.</param>
            /// <returns>An <see cref="Action"/> wrapping the logic in a try-finally block.</returns>
            public static Action Block(Action tryAction, Action finallyAction)
            {
                return () =>
                {
                    try
                    {
                        tryAction?.Invoke();
                    }
                    finally
                    {
                        finallyAction?.Invoke();
                    }
                };
            }
        }

        /// <summary>
        /// Provides methods for running actions in a try-catch-finally block.
        /// </summary>
        public static class TryAndCatchAndFinally
        {
            /// <summary>
            /// Executes the specified <paramref name="tryAction"/> in a try block. 
            /// If an exception of type <typeparamref name="TException"/> occurs, 
            /// the <paramref name="catchAction"/> is invoked. 
            /// The <paramref name="finallyAction"/> is always executed afterward.
            /// </summary>
            /// <typeparam name="TException">The type of exception to catch.</typeparam>
            /// <param name="tryAction">The action to attempt in the try block.</param>
            /// <param name="catchAction">
            /// The action invoked if an exception of type <typeparamref name="TException"/> is thrown.
            /// </param>
            /// <param name="finallyAction">The action to perform after the try/catch blocks complete.</param>
            public static void Run<TException>(
                Action tryAction,
                Action<TException> catchAction,
                Action finallyAction
            )
                where TException : Exception
            {
                try
                {
                    tryAction?.Invoke();
                }
                catch (TException ex)
                {
                    catchAction?.Invoke(ex);
                }
                finally
                {
                    finallyAction?.Invoke();
                }
            }

            /// <summary>
            /// Executes the specified <paramref name="tryAction"/> in a try block. 
            /// If any exception occurs, the <paramref name="catchAction"/> is invoked. 
            /// The <paramref name="finallyAction"/> is always executed afterward.
            /// </summary>
            /// <param name="tryAction">The action to attempt in the try block.</param>
            /// <param name="catchAction">The action invoked if an exception is thrown.</param>
            /// <param name="finallyAction">The action to perform after the try/catch blocks complete.</param>
            public static void Run(
                Action tryAction,
                Action<Exception> catchAction,
                Action finallyAction
            )
            {
                try
                {
                    tryAction?.Invoke();
                }
                catch (Exception ex)
                {
                    catchAction?.Invoke(ex);
                }
                finally
                {
                    finallyAction?.Invoke();
                }
            }

            /// <summary>
            /// Executes the specified <paramref name="tryFunc"/> in a try block and returns its result.
            /// If an exception of type <typeparamref name="TException"/> occurs, 
            /// the <paramref name="catchFunc"/> is invoked. 
            /// The <paramref name="finallyAction"/> is always executed afterward.
            /// </summary>
            /// <typeparam name="TException">The type of exception to catch.</typeparam>
            /// <typeparam name="TResult">The return type of the function.</typeparam>
            /// <param name="tryFunc">The function to attempt in the try block.</param>
            /// <param name="catchFunc">The function to produce a fallback value if <typeparamref name="TException"/> is thrown.</param>
            /// <param name="finallyAction">The action to perform after the try/catch blocks complete.</param>
            /// <returns>The result of <paramref name="tryFunc"/> or the fallback from <paramref name="catchFunc"/>.</returns>
            public static TResult Run<TException, TResult>(
                Func<TResult> tryFunc,
                Func<TException, TResult> catchFunc,
                Action finallyAction
            )
                where TException : Exception
            {
                try
                {
                    return tryFunc != null ? tryFunc() : default;
                }
                catch (TException ex)
                {
                    return catchFunc != null ? catchFunc(ex) : default;
                }
                finally
                {
                    finallyAction?.Invoke();
                }
            }

            /// <summary>
            /// Executes the specified <paramref name="tryFunc"/> in a try block and returns its result.
            /// If any exception occurs, the <paramref name="catchFunc"/> is invoked. 
            /// The <paramref name="finallyAction"/> is always executed afterward.
            /// </summary>
            /// <typeparam name="TResult">The return type of the function.</typeparam>
            /// <param name="tryFunc">The function to attempt in the try block.</param>
            /// <param name="catchFunc">The function to produce a fallback value if any exception is thrown.</param>
            /// <param name="finallyAction">The action to perform after the try/catch blocks complete.</param>
            /// <returns>The result of <paramref name="tryFunc"/> or the fallback from <paramref name="catchFunc"/>.</returns>
            public static TResult Run<TResult>(
                Func<TResult> tryFunc,
                Func<Exception, TResult> catchFunc,
                Action finallyAction
            )
            {
                try
                {
                    return tryFunc != null ? tryFunc() : default;
                }
                catch (Exception ex)
                {
                    return catchFunc != null ? catchFunc(ex) : default;
                }
                finally
                {
                    finallyAction?.Invoke();
                }
            }

            /// <summary>
            /// Example of a multiple-catch scenario in try-catch-finally, 
            /// catching two distinct exception types and having a final fallback for others.
            /// </summary>
            /// <typeparam name="TException1">The first exception type to catch.</typeparam>
            /// <typeparam name="TException2">The second exception type to catch.</typeparam>
            /// <param name="tryAction">The action attempted in the try block.</param>
            /// <param name="catchAction1">The action if <typeparamref name="TException1"/> is caught.</param>
            /// <param name="catchAction2">The action if <typeparamref name="TException2"/> is caught.</param>
            /// <param name="catchAllAction">The action if an exception other than <typeparamref name="TException1"/> or <typeparamref name="TException2"/> is thrown.</param>
            /// <param name="finallyAction">The action to always execute after the try/catch blocks.</param>
            public static void Run<TException1, TException2>(
                Action tryAction,
                Action<TException1> catchAction1,
                Action<TException2> catchAction2,
                Action<Exception> catchAllAction,
                Action finallyAction
            )
                where TException1 : Exception
                where TException2 : Exception
            {
                try
                {
                    tryAction?.Invoke();
                }
                catch (TException1 ex)
                {
                    catchAction1?.Invoke(ex);
                }
                catch (TException2 ex)
                {
                    catchAction2?.Invoke(ex);
                }
                catch (Exception ex)
                {
                    catchAllAction?.Invoke(ex);
                }
                finally
                {
                    finallyAction?.Invoke();
                }
            }
        }
    }
}