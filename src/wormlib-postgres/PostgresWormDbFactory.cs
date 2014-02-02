using System;
using Worm.Generator.Writing;
using Worm.Postgres.Writing;

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

		#region IWormDbFactory: GetClassWriter
		public IWormClassWriter GetClassWriter()
		{
			return new PostgresWormClassWriter();
		}
		#endregion
	}
}