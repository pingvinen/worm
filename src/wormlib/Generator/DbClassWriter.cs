using System;
using Worm.Generator.Templates;

namespace Worm.Generator
{
	public class DbClassWriter
	{
		protected WormFactory factory;

		public DbClassWriter(WormFactory factory)
		{
			this.factory = factory;
		}

		public CodeFile Generate(PocoEntity model)
		{
			var template = this.factory.GetWormDbClassTemplate();
			template.Model = model;

			var provider = model.DbFactory.GetTemplateProvider();

			template.DbGetByIdOrDefaultTemplate = provider.GetDbGetByIdOrDefaultTemplate();
			template.DbGetByIdOrDefaultTemplate.Poco = model;

			template.DbInsertTemplate = provider.GetDbInsertTemplate();
			template.DbInsertTemplate.Poco = model;

			template.DbUpdateTemplate = provider.GetDbUpdateTemplate();
			template.DbUpdateTemplate.Poco = model;

			return new CodeFile() {
				Filename = model.WormFilename,
				Content = template.TransformText()
			};
		}
	}
}