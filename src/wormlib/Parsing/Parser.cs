using System;
using Worm.Parsing.Internals;
using Worm.Parsing.Internals.Reflection;
using Worm.DataAnnotations;
using Worm.CodeGeneration;

namespace Worm.Parsing
{
	public class Parser
	{
		public ICompiler Compiler { get; set; }

		protected WormFactory factory;

		public Parser(WormFactory factory)
		{
			this.factory = factory;
			this.Compiler = new XbuildCompiler();
		}

		#region Parse
		public PocoModel Parse(string projectFileName, string configurationToCompile = "Debug")
		{
			PocoModel result = new PocoModel();

			TypeToEntity typeToEntity = this.factory.GetTypeToEntity();
			WAssembly asm = new WAssembly(this.Compiler.CompileProject(projectFileName, configurationToCompile));

			foreach (WType cur in asm.GetTypes(xx => xx.HasAttribute(typeof(WormDbFactoryAttribute)) && !xx.Name.StartsWith("Worm", StringComparison.InvariantCulture)))
			{
				result.Entities.Add(typeToEntity.Parse(cur));
			}

			return result;
		}
		#endregion
	}
}