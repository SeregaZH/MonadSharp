using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MonadSharp.Tests
{
  [TestClass]
  public class MaybeTests
  {
    [TestMethod]
    public void TestWithForNullReference()
    {
      TestWithFake target = null;
      var result = target.With(m => m.StringValue);
      Assert.IsNull(result);
    }

    [TestMethod]
    public void TestWithForNullableObject()
    {
      int? target = null;
      var result = target.With(m => m.Value);
      Assert.IsNull(result);
    }

    [TestMethod]
    public void TestWithForNoneNullableObject()
    {
      int? target = 100;
      var result = target.With(m => m.Value);
      Assert.AreEqual(result, 100);
    }

    [TestMethod]
    public void TestDefaultForNoneNullableObject()
    {
      int? target = 100;
      var result = target.Default();
      Assert.AreEqual(result, 100);
    }

    [TestMethod]
    public void TestDefaultForNullableObject()
    {
      int? target = null;
      var result = target.Default();
      Assert.AreEqual(result, default(int));
    }

    [TestMethod]
    public void TestDefaultForNullableObjectWithDefaultValue()
    {
      int? target = null;
      var result = target.Default(100);
      Assert.AreEqual(result, 100);
    }
  }

  internal sealed class TestWithFake
  {
    public string StringValue { get; set; }

    public int IntValue { get; set; }

    public int? NullableIntValue { get; set; }
  }
}
