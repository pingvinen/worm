using System;
using Worm.CodeGeneration.Internals;
using Worm.CodeGeneration;

namespace Worm
{
	public interface IWormDbFactory
	{
		IWormDbConnection CreateConnection();
		IWormTemplateProvider GetTemplateProvider();
		ISchema GetSchema();
	}
}