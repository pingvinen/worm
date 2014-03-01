using System;
using Worm;
using Worm.CodeGeneration.Internals;

namespace Wormlibtests.Parsing.Internals
{
	public class UniTestWormDbFactory : IWormDbFactory
	{
		public UniTestWormDbFactory()
		{
		}

		public virtual IWormDbConnection CreateConnection()
		{
			throw new NotImplementedException();
		}

		public virtual IWormTemplateProvider GetTemplateProvider()
		{
			throw new NotImplementedException();
		}
	}
}