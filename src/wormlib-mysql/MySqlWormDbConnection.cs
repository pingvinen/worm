using System;
using MySql.Data.MySqlClient;

namespace Worm.MySql
{
	public class MySqlWormDbConnection : IWormDbConnection
	{
		protected string connectionString;
		protected MySqlConnection connection;
		
		public MySqlWormDbConnection(string connectionString)
		{
			this.connectionString = connectionString;
			this.connection = new MySqlConnection(connectionString);
			this.connection.Open();
		}
		
		
		#region IWormDbConnection implementation
		public IWormDbQuery CreateQuery ()
		{
			return this.CreateQuery(String.Empty);
		}
		
		public IWormDbQuery CreateQuery (string sql)
		{
			var cmd = this.connection.CreateCommand();
			cmd.CommandText = sql;
			return new WormDbQuery(cmd);
		}		
		#endregion
		
		#region IDisposable implementation
		~MySqlWormDbConnection()
		{
			this.Dispose(false);
		}
		
		// Track whether Dispose has been called.
		private bool disposed = false;
		
		// Implement IDisposable.
		// Do not make this method virtual.
		// A derived class should not be able to override this method.
		public void Dispose()
		{
			this.Dispose(true);
			// This object will be cleaned up by the Dispose method.
			// Therefore, you should call GC.SupressFinalize to
			// take this object off the finalization queue
			// and prevent finalization code for this object
			// from executing a second time.
			GC.SuppressFinalize(this);
		}
		
		// Dispose(bool disposing) executes in two distinct scenarios.
		// If disposing equals true, the method has been called directly
		// or indirectly by a user's code. Managed and unmanaged resources
		// can be disposed.
		// If disposing equals false, the method has been called by the
		// runtime from inside the finalizer and you should not reference
		// other objects. Only unmanaged resources can be disposed.
		protected virtual void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if (!this.disposed)
			{
				// If disposing equals true, dispose all managed
				// and unmanaged resources.
				if (disposing)
				{
					// Dispose managed resources.
					if (this.connection != default(MySqlConnection))
					{
						this.connection.Close();
						this.connection = null;
					}
				}
				
				// Note disposing has been done.
				disposed = true;
			}
		}
		#endregion
	}
}

