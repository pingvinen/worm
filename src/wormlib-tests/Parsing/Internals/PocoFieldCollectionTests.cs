using System;
using Moq;
using NUnit.Framework;
using Worm.Parsing.Internals;
using Worm.CodeGeneration.Internals;
using System.Collections.Generic;
using System.Linq;

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

		#region GetPrimaryKeyField
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
		#endregion

		#region GetPublicFields
		[Test]
		public void GetPublicFields_emptyFieldSet()
		{
			IEnumerable<PocoField> actual = this.collection.GetPublicFields();
			Assert.IsEmpty(actual);
		}

		[Test]
		public void GetPublicFields_returnsPublicFieldsOnly()
		{
			this.field1.SetupGet(xx => xx.AccessModifier).Returns(AccessModifier.Public);
			this.field1.SetupGet(xx => xx.HasSetter).Returns(true);
			this.field1.SetupGet(xx => xx.IsPrimaryKey).Returns(false);

			this.field2.SetupGet(xx => xx.AccessModifier).Returns(AccessModifier.Private);
			this.field2.SetupGet(xx => xx.HasSetter).Returns(true);
			this.field2.SetupGet(xx => xx.IsPrimaryKey).Returns(false);

			this.field3.SetupGet(xx => xx.AccessModifier).Returns(AccessModifier.Public);
			this.field3.SetupGet(xx => xx.HasSetter).Returns(true);
			this.field3.SetupGet(xx => xx.IsPrimaryKey).Returns(false);

			this.collection.Add(this.field1.Object);
			this.collection.Add(this.field2.Object);
			this.collection.Add(this.field3.Object);

			IList<PocoField> actual = this.collection.GetPublicFields().ToList();

			Assert.AreEqual(this.field1.Object, actual[0]);
			Assert.AreEqual(this.field3.Object, actual[1]);
		}

		[Test]
		public void GetPublicFields_returnsPublicFieldsOnly_butNotPrimaryKeyFields()
		{
			this.field1.SetupGet(xx => xx.AccessModifier).Returns(AccessModifier.Public);
			this.field1.SetupGet(xx => xx.HasSetter).Returns(true);
			this.field1.SetupGet(xx => xx.IsPrimaryKey).Returns(true);

			this.collection.Add(this.field1.Object);

			IList<PocoField> actual = this.collection.GetPublicFields().ToList();

			Assert.IsEmpty(actual);
		}

		[Test]
		public void GetPublicFields_returnsPublicFieldsOnly_onlyIfFieldHasASetter()
		{
			this.field1.SetupGet(xx => xx.AccessModifier).Returns(AccessModifier.Public);
			this.field1.SetupGet(xx => xx.HasSetter).Returns(false);
			this.field1.SetupGet(xx => xx.IsPrimaryKey).Returns(false);

			this.collection.Add(this.field1.Object);

			IList<PocoField> actual = this.collection.GetPublicFields().ToList();

			Assert.IsEmpty(actual);
		}
		#endregion

		#region GetInsertFields
		[Test]
		public void GetInsertFields_emptyFieldSet()
		{
			IEnumerable<PocoField> actual = this.collection.GetInsertFields();
			Assert.IsEmpty(actual);
		}

		[Test]
		public void GetInsertFields_returnsNonPrimaryKeyFields()
		{
			this.field1.SetupGet(xx => xx.IsPrimaryKey).Returns(false);
			this.field2.SetupGet(xx => xx.IsPrimaryKey).Returns(true);
			this.field3.SetupGet(xx => xx.IsPrimaryKey).Returns(false);

			this.collection.Add(this.field1.Object);
			this.collection.Add(this.field2.Object);
			this.collection.Add(this.field3.Object);

			IList<PocoField> actual = this.collection.GetInsertFields().ToList();

			Assert.AreEqual(this.field1.Object, actual[0]);
			Assert.AreEqual(this.field3.Object, actual[1]);
		}
		#endregion
	}
}