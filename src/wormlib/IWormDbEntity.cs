using System;
using System.Data;

namespace Worm
{
	public interface IWormDbEntity
	{
		void DbBuildQuery_GetId(object id, IDbConnection connection);
	}
}