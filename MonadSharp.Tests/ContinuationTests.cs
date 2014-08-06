using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonadSharp.Continuation;

namespace MonadSharp.Tests
{
  [TestClass]
  public class ContinuationTests
  {
    [TestMethod]
    public void TestActionContinueWith()
    {
      var target = new StringBuilder();
      var buildMethod = (new Action<StringBuilder>(f => f.Append("test1")))
        .ContinueWith(s => s.Append("test2"))
        .ContinueWith(s=> s.Append("test3"));
      buildMethod.Invoke(target);
      Assert.AreEqual("test1test2test3", target.ToString());
    }

    [TestMethod]
    public void TestFunctionContinueWith()
    {
      var baseFunction = (new Func<int, int>(a => a * a));
      var computeFunction = new Func<int, int>(a => a);

      for (int i = 0; i < 3; i++)
      {
         computeFunction = computeFunction.ContinueWith(baseFunction);
      }

      var result = computeFunction.Invoke(2);
      Assert.AreEqual(256 ,result);
    }

    [TestMethod]
    public void TestSelectManyContinuation()
    {
      var f1 = new Func<int, int>(x => x + x);
      var f2 = new Func<int, int>(x => x*x);

      var f3 = from a in f1
               from b in f2
               select b + a;
      var result = f3(3);
      Assert.AreEqual(15, result);
    }
  }
}
