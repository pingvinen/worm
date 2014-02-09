using System;

namespace Worm.DataAnnotations
{
	[AttributeUsage(AttributeTargets.All)]
	public class WormIgnoreAttribute : Attribute
	{
		public WormIgnoreAttribute ()
		{
		}
	}
}

