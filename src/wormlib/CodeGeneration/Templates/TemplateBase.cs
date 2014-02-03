using System;

namespace Worm.CodeGeneration.Templates
{
	public abstract class TemplateBase
	{
		public virtual PocoEntity Poco { get; set; }

		public abstract string Render();
	}
}

