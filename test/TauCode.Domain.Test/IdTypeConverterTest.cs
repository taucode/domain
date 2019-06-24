using NUnit.Framework;
using System;
using System.ComponentModel;
using TauCode.Domain.Identities;

namespace TauCode.Domain.Test
{
    [TestFixture]
    public class IdTypeConverterTest
    {
        private TypeConverter _idTypeConverter;

        [SetUp]
        public void SetUp()
        {
            _idTypeConverter = TypeDescriptor.GetConverter(typeof(IdTypeConverterTestId));
        }

        [Test]
        public void CanConvertFrom_String_ReturnsTrue()
        {
            // Arrange

            // Act
            var result = _idTypeConverter.CanConvertFrom(typeof(string));

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CanConvertFrom_Guid_ReturnsTrue()
        {
            // Arrange

            // Act
            var result = _idTypeConverter.CanConvertFrom(typeof(Guid));

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CanConvertFrom_Int_ReturnsFalse()
        {
            // Arrange

            // Act
            var result = _idTypeConverter.CanConvertFrom(typeof(int));

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ConvertFrom_StringContainingValidGuid_ReturnsId()
        {
            // Arrange

            // Act
            var id = _idTypeConverter.ConvertFrom("6ea4a9e2-7aed-4992-83e9-257d7c9bbb4e");

            // Assert
            Assert.That(id, Is.TypeOf<IdTypeConverterTestId>());
            Assert.That(((IdTypeConverterTestId)id).Id, Is.EqualTo(new Guid("6ea4a9e2-7aed-4992-83e9-257d7c9bbb4e")));
        }

        [Test]
        public void ConvertFrom_StringContainingInvalidGuid_ThrowsNotSupportedException()
        {
            // Arrange

            // Act and assert
            Assert.Throws<NotSupportedException>(() => _idTypeConverter.ConvertFrom("invalid-guid"));
        }

        [Test]
        public void ConvertFrom_ValidGuid_ReturnsId()
        {
            // Arrange

            // Act
            var id = _idTypeConverter.ConvertFrom(new Guid("6ea4a9e2-7aed-4992-83e9-257d7c9bbb4e"));

            // Assert
            Assert.That(id, Is.TypeOf<IdTypeConverterTestId>());
            Assert.That(((IdTypeConverterTestId)id).Id, Is.EqualTo(new Guid("6ea4a9e2-7aed-4992-83e9-257d7c9bbb4e")));
        }

        [Test]
        public void ConvertFrom_Int_ThrowsNotSupportedException()
        {
            // Arrange

            // Act and assert
            Assert.Throws<NotSupportedException>(() => _idTypeConverter.ConvertFrom(5));
        }
    }

    public class IdTypeConverterTestId : IdBase
    {
        public IdTypeConverterTestId()
        {
        }

        public IdTypeConverterTestId(Guid id)
            : base(id)
        {
        }

        public IdTypeConverterTestId(string id)
            : base(id)
        {
        }
    }
}
