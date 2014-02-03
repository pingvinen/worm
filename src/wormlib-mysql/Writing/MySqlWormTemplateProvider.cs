using System;
using Worm.Generator.Writing;
using Worm.Generator.Templates;

namespace Worm.MySql.Writing
{
	public class MySqlWormTemplateProvider : IWormTemplateProvider
	{
		public virtual DbGetByIdOrDefaultTemplateBase GetDbGetByIdOrDefaultTemplate()
		{
			return new DbBuildQueryGetIdTemplate();
		}

		public virtual DbInsertTemplateBase GetDbInsertTemplate()
		{
			return new DbInsertTemplate();
		}

		public virtual DbUpdateTemplateBase GetDbUpdateTemplate()
		{
			return new DbUpdateTemplate();
		}
	}
}

