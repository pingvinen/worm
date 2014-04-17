using System;
using Worm.CodeGeneration;
using System.Collections.Generic;
using Worm.CodeGeneration.Internals;
using System.Text;

namespace Worm.MySql.CodeGeneration.SchemaWriting
{
	internal class Table
	{
		public Table()
		{
			this.Columns = new List<Column>();
			this.DefaultCharset = "utf8";
			this.Engine = "InnoDb";
		}

		public string Name { get; set; }
		public string DefaultCharset { get; set; }
		public string Engine { get; set; }
		public List<Column> Columns { get; set; }

		public void PopulateFromPocoEntity(PocoEntity entity)
		{
			this.Name = entity.TableName;
			Column col;
			foreach (PocoField field in entity.Fields)
			{
				col = new Column(new TypeMapper());
				col.PopulateFromEntityField(field);
				this.Columns.Add(col);
			}
		}

		#region GenerateScript
		public string GenerateScript()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("DROP TABLE IF EXISTS `{0}`;", this.Name);
			sb.AppendLine();
			sb.AppendLine();

			sb.AppendFormat("CREATE TABLE `{0}` (", this.Name);
			sb.AppendLine();

			foreach (Column c in this.Columns)
			{
				sb.AppendFormat("\t{0},", c.GenerateScript());
				sb.AppendLine();
			}

			sb.Remove(sb.Length - 2, 1); // remove the last comma

			string pk = this.GetPrimaryKey();
			if (!String.IsNullOrEmpty(pk))
			{
				sb.AppendFormat("\t, PRIMARY KEY ({0})", pk);
				sb.AppendLine();
			}

			sb.AppendFormat(") ENGINE={0} DEFAULT CHARSET={1};", this.Engine, this.DefaultCharset);
			sb.AppendLine();

			return sb.ToString();
		}
		#endregion

		#region Get primary key
		private string GetPrimaryKey()
		{
			string primaryKeyString = String.Empty;

			foreach (Column c in this.Columns)
			{
				if (c.IsPrimaryKey)
				{
					primaryKeyString = String.Concat(primaryKeyString, String.Format(", `{0}`", c.Name));
				}
			}

			if (!String.IsNullOrEmpty(primaryKeyString))
			{
				primaryKeyString = primaryKeyString.Remove(0, 2); // remove the first comma
			}

			return primaryKeyString;
		}
		#endregion
	}
}