using System;
using System.Collections.Generic;

namespace Worm.MySql.CodeGeneration.SchemaWriting
{
	internal class TypeMapper
	{
		private static readonly IDictionary<string, string> csharpToMySql = new Dictionary<string, string>() {
			  {"Int16", "smallint"}
			, {"Int32", "int"}
			, {"Int64", "long"}
			, {"Single", "float"}
			, {"Boolean", "bit(1)"}
			, {"Double", "double"}
			, {"Decimal", "decimal"}
			, {"String", "varchar(255)"}
			, {"DateTime", "datetime"}
			, {"TimeSpan", "time"}
		};

		public string GetMySqlType(string csharpTypeName)
		{
			if (!csharpToMySql.ContainsKey(csharpTypeName))
			{
				return "text"; // assume that this is a complex type that needs to be serialized
			}

			return csharpToMySql[csharpTypeName];
		}
	}
}