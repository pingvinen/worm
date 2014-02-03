using System;
using Worm.Generator.Writing;
using Worm.Generator.Templates;

namespace Worm.Postgres.Writing
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

