using System;

namespace MonadSharp.Continuation
{
  /// <summary>
  /// Continuation monad implementation for C# types
  /// </summary>
  public static class ContinuationExtension
  {
    /// <summary>
    /// Add second action to the continuation for the first action
    /// </summary>
    /// <param name="baseAction">First action</param>
    /// <param name="nextAction">Continuation action</param>
    /// <typeparam name="TInput">Input type</typeparam>
    /// <returns>Action that represent base action with nextAction as continuation</returns>
    public static Action<TInput> ContinueWith<TInput>(this Action<TInput> baseAction, Action<TInput> nextAction)
    {
      return input =>
        {
          baseAction.Invoke(input);
          nextAction.Invoke(input);
        };
    }

    /// <summary>
    /// Continuation monad for function
    /// </summary>
    /// <param name="baseFunction">Base function</param>
    /// <param name="continueFunction">Function for continuation</param>
    /// <typeparam name="TInput">Input type for base function</typeparam>
    /// <typeparam name="TContinue">Return type for base function</typeparam>
    /// <typeparam name="TResult">Continuation function result</typeparam>
    /// <returns>Function for represent chain of functions</returns>
    public static Func<TInput, TResult> ContinueWith<TInput, TContinue, TResult>(
      this Func<TInput, TContinue> baseFunction, Func<TContinue, TResult> continueFunction)
    {
      return input =>
        {
          var result = baseFunction(input);
          return continueFunction(result);
        };
    }

    /// <summary>
    /// Select maybe implementation for continuation monad
    /// </summary>
    /// <param name="baseFunction">Base function</param>
    /// <param name="continueFunction">Continuatoin function</param>
    /// <param name="resultSelector">Function for creating computation result</param>
    /// <typeparam name="TInput">Input arguments type</typeparam>
    /// <typeparam name="TContinue">Continuation return type</typeparam>
    /// <typeparam name="TResult">Result type</typeparam>
    /// <returns></returns>
    public static Func<TInput, TResult> SelectMany<TInput, TContinue, TResult>(
      this Func<TInput, TContinue> baseFunction, Func<TInput, Func<TInput,TContinue>> continueFunction,
      Func<TContinue, TContinue, TResult> resultSelector)
    {
      return input =>
        {
          var baseResult = baseFunction(input);
          var continuation = continueFunction(input)(input);
          return resultSelector(baseResult, continuation);
        };
    }
  }
}
