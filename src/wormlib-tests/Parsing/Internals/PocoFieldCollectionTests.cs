using System;
using Moq;
using NUnit.Framework;
using Worm.Parsing.Internals;
using Worm.CodeGeneration.Internals;

namespace Wormlibtests.Parsing.Internals
{
	[TestFixture]
	public class PocoFieldCollectionTests
	{
		private PocoFieldCollection collection;
		private Mock<PocoField> field1;
		private Mock<PocoField> field2;
		private Mock<PocoField> field3;

		[SetUp]
		public void Setup()
		{
			this.field1 = new Mock<PocoField>();
			this.field2 = new Mock<PocoField>();
			this.field3 = new Mock<PocoField>();

			this.collection = new PocoFieldCollection();
		}

		[Test]
		public void GetPrimaryKeyField_emptyFieldSet()
		{
			Assert.AreEqual(null, this.collection.GetPrimaryKeyField());
		}

		[Test]
		public void GetPrimaryKeyField_noPrimaryKeyField()
		{
			this.collection.Add(this.field1.Object);
			this.collection.Add(this.field2.Object);

			Assert.AreEqual(null, this.collection.GetPrimaryKeyField());
		}

		[Test]
		public void GetPrimaryKeyField_onePrimaryKeyField()
		{
			this.field1.SetupGet(xx => xx.IsPrimaryKey).Returns(true);

			this.collection.Add(this.field1.Object);
			this.collection.Add(this.field2.Object);

			Assert.AreEqual(this.field1.Object, this.collection.GetPrimaryKeyField());
		}

		[Test]
		public void GetPrimaryKeyField_returnsFirstPrimaryKeyField()
		{
			this.field1.SetupGet(xx => xx.IsPrimaryKey).Returns(false);
			this.field2.SetupGet(xx => xx.IsPrimaryKey).Returns(true);
			this.field3.SetupGet(xx => xx.IsPrimaryKey).Returns(true);

			this.collection.Add(this.field1.Object);
			this.collection.Add(this.field2.Object);
			this.collection.Add(this.field3.Object);

			Assert.AreEqual(this.field2.Object, this.collection.GetPrimaryKeyField());
		}
	}
}