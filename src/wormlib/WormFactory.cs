using System;
using Worm.Generator.Templates;

namespace Worm
{
	public class WormFactory
	{
		public virtual WormDbClassTemplate GetWormDbClassTemplate()
		{
			return new WormDbClassTemplate();
		}
	}
}

