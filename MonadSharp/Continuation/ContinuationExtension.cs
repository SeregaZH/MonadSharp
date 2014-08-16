using System;

namespace MonadSharp.Continuation
{
  /// <summary>
  /// Continuation monad implementation for C# types
  /// </summary>
  public static class ContinuationExtension
  {
    /// <summary>
    /// Unit function for continuation monad
    /// </summary>
    /// <param name="input">value to convert</param>
    /// <typeparam name="TInput">input type</typeparam>
    /// <typeparam name="TResult">result type</typeparam>
    /// <returns>value converted to continuation. Func(TInput, TResult) - monoid</returns>
    public static Func<Func<TInput, TResult>, TResult> ToContinuation<TInput, TResult>(this TInput input)
    {
      return func => func(input);
    }

    /// <summary>
    /// Select many implementation for continuation monad
    /// </summary>
    /// <param name="input">first continuation monoid</param>
    /// <param name="value">second continuation monoid depend on input</param>
    /// <param name="selector">selector</param>
    /// <typeparam name="TInput">first monoid type</typeparam>
    /// <typeparam name="TValue">second monoid type</typeparam>
    /// <typeparam name="TSelect">selector return type</typeparam>
    /// <typeparam name="TResult">result type</typeparam>
    /// <returns>resulted continuation monoid</returns>
    public static Func<Func<TSelect, TResult>, TResult> SelectMany<TInput, TValue, TSelect, TResult>(
             this Func<Func<TInput, TResult>, TResult> input,
                  Func<TInput, Func<Func<TValue, TResult>, TResult>> value,
                  Func<TInput, TValue, TSelect> selector)
    {
      return map => input(a => value(a)(x => map(selector(a, x))));
    }

  }
}
