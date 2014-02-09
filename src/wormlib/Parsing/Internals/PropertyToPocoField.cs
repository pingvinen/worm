using System;
using Worm.CodeGeneration.Internals;
using Worm.Parsing.Internals.Reflection;

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
			PocoField result = this.factory.getPocoField();

			result.Name = property.Name;

			return result;
		}
	}
}