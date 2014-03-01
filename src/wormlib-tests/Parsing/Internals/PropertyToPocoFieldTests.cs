using System;
using NUnit.Framework;
using Moq;
using Worm.Parsing.Internals;
using Worm;
using Worm.CodeGeneration.Internals;
using Worm.Parsing.Internals.Reflection;
using Worm.DataAnnotations;

namespace Wormlibtests.Parsing.Internals
{
	[TestFixture]
	public class PropertyToPocoFieldTests
	{
		private PropertyToPocoField propToEntity;

		private Mock<WormFactory> wormFactory;
		private Mock<PocoField> pocoField;
		private Mock<WProperty> property;

		[SetUp]
		public void Setup()
		{
			this.wormFactory = new Mock<WormFactory>();
			this.pocoField = new Mock<PocoField>();
			this.property = new Mock<WProperty>(null);

			this.wormFactory.Setup(xx => xx.GetPocoField()).Returns(this.pocoField.Object);

			this.propToEntity = new PropertyToPocoField(wormFactory.Object);
		}

		[Test]
		public void Parse_NameIsSet()
		{
			this.property.SetupGet(xx => xx.Name).Returns("Name");

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.Name = It.Is<String>(actual => "Name".Equals(actual)), Times.Once);
		}

		[Test]
		public void Parse_AllowNullIsSet_HasAttribute_ValueIsFalse()
		{
			var attr = new WormAllowNullAttribute(false);
			this.property.Setup(xx => xx.GetAttribute<WormAllowNullAttribute>()).Returns(attr);

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.AllowNull = It.Is<bool>(actual => actual == true), Times.Once); // the default value in the class
			this.pocoField.VerifySet(xx => xx.AllowNull = It.Is<bool>(actual => actual == false), Times.Once);
		}

		[Test]
		public void Parse_AllowNullIsSet_HasAttribute_ValueIsTrue()
		{
			var attr = new WormAllowNullAttribute(true);
			this.property.Setup(xx => xx.GetAttribute<WormAllowNullAttribute>()).Returns(attr);

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.AllowNull = It.Is<bool>(actual => actual == true), Times.AtMost(2)); // the default value in the class
		}

		[Test]
		public void Parse_AllowNullIsSet_UnlessAttributeIsNotDefined()
		{
			this.property.Setup(xx => xx.GetAttribute<WormAllowNullAttribute>()).Returns(default(WormAllowNullAttribute));

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.AllowNull = It.Is<bool>(actual => actual == true), Times.Once); // the default value in the class
		}

		[Test]
		public void Parse_ColumnNameIsSet_HasAttribute()
		{
			var attr = new WormColumnNameAttribute("das_column");
			this.property.Setup(xx => xx.GetAttribute<WormColumnNameAttribute>()).Returns(attr);

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.ColumnName = It.Is<string>(actual => "das_column".Equals(actual)), Times.Once);
		}

		[Test]
		public void Parse_ColumnNameIsSet_DefaultToPropertyName()
		{
			this.property.Setup(xx => xx.GetAttribute<WormColumnNameAttribute>()).Returns(default(WormColumnNameAttribute));
			this.property.SetupGet(xx => xx.Name).Returns("PropertyName");

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.ColumnName = It.Is<string>(actual => "PropertyName".Equals(actual)), Times.Once);
		}

		[Test]
		public void Parse_HasGetterIsSet()
		{
			this.property.SetupGet(xx => xx.HasGetter).Returns(true);

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.HasGetter = It.Is<bool>(actual => actual == true), Times.Once);
		}

		[Test]
		public void Parse_ReturnsInstance()
		{
			PocoField actual = this.propToEntity.Parse(this.property.Object);

			Assert.AreSame(this.pocoField.Object, actual);
		}
	}
}