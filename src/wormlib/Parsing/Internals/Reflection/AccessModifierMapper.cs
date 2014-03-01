using System;
using Worm.CodeGeneration.Internals;
using System.Reflection;

namespace Worm.Parsing.Internals.Reflection
{
	public class AccessModifierMapper
	{
		public virtual AccessModifier Map(MethodBase mb)
		{
			return this.Map(mb.IsPublic, mb.IsPrivate, mb.IsFamily, mb.IsAssembly);
		}

		public virtual AccessModifier Map(bool isPublic, bool isPrivate, bool isProtected, bool isInternal)
		{
			if (isPrivate)
			{
				return AccessModifier.Private;
			}

			if (isProtected)
			{
				return AccessModifier.Protected;
			}

			if (isInternal)
			{
				return AccessModifier.Internal;
			}

			if (isPublic)
			{
				return AccessModifier.Public;
			}

			throw new InvalidOperationException("I was unable to map the set of parameters to an AccessModifier value");
		}
	}
}