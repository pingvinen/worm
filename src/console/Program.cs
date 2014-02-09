using System;
using Worm.Parsing;
using Worm.CodeGeneration;
using Worm.CodeGeneration.Internals;

namespace console
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			var parser = new Parser(new Worm.WormFactory());
			PocoModel model = parser.Parse("/home/pingvinen/gitclones/me/worm/src/consumer/consumer.csproj");

			foreach (PocoEntity entity in model.Entities)
			{
				Console.WriteLine("DbFactory: {0}", entity.DbFactory.GetType().Name);
				Console.WriteLine("PocoClassName: {0}", entity.PocoClassName);
				Console.WriteLine("PocoFilename: {0}", entity.PocoFilename);
				Console.WriteLine("PocoNamespace: {0}", entity.PocoNamespace);
				Console.WriteLine("TableName: {0}", entity.TableName);
				Console.WriteLine();

				foreach (PocoField field in entity.Fields)
				{
					Console.WriteLine("AccessModifier: {0}", field.AccessModifier);
					Console.WriteLine("AllowNull: {0}", field.AllowNull);
					Console.WriteLine("ColumnName: {0}", field.ColumnName);
					Console.WriteLine("DoPersist: {0}", field.DoPersist);
					Console.WriteLine("HasGetter: {0}", field.HasGetter);
					Console.WriteLine("HasSetter: {0}", field.HasSetter);
					Console.WriteLine("IdGenerator: {0}", field.IdGenerator);
					Console.WriteLine("IsEnum: {0}", field.IsEnum);
					Console.WriteLine("IsPrimaryKey: {0}", field.IsPrimaryKey);
					Console.WriteLine("Name: {0}", field.Name);
					Console.WriteLine("StorageType: {0}", field.StorageType);
					Console.WriteLine("Type: {0}", field.Type);
					Console.WriteLine();
				}

				Console.WriteLine();
			}
		}
	}
}
