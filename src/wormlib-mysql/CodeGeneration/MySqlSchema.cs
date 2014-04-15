using System;
using Worm.CodeGeneration;

namespace Worm.MySql.CodeGeneration
{
	public class MySqlSchema : ISchema
	{
		public MySqlSchema()
		{
			this.Name = "mysql";
		}

		#region ISchema: AddEntity
		public void AddEntity(PocoEntity entity)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region ISchema: Render
		public string Render()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region ISchema: Name
		public string Name { get; set; }
		#endregion
	}
}
