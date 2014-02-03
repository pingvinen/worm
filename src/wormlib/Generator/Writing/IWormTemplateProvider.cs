using System;
using Worm.Generator.Templates;

namespace Worm.Generator.Writing
{
	public interface IWormTemplateProvider
	{
		DbGetByIdOrDefaultTemplateBase GetDbGetByIdOrDefaultTemplate();
		DbInsertTemplateBase GetDbInsertTemplate();
		DbUpdateTemplateBase GetDbUpdateTemplate();
	}
}

