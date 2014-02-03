using System;
using Worm.Generator.Writing;
using Worm.MySql.Writing;

namespace Worm.MySql
{
	public class MySqlWormDbFactory : IWormDbFactory
	{
		protected string connectionString;
		
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
	}
}

