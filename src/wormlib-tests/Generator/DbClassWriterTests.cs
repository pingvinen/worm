using System;
using NUnit.Framework;
using Moq;
using Worm.Generator;
using Worm.Generator.Writing;
using Worm.Generator.Templates;
using Worm;

namespace Wormlibtests.Generator
{
	[TestFixture]
	public class DbClassWriterTests
	{
		private DbClassWriter writer;

		private Mock<PocoEntity> poco;
		private Mock<WormFactory> factory;
		private Mock<IWormDbFactory> dbFactory;
		private Mock<IWormTemplateProvider> templateProvider;
		private Mock<WormDbClassTemplate> dbClassTemplate;
		private Mock<DbGetByIdOrDefaultTemplateBase> getByIdOrDefault;
		private Mock<DbUpdateTemplateBase> update;
		private Mock<DbInsertTemplateBase> insert;

		[SetUp]
		public void Setup()
		{
			this.poco = new Mock<PocoEntity>();
			this.factory = new Mock<WormFactory>();
			this.dbFactory = new Mock<IWormDbFactory>();
			this.templateProvider = new Mock<IWormTemplateProvider>();
			this.dbClassTemplate = new Mock<WormDbClassTemplate>();
			this.getByIdOrDefault = new Mock<DbGetByIdOrDefaultTemplateBase>();
			this.update = new Mock<DbUpdateTemplateBase>();
			this.insert = new Mock<DbInsertTemplateBase>();

			this.dbClassTemplate.SetupAllProperties();
			this.poco.SetupGet<IWormDbFactory>(xx => xx.DbFactory).Returns(this.dbFactory.Object);
			this.dbFactory.Setup(xx => xx.GetTemplateProvider()).Returns(this.templateProvider.Object);
			this.factory.Setup(xx => xx.GetWormDbClassTemplate()).Returns(this.dbClassTemplate.Object);
			this.templateProvider.Setup(xx => xx.GetDbGetByIdOrDefaultTemplate()).Returns(this.getByIdOrDefault.Object);
			this.templateProvider.Setup(xx => xx.GetDbUpdateTemplate()).Returns(this.update.Object);
			this.templateProvider.Setup(xx => xx.GetDbInsertTemplate()).Returns(this.insert.Object);

			this.writer = new DbClassWriter(this.factory.Object);
		}

		[Test]
		public void Generate_SetsGetByIdOrDefaultTemplate()
		{
			this.writer.Generate(this.poco.Object);

			this.dbClassTemplate.VerifySet(xx => xx.DbGetByIdOrDefaultTemplate = It.Is<DbGetByIdOrDefaultTemplateBase>(actual => this.getByIdOrDefault.Object.Equals(actual)), Times.Once);
		}

		[Test]
		public void Generate_SetsGetByIdOrDefaultTemplateModel()
		{
			this.writer.Generate(this.poco.Object);

			this.getByIdOrDefault.VerifySet(xx => xx.Poco = It.Is<PocoEntity>(actual => this.poco.Object.Equals(actual)), Times.Once);
		}

		[Test]
		public void Generate_SetsInsertTemplate()
		{
			this.writer.Generate(this.poco.Object);

			this.dbClassTemplate.VerifySet(xx => xx.DbInsertTemplate = It.Is<DbInsertTemplateBase>(actual => this.insert.Object.Equals(actual)), Times.Once);
		}

		[Test]
		public void Generate_SetsInsertTemplateModel()
		{
			this.writer.Generate(this.poco.Object);

			this.insert.VerifySet(xx => xx.Poco = It.Is<PocoEntity>(actual => this.poco.Object.Equals(actual)), Times.Once);
		}

		[Test]
		public void Generate_SetsUpdateTemplate()
		{
			this.writer.Generate(this.poco.Object);

			this.dbClassTemplate.VerifySet(xx => xx.DbUpdateTemplate = It.Is<DbUpdateTemplateBase>(actual => this.update.Object.Equals(actual)), Times.Once);
		}

		[Test]
		public void Generate_SetsUpdateTemplateModel()
		{
			this.writer.Generate(this.poco.Object);

			this.update.VerifySet(xx => xx.Poco = It.Is<PocoEntity>(actual => this.poco.Object.Equals(actual)), Times.Once);
		}

		[Test]
		public void Generate_returnsCodeFile_FilenameIsSet()
		{
			this.poco.SetupGet(xx => xx.WormFilename).Returns("worm filename");

			var result = this.writer.Generate(this.poco.Object);

			Assert.AreEqual("worm filename", result.Filename);
		}

		[Test]
		public void Generate_returnsCodeFile_ContentIsSet()
		{
			this.dbClassTemplate.Setup(xx => xx.TransformText()).Returns("generated code");

			var result = this.writer.Generate(this.poco.Object);

			Assert.AreEqual("generated code", result.Content);
		}
	}
}