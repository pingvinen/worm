using System;
using Worm.CodeGeneration.Templates;

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

