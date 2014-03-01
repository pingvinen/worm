using System;

namespace Worm.DataAnnotations
{
	public class WormStorageTypeAttribute : Attribute
	{
		public WormStorageTypeAttribute (string dbType)
		{
			this.Value = dbType;
		}

		public string Value { get; set; }
	}
}

