using System;
using Worm.Generator.Templates;
using Worm.MySql.Writing.Templates;

namespace Worm.MySql.Writing
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

