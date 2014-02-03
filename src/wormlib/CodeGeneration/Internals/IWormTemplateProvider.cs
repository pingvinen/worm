using System;
using Worm.CodeGeneration.Templates;

namespace Worm.CodeGeneration.Internals
{
	public interface IWormTemplateProvider
	{
		DbGetByIdOrDefaultTemplateBase GetDbGetByIdOrDefaultTemplate();
		DbInsertTemplateBase GetDbInsertTemplate();
		DbUpdateTemplateBase GetDbUpdateTemplate();
	}
}

