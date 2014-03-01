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

			// allow null
			var allowNullAttribute = property.GetAttribute<WormAllowNullAttribute>();
			if (allowNullAttribute != default(WormAllowNullAttribute))
			{
				result.AllowNull = allowNullAttribute.Value;
			}

			// column name
			string columnName = this.GetValueFromAttributeOrDefault<WormColumnNameAttribute,string>(
				  property
				, (xx) => xx.Value
				, property.Name
			);
			result.ColumnName = columnName;

			// has getter
			result.HasGetter = property.HasGetter;

			// has setter
			result.HasSetter = property.HasSetter;

			return result;
		}

		#region Get value from attribute or default
		/// <summary>
		/// Get the value of a given attribute, or a given default value
		/// </summary>
		/// <returns>The value from the attribute (if defined), the default value otherwise</returns>
		/// <param name="property">The property</param>
		/// <param name="getter">Function that gets the value from the attribute</param>
		/// <param name="defaultValue">Value to return if attribute is not defined</param>
		/// <typeparam name="TAttribute">The type of the attribute</typeparam>
		/// <typeparam name="TResult">The data type</typeparam>
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
		#endregion
	}
}