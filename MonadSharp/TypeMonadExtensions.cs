namespace MonadSharp
{
  /// <summary>
  /// Inline methods for work with C# type system
  /// </summary>
  public static class TypeMonadExtensions
  {
    /// <summary>
    /// cast input value to specific type
    /// return null if cast failed
    /// </summary>
    /// <param name="oInput">value to cast</param>
    /// <typeparam name="TInput">source type</typeparam>
    /// <typeparam name="TType">target type</typeparam>
    /// <returns>casted value</returns>
    public static TType As<TInput, TType>(this TInput oInput) 
      where TType : class
      where TInput: class 
    {
      return oInput as TType;
    }
  }
}
