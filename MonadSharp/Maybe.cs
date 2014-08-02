using System;

namespace MonadSharp
{
  /// <summary>
  /// Maybe monad implementation
  /// Implementation for class. Nothing value is null
  /// </summary>
  public static class Maybe
  {
    /// <summary>
    /// Evaluate function if input is not null
    /// </summary>
    /// <param name="oInput">Input object</param>
    /// <param name="evaluator">Function such apply for none null value</param>
    /// <typeparam name="TInput">Generic type (class constraint)</typeparam>
    /// <typeparam name="TOutput">Generic type (class constraint)</typeparam>
    /// <returns>Result of evaluation else null</returns>
    public static TOutput With<TInput, TOutput>(this TInput oInput, Func<TInput, TOutput> evaluator)
      where TInput : class
      where TOutput : class
    {
      return oInput == null ? null : evaluator(oInput);
    }

    /// <summary>
    /// Evaluate function if input is not null for support nullable
    /// </summary>
    /// <typeparam name="TOutput">Value type (struct constraint)</typeparam>
    /// <typeparam name="TInput">Value type (struct constraint)</typeparam>
    /// <param name="oInput">Input nullable value</param>
    /// <param name="evaluator">Function such apply for none null value</param>
    /// <returns>Return nullable value for result</returns>
    public static TOutput? With<TOutput, TInput>(this TInput? oInput, Func<TInput?, TOutput> evaluator)
      where TOutput : struct
      where TInput : struct
    {
      return oInput.HasValue ? evaluator(oInput) : (TOutput?) null;
    }

    /// <summary>
    /// Extract nullable value
    /// Use default value for Value type if input is null
    /// </summary>
    /// <param name="oInput">Nullable type</param>
    /// <typeparam name="TOutput">Value type (struct constraint)</typeparam>
    /// <returns>Extracted none nulable value</returns>
    public static TOutput Default<TOutput>(this TOutput? oInput)
      where TOutput : struct
    {
      return oInput.HasValue ? oInput.Value : default(TOutput);
    }

    /// <summary>
    /// Extract nullable value
    /// Use default value if input is null
    /// </summary>
    /// <param name="oInput">nullable value to extract</param>
    /// <param name="defaultValue">return this value if nullable is null</param>
    /// <typeparam name="TOutput">value type (struct constraint)</typeparam>
    /// <returns>extracted value</returns>
    public static TOutput Default<TOutput>(this TOutput? oInput, TOutput defaultValue)
      where TOutput: struct 
    {
      return oInput.HasValue ? oInput.Value : defaultValue;
    }

    /// <summary>
    /// Evaluate function if input is not null else return default value
    /// </summary>
    /// <param name="oInput">Input value</param>
    /// <param name="evaluator">Function such apply for none null value</param>
    /// <param name="failValue">value returned if input null</param>
    /// <typeparam name="TInput">Generic type (class constraint)</typeparam>
    /// <typeparam name="TOutput">Generic type (class constraint)</typeparam>
    /// <returns>Result of evaluation else fail value</returns>
    public static TOutput Returns<TInput, TOutput>(this TInput oInput, Func<TInput, TOutput> evaluator,
                                                   TOutput failValue)
      where TInput : class
    {
      return oInput == null ? failValue : evaluator(oInput);
    }

    /// <summary>
    /// Evaluate function if input is not null else return default value
    /// support nullable types
    /// </summary>
    /// <param name="oInput">nullable value</param>
    /// <param name="evaluator">Function such apply for none null value</param>
    /// <param name="failValue">value returned if input null</param>
    /// <typeparam name="TOutput">Value type (struct constraint)</typeparam>
    /// <typeparam name="TInput">Value type (struct constraint)</typeparam>
    /// <returns>Result of evaluation else fail value</returns>
    public static TOutput? Returns<TInput, TOutput>(this TInput? oInput, Func<TInput?, TOutput> evaluator,
                                                    TOutput failValue)
      where TInput : struct
      where TOutput : struct
    {
      return oInput.HasValue ? evaluator(oInput) : failValue;
    }
  }
}
