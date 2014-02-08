using System;
using System.Reflection;
using Microsoft.Build.Execution;

namespace Worm.Parsing.Internals
{
	public class MicrosoftBuildCompiler : ICompiler
	{
		public MicrosoftBuildCompiler()
		{
		}

		public Assembly CompileProject(string projectFilename, string configuration = "Debug")
		{
			var mgr = new BuildManager();
			var parms = new BuildParameters();
			var proj = new ProjectInstance(projectFilename);

			var req = new BuildRequestData(proj, new string[] {configuration});

			BuildResult result = mgr.Build(parms, req);
			Console.WriteLine("Overall build result: {0}", result.OverallResult);

			throw new NotImplementedException("Not fully implemented yet");
		}
	}
}