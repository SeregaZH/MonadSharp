using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonadSharp.Continuation;

namespace MonadSharp.Tests
{
  [TestClass]
  public class ContinuationTests
  {
  
    [TestMethod]
    public void TestSelectManyContinuation()
    {
      Func<int, Func<Func<int, int>, int>> factorial = null;
      factorial = n => n == 0 ? 1.ToContinuation<int, int>()
                        : from x in n.ToContinuation<int, int>()
                          from y in factorial(x - 1)
                          select x * y;

      Func<int, int> fac = n => factorial(n)(x => x);
      var result = fac(3);
      Assert.AreEqual(6, result);
    }
  }
}
