using System;

namespace Worm
{
	public interface IWormDbConnection : IDisposable
	{
		IWormDbQuery CreateQuery();
		IWormDbQuery CreateQuery(string sql);
	}
}