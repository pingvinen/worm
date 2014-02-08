using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Worm.Parsing.Internals.Reflection
{
	public class WType
	{
		protected Type type;

		public WType()
		{
		}

		public WType(Type type)
		{
			this.type = type;
		}

		public virtual string Name
		{
			get
			{
				return this.type.Name;
			}
		}

		public virtual bool HasAttribute(Type attributeType)
		{
			return this.type.GetCustomAttributes(attributeType, true).Count() > 0;
		}
	}
}