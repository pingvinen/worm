using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace Worm.CodeGeneration.Internals
{
	public class PocoFieldCollection : Collection<PocoField>
	{
		public PocoField GetPrimaryKeyField()
		{
			return base.Items.FirstOrDefault(xx => xx.IsPrimaryKey);
		}

		public virtual new void Add(PocoField item)
		{
			//
			// This override is only here to make
			// it possible to mock it in tests
			//

			base.Add(item);
		}

		public virtual IEnumerable<PocoField> GetPublicFields()
		{
			return (
				from yy in base.Items
				where 
				yy.AccessModifier == AccessModifier.Public 
				&& yy.HasSetter
				&& !yy.IsPrimaryKey
				select yy
			).AsEnumerable();
		}

		public virtual IEnumerable<PocoField> GetInsertFields()
		{
			return (
				from yy in base.Items
				where !yy.IsPrimaryKey
				select yy
			).AsEnumerable();
		}

		public virtual IEnumerable<PocoField> GetValueTrackedFields()
		{
			return (
				from yy in base.Items
				where 
				yy.HasSetter
				&& !yy.IsPrimaryKey
				select yy
			).AsEnumerable();
		}
	}
}