using System;

namespace Worm.Generator.Writing
{
	public interface IWormClassWriter
	{
		string GenerateCode(PocoModel model);
	}
}