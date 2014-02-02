using System;

namespace Worm.Generator
{
	/// <summary>
	/// Represents a POCO that needs storage code
	/// </summary>
	public class PocoModel
	{
		public PocoModel()
		{
			this.Fields = new PocoFieldCollection();
			this.Methods = new MethodCollection();
		}

		#region Fields representing the input POCO
		/// <summary>
		/// Gets or sets the full filename of the POCO class.
		/// </summary>
		public virtual string PocoFilename { get; set; }

		/// <summary>
		/// Gets or sets the namespace of the POCO class.
		/// </summary>
		public virtual string PocoNamespace { get; set; }

		/// <summary>
		/// Gets or sets the name of the POCO class.
		/// </summary>
		public virtual string PocoClassName { get; set; }

		/// <summary>
		/// Gets or sets the fields of the POCO class.
		/// </summary>
		public virtual PocoFieldCollection Fields { get; set; }

		/// <summary>
		/// Gets or sets an instance of the DB factory
		/// that this POCO should use.
		/// </summary>
		public virtual IWormDbFactory DbFactory { get; set; }
		#endregion

		#region Fields representing the output class
		/// <summary>
		/// Gets or sets the full filename of the output worm class.
		/// </summary>
		public virtual string WormFilename { get; set; }

		/// <summary>
		/// Gets or sets the name of the table that stores this POCO.
		/// </summary>
		public virtual string TableName { get; set; }

		/// <summary>
		/// Gets or sets the name of the output worm class.
		/// </summary>
		public virtual string WormClassName { get; set; }

		/// <summary>
		/// Gets or sets the namespace of the output worm class.
		/// </summary>
		public virtual string WormNamespace { get; set; }

		/// <summary>
		/// Gets or sets the methods that are in the existing db class
		/// </summary>
		public virtual MethodCollection Methods { get; set; }
		#endregion
	}
}