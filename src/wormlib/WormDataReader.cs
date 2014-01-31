using System;
using System.Data;

namespace Worm
{
	public class WormDataReader : IWormDataReader
	{
		protected IDataReader reader;

		public WormDataReader (IDataReader reader)
		{
			this.reader = reader;
		}

		public bool Read()
		{
			return this.reader.Read();
		}

		public T GetOrDefault<T>(string column, T defaultValue)
		{
			if (this.reader[column] == null || this.reader[column] == DBNull.Value)
			{
				return defaultValue;
			}

			return (T)this.reader[column];
		}
	}
}

