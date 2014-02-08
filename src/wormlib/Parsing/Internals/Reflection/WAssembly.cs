using System;
using System.Reflection;
using System.Collections.Generic;

namespace Worm.Parsing.Internals.Reflection
{
	public class WAssembly
	{
		protected Assembly assembly;

		public WAssembly()
		{
		}

		public WAssembly(Assembly asm)
		{
			this.assembly = asm;

			asm.GetReferencedAssemblies();
		}

		public virtual IList<AssemblyName> GetReferencedAssemblies()
		{
			return new List<AssemblyName>(this.assembly.GetReferencedAssemblies());
		}

		public virtual IEnumerable<WType> GetTypes(Func<WType, bool> predicate)
		{
			WType w;

			foreach (Type t in this.assembly.GetTypes())
			{
				w = new WType(t);
				if (predicate(w))
				{
					yield return w;
				}
			}
		}

		public virtual IEnumerable<WType> GetTypes()
		{
			return this.GetTypes(xx => { return true; });
		}
	}
}