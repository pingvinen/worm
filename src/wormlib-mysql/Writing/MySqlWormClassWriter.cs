using System;
using Worm.Generator.Writing;
using Worm.Generator;
using Worm.Generator.Templates;

namespace Worm.MySql.Writing
{
	public class MySqlWormClassWriter : IWormClassWriter
	{
		#region IWormClassWriter: Generator code
		public string GenerateCode(PocoModel model)
		{
			return this.GetFile(model).Content;
		}
		#endregion

		public CodeFile GetFile(PocoModel model)
		{
			model = new PocoModel() {
				DbFactory = new MySqlWormDbFactory(String.Empty),
				PocoClassName = "MixTypes",
				PocoFilename = "MixTypes.cs",
				PocoNamespace = "Spike.Entities",
				TableName = "mix_types",
				WormClassName = "WormMixTypes",
				WormFilename = "HelloWorld.cs",
				WormNamespace = "Spike.Entities.Db"
			};
			model.Fields.Add(new PocoField() {
				AccessModifier = AccessModifier.Public,
				ColumnName = "id",
				HasGetter = true,
				HasSetter = true,
				IdGenerator = Worm.DataAnnotations.WormIdGenerator.AutoIncrement,
				IsPrimaryKey = true,
				Name = "Id",
				StorageType = "int",
				Type = "int"
			});

			model.Fields.Add(new PocoField() {
				AccessModifier = AccessModifier.Public,
				ColumnName = "my_string",
				HasGetter = true,
				HasSetter = true,
				Name = "MyString",
				StorageType = "varchar(255)",
				Type = "string"
			});

			/*
			model.Methods.Add(new Method() {
				Name = "DbBuildQuery_GetId",
				Signature = "public MixTypes DbBuildQuery_GetId(int id, IWormDbConnection db)",
				Body = @"int i = 2 + 3;
return default(YourMother);"
			});
			//*/





			var dbClassTemplate = new WormDbClassTemplate();
			dbClassTemplate.Model = model;
			dbClassTemplate.DbGetByIdOrDefaultTemplate = new DbBuildQueryGetIdTemplate() { Model = model };
			dbClassTemplate.DbInsertTemplate = new DbInsertTemplate() { Model = model };
			dbClassTemplate.DbUpdateTemplate = new DbUpdateTemplate() { Model = model };

			CodeFile file = new CodeFile() {
				Filename = model.WormFilename,
				Content = dbClassTemplate.TransformText()
			};

			return file;
		}
	}
}
