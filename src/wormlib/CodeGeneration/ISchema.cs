using System;

namespace Worm.CodeGeneration
{
	public interface ISchema
	{
		string Name { get; }

		void AddEntity(PocoEntity entity);

		string Render();
	}
}
