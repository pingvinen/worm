using System;
using Worm.CodeGeneration;
using Worm.Parsing.Internals.Reflection;
using Worm.DataAnnotations;
using System.IO;

namespace Worm.Parsing.Internals
{
	public class TypeToEntity
	{
		protected WormFactory factory;

		public TypeToEntity(WormFactory factory)
		{
			this.factory = factory;
		}

		public virtual PocoEntity Parse(WType type)
		{
			PocoEntity result = this.factory.GetPocoEntity();

			// this attribute is always set, otherwise we would not be here
			result.DbFactory = (IWormDbFactory)type.GetAttribute<WormDbFactoryAttribute>().DbFactoryType.CreateInstance();

			result.PocoClassName = type.Name;
			result.PocoNamespace = type.Namespace;
			result.PocoFilename = String.Format("{0}{1}{2}.cs", 
				type.Namespace.Replace(".", Path.DirectorySeparatorChar.ToString())
				, Path.DirectorySeparatorChar
				, type.Name
			);
			result.TableName = this.GetTableName(type);

			this.AddProperties(result, type);

			return result;
		}

		#region Table name
		protected string GetTableName(WType type)
		{
			WormTableAttribute attr = type.GetAttribute<WormTableAttribute>();
			if (attr != default(WormTableAttribute))
			{
				return attr.TableName;
			}

			return type.Name;
		}
		#endregion

		#region Add properties
		protected void AddProperties(PocoEntity result, WType type)
		{
			PropertyToPocoField converter = this.factory.GetPropertyToPocoField();
			foreach (WProperty property in type.GetProperties())
			{
				if (property.GetAttribute<WormIgnoreAttribute>() == default(WormIgnoreAttribute))
				{
					result.Fields.Add(converter.Parse(property));
				}
			}
		}
		#endregion
	}
}