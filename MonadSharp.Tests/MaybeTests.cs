using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonadSharp.Maybe;

//this tests can be used as library usage examples
namespace MonadSharp.Tests
{
  [TestClass]
  public class MaybeTests
  {
    [TestMethod]
    public void TestWithForNullReferenceReferenceTypeOutput()
    {
      TestWithFake target = null;
      var result = target.ToMaybe().With(m => m.StringValue).Value;
      Assert.IsNull(result);
    }

    [TestMethod]
    public void TestWithForNullReferenceValueTypeOutput()
    {
      TestWithFake target = null;
      var result = target.ToMaybe().With(m => m.IntValue).Value;
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void TestWithForNullReferenceNullableTypeOutput()
    {
      TestWithFake target = null;
      var result = target.ToMaybe().With(m => m.NullableIntValue).Value;
      Assert.IsNull(result);
    }

    [TestMethod]
    public void TestWithForNullReferenceValueTypeWithMethodCallReferenceOutput()
    {
      TestWithFake target = null;
      var result = target
        .ToMaybe()
        .With(m => m.IntValue)
        .With(m=>m.ToString())
        .Value;
      Assert.IsNull(result);
    }

    [TestMethod]
    public void TestReturnsForNullReferenceReferenceTypeOutput()
    {
      TestWithFake target = null;
      var result = target.ToMaybe().Returns(m => m.StringValue, "fail").Value;
      Assert.AreEqual("fail",result);
    }

    [TestMethod]
    public void TestReturnsForNullReferenceValueTypeOutput()
    {
      TestWithFake target = null;
      var result = target.ToMaybe().Returns(m => m.IntValue, 42).Value;
      Assert.AreEqual(42, result);
    }

    [TestMethod]
    public void TestReturnsForNullReferenceNullableTypeOutput()
    {
      TestWithFake target = null;
      var result = target.ToMaybe().Returns(m => m.NullableIntValue, 12).Value;
      Assert.AreEqual(12, result);
    }

    [TestMethod]
    public void TestIfForValidCriteria()
    {
      TestWithFake target = new TestWithFake
      {
        IntValue = 100
      };
      var result = target.ToMaybe().If(t => t.IntValue == 100).Value;
      Assert.AreEqual(target, result);
    }

    [TestMethod]
    public void TestIfForInValidCriteria()
    {
      TestWithFake target = new TestWithFake
      {
        IntValue = 100
      };
      var result = target.ToMaybe().If(t => t.IntValue != 100).Value;
      Assert.IsNull(result);
    }

    [TestMethod]
    public void TestIfForNullReference()
    {
      TestWithFake target = null;
      var result = target.ToMaybe().If(t => t.IntValue != 100).Value;
      Assert.IsNull(result);
    }

    [TestMethod]
    public void TestWithForNotNullReference()
    {
      TestWithFake target = new TestWithFake
        {
          StringValue = "testValue"
        };
      var result = target.ToMaybe().With(m => m.StringValue).Value;
      Assert.AreEqual("testValue", result);
    }

    [TestMethod]
    public void TestWithForNullableObjectNullReference()
    {
      int? target = null;
      var result = target.ToMaybe().With(m => m.Value).Value;
      Assert.AreEqual(default(int), result);
    }

    [TestMethod]
    public void TestWithForNotNullNullableObject()
    {
      int? target = 100;
      var result = target.ToMaybe().With(m => m.Value).Value;
      Assert.AreEqual(result, 100);
    }

    [TestMethod]
    public void TestDefaultForNullableObjectWithDefaultValue()
    {
      int? target = null;
      var result = target.ToMaybe().Default(100);
      Assert.AreEqual(result, 100);
    }

    [TestMethod]
    public void TestSelectManyForNotNullStrings()
    {
      const string firstTargetString = "testString1";
      const string secondTargetString = "testString2";
      var result = from a in firstTargetString.ToMaybe()
                   from b in secondTargetString.ToMaybe()
                   select a + b;
      Assert.AreEqual("testString1testString2", result.Value);
    }

    [TestMethod]
    public void TestSelectManyForNullStrings()
    {
      const string firstTargetString = null;
      const string secondTargetString = null;
      var result = from a in firstTargetString.ToMaybe()
                   from b in secondTargetString.ToMaybe()
                   select a + b;
      Assert.IsNull(result.Value);
    }
  }

  internal sealed class TestWithFake
  {
    public string StringValue { get; set; }

    public int IntValue { get; set; }

    public int? NullableIntValue { get; set; }
  }
}
