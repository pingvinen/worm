using System;
using System.IO;
using Worm.Parsing;
using Worm.CodeGeneration;
using Worm.CodeGeneration.Internals;
using Worm;
using Microsoft.Build.BuildEngine;
using System.Linq;

namespace fullflowconsole
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			var parser = new Parser(new Worm.WormFactory());
			string workingDir = Directory.GetCurrentDirectory();

			DirectoryInfo libRoot = Directory.GetParent(workingDir).Parent.Parent.GetDirectories("fullflow-lib")[0];
			string modelProjectFile = String.Format("{0}/fullflow-lib.csproj", libRoot.FullName);

			PocoModel model = parser.Parse(modelProjectFile);

			foreach (PocoEntity entity in model.Entities)
			{
				Console.WriteLine("DbFactory: {0}", entity.DbFactory.GetType().Name);
				Console.WriteLine("PocoClassName: {0}", entity.PocoClassName);
				Console.WriteLine("PocoFilename: {0}", entity.PocoFilename);
				Console.WriteLine("PocoNamespace: {0}", entity.PocoNamespace);
				Console.WriteLine("TableName: {0}", entity.TableName);
				Console.WriteLine();

				foreach (PocoField field in entity.Fields)
				{
					Console.WriteLine("AccessModifier: {0}", field.AccessModifier);
					Console.WriteLine("AllowNull: {0}", field.AllowNull);
					Console.WriteLine("ColumnName: {0}", field.ColumnName);
					Console.WriteLine("HasGetter: {0}", field.HasGetter);
					Console.WriteLine("HasSetter: {0}", field.HasSetter);
					Console.WriteLine("IdGenerator: {0}", field.IdGenerator);
					Console.WriteLine("IsEnum: {0}", field.IsEnum);
					Console.WriteLine("IsPrimaryKey: {0}", field.IsPrimaryKey);
					Console.WriteLine("Name: {0}", field.Name);
					Console.WriteLine("StorageType: {0}", field.StorageType);
					Console.WriteLine("Type: {0}", field.Type);
					Console.WriteLine();
				}

				Console.WriteLine();
			}

			Engine eng = new Engine();
			Project proj = new Project(eng);
			proj.Load(modelProjectFile);
			BuildItemGroup compileBuildItemGroup = GetCompileIncludeBuildItemGroup(proj);

			var writer = new DbClassWriter(new WormFactory());
			foreach (PocoEntity entity in model.Entities)
			{
				CodeFile cf = writer.Generate(entity);
				//cf.Filename = cf.Filename.Replace("fullflowlib", "fullflow-lib");
				WriteFile(libRoot, cf);
				AddCodeFileToProject(compileBuildItemGroup, cf);
			}

			proj.Save(modelProjectFile);

			// compile the project again
			parser.Parse(modelProjectFile);

			BuildItemGroup nonCodeBuildItemGroup = GetNoneIncludeBuildItemGroup(proj);
			var schemaWriter = new SchemaWriter();
			foreach (PocoEntity entity in model.Entities)
			{
				schemaWriter.AddEntity(entity);
			}
			foreach (CodeFile cf in schemaWriter.Render())
			{
				WriteFile(libRoot, cf);
				AddNonCodeFileToProject(nonCodeBuildItemGroup, cf);
			}

			proj.Save(modelProjectFile);
		}

		private static void WriteFile(DirectoryInfo libRoot, CodeFile cf)
		{
			string filename = cf.Filename;
			string fileDir = Path.Combine(libRoot.FullName, Path.GetDirectoryName(cf.Filename));
			Directory.CreateDirectory(fileDir);
			filename = Path.Combine(fileDir, Path.GetFileName(filename));

			File.WriteAllText(filename, cf.Content);
		}

		private static void AddCodeFileToProject(BuildItemGroup itemGroup, CodeFile cf)
		{
			WorkerAddFileToProject(itemGroup, "Compile", cf);
		}

		private static void AddNonCodeFileToProject(BuildItemGroup itemGroup, CodeFile cf)
		{
			WorkerAddFileToProject(itemGroup, "None", cf);
		}

		private static void WorkerAddFileToProject(BuildItemGroup itemGroup, string itemName, CodeFile cf)
		{
			string include = cf.Filename.Replace("/", "\\");
			itemGroup.AddNewItem(itemName, include);
		}

		private static BuildItemGroup GetCompileIncludeBuildItemGroup(Project proj)
		{
			return GetAndEnsureBuildItemGroup(proj, "Compile");
		}

		private static BuildItemGroup GetNoneIncludeBuildItemGroup(Project proj)
		{
			return GetAndEnsureBuildItemGroup(proj, "None");
		}

		private static BuildItemGroup GetAndEnsureBuildItemGroup(Project proj, string itemType)
		{
			foreach (BuildItemGroup itemGroup in proj.ItemGroups)
			{
				foreach (BuildItem item in itemGroup)
				{
					if (item.Name.Equals(itemType) && !item.Include.Equals(String.Empty))
					{
						return itemGroup;
					}
				}
			}

			return proj.AddNewItemGroup();
		}
	}
}
