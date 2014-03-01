using System;
using NUnit.Framework;
using Moq;
using Worm.Parsing.Internals.Reflection;
using System.Reflection;
using Worm.CodeGeneration.Internals;

namespace Wormlibtests.Parsing.Internals.Reflection
{
	[TestFixture]
	public class WPropertyTests
	{
		private WProperty property;

		private Mock<PropertyInfo> pi;
		private Mock<Type> propertyType;

		[SetUp]
		public void Setup()
		{
			this.pi = new Mock<PropertyInfo>();
			this.propertyType = new Mock<Type>();

			this.property = new WProperty(this.pi.Object);
		}

		[Test]
		public void Name()
		{
			this.pi.SetupGet(xx => xx.Name).Returns("Name");

			Assert.AreEqual("Name", this.property.Name);
		}

		[Test]
		public void HasGetter()
		{
			this.pi.SetupGet(xx => xx.CanRead).Returns(true);

			Assert.AreEqual(true, this.property.HasGetter);
		}

		[Test]
		public void HasSetter()
		{
			this.pi.SetupGet(xx => xx.CanWrite).Returns(true);

			Assert.AreEqual(true, this.property.HasSetter);
		}

		[Test]
		public void IsEnum()
		{
			this.pi.SetupGet(xx => xx.PropertyType).Returns(this.propertyType.Object);
			this.propertyType.SetupGet(xx => xx.IsEnum).Returns(true);

			Assert.AreEqual(true, this.property.IsEnum);
		}

		[Test]
		public void Type()
		{
			this.pi.SetupGet(xx => xx.PropertyType).Returns(this.propertyType.Object);

			Assert.AreEqual(this.propertyType.Object, this.property.Type);
		}
	}
}