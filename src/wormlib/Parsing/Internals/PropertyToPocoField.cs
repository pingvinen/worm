using System;
using Worm.CodeGeneration.Internals;
using Worm.Parsing.Internals.Reflection;
using Worm.DataAnnotations;

namespace Worm.Parsing.Internals
{
	public class PropertyToPocoField
	{
		protected WormFactory factory;

		public PropertyToPocoField(WormFactory factory)
		{
			this.factory = factory;
		}

		public virtual PocoField Parse(WProperty property)
		{
			PocoField result = this.factory.GetPocoField();

			result.Name = property.Name;

			var allowNullAttribute = property.GetAttribute<WormAllowNullAttribute>();
			if (allowNullAttribute != default(WormAllowNullAttribute))
			{
				result.AllowNull = allowNullAttribute.Value;
			}

			return result;
		}
	}
}