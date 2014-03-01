using System;
using NUnit.Framework;
using Moq;
using Worm.Parsing.Internals.Reflection;
using System.Reflection;
using Worm.CodeGeneration.Internals;
using Worm.DataAnnotations;

namespace Wormlibtests.Parsing.Internals.Reflection
{
	[TestFixture]
	public class WPropertyTests
	{
		private WProperty property;

		private Mock<PropertyInfo> pi;
		private Mock<Type> propertyType;
		private Mock<AccessModifierMapper> accessMapper;
		private Mock<MethodInfo> methodInfo;

		[SetUp]
		public void Setup()
		{
			this.pi = new Mock<PropertyInfo>();
			this.propertyType = new Mock<Type>();
			this.accessMapper = new Mock<AccessModifierMapper>();
			this.methodInfo = new Mock<MethodInfo>();

			this.property = new WProperty(this.pi.Object, this.accessMapper.Object);
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
		public void AccessModifier_CanWrite()
		{
			this.pi.SetupGet(xx => xx.CanWrite).Returns(true);
			this.pi.Setup(xx => xx.GetSetMethod(true)).Returns(this.methodInfo.Object);
			this.accessMapper.Setup(xx => xx.Map(this.methodInfo.Object)).Returns(AccessModifier.Protected);

			Assert.AreEqual(AccessModifier.Protected, this.property.AccessModifier);
		}

		[Test]
		public void AccessModifier_CanRead()
		{
			this.pi.SetupGet(xx => xx.CanWrite).Returns(false);
			this.pi.SetupGet(xx => xx.CanRead).Returns(true);
			this.pi.Setup(xx => xx.GetGetMethod(true)).Returns(this.methodInfo.Object);
			this.accessMapper.Setup(xx => xx.Map(this.methodInfo.Object)).Returns(AccessModifier.Protected);

			Assert.AreEqual(AccessModifier.Protected, this.property.AccessModifier);
		}

		[Test]
		public void AccessModifier_NeitherReadNorWrite()
		{
			this.pi.SetupGet(xx => xx.CanWrite).Returns(false);
			this.pi.SetupGet(xx => xx.CanRead).Returns(false);

			Assert.AreEqual(AccessModifier.Private, this.property.AccessModifier);
		}

		[Test]
		public void Type()
		{
			this.pi.SetupGet(xx => xx.PropertyType).Returns(this.propertyType.Object);

			Assert.AreEqual(this.propertyType.Object, this.property.Type);
		}

		[Test]
		public void GetAttribute_NoMatches_ReturnDefault()
		{
			//var attr1 = new WormColumnNameAttribute("attr1");
			this.pi.Setup(xx => xx.GetCustomAttributes(typeof(WormColumnNameAttribute), true)).Returns(new object[]{});

			Assert.AreEqual(default(WormColumnNameAttribute), this.property.GetAttribute<WormColumnNameAttribute>());
		}

		[Test]
		public void GetAttribute_OneMatch_IsReturned()
		{
			var attr1 = new WormColumnNameAttribute("attr1");
			this.pi.Setup(xx => xx.GetCustomAttributes(typeof(WormColumnNameAttribute), true)).Returns(new object[]{attr1});

			Assert.AreSame(attr1, this.property.GetAttribute<WormColumnNameAttribute>());
		}

		[Test]
		public void GetAttribute_MultipleMatches_FirstIsReturned()
		{
			var attr1 = new WormColumnNameAttribute("attr1");
			var attr2 = new WormColumnNameAttribute("attr2");
			this.pi.Setup(xx => xx.GetCustomAttributes(typeof(WormColumnNameAttribute), true)).Returns(new object[]{attr1,attr2});

			Assert.AreSame(attr1, this.property.GetAttribute<WormColumnNameAttribute>());
		}
	}
}