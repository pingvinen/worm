using System;
using Worm.MySql.CodeGeneration.Templates;
using Worm.CodeGeneration.Templates;

namespace Worm.MySql.CodeGeneration
{
	public class DbInsertTemplate : DbInsertTemplateBase
	{
		public override string Render()
		{
			var template = new DbInsertTemplateT4();
			template.Model = base.Poco;

			string source = template.TransformText();

			source = source.Replace("\n", "\n\t\t");

			return source;
		}
	}
}