using System;

namespace Worm.Generator.Templates
{
	public abstract class TemplateBase
	{
		public virtual PocoEntity Poco { get; set; }

		public abstract string Render();
	}
}

