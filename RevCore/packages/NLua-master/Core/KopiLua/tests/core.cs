
using System;
using NUnit.Framework;
using System.IO;
using KopiLua;

namespace Tests.iOS
{
	[TestFixture]
	public class core
	{
		LuaState state;
		string GetTestPath(string name)
		{
			string filePath = Path.Combine (Path.Combine ("LuaTests", "core"), name + ".lua");
			return filePath;
		}

		void AssertFile (string path)
		{
			string error = string.Empty;

			int result = Lua.LuaLLoadFile(state, path);

			if (result != 0) {
				CharPtr pstring = Lua.LuaToString (state, 1);
				if (pstring != null)
					error = pstring.ToString();
			}

			Assert.True(result == 0, "Fail loading file: " + path + "ERROR:" + error);

			result = Lua.LuaPCall(state, 0, -1, 0);

			if (result != 0) {
				CharPtr pstring = Lua.LuaToString(state, 1);
				if (pstring != null)
					error = pstring.ToString();
			}


			Assert.True(result == 0, "Fail calling file: " + path + " ERROR: " + error);
		}

		void TestLuaFile (string name)
		{
			string path = GetTestPath (name);
			AssertFile (path);
		}


		[SetUp]
		public void Setup()
		{
			state = Lua.LuaLNewState ();
			Lua.LuaLOpenLibs (state);
		}

		[TearDown]
		public void TearDown ()
		{
			Lua.LuaClose (state);
			state = null;
		}

		[Test]
		public void Bisect ()
		{
			TestLuaFile ("bisect");
		}

		[Test]
		public void CF ()
		{
			TestLuaFile ("cf");
		}

		[Test]
		public void Env ()
		{
			TestLuaFile ("env");
		}

		[Test]
		public void Factorial ()
		{
			TestLuaFile ("factorial");
		}

		[Test]
		public void FibFor ()
		{
			TestLuaFile ("fibfor");
		}

		[Test]
		public void Life ()
		{
			TestLuaFile ("life");
		}

		[Test]
		public void Printf ()
		{
			TestLuaFile ("printf");
		}

		[Test]
		public void ReadOnly ()
		{
			TestLuaFile ("readonly");
		}

		[Test]
		public void Sieve ()
		{
			TestLuaFile ("sieve");
		}

		[Test]
		public void Sort ()
		{
			TestLuaFile ("sort");
		}

		[Test]
		public void TraceGlobals ()
		{
			TestLuaFile ("trace-globals");
		}
	}
}
