using System;
using Worm.Parsing;

namespace console
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			var parser = new Parser();
			parser.Parse("/home/pingvinen/gitclones/me/worm/src/consumer/consumer.csproj");
		}
	}
}
