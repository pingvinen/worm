using System;
using System.Collections.Generic;

namespace Worm.CodeGeneration
{
	public class PocoModel
	{
		public virtual IList<PocoEntity> Entities { get; set; }

		public PocoModel()
		{
			this.Entities = new List<PocoEntity>();
		}
	}
}