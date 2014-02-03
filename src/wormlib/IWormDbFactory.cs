using System;
using Worm.Generator.Writing;

namespace Worm
{
	public interface IWormDbFactory
	{
		IWormDbConnection CreateConnection();
		IWormTemplateProvider GetTemplateProvider();
	}
}