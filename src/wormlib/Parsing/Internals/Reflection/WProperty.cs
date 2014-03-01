using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Worm.Parsing.Internals.Reflection
{
	public class WProperty
	{
		protected PropertyInfo pi;

		public WProperty(PropertyInfo pi)
		{
			this.pi = pi;
		}

		public virtual string Name
		{
			get
			{
				return this.pi.Name;
			}
		}

		public virtual bool HasGetter
		{
			get
			{
				return this.pi.GetGetMethod() != null;
			}
		}

		#region Get attribute
		public virtual TAttribute GetAttribute<TAttribute>() where TAttribute : Attribute
		{
			object[] x = this.pi.GetCustomAttributes(typeof(TAttribute), true);
			if (x.Length == 0)
			{
				return default(TAttribute);
			}

			return (TAttribute)x[0];
		}
		#endregion
	}
}