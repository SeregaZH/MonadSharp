using System;

namespace MonadSharp.Maybe
{
  /// <summary>
  /// Maybe monad implementation
  /// Implementation for class. Nothing value is null
  /// </summary>
  public static class MaybeExtensions
  {
    /// <summary>
    /// Unit function for maybe
    /// </summary>
    /// <param name="value">object to conversion</param>
    /// <typeparam name="TInput">type to convert</typeparam>
    /// <returns>create monoid with initial value</returns>
    public static IMaybeMonoid<TInput> ToMaybe<TInput>(this TInput value)
    {
      return new MaybeMonoid<TInput>(value);
    }

    /// <summary>
    /// Evaluate function if input is not default
    /// </summary>
    /// <param name="oInput">input monoid</param>
    /// <param name="evaluator">monoid value evaluator</param>
    /// <typeparam name="TInput">input evaluator parameter type</typeparam>
    /// <typeparam name="TOutput">output evaluator parameter type</typeparam>>
    /// <returns>Result of evaluation, if input haven't value - nothing</returns>
    public static IMaybeMonoid<TOutput> With<TInput, TOutput>(this IMaybeMonoid<TInput> oInput, Func<TInput, TOutput> evaluator)
    {
      return oInput.HasValue ? evaluator(oInput.Value).ToMaybe() : MaybeMonoid<TOutput>.Nothing();
    }

    /// <summary>
    /// extract value from maybe if maybe hasn't value use default value 
    /// </summary>
    /// <param name="oInput">monoid to extract</param>
    /// <param name="defaultValue">use this value if monoid haven't value</param>
    /// <typeparam name="TOutput">output type</typeparam>
    /// <returns>extracted value</returns>
    public static TOutput Default<TOutput>(this IMaybeMonoid<TOutput> oInput, TOutput defaultValue)
    {
      return oInput.HasValue ? oInput.Value : defaultValue;
    }

    /// <summary>
    /// Evaluate function if monoid has value else return fail value
    /// </summary>
    /// <param name="oInput">input monoid</param>
    /// <param name="evaluator">monoid value evaluator</param>
    /// <param name="failValue">value returned if input haven't value</param>
    /// <typeparam name="TInput">input evaluator parameter type</typeparam>
    /// <typeparam name="TOutput">output evaluator parameter type</typeparam>
    /// <returns>Result of evaluation, if input haven't value - failValue</returns>
    public static IMaybeMonoid<TOutput> Returns<TInput, TOutput>(this IMaybeMonoid<TInput> oInput, Func<TInput, TOutput> evaluator,
                                                   TOutput failValue)
    {
      return oInput.HasValue ? evaluator(oInput.Value).ToMaybe() : failValue.ToMaybe();
    }

    /// <summary>
    /// Check some condition on the maybe chain
    /// </summary>
    /// <param name="oInput">monoid to check</param>
    /// <param name="predicate">check predicate</param>
    /// <typeparam name="TInput">input predicate parameter type</typeparam>
    /// <returns>Input monoid if monoid have value and predicate is truly, else nothing</returns>
    public static IMaybeMonoid<TInput> If<TInput>(this IMaybeMonoid<TInput> oInput, Predicate<TInput> predicate)
    {
      return oInput.HasValue && predicate(oInput.Value)? oInput : MaybeMonoid<TInput>.Nothing();
    }

    /// <summary>
    /// Select Many implementation
    /// </summary>
    /// <param name="oInput">caller source monoid</param>
    /// <param name="selector">factory function for selector</param>
    /// <param name="resultSelector">factory function for result</param>
    /// <typeparam name="TSource">Generic type (class constraint)</typeparam>
    /// <typeparam name="TResult">Generic type (class constraint)</typeparam>
    /// <typeparam name="TSelect">Generic type (class constraint)</typeparam>
    /// <returns>Result of select function, only if bouth monoids nave value, else return nothing</returns>
    public static IMaybeMonoid<TResult> SelectMany<TSource, TSelect, TResult>(this IMaybeMonoid<TSource> oInput, Func<TSource, IMaybeMonoid<TSelect>> selector, Func<TSource, TSelect, TResult> resultSelector)
      where TResult : class 
      where TSource : class
      where TSelect : class 
    {
      if (oInput.HasValue)
      {
        var selected = selector(oInput.Value);
        if (selected.HasValue)
          return resultSelector(oInput.Value, selected.Value).ToMaybe();
      }
      return MaybeMonoid<TResult>.Nothing();
    }
  }
}
