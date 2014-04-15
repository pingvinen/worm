using System;
using Worm.Postgres.CodeGeneration;
using Worm.CodeGeneration.Internals;
using Worm.CodeGeneration;

namespace Worm.Postgres
{
	public class PostgresWormDbFactory : IWormDbFactory
	{
		protected string connectionString;

		public PostgresWormDbFactory() : this(String.Empty)
		{
		}

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

		#region IWormDbFactory: GetSchema
		public virtual ISchema GetSchema()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}