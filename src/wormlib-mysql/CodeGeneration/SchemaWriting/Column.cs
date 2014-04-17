using System;
using Worm.CodeGeneration.Internals;
using System.Text;
using Worm.DataAnnotations;

namespace Worm.MySql.CodeGeneration.SchemaWriting
{
	internal class Column
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public bool IsPrimaryKey { get; set; }
		public bool AllowsNull { get; set; }
		public bool IsAutoIncrement { get; set; }

		private readonly TypeMapper typeMapper;

		public Column(TypeMapper typeMapper)
		{
			this.typeMapper = typeMapper;
		}

		public void PopulateFromEntityField(PocoField field)
		{
			this.Name = field.ColumnName;
			this.Type = this.GetDataType(field);
			this.IsPrimaryKey = field.IsPrimaryKey;
			this.AllowsNull = field.AllowNull;
			this.IsAutoIncrement = false;

			if (this.IsPrimaryKey)
			{
				this.IsAutoIncrement = field.IdGenerator == WormIdGenerator.AutoIncrement;
			}
		}

		#region GetDataType
		private string GetDataType(PocoField field)
		{
			if (String.IsNullOrEmpty(field.StorageType))
			{
				return this.typeMapper.GetMySqlType(field.Type);
			}
			else
			{
				return field.StorageType;
			}
		}
		#endregion

		#region Generate script
		public string GenerateScript()
		{
			var sb = new StringBuilder();

			sb.AppendFormat("`{0}` ", this.Name);
			sb.Append(this.Type);

			if (!this.AllowsNull)
			{
				sb.Append(" NOT NULL");
			}

			if (this.IsAutoIncrement)
			{
				sb.Append(" AUTO_INCREMENT");
			}

			return sb.ToString();
		}
		#endregion
	}
}