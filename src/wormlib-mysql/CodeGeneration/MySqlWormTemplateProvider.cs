using System;
using Worm.CodeGeneration.Internals;
using Worm.CodeGeneration.Templates;

namespace Worm.MySql.CodeGeneration
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

