using System;

namespace Worm.DataAnnotations
{
	[AttributeUsage(AttributeTargets.All)]
	public class WormAllowNullAttribute : Attribute
	{
		public WormAllowNullAttribute(bool doAllowNull=false)
		{
			this.Value = doAllowNull;
		}

		public bool Value { get; set; }
	}
}

