using System;
using Worm.CodeGeneration;
using System.Collections.Generic;
using Worm.CodeGeneration.Internals;

namespace Worm.Postgres.CodeGeneration.Templates
{
	public partial class DbInsertTemplateT4
	{
		public PocoEntity Model { get; set; }

		public IList<string> GetColumnList()
		{
			List<string> columns = new List<string>();
			foreach (PocoField field in this.Model.Fields.GetInsertFields())
			{
				columns.Add(field.ColumnName);
			}

			return columns;
		}

		public IList<string> GetInsertValues()
		{
			List<string> columns = new List<string>();
			foreach (PocoField field in this.Model.Fields.GetInsertFields())
			{
				columns.Add(":"+field.Name);
			}

			return columns;
		}
	}
}