using System;
using Worm.CodeGeneration;
using System.Diagnostics;
using System.IO;
using Worm.Parsing.Internals;
using Worm.Parsing.Internals.Reflection;
using Worm.DataAnnotations;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Worm.Parsing
{
	public class Parser
	{
		public ICompiler Compiler { get; set; }

		public Parser()
		{
			this.Compiler = new XbuildCompiler();
		}

		protected void LoadReferencedAssemblies(WAssembly asm)
		{
			var loaded = new Dictionary<string, bool>();
			foreach (Assembly x in AppDomain.CurrentDomain.GetAssemblies())
			{
				loaded.Add(x.GetName().Name+".dll", true);
			}

			FileInfo fi;
			foreach (string fullpath in Directory.GetFiles("/tmp/buildtests/", "*.dll"))
			{
				fi = new FileInfo(fullpath);

				if (!loaded.ContainsKey(fi.Name))
				{
					Assembly.LoadFile(fullpath);
					loaded.Add(fi.Name, true);
				}
			}
		}

		public void Parse(string projectFileName, string configurationToCompile = "Debug")
		{
			WAssembly asm = new WAssembly(this.Compiler.CompileProject(projectFileName, configurationToCompile));
			this.LoadReferencedAssemblies(asm);

			foreach (WType cur in asm.GetTypes(xx => { return xx.HasAttribute(typeof(WormDbFactoryAttribute)); }))
			{
				Console.WriteLine(cur.Name);
			}
		}
	}
}