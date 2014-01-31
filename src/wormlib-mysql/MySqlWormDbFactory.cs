using System;

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
		#endregion IWormDbFactory: create connection
	}
}

