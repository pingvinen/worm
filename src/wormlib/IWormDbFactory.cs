using System;

namespace Worm
{
	public interface IWormDbFactory
	{
		IWormDbConnection CreateConnection();
	}
}