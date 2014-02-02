using System;

namespace Worm.DataAnnotations
{
	public class WormDbFactoryAttribute : Attribute
	{
		public WormDbFactoryAttribute(Type dbFactory)
		{
			// ensure that dbFactory implements IWormDbFactory
		}
	}
}

