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
		private Mock<Type> propertyType;

		[SetUp]
		public void Setup()
		{
			this.wormFactory = new Mock<WormFactory>();
			this.pocoField = new Mock<PocoField>();
			this.property = new Mock<WProperty>(null);
			this.propertyType = new Mock<Type>();

			this.wormFactory.Setup(xx => xx.GetPocoField()).Returns(this.pocoField.Object);
			this.property.SetupGet(xx => xx.Type).Returns(this.propertyType.Object);

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
		public void Parse_AccessModifierIsSet()
		{
			this.property.SetupGet(xx => xx.AccessModifier).Returns(AccessModifier.Protected);

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.AccessModifier = It.Is<AccessModifier>(actual => AccessModifier.Protected == actual), Times.Once);
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
		public void Parse_HasSetterIsSet()
		{
			this.property.SetupGet(xx => xx.HasSetter).Returns(true);

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.HasSetter = It.Is<bool>(actual => actual == true), Times.Once);
		}

		[Test]
		public void Parse_IsEnumIsSet()
		{
			this.property.SetupGet(xx => xx.IsEnum).Returns(true);

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.IsEnum = It.Is<bool>(actual => actual == true), Times.Once);
		}

		[Test]
		public void Parse_IsPrimaryKeyIsSet_NoAttribute()
		{
			this.property.Setup(xx => xx.GetAttribute<WormPrimaryKeyAttribute>()).Returns(default(WormPrimaryKeyAttribute));

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.IsPrimaryKey = It.Is<bool>(actual => actual == false), Times.Once);
		}

		[Test]
		public void Parse_IsPrimaryKeyIsSet_WithAttribute()
		{
			this.property.Setup(xx => xx.GetAttribute<WormPrimaryKeyAttribute>()).Returns(new WormPrimaryKeyAttribute());

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.IsPrimaryKey = It.Is<bool>(actual => actual == true), Times.Once);
		}

		[Test]
		public void Parse_IdGeneratorIsSet_NotPrimaryKey()
		{
			this.pocoField.SetupGet(xx => xx.IsPrimaryKey).Returns(false);

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.IdGenerator = It.IsAny<WormIdGenerator>(), Times.Never);
		}

		[Test]
		public void Parse_IdGeneratorIsSet_IsPrimaryKey_NoAttribute()
		{
			this.pocoField.SetupGet(xx => xx.IsPrimaryKey).Returns(true);
			this.property.Setup(xx => xx.GetAttribute<WormIdGeneratorAttribute>()).Returns(default(WormIdGeneratorAttribute));

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.IdGenerator = It.Is<WormIdGenerator>(actual => WormIdGenerator.AutoIncrement == actual), Times.Once);
		}

		[Test]
		public void Parse_IdGeneratorIsSet_IsPrimaryKey_WithAttribute()
		{
			var attr = new WormIdGeneratorAttribute(WormIdGenerator.Uuid);
			this.pocoField.SetupGet(xx => xx.IsPrimaryKey).Returns(true);
			this.property.Setup(xx => xx.GetAttribute<WormIdGeneratorAttribute>()).Returns(attr);

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.IdGenerator = It.Is<WormIdGenerator>(actual => WormIdGenerator.Uuid == actual), Times.Once);
		}

		[Test]
		public void Parse_StorageTypeIsSet_NoAttribute()
		{
			this.property.Setup(xx => xx.GetAttribute<WormStorageTypeAttribute>()).Returns(default(WormStorageTypeAttribute));

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.StorageType = It.Is<string>(actual => String.Empty.Equals(actual)), Times.Once);
		}

		[Test]
		public void Parse_StorageTypeIsSet_WithAttribute()
		{
			var attr = new WormStorageTypeAttribute("text");
			this.property.Setup(xx => xx.GetAttribute<WormStorageTypeAttribute>()).Returns(attr);

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.StorageType = It.Is<string>(actual => "text".Equals(actual)), Times.Once);
		}

		[Test]
		public void Parse_TypeIsSet()
		{
			this.propertyType.SetupGet(xx => xx.Name).Returns("TypeOfValue");

			this.propToEntity.Parse(this.property.Object);

			this.pocoField.VerifySet(xx => xx.Type = It.Is<string>(actual => "TypeOfValue".Equals(actual)), Times.Once);
		}

		[Test]
		public void Parse_ReturnsInstance()
		{
			PocoField actual = this.propToEntity.Parse(this.property.Object);

			Assert.AreSame(this.pocoField.Object, actual);
		}
	}
}