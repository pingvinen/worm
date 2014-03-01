using System;

namespace Worm.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class WormPrimaryKeyAttribute : Attribute
	{
		public WormPrimaryKeyAttribute ()
		{
		}
	}
}

