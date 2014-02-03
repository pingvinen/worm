using System;
using Worm.Postgres.CodeGeneration;
using Worm.CodeGeneration.Internals;

namespace Worm.Postgres
{
	public class PostgresWormDbFactory : IWormDbFactory
	{
		protected string connectionString;

		public PostgresWormDbFactory(string connectionString)
		{
			this.connectionString = connectionString;
		}

		#region IWormDbFactory: create connection
		public virtual IWormDbConnection CreateConnection()
		{
			return new PostgresWormDbConnection(this.connectionString);
		}
		#endregion

		#region IWormDbFactory: GetTemplateProvider
		public IWormTemplateProvider GetTemplateProvider()
		{
			return new PostgressWormTemplateProvider();
		}
		#endregion
	}
}