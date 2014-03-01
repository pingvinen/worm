using System;
using System.Reflection;
using Worm.CodeGeneration.Internals;

namespace Worm.Parsing.Internals.Reflection
{
	public class WProperty
	{
		protected PropertyInfo pi;

		protected AccessModifierMapper accessMapper;

		public WProperty(PropertyInfo pi, AccessModifierMapper accessMapper)
		{
			this.accessMapper = accessMapper;
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
				return this.pi.CanRead;
			}
		}

		public virtual bool HasSetter
		{
			get
			{
				return this.pi.CanWrite;
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
					mi = this.pi.GetSetMethod(true);
				}
				else if (this.pi.CanRead)
				{
					mi = this.pi.GetGetMethod(true);
				}


				if (mi == null)
				{
					return AccessModifier.Private;
				}

				return this.accessMapper.Map(mi);
			}
		}

		public virtual Type Type
		{
			get
			{
				return this.pi.PropertyType;
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