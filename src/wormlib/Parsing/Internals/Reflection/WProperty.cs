using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using Worm.CodeGeneration.Internals;

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

		public virtual bool HasSetter
		{
			get
			{
				return this.pi.GetSetMethod() != null;
			}
		}

		public virtual bool IsEnum
		{
			get
			{
				return this.pi.PropertyType.IsEnum;
			}
		}

		public virtual AccessModifier AccessModifier
		{
			get
			{
				MethodInfo mi = null;

				if (this.pi.CanWrite)
				{
					mi = this.pi.GetSetMethod();
				}
				else if (this.pi.CanRead)
				{
					mi = this.pi.GetGetMethod();
				}


				if (mi == null)
				{
					return AccessModifier.Private;
				}

				return (new AccessModifierMapper()).Map(mi);
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