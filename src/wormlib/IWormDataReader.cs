using System;

namespace Worm
{
	public interface IWormDataReader
	{
		T GetOrDefault<T>(string column, T defaultValue);

		bool Read();
	}
}