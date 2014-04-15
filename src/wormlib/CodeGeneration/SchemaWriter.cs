using System;
using System.Collections.Generic;

namespace Worm.CodeGeneration
{
	public class SchemaWriter
	{
		private IDictionary<IWormDbFactory, ISchema> schemaByFactory = new Dictionary<IWormDbFactory, ISchema>();

		#region Add entity
		public void AddEntity(PocoEntity entity)
		{
			if (!this.schemaByFactory.ContainsKey(entity.DbFactory))
			{
				this.schemaByFactory.Add(entity.DbFactory, entity.DbFactory.GetSchema());
			}

			this.schemaByFactory[entity.DbFactory].AddEntity(entity);
		}
		#endregion

		#region Render
		public IList<CodeFile> Render()
		{
			List<CodeFile> files = new List<CodeFile>();

			foreach (KeyValuePair<IWormDbFactory, ISchema> kvp in this.schemaByFactory)
			{
				files.Add(new CodeFile() {
					Content = kvp.Value.Render(),
					Filename = String.Format("generated_schema_{0}.{1}", kvp.Value.Name, kvp.Value.FileExtension)
				});
			}

			return files;
		}
		#endregion
	}
}
