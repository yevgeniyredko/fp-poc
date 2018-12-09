using System;
using NUnit.Framework;

namespace Fp.Tests
{
	// Взял тесты из https://github.com/kontur-courses/fp/blob/5f1673709e6269ec6d198df4112e20f737c26521/ErrorHandling/QuerySyntax/ResultQueryExpression_Should.cs
    [TestFixture]
    public class ResultTests
    {
	    [Test]
	    public void SupportQueryExpressions()
	    {
		    async Result<Guid> TestMethod()
		    {
			    var i = await "1358571172".ParseIntResult();
			    var hex = await Convert.ToString(i, 16).AsResult();
			    var guid = await (hex + hex + hex + hex).ParseGuidResult();
				return guid;
		    }

		    var res = TestMethod();
			Assert.AreEqual(res, Result.Ok(Guid.Parse("50FA26A450FA26A450FA26A450FA26A4")));
	    }

	    [Test]
	    public void SupportQueryExpressions_WithComplexSelect()
	    {
		    async Result<string> TestMethod()
		    {
			    var i = await "1358571172".ParseIntResult();
			    var hex = await Convert.ToString(i, 16).AsResult();
			    var guid = await (hex + hex + hex + hex).ParseGuidResult();
				return i + " -> " + guid;
		    }

		    var res = TestMethod();
		    Assert.AreEqual(res, Result.Ok("1358571172 -> 50fa26a4-50fa-26a4-50fa-26a450fa26a4"));
	    }

	    [Test]
	    public void ReturnFail_FromSelectMany_WhenErrorAtTheEnd()
	    {
		    async Result<Guid> TestMethod()
		    {
			    var i = await "0".ParseIntResult();
			    var hex = await Convert.ToString(i, 16).AsResult();
			    var guid = await (hex + hex + hex + hex).ParseGuidResult("error is here");
				return guid;
		    }

		    var res = TestMethod();
		    Assert.AreEqual(res, Result.Fail<Guid>("error is here"));
	    }

	    [Test]
	    public void ReturnFail_FromSelectMany_WhenExceptionOnSomeStage()
	    {
		    async Result<Guid> TestMethod()
		    {
			    var i = await "1358571172".ParseIntResult();
			    var hex = await Result.Of(() => Convert.ToString(i, 100500), "error is here");
			    var guid = await (hex + hex + hex + hex).ParseGuidResult();
				return guid;
		    }

		    var res = TestMethod();
		    Assert.AreEqual(res, Result.Fail<Guid>("error is here"));
	    }

	    [Test]
	    public void ReturnFail_FromSelectMany_WhenErrorAtTheBeginning()
	    {
		    async Result<Guid> TestMethod()
		    {
			    var i = await "UNPARSABLE".ParseIntResult("error is here");
			    var hex = await Convert.ToString(i, 16).AsResult();
			    var guid = await (hex + hex + hex + hex).ParseGuidResult();
				return guid;
		    }

		    var res = TestMethod();
			Assert.AreEqual(res, Result.Fail<Guid>("error is here"));
	    }
    }

	public static class ParseResultExtensions
	{
		public static Result<int> ParseIntResult(this string s, string error = null)
		{
			int v;
			return int.TryParse(s, out v)
				? v.AsResult()
				: Result.Fail<int>(error ?? "Не число " + s);
		}

		public static Result<Guid> ParseGuidResult(this string s, string error = null)
		{
			Guid v;
			return Guid.TryParse(s, out v)
				? v.AsResult()
				: Result.Fail<Guid>(error ?? "Не GUID " + s);
		}
	}
}