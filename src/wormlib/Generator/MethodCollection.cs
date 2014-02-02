using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Worm.Generator
{
	public class MethodCollection : Collection<Method>
	{
		public Method GetMethodByName(string name)
		{
			return base.Items.Where(xx => xx.Name.Equals(name)).FirstOrDefault();
		}
	}
}