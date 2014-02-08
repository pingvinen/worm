using System;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace Worm.Parsing.Internals
{
	public class XbuildCompiler : ICompiler
	{
		public string XbuildPath { get; set; }
		public string OutputPath { get; set; }
		public bool CleanOutputPathBeforeCompiling { get; set; }

		public XbuildCompiler()
		{
			this.XbuildPath = "/opt/monodevelop/bin/xbuild";
			this.OutputPath = "/tmp/buildtests";
			this.CleanOutputPathBeforeCompiling = true;
		}


		#region ICompiler implementation
		public Assembly CompileProject(string projectFilename, string configuration = "Debug")
		{
			if (this.CleanOutputPathBeforeCompiling)
			{
				this.CleanOutputPath();
			}

			string assemblyFile = this.GetAssemblyFilename(projectFilename);

			var psi = new ProcessStartInfo(this.XbuildPath, String.Format("/property:OutputPath={0} {1}",
				this.OutputPath
				, projectFilename
			));

			Process p = Process.Start(psi);
			p.WaitForExit();

			return Assembly.LoadFile(String.Format("{0}/{1}", this.OutputPath, assemblyFile));
		}
		#endregion

		protected void CleanOutputPath()
		{
			if (!Directory.Exists(this.OutputPath))
			{
				return;
			}

			Directory.Delete(this.OutputPath, true);
		}

		protected string GetAssemblyFilename(string projectFilename)
		{
			var xml = XElement.Load(projectFilename);
			var ns = "{" + xml.GetDefaultNamespace() + "}";
			var group = xml.Elements(ns + "PropertyGroup").First();
			var node = group.Elements(ns + "AssemblyName").FirstOrDefault();

			string assemblyName = node.Value;
			string outputType = group.Elements(ns + "OutputType").FirstOrDefault().Value;
			string ext;

			switch (outputType)
			{
				case "Library":
					ext = "dll";
					break;

				default:
					ext = "exe";
					break;
			}

			return String.Format("{0}.{1}", assemblyName, ext);
		}
	}
}