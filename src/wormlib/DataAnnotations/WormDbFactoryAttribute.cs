using System;
using Worm.Parsing.Internals.Reflection;

namespace Worm.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Class)]
	public class WormDbFactoryAttribute : Attribute
	{
		public WormDbFactoryAttribute(Type dbFactoryType)
		{
			this.DbFactoryType = new WType(dbFactoryType);

			if (!this.DbFactoryType.Implements(typeof(IWormDbFactory)))
			{
				throw new ArgumentException("The type given to WormDbFactoryAttribute must implement IWormDbFactory");
			}
		}

		public virtual WType DbFactoryType { get; set; }
	}
}

