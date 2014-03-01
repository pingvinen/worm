using System;
using NUnit.Framework;
using Worm;
using Worm.Postgres;
using Worm.DataAnnotations;
using Worm.CodeGeneration;
using Worm.CodeGeneration.Internals;

namespace Functionaltests.Postgres.CodeGeneration
{
	[TestFixture]
	public class WormDbClassGeneratorTests
	{
		[Test]
		public void Test()
		{
			var dbClassWriter = new DbClassWriter(new WormFactory());
			var entity = new PocoEntity() {
				DbFactory = new PostgresWormDbFactory(String.Empty),
				PocoClassName = "TestEntity",
				PocoFilename = "TestEntity.cs",
				PocoNamespace = "Test.Entities",
				TableName = "test_entities",
				WormClassName = "WormTestEntity",
				WormFilename = "WormTestEntity.cs",
				WormNamespace = "Test.Entities.Db"
			};

			entity.Fields.Add(new PocoField() {
				AccessModifier = AccessModifier.Public,
				ColumnName = "id",
				HasGetter = true,
				HasSetter = true,
				IdGenerator = WormIdGenerator.AutoIncrement,
				IsPrimaryKey = true,
				Name = "Id",
				StorageType = "int",
				Type = "int"
			});

			entity.Fields.Add(new PocoField() {
				AccessModifier = AccessModifier.Public,
				ColumnName = "my_string",
				HasGetter = true,
				HasSetter = true,
				Name = "MyString",
				StorageType = "varchar(255)",
				Type = "string"
			});

			entity.Fields.Add(new PocoField() {
				AccessModifier = AccessModifier.Public,
				ColumnName = "my_int",
				HasGetter = true,
				HasSetter = true,
				Name = "MyInt",
				StorageType = "int",
				Type = "int"
			});

			var result = dbClassWriter.Generate(entity);

			System.IO.File.WriteAllText("/tmp/postgres_" + result.Filename, result.Content);

			Assert.AreEqual("WormTestEntity.cs", result.Filename);
		}
	}
}