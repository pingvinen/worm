using System;
using Worm.CodeGeneration.Internals;
using Worm.CodeGeneration.Templates;

namespace Worm.Postgres.CodeGeneration
{
	public class PostgressWormTemplateProvider : IWormTemplateProvider
	{
		public virtual DbGetByIdOrDefaultTemplateBase GetDbGetByIdOrDefaultTemplate()
		{
			throw new NotImplementedException();
		}

		public virtual DbInsertTemplateBase GetDbInsertTemplate()
		{
			throw new NotImplementedException();
		}

		public virtual DbUpdateTemplateBase GetDbUpdateTemplate()
		{
			throw new NotImplementedException();
		}
	}
}

