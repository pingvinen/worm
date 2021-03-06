using System;
using Worm.CodeGeneration.Templates;
using Worm.CodeGeneration;
using Worm.Parsing.Internals;
using Worm.CodeGeneration.Internals;

namespace Worm
{
	public class WormFactory
	{
		public virtual WormDbClassTemplate GetWormDbClassTemplate()
		{
			return new WormDbClassTemplate();
		}

		public virtual PocoEntity GetPocoEntity()
		{
			return new PocoEntity();
		}

		public virtual PocoField GetPocoField()
		{
			return new PocoField();
		}

		public virtual TypeToEntity GetTypeToEntity()
		{
			return new TypeToEntity(this);
		}

		public virtual PropertyToPocoField GetPropertyToPocoField()
		{
			return new PropertyToPocoField(this);
		}
	}
}

