using System;
using Worm.DataAnnotations;

namespace Worm.Generator
{
	/// <summary>
	/// Represents a field in a POCO
	/// </summary>
	public class PocoField
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Worm.Generator.PocoField"/> class.
		/// </summary>
		public PocoField()
		{
			this.AllowNull = true;
			this.IsEnum = false;
		}

		/// <summary>
		/// Gets or sets the access modifier of the field.
		/// </summary>
		public virtual AccessModifier AccessModifier { get; set; }

		/// <summary>
		/// Gets or sets the type of the POCO field.
		/// </summary>
		public virtual string Type { get; set; }

		/// <summary>
		/// Gets or sets whether this is an enum type field
		/// </summary>
		public virtual bool IsEnum { get; set; }

		/// <summary>
		/// Gets or sets the name of the POCO field.
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// Gets or sets whether the POCO field has a getter.
		/// </summary>
		public virtual bool HasGetter { get; set; }

		/// <summary>
		/// Gets or sets whether the POCO field has a setter.
		/// </summary>
		public virtual bool HasSetter { get; set; }

		/// <summary>
		/// Gets or sets the datatype to use in the database.
		/// </summary>
		public virtual string StorageType { get; set; }

		/// <summary>
		/// Gets or sets whether this is a primary key field.
		/// </summary>
		public virtual bool IsPrimaryKey { get; set; }

		/// <summary>
		/// Gets or sets how the ID is generated (if this is the PK field).
		/// </summary>
		public virtual WormIdGenerator IdGenerator { get; set; }

		/// <summary>
		/// Gets or sets whether this field needs to be persisted.
		/// </summary>
		public virtual bool DoPersist { get; set; }

		/// <summary>
		/// Gets or sets whether this field allows <c>Null</c> as a value.
		/// </summary>
		public virtual bool AllowNull { get; set; }

		/// <summary>
		/// Gets or sets the name of the column.
		/// </summary>
		public virtual string ColumnName { get; set; }
	}
}