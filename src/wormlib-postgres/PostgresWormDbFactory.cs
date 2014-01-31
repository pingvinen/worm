using System;
using Npgsql;

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
		#endregion IWormDbFactory: create connection
	}
}