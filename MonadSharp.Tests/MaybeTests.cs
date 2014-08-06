using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonadSharp.Maybe;

//this tests can be used as library usage examples
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
    public void TestReturnsForNullReference()
    {
      TestWithFake target = null;
      var result = target.Returns(m => m.StringValue, "fail");
      Assert.AreEqual("fail",result);
    }

    [TestMethod]
    public void TestReturnsForNotNullReference()
    {
      TestWithFake target = new TestWithFake
        {
          StringValue = "testValue"
        };
      var result = target.Returns(m => m.StringValue, "fail");
      Assert.AreEqual("testValue", result);
    }

    [TestMethod]
    public void TestIfForValidCriteria()
    {
      TestWithFake target = new TestWithFake
      {
        IntValue = 100
      };
      var result = target.If(t => t.IntValue == 100);
      Assert.AreEqual(target, result);
    }

    [TestMethod]
    public void TestIfForInValidCriteria()
    {
      TestWithFake target = new TestWithFake
      {
        IntValue = 100
      };
      var result = target.If(t => t.IntValue != 100);
      Assert.IsNull(result);
    }

    [TestMethod]
    public void TestIfForNullReference()
    {
      TestWithFake target = null;
      var result = target.If(t => t.IntValue != 100);
      Assert.IsNull(result);
    }

    [TestMethod]
    public void TestWithForNotNullReference()
    {
      TestWithFake target = new TestWithFake
        {
          StringValue = "testValue"
        };
      var result = target.With(m => m.StringValue);
      Assert.AreEqual("testValue", result);
    }

    [TestMethod]
    public void TestWithForNullableObjectNullReference()
    {
      int? target = null;
      var result = target.With(m => m.Value);
      Assert.IsNull(result);
    }

    [TestMethod]
    public void TestWithForNotNullNullableObject()
    {
      int? target = 100;
      var result = target.With(m => m.Value);
      Assert.AreEqual(result, 100);
    }

    [TestMethod]
    public void TestDefaultForNotNullNullableObject()
    {
      int? target = 100;
      var result = target.Default();
      Assert.AreEqual(result, 100);
    }

    [TestMethod]
    public void TestDefaultForNullNullableObject()
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

    [TestMethod]
    public void TestSelectManyForNotNullStrings()
    {
      const string firstTargetString = "testString1";
      const string secondTargetString = "testString2";
      var result = from a in firstTargetString
                   from b in secondTargetString
                   select a + b;
      Assert.AreEqual("testString1testString2", result);
    }

    [TestMethod]
    public void TestSelectManyForNullStrings()
    {
      const string firstTargetString = null;
      const string secondTargetString = null;
      var result = from a in firstTargetString
                   from b in secondTargetString
                   select a + b;
      Assert.IsNull(result);
    }
  }

  internal sealed class TestWithFake
  {
    public string StringValue { get; set; }

    public int IntValue { get; set; }

    public int? NullableIntValue { get; set; }
  }
}
