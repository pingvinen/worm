using System;
using Worm.Postgres.CodeGeneration.Templates;
using Worm.CodeGeneration.Templates;

namespace Worm.Postgres.CodeGeneration
{
	public class DbBuildQueryGetIdTemplate : DbGetByIdOrDefaultTemplateBase
	{
		public override string Render()
		{
			var template = new DbGetByIdOrDefaultT4();
			template.Model = base.Poco;

			string source = template.TransformText();

			source = source.Replace("\n", "\n\t\t");

			return source;
		}
	}
}

