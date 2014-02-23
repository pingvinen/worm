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

		#region Name
		public virtual string Name
		{
			get
			{
				return this.type.Name;
			}
		}
		#endregion

		#region Namespace
		public virtual string Namespace
		{
			get
			{
				return this.type.Namespace;
			}
		}
		#endregion

		#region Has attribute
		public virtual bool HasAttribute(Type attributeType)
		{
			return this.type.GetCustomAttributes(attributeType, true).Any();
		}
		#endregion

		#region Get attribute
		public virtual TAttribute GetAttribute<TAttribute>() where TAttribute : Attribute
		{
			object[] x = this.type.GetCustomAttributes(typeof(TAttribute), true);
			if (x.Length == 0)
			{
				return default(TAttribute);
			}

			return (TAttribute)x[0];
		}
		#endregion

		#region Implements
		public virtual bool Implements(Type interfaceType)
		{
			Type[] faces = this.type.GetInterfaces();

			foreach (Type cur in faces)
			{
				if (cur == interfaceType)
				{
					return true;
				}
			}

			return false;

			/**
			 * We use a loop on GetInterfaces
			 * instead of GetInterface(string), because
			 * the latter does not catch generic interfaces
			 * like IList<String>
			 */
		}
		#endregion

		#region Create instance
		public virtual object CreateInstance()
		{
			return Activator.CreateInstance(this.type);
		}
		#endregion

		#region Get properties
		public virtual IEnumerable<WProperty> GetProperties()
		{
			foreach (PropertyInfo pi in this.type.GetProperties())
			{
				yield return new WProperty(pi);
			}
		}
		#endregion
	}
}