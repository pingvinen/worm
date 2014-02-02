using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace Worm.Generator
{
	public class PocoFieldCollection : Collection<PocoField>
	{
		public PocoField GetPrimaryKeyField()
		{
			return base.Items.Where(xx => xx.IsPrimaryKey).FirstOrDefault();
		}

		public IEnumerable<PocoField> GetPublicFields()
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

		public IEnumerable<PocoField> GetInsertFields()
		{
			return (
				from yy in base.Items
				where !yy.IsPrimaryKey
				select yy
			).AsEnumerable();
		}
	}
}