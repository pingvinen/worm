using System;
using Worm.CodeGeneration;
using Worm.Parsing.Internals.Reflection;
using Worm.DataAnnotations;
using System.IO;
using System.Linq;

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

			result.WormClassName = String.Format("Worm{0}", type.Name);
			result.WormNamespace = String.Format("{0}.Db", result.PocoNamespace);
			result.WormFilename = String.Format("{0}{1}.cs", 
				  this.NamespaceAsPathAssumingUseOfRootNamespace(result.WormNamespace)
				, result.WormClassName
			);

			return result;
		}

		private string NamespaceAsPathAssumingUseOfRootNamespace(string ns)
		{
			string[] parts = ns.Split(new char[]{ '.' }, StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length == 1)
			{
				return String.Empty; // assuming that the first level namespace represents the root
			}

			string path = String.Join(Path.DirectorySeparatorChar.ToString(), parts.Skip(1));
			return String.Format("{0}{1}", path, Path.DirectorySeparatorChar);
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