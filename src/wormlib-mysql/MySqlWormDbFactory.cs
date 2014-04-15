using System;
using Worm.MySql.CodeGeneration;
using Worm.CodeGeneration.Internals;
using Worm.CodeGeneration;

namespace Worm.MySql
{
	public class MySqlWormDbFactory : IWormDbFactory
	{
		protected string connectionString;
	
		public MySqlWormDbFactory() : this(String.Empty)
		{
		}
			
		public MySqlWormDbFactory(string connectionString)
		{
			this.connectionString = connectionString;
		}
		
		#region IWormDbFactory: create connection
		public virtual IWormDbConnection CreateConnection()
		{
			return new MySqlWormDbConnection(this.connectionString);
		}
		#endregion

		#region IWormDbFactory: GetTemplateProvider
		public virtual IWormTemplateProvider GetTemplateProvider()
		{
			return new MySqlWormTemplateProvider();
		}
		#endregion

		#region IWormDbFactory: GetSchema
		public virtual ISchema GetSchema()
		{
			var schema = new MySqlSchema();
			if (!this.connectionString.Equals(String.Empty))
			{
				// schema.Name = <db name from connection string>
			}

			return schema;
		}
		#endregion
	}
}

