using NUnit.Framework;
using System;
using TauCode.Domain.Identities;

namespace TauCode.Domain.Tests
{
    [TestFixture]
    public class IdTests
    {
        [Test]
        public void Constructor_NoIdProvided_IsCreatedWithGeneratedId()
        {
            // Arrange

            // Act
            var id = new TestId();

            // Assert
            Assert.That(id.Id, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void Constructor_GuidProvided_IsCreatedWithProvidedGuidAsId()
        {
            // Arrange
            var guid = new Guid("afa60c9b-3d66-4e0a-ac16-50921a90f724");

            // Act
            var id = new TestId(guid);

            // Assert
            Assert.That(id.Id, Is.EqualTo(guid));
        }

        [Test]
        public void Constructor_StringProvided_IsCreatedWithGuidOfProvidedStringAsId()
        {
            // Arrange
            const string guidString = "7a8336a2-dc2e-478c-a45e-b79212b93d8d";

            // Act
            var id = new TestId(guidString);

            // Assert
            Assert.That(id.Id, Is.EqualTo(new Guid(guidString)));
        }

        [Test]
        public void Equals_SameId_ReturnsTrue()
        {
            // Arrange
            var id1 = new TestId();
            var id2 = id1;

            // Act - Equals from IEquatable
            var resultEquatable = id1.Equals(id2);

            // Act - Equals from object
            var resultObject = id1.Equals((object)id2);

            // Assert
            Assert.That(resultEquatable, Is.True);
            Assert.That(resultObject, Is.True);
        }

        [Test]
        public void Equals_IdWithSameValue_ReturnsTrue()
        {
            // Arrange
            var id1 = new TestId("cf58a681-100c-471a-9147-37ebe09c721d");
            var id2 = new TestId("cf58a681-100c-471a-9147-37ebe09c721d");

            // Act - Equals from IEquatable
            var resultEquatable = id1.Equals(id2);

            // Act - Equals from object
            var resultObject = id1.Equals((object)id2);

            // Assert
            Assert.That(resultEquatable, Is.True);
            Assert.That(resultObject, Is.True);
        }

        [Test]
        public void Equals_Null_ReturnsFalse()
        {
            // Arrange
            var id1 = new TestId("cf58a681-100c-471a-9147-37ebe09c721d");
            var id2 = (IdBase)null;

            // Act - Equals from IEquatable
            var resultEquatable = id1.Equals(id2);

            // Act - Equals from object
            var resultObject = id1.Equals((object)id2);

            // Assert
            Assert.That(resultEquatable, Is.False);
            Assert.That(resultObject, Is.False);
        }

        [Test]
        public void Equals_IdWithDifferentValue_ReturnsFalse()
        {
            // Arrange
            var id1 = new TestId("cf58a681-100c-471a-9147-37ebe09c721d");
            var id2 = new TestId("003d9337-b323-4b26-9e29-fc6793d93212");

            // Act - Equals from IEquatable
            var resultEquatable = id1.Equals(id2);

            // Act - Equals from object
            var resultObject = id1.Equals((object)id2);

            // Assert
            Assert.That(resultEquatable, Is.False);
            Assert.That(resultObject, Is.False);
        }

        [Test]
        public void GetHashCode_ReturnsSameForIdsWithSameValue()
        {
            // Arrange
            var id1 = new TestId("75d9fa21-6209-4960-b8c2-095c817e8e88");
            var id2 = new TestId("75d9fa21-6209-4960-b8c2-095c817e8e88");

            // Act
            var hashCode1 = id1.GetHashCode();
            var hashCode2 = id2.GetHashCode();

            // Assert
            Assert.That(hashCode1, Is.EqualTo(hashCode2));
        }

        [Test]
        public void GetHashCode_IsDifferentForIdsWithDifferentValue()
        {
            // Arrange
            var id1 = new TestId("75d9fa21-6209-4960-b8c2-095c817e8e88");
            var id2 = new TestId("75d9fa21-6209-4960-b8c2-095c817e8e89");

            // Act
            var hashCode1 = id1.GetHashCode();
            var hashCode2 = id2.GetHashCode();

            // Assert
            Assert.That(hashCode1, Is.Not.EqualTo(hashCode2));
        }

        [Test]
        public void Equals_IdWithDifferentTypesButSaveValue_ReturnsFale()
        {
            // Arrange
            var id1 = new TestId("cf58a681-100c-471a-9147-37ebe09c721d");
            var id2 = new OtherTestId("cf58a681-100c-471a-9147-37ebe09c721d");

            // Act - Equals from IEquatable
            var resultEquatable = id1.Equals(id2);

            // Act - Equals from object
            var resultObject = id1.Equals((object)id2);

            // Assert
            Assert.That(resultEquatable, Is.False);
            Assert.That(resultObject, Is.False);
        }

        [Test]
        public void OperatorEquals_IdsWithSameValue_ReturnsTrue()
        {
            // Arrange
            var id1 = new TestId("cf58a681-100c-471a-9147-37ebe09c721d");
            var id2 = new TestId("cf58a681-100c-471a-9147-37ebe09c721d");

            // Act - Equals from IEquatable
            var resultEquatable = id1.Equals(id2);

            // Act - Equals from object
            var resultObject = id1.Equals((object)id2);

            // Act - Equals from operator ==
            var resultOperatorEquals = id1 == id2;

            // Assert
            Assert.That(resultEquatable, Is.True);
            Assert.That(resultObject, Is.True);
            Assert.That(resultOperatorEquals, Is.True);
        }

        [Test]
        public void ToString_ReturnsTheStringValueOfTheId()
        {
            // Arrange
            var id = new TestId("10c8f55a-ddda-484d-bf98-53cbf41f66c1");

            // Act
            var result = id.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("10c8f55a-ddda-484d-bf98-53cbf41f66c1"));
        }

        private class TestId : IdBase
        {
            public TestId() : base()
            {

            }

            public TestId(Guid id) : base(id)
            {

            }

            public TestId(string id) : base(id)
            {

            }
        }

        private class OtherTestId : IdBase
        {
            public OtherTestId() : base()
            {

            }

            public OtherTestId(Guid id) : base(id)
            {

            }

            public OtherTestId(string id) : base(id)
            {

            }
        }
    }
}
