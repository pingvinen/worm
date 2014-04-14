using System;
using Worm.DataAnnotations;

namespace fullflowlib
{
	[WormDbFactory(typeof(Worm.MySql.MySqlWormDbFactory))]
	[WormTable("persons")]
	public class Person
	{
		[WormPrimaryKey]
		[WormIdGenerator(WormIdGenerator.AutoIncrement)]
		public int Id { get; set; }

		public string Name { get; set; }

		[WormColumnName("age_in_years")]
		public int Age { get; set; }

		[WormIgnore]
		public int RuntimeState { get; set; }

		public Role Role { get; set; }

		protected decimal InternalState { get; set; }
	}
}

