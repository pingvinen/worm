using System;
using System.Data;

namespace Worm
{
	public class WormDbQuery : IWormDbQuery
	{
		protected IDbCommand command;

		public WormDbQuery (IDbCommand command)
		{
			this.command = command;
		}

		#region IWormDbQuery implementation
		public string Sql
		{
			get
			{
				return this.command.CommandText;
			}

			set
			{
				this.command.CommandText = value;
			}
		}

		public IWormDataReader ExecuteReader()
		{
			var reader = this.command.ExecuteReader();
			return new WormDataReader(reader);
		}

		public int ExecuteNonQuery()
		{
			return this.command.ExecuteNonQuery();
		}

		public T ExecuteScalar<T>()
		{
			return (T)this.command.ExecuteScalar();
		}

		public IWormDbQuery AddParam (string name, object value)
		{
			var par = this.command.CreateParameter();
			par.ParameterName = name;
			par.Value = value;

			this.command.Parameters.Add(par);

			return this;
		}
		#endregion
	}
}