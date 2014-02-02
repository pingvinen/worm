using System;
using NUnit.Framework;
using Moq;
using Worm.Generator;
using Worm.MySql.Writing;

namespace MySqlGeneratorTests
{
	[TestFixture]
	public class WriterClassDefinitionTests
	{
		private MySqlWormClassWriter writer;

		private Mock<PocoModel> model;

		[SetUp]
		public void Setup()
		{
			this.model = new Mock<PocoModel>();

			this.writer = new MySqlWormClassWriter();
		}

		[NUnit.Framework.Test]
		public void Usings()
		{
			CodeFile file = this.writer.GetFile(this.model.Object);

			System.IO.File.WriteAllText("/tmp/" + file.Filename, file.Content);

			Assert.AreEqual("HelloWorld.cs", file.Filename);

		}
	}
}