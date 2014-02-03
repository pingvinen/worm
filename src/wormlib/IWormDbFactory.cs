using System;
using Worm.CodeGeneration.Internals;

namespace Worm
{
	public interface IWormDbFactory
	{
		IWormDbConnection CreateConnection();
		IWormTemplateProvider GetTemplateProvider();
	}
}