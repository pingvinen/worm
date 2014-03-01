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

			string columnName = this.GetValueFromAttributeOrDefault<WormColumnNameAttribute,string>(
				  property
				, (xx) => xx.Value
				, property.Name
			);
			result.ColumnName = columnName;

			return result;
		}

		private TResult GetValueFromAttributeOrDefault<TAttribute, TResult>(
			  WProperty property
			, Func<TAttribute,TResult> getter
			, TResult defaultValue)
			where TAttribute : Attribute
		{
			var attr = property.GetAttribute<TAttribute>();
			if (attr != default(TAttribute))
			{
				return getter(attr);
			}

			return defaultValue;
		}
	}
}