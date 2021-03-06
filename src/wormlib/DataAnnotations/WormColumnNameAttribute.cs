using System;

namespace Worm.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class WormColumnNameAttribute : Attribute
	{
		public WormColumnNameAttribute (string columnName)
		{
			this.Value = columnName;
		}

		public string Value { get; set; }
	}
}