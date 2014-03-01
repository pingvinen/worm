using System;

namespace Worm.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class WormIdGeneratorAttribute : Attribute
	{
		public WormIdGeneratorAttribute (WormIdGenerator generator)
		{
			this.IdGenerator = generator;
		}

		public virtual WormIdGenerator IdGenerator { get; private set; }
	}
}

