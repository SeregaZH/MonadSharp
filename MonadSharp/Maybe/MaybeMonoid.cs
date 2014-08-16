namespace MonadSharp.Maybe
{
  /// <summary>
  /// Represent simple maybe monoid for reference types
  /// </summary>
  /// <typeparam name="T">Monoid type (class constraint)</typeparam>
  public class MaybeMonoid<T> : IMaybeMonoid<T>
  {
    private readonly bool _hasValue;
    
    /// <summary>
    /// Init monoid with default value
    /// </summary>
    public MaybeMonoid()
    {
      Value = default(T);
      _hasValue = false;
    }

    /// <summary>
    /// Init monoid with some value
    /// </summary>
    /// <param name="value"></param>
    public MaybeMonoid(T value)
    {
      Value = value;
      _hasValue = true;
    }

    /// <summary>
    /// Factory function for construct empty maybe
    /// </summary>
    public static IMaybeMonoid<T> Nothing()
    {
      return new MaybeMonoid<T>(); 
    }

    public T Value { get; private set; }

    public bool HasValue
    {
      get
      {
        return _hasValue 
          && (object) Value != default(object);
      }
    }
  }
}
