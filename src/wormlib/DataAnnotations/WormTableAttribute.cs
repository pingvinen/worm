using System;

namespace Worm.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Class)]
	public class WormTableAttribute : Attribute
	{
		public WormTableAttribute(string tableName)
		{
			this.TableName = tableName;
		}

		public virtual string TableName { get; private set; }
	}
}