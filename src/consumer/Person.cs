using System;
using Worm.DataAnnotations;

namespace consumer
{
	[WormDbFactory(typeof(Worm.MySql.MySqlWormDbFactory))]
	public class Person
	{
		[WormPrimaryKey]
		[WormIdGenerator(WormIdGenerator.AutoIncrement)]
		public int Id { get; set; }

		public string Name { get; set; }

		public int Age { get; set; }
	}
}

