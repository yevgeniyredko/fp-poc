using System;
using NUnit.Framework;

namespace Fp.Tests
{
    [TestFixture]
    public class ResultTests
    {
        public static Result<int> GetOkResult(int n) => Result.Ok(n);
        public static Result<int> GetFailResult() => Result.Fail<int>("Fail");
        
        public static async Result<int> M()
        {
            var a = await GetOkResult(5);
            Console.WriteLine(a);
            var b = await GetFailResult();
            Console.WriteLine(b);

            return b;
        }
        
        [Test]
        public void DoSomething_WhenSomething()
        {
            var a = M();
            
        }
    }
}