/*
** $Id: lua.c,v 1.160.1.2 2007/12/28 15:32:23 roberto Exp $
** Lua stand-alone interpreter
** See Copyright Notice in lua.h
*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using KopiLua;

namespace KopiLua
{
	public class Program
	{
		//#define lua_c

		//#include "lua.h"

		//#include "lauxlib.h"
		//#include "lualib.h"



		static LuaState globalL = null;

		static CharPtr progname = Lua.LUA_PROGNAME;



		static void lstop(LuaState L, LuaDebug ar)
		{
			Lua.LuaSetHook(L, null, 0, 0);
			Lua.LuaLError(L, "interrupted!");
		}


		static void laction(int i)
		{
			//signal(i, SIG_DFL); /* if another SIGINT happens before lstop,
			//						  terminate process (default action) */
			Lua.LuaSetHook(globalL, lstop, Lua.LUA_MASKCALL | Lua.LUA_MASKRET | Lua.LUA_MASKCOUNT, 1);
		}


		static void print_usage()
		{
			Console.Error.Write(
			"usage: {0} [options] [script [args]].\n" +
			"Available options are:\n" +
			"  -e stat  execute string " + Lua.LUA_QL("stat").ToString() + "\n" +
			"  -l name  require library " + Lua.LUA_QL("name").ToString() + "\n" +
			"  -i       enter interactive mode after executing " + Lua.LUA_QL("script").ToString() + "\n" +
			"  -v       show version information\n" +
			"  --       stop handling options\n" +
			"  -        execute stdin and stop handling options\n"
			,
			progname);
			Console.Error.Flush();
		}


		static void l_message(CharPtr pname, CharPtr msg)
		{
			if (pname != null) Lua.fprintf(Lua.stderr, "%s: ", pname);
			Lua.fprintf(Lua.stderr, "%s\n", msg);
			Lua.fflush(Lua.stderr);
		}


		static int report(LuaState L, int status)
		{
			if ((status!=0) && !Lua.LuaIsNil(L, -1))
			{
				CharPtr msg = Lua.LuaToString(L, -1);
				if (msg == null) msg = "(error object is not a string)";
				l_message(progname, msg);
				Lua.LuaPop(L, 1);
			}
			return status;
		}


		static int traceback(LuaState L)
		{
			if (Lua.LuaIsString(L, 1)==0)  /* 'message' not a string? */
				return 1;  /* keep it intact */
			Lua.LuaGetField(L, Lua.LUA_GLOBALSINDEX, "debug");
			if (!Lua.LuaIsTable(L, -1))
			{
				Lua.LuaPop(L, 1);
				return 1;
			}
			Lua.LuaGetField(L, -1, "traceback");
			if (!Lua.LuaIsFunction(L, -1))
			{
				Lua.LuaPop(L, 2);
				return 1;
			}
			Lua.LuaPushValue(L, 1);  /* pass error message */
			Lua.LuaPushInteger(L, 2);  /* skip this function and traceback */
			Lua.LuaCall(L, 2, 1);  /* call debug.traceback */
			return 1;
		}


		static int docall(LuaState L, int narg, int clear)
		{
			int status;
			int base_ = Lua.LuaGetTop(L) - narg;  /* function index */
			Lua.LuaPushCFunction(L, traceback);  /* push traceback function */
			Lua.LuaInsert(L, base_);  /* put it under chunk and args */
			//signal(SIGINT, laction);
			status = Lua.LuaPCall(L, narg, ((clear!=0) ? 0 : Lua.LUA_MULTRET), base_);
			//signal(SIGINT, SIG_DFL);
			Lua.LuaRemove(L, base_);  /* remove traceback function */
			/* force a complete garbage collection in case of errors */
			if (status != 0) Lua.LuaGC(L, Lua.LUA_GCCOLLECT, 0);
			return status;
		}


		static void print_version()
		{
			l_message(null, Lua.LUA_RELEASE + "  " + Lua.LUA_COPYRIGHT);
		}


		static int getargs(LuaState L, string[] argv, int n)
		{
			int narg;
			int i;
			int argc = argv.Length;	/* count total number of arguments */
			narg = argc - (n + 1);  /* number of arguments to the script */
			Lua.LuaLCheckStack(L, narg + 3, "too many arguments to script");
			for (i = n + 1; i < argc; i++)
			Lua.LuaPushString(L, argv[i]);
			Lua.LuaCreateTable(L, narg, n + 1);
			for (i = 0; i < argc; i++)
			{
				Lua.LuaPushString(L, argv[i]);
				Lua.LuaRawSetI(L, -2, i - n);
			}
			return narg;
		}


		static int dofile(LuaState L, CharPtr name)
		{
			int status = (Lua.LuaLLoadFile(L, name)!=0) || (docall(L, 0, 1)!=0) ? 1 : 0;
			return report(L, status);
		}


		static int dostring(LuaState L, CharPtr s, CharPtr name)
		{
			int status = (Lua.LuaLLoadBuffer(L, s, (uint)Lua.strlen(s), name)!=0) || (docall(L, 0, 1)!=0) ? 1 : 0;
			return report(L, status);
		}


		static int dolibrary(LuaState L, CharPtr name)
		{
			Lua.LuaGetGlobal(L, "require");
			Lua.LuaPushString(L, name);
			return report(L, docall(L, 1, 1));
		}


		static CharPtr get_prompt(LuaState L, int firstline)
		{
			CharPtr p;
			Lua.LuaGetField(L, Lua.LUA_GLOBALSINDEX, (firstline!=0) ? "_PROMPT" : "_PROMPT2");
			p = Lua.LuaToString(L, -1);
			if (p == null) p = ((firstline!=0) ? Lua.LUA_PROMPT : Lua.LUA_PROMPT2);
			Lua.LuaPop(L, 1);  /* remove global */
			return p;
		}


		static int incomplete(LuaState L, int status)
		{
			if (status == Lua.LUA_ERRSYNTAX)
			{
				uint lmsg;
				CharPtr msg = Lua.LuaToLString(L, -1, out lmsg);
				CharPtr tp = msg + lmsg - (Lua.strlen(Lua.LUA_QL("<eof>")));
				if (Lua.strstr(msg, Lua.LUA_QL("<eof>")) == tp)
				{
					Lua.LuaPop(L, 1);
					return 1;
				}
			}
			return 0;  /* else... */
		}


		static int pushline(LuaState L, int firstline)
		{
			CharPtr buffer = new char[Lua.LUA_MAXINPUT];
			CharPtr b = new CharPtr(buffer);
			int l;
			CharPtr prmt = get_prompt(L, firstline);
			if (!Lua.lua_readline(L, b, prmt))
				return 0;  /* no input */
			l = Lua.strlen(b);
			if (l > 0 && b[l - 1] == '\n')  /* line ends with newline? */
				b[l - 1] = '\0';  /* remove it */
			if ((firstline!=0) && (b[0] == '='))  /* first line starts with `=' ? */
				Lua.LuaPushFString(L, "return %s", b + 1);  /* change it to `return' */
			else
				Lua.LuaPushString(L, b);
			Lua.lua_freeline(L, b);
			return 1;
		}


		static int loadline(LuaState L)
		{
			int status;
			Lua.LuaSetTop(L, 0);
			if (pushline(L, 1)==0)
				return -1;  /* no input */
			for (; ; )
			{  /* repeat until gets a complete line */
				status = Lua.LuaLLoadBuffer(L, Lua.LuaToString(L, 1), Lua.LuaStrLen(L, 1), "=stdin");
				if (incomplete(L, status)==0) break;  /* cannot try to add lines? */
				if (pushline(L, 0)==0)  /* no more input? */
					return -1;
				Lua.LuaPushLiteral(L, "\n");  /* add a new line... */
				Lua.LuaInsert(L, -2);  /* ...between the two lines */
				Lua.LuaConcat(L, 3);  /* join them */
			}
			Lua.lua_saveline(L, 1);
			Lua.LuaRemove(L, 1);  /* remove line */
			return status;
		}


		static void dotty(LuaState L)
		{
			int status;
			CharPtr oldprogname = progname;
			progname = null;
			while ((status = loadline(L)) != -1)
			{
				if (status == 0) status = docall(L, 0, 0);
				report(L, status);
				if (status == 0 && Lua.LuaGetTop(L) > 0)
				{  /* any result to print? */
					Lua.LuaGetGlobal(L, "print");
					Lua.LuaInsert(L, 1);
					if (Lua.LuaPCall(L, Lua.LuaGetTop(L) - 1, 0, 0) != 0)
						l_message(progname, Lua.LuaPushFString(L,
											   "error calling " + Lua.LUA_QL("print").ToString() + " (%s)",
											   Lua.LuaToString(L, -1)));
				}
			}
			Lua.LuaSetTop(L, 0);  /* clear stack */
			Lua.fputs("\n", Lua.stdout);
			Lua.fflush(Lua.stdout);
			progname = oldprogname;
		}


		static int handle_script(LuaState L, string[] argv, int n)
		{
			int status;
			CharPtr fname;
			int narg = getargs(L, argv, n);  /* collect arguments */
			Lua.LuaSetGlobal(L, "arg");
			fname = argv[n];
			if (Lua.strcmp(fname, "-") == 0 && Lua.strcmp(argv[n - 1], "--") != 0)
				fname = null;  /* stdin */
			status = Lua.LuaLLoadFile(L, fname);
			Lua.LuaInsert(L, -(narg + 1));
			if (status == 0)
				status = docall(L, narg, 0);
			else
				Lua.LuaPop(L, narg);
			return report(L, status);
		}


		/* check that argument has no extra characters at the end */
		//#define notail(x)	{if ((x)[2] != '\0') return -1;}


		static int collectargs(string[] argv, ref int pi, ref int pv, ref int pe)
		{
			int i;
			for (i = 1; i < argv.Length; i++)
			{
				if (argv[i][0] != '-')  /* not an option? */
					return i;
				switch (argv[i][1])
				{  /* option */
					case '-':
						if (argv[i].Length != 2) return -1;
						return (i + 1) >= argv.Length ? i + 1 : 0;

					case '\0':
						return i;

					case 'i':
						if (argv[i].Length != 2) return -1;
						pi = 1;
						if (argv[i].Length != 2) return -1;
						pv = 1;
						break;

					case 'v':
						if (argv[i].Length != 2) return -1;
						pv = 1;
						break;

					case 'e':
						pe = 1;
						if (argv[i].Length == 2)
						{
							i++;
							if (argv[i] == null) return -1;
						}
						break;

					case 'l':
						if (argv[i].Length == 2)
						{
							i++;
							if (i >= argv.Length) return -1;
						}
						break;
					default: return -1;  /* invalid option */
				}
			}
			return 0;
		}


		static int runargs(LuaState L, string[] argv, int n)
		{
			int i;
			for (i = 1; i < n; i++)
			{
				if (argv[i] == null) continue;
				Lua.LuaAssert(argv[i][0] == '-');
				switch (argv[i][1])
				{  /* option */
					case 'e':
						{
							string chunk = argv[i].Substring(2);
							if (chunk == "") chunk = argv[++i];
							Lua.LuaAssert(chunk != null);
							if (dostring(L, chunk, "=(command line)") != 0)
								return 1;
							break;
						}
					case 'l':
						{
							string filename = argv[i].Substring(2);
							if (filename == "") filename = argv[++i];
							Lua.LuaAssert(filename != null);
							if (dolibrary(L, filename) != 0)
								return 1;  /* stop if file fails */
							break;
						}
					default: break;
				}
			}
			return 0;
		}


		static int handle_luainit(LuaState L)
		{
			CharPtr init = Lua.getenv(Lua.LUA_INIT);
			if (init == null) return 0;  /* status OK */
			else if (init[0] == '@')
				return dofile(L, init + 1);
			else
				return dostring(L, init, "=" + Lua.LUA_INIT);
		}


		public class Smain
		{
			public int argc;
			public string[] argv;
			public int status;
		};


		static int pmain(LuaState L)
		{
			Smain s = (Smain)Lua.LuaToUserData(L, 1);
			string[] argv = s.argv;
			int script;
			int has_i = 0, has_v = 0, has_e = 0;
			globalL = L;
			if ((argv.Length>0) && (argv[0]!="")) progname = argv[0];
			Lua.LuaGC(L, Lua.LUA_GCSTOP, 0);  /* stop collector during initialization */
			Lua.LuaLOpenLibs(L);  /* open libraries */
			Lua.LuaGC(L, Lua.LUA_GCRESTART, 0);
			s.status = handle_luainit(L);
			if (s.status != 0) return 0;
			script = collectargs(argv, ref has_i, ref has_v, ref has_e);
			if (script < 0)
			{  /* invalid args? */
				print_usage();
				s.status = 1;
				return 0;
			}
			if (has_v!=0) print_version();
			s.status = runargs(L, argv, (script > 0) ? script : s.argc);
			if (s.status != 0) return 0;
			if (script!=0)
				s.status = handle_script(L, argv, script);
			if (s.status != 0) return 0;
			if (has_i!=0)
				dotty(L);
			else if ((script==0) && (has_e==0) && (has_v==0))
			{
				if (Lua.lua_stdin_is_tty()!=0)
				{
					print_version();
					dotty(L);
				}
				else dofile(L, null);  /* executes stdin as a file */
			}
			return 0;
		}


		static int Main(string[] args)
		{
			// prepend the exe name to the arg list as it's done in C
			// so that we don't have to change any of the args indexing
			// code above
			List<string> newargs = new List<string>(args);
			newargs.Insert(0, Assembly.GetExecutingAssembly().Location);
			args = (string[])newargs.ToArray();

			int status;
			Smain s = new Smain();
			LuaState L = Lua.LuaOpen();  /* create state */
			if (L == null)
			{
				l_message(args[0], "cannot create state: not enough memory");
				return Lua.EXIT_FAILURE;
			}
			s.argc = args.Length;
			s.argv = args;
			status = Lua.LuaCPCall(L, pmain, s);
			report(L, status);
			Lua.LuaClose(L);
			return (status!=0) || (s.status!=0) ? Lua.EXIT_FAILURE : Lua.EXIT_SUCCESS;
		}

	}
}
