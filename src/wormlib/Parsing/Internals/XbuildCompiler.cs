using System;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

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

			Assembly asm = Assembly.LoadFile(String.Format("{0}/{1}", this.OutputPath, assemblyFile));

			this.LoadAssemblies();

			return asm;
		}
		#endregion

		#region Clean output path
		protected void CleanOutputPath()
		{
			if (!Directory.Exists(this.OutputPath))
			{
				return;
			}

			Directory.Delete(this.OutputPath, true);
		}
		#endregion

		#region Get assembly filename
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
		#endregion

		#region Load assemblies
		protected void LoadAssemblies()
		{
			var loaded = new Dictionary<string, bool>();
			foreach (Assembly x in AppDomain.CurrentDomain.GetAssemblies())
			{
				loaded.Add(x.GetName().Name+".dll", true);
			}

			FileInfo fi;
			foreach (string fullpath in this.GetFiles(this.OutputPath, "*.dll", "*.DLL", "*.exe", "*.EXE"))
			{
				fi = new FileInfo(fullpath);

				if (!loaded.ContainsKey(fi.Name))
				{
					Assembly.LoadFile(fullpath);
					loaded.Add(fi.Name, true);
				}
			}
		}
		#endregion

		#region Get files
		protected IEnumerable<string> GetFiles(string directory, params string[] patterns)
		{
			foreach (string curPattern in patterns)
			{
				foreach (string filename in Directory.GetFiles(directory, curPattern))
				{
					yield return filename;
				}
			}
		}
		#endregion
	}
}