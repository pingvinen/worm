using System;
using System.Reflection;

namespace Worm.Parsing.Internals
{
	public interface ICompiler
	{
		Assembly CompileProject(string projectFilename, string configuration = "Debug");
	}
}