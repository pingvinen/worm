using System;

namespace Worm.Generator.Templates
{
	public abstract class TemplateBase
	{
		public PocoModel Model { get; set; }

		public abstract string Render();
	}
}

