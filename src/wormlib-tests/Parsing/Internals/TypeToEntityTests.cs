using System;
using NUnit.Framework;
using Moq;
using Worm;
using Worm.Parsing.Internals;
using Worm.Parsing.Internals.Reflection;
using Worm.CodeGeneration;
using Worm.DataAnnotations;
using System.Collections.Generic;
using System.IO;
using Worm.CodeGeneration.Internals;

namespace Wormlibtests.Parsing.Internals
{
	[TestFixture]
	public class TypeToEntityTests
	{
		private TypeToEntity typeToEntity;

		private Mock<WormFactory> factory;
		private Mock<WType> entityType;
		private Mock<PocoEntity> pocoEntity;
		private Mock<IWormDbFactory> dbFactory;
		private Mock<WormDbFactoryAttribute> dbFactoryAttr;
		private Mock<WType> dbFactoryType;
		private Mock<WormTableAttribute> tableAttr;
		private Mock<WProperty> property1;
		private Mock<WProperty> property2;
		private Mock<PocoFieldCollection> pocoFieldCollection;
		private Mock<PropertyToPocoField> propertyToPocoField;
		private Mock<PocoField> pocoField1;
		private Mock<PocoField> pocoField2;

		[SetUp]
		public void Setup()
		{
			this.factory = new Mock<WormFactory>();
			this.entityType = new Mock<WType>();
			this.pocoEntity = new Mock<PocoEntity>();
			this.dbFactory = new Mock<IWormDbFactory>();
			this.dbFactoryType = new Mock<WType>();
			this.dbFactoryAttr = new Mock<WormDbFactoryAttribute>(typeof(UniTestWormDbFactory));
			this.dbFactoryAttr.SetupGet(xx => xx.DbFactoryType).Returns(this.dbFactoryType.Object);
			this.dbFactoryType.Setup(xx => xx.Implements(It.IsAny<Type>())).Returns(true);
			this.tableAttr = new Mock<WormTableAttribute>(String.Empty);
			this.property1 = new Mock<WProperty>(null, null);
			this.property2 = new Mock<WProperty>(null, null);
			this.pocoField1 = new Mock<PocoField>();
			this.pocoField2 = new Mock<PocoField>();
			this.propertyToPocoField = new Mock<PropertyToPocoField>(this.factory.Object);
			this.pocoFieldCollection = new Mock<PocoFieldCollection>();

			this.entityType.SetupGet(xx => xx.Namespace).Returns("Name.Space");
			this.entityType.SetupGet(xx => xx.Name).Returns("Name");
			this.entityType.Setup(xx => xx.GetProperties()).Returns(new List<WProperty>());

			this.pocoEntity.SetupGet(xx => xx.Fields).Returns(this.pocoFieldCollection.Object);
			this.pocoEntity.SetupGet(xx => xx.WormClassName).Returns("WormClassName");
			this.pocoEntity.SetupGet(xx => xx.WormNamespace).Returns("WormNamespace");

			this.entityType.Setup(xx => xx.GetAttribute<WormDbFactoryAttribute>()).Returns(this.dbFactoryAttr.Object);
			this.dbFactoryAttr.SetupGet(xx => xx.DbFactoryType).Returns(this.dbFactoryType.Object);
			this.dbFactoryType.Setup(xx => xx.CreateInstance()).Returns(this.dbFactory.Object);

			this.factory.Setup(xx => xx.GetPocoEntity()).Returns(this.pocoEntity.Object);
			this.factory.Setup(xx => xx.GetPropertyToPocoField()).Returns(this.propertyToPocoField.Object);

			this.typeToEntity = new TypeToEntity(this.factory.Object);
		}

		[Test]
		public void Parse_dbFactory_isSet()
		{
			this.typeToEntity.Parse(this.entityType.Object);

			this.pocoEntity.VerifySet(xx => xx.DbFactory = It.Is<IWormDbFactory>(actual => this.dbFactory.Object.Equals(actual)), Times.Once);
		}

		[Test]
		public void Parse_pocoClassName_isSet()
		{
			this.entityType.SetupGet(xx => xx.Name).Returns("Name");

			this.typeToEntity.Parse(this.entityType.Object);

			this.pocoEntity.VerifySet(xx => xx.PocoClassName = It.Is<String>(actual => "Name".Equals(actual)), Times.Once);
		}

		[Test]
		public void Parse_pocoNamespace_isSet()
		{
			this.entityType.SetupGet(xx => xx.Namespace).Returns("Name.Space");

			this.typeToEntity.Parse(this.entityType.Object);

			this.pocoEntity.VerifySet(xx => xx.PocoNamespace = It.Is<String>(actual => "Name.Space".Equals(actual)), Times.Once);
		}

		[Test]
		public void Parse_pocoFilename_isSet()
		{
			this.entityType.SetupGet(xx => xx.Namespace).Returns("Name.Space");
			this.entityType.SetupGet(xx => xx.Name).Returns("Name");

			this.typeToEntity.Parse(this.entityType.Object);

			string expected = String.Format("Name{0}Space{0}Name.cs", Path.DirectorySeparatorChar);
			this.pocoEntity.VerifySet(xx => xx.PocoFilename = It.Is<String>(actual => expected.Equals(actual)), Times.Once);
		}

		[Test]
		public void Parse_tableName_isSet_default()
		{
			this.entityType.SetupGet(xx => xx.Name).Returns("Name");

			this.typeToEntity.Parse(this.entityType.Object);

			this.pocoEntity.VerifySet(xx => xx.TableName = It.Is<String>(actual => "Name".Equals(actual)), Times.Once);
		}

		[Test]
		public void Parse_tableName_isSet_definedByAttribute()
		{
			this.entityType.SetupGet(xx => xx.Name).Returns("Name");
			this.entityType.Setup(xx => xx.GetAttribute<WormTableAttribute>()).Returns(this.tableAttr.Object);
			this.tableAttr.SetupGet(xx => xx.TableName).Returns("TableNameOfDoom");

			this.typeToEntity.Parse(this.entityType.Object);

			this.pocoEntity.VerifySet(xx => xx.TableName = It.Is<String>(actual => "TableNameOfDoom".Equals(actual)), Times.Once);
		}

		[Test]
		public void Parse_addsProperties()
		{
			this.entityType.Setup(xx => xx.GetProperties()).Returns(new List<WProperty>() {
				  this.property1.Object
				, this.property2.Object
			});

			this.propertyToPocoField.Setup(xx => xx.Parse(this.property1.Object)).Returns(this.pocoField1.Object);
			this.propertyToPocoField.Setup(xx => xx.Parse(this.property2.Object)).Returns(this.pocoField2.Object);

			this.typeToEntity.Parse(this.entityType.Object);

			this.pocoFieldCollection.Verify(xx => xx.Add(this.pocoField1.Object), Times.Once);
			this.pocoFieldCollection.Verify(xx => xx.Add(this.pocoField2.Object), Times.Once);
		}

		[Test]
		public void Parse_addsProperties_butNotIgnoredFields()
		{
			this.property1.Setup(xx => xx.GetAttribute<WormIgnoreAttribute>()).Returns(new WormIgnoreAttribute());

			this.entityType.Setup(xx => xx.GetProperties()).Returns(new List<WProperty>() {
				  this.property1.Object
				, this.property2.Object
			});

			this.propertyToPocoField.Setup(xx => xx.Parse(this.property1.Object)).Returns(this.pocoField1.Object);
			this.propertyToPocoField.Setup(xx => xx.Parse(this.property2.Object)).Returns(this.pocoField2.Object);

			this.typeToEntity.Parse(this.entityType.Object);

			this.pocoFieldCollection.Verify(xx => xx.Add(this.pocoField1.Object), Times.Never);
			this.pocoFieldCollection.Verify(xx => xx.Add(this.pocoField2.Object), Times.Once);
		}

		[Test]
		public void Parse_wormClassName_isSet()
		{
			this.entityType.SetupGet(xx => xx.Name).Returns("Entity");

			this.typeToEntity.Parse(this.entityType.Object);

			this.pocoEntity.VerifySet(xx => xx.WormClassName = It.Is<String>(actual => "WormEntity".Equals(actual)), Times.Once);
		}

		[Test]
		public void Parse_wormNamespace_isSet()
		{
			this.pocoEntity.SetupGet(xx => xx.PocoNamespace).Returns("Name.Space");

			this.typeToEntity.Parse(this.entityType.Object);

			this.pocoEntity.VerifySet(xx => xx.WormNamespace = It.Is<String>(actual => "Name.Space.Db".Equals(actual)), Times.Once);
		}

		[Test]
		public void Parse_wormFilename_isSet()
		{
			this.pocoEntity.SetupGet(xx => xx.WormNamespace).Returns("Name.Space.Db");
			this.pocoEntity.SetupGet(xx => xx.WormClassName).Returns("WormEntity");

			this.typeToEntity.Parse(this.entityType.Object);

			string expected = String.Format("Space{0}Db{0}WormEntity.cs", Path.DirectorySeparatorChar);
			this.pocoEntity.VerifySet(xx => xx.WormFilename = It.Is<String>(actual => expected.Equals(actual)), Times.Once);
		}

		[Test]
		public void Parse_wormFilename_isSet_fileInRootNamespace()
		{
			this.pocoEntity.SetupGet(xx => xx.WormNamespace).Returns("Name");
			this.pocoEntity.SetupGet(xx => xx.WormClassName).Returns("WormEntity");

			this.typeToEntity.Parse(this.entityType.Object);

			string expected = "WormEntity.cs";
			this.pocoEntity.VerifySet(xx => xx.WormFilename = It.Is<String>(actual => expected.Equals(actual)), Times.Once);
		}
	}
}