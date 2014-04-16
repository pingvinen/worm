using System;
using Microsoft.Build.BuildEngine;
using System.Linq;

namespace fullflowconsole
{
	public class CsProject
	{
		private readonly Engine engine;
		private Project project;

		public CsProject()
		{
			this.engine = new Engine();
		}

		#region Load
		public void Load(string fullPathToCsprojFile)
		{
			this.Filename = fullPathToCsprojFile;
			this.project = new Project(this.engine);
			this.project.Load(this.Filename);
		}
		#endregion

		#region Save
		public void Save()
		{
			this.project.Save(this.Filename);
		}
		#endregion

		#region Add file convenience
		public void AddCodeFile(string filename)
		{
			this.AddFile("Compile", filename);
		}

		public void AddNonCodeFile(string filename)
		{
			this.AddFile("None", filename);
		}
		#endregion

		#region Add file worker
		private void AddFile(string type, string filename)
		{
			BuildItemGroup itemGroup = this.GetBuildItemGroup(type);

			string include = filename.Replace("/", "\\");

			if (!this.ItemGroupContainsInclude(itemGroup, include))
			{
				itemGroup.AddNewItem(type, include);
			}
		}
		#endregion

		#region Item group contains
		private bool ItemGroupContainsInclude(BuildItemGroup itemGroup, string include)
		{
			foreach (BuildItem item in itemGroup)
			{
				if (item.Include.Equals(include))
				{
					return true;
				}
			}

			return false;
		}
		#endregion

		#region GetBuildItemGroup
		private BuildItemGroup GetBuildItemGroup(string type)
		{
			foreach (BuildItemGroup itemGroup in this.project.ItemGroups)
			{
				foreach (BuildItem item in itemGroup)
				{
					if (item.Name.Equals(type) && !item.Include.Equals(String.Empty))
					{
						return itemGroup;
					}
				}
			}

			return this.project.AddNewItemGroup();
		}
		#endregion

		public string Filename { get; private set; }
	}
}
