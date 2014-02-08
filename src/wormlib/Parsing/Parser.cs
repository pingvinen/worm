using System;
using Worm.CodeGeneration;
using System.Diagnostics;
using System.IO;
using Worm.Parsing.Internals;
using System.Reflection;

namespace Worm.Parsing
{
	public class Parser
	{
		public ICompiler Compiler { get; set; }

		public Parser()
		{
			this.Compiler = new XbuildCompiler();
		}

		public void Parse(string projectFileName, string configurationToCompile = "Debug")
		{
			Assembly asm = this.Compiler.CompileProject(projectFileName, configurationToCompile);

			foreach (Type cur in asm.GetTypes())
			{
				Console.WriteLine(cur.Name);
			}
		}
	}
}