using System;
using System.Collections.Generic;

namespace Worm
{
	public interface IWormDbQuery
	{
		string Sql { get; set; }
		IWormDbQuery AddParam(string name, object value);
		IWormDataReader ExecuteReader();
		int ExecuteNonQuery();
		T ExecuteScalar<T>();
	}
}