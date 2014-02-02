using System;
using Worm.Generator.Templates;
using Worm.MySql.Writing.Templates;

namespace Worm.MySql.Writing
{
	public class DbInsertTemplate : DbInsertTemplateBase
	{
		public override string Render()
		{
			var template = new DbInsertTemplateT4();
			template.Model = base.Model;

			string source = template.TransformText();

			source = source.Replace("\n", "\n\t\t");

			return source;
		}
	}
}