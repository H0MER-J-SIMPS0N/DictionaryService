using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DictionaryService.Tests
{
    public class StorageServiceTests
    {
        [Fact]
        public void AddNullValueAndNotNullKey()
        {
            // Arrange
            StorageService service = new StorageService();

            // Act
            service.Add("dghskjdf", string.Empty);

            // Assert
            Assert.Equal(string.Empty, service.Get("dghskjdf"));
        }

        [Fact]
        public void AddNotNullAndGetKeyInDictionary()
        {
            // Arrange
            StorageService service = new StorageService();

            // Act
            service.Add("dghskjdf", "662354234");

            // Assert
            Assert.Equal("662354234", service.Get("dghskjdf"));
        }

        [Fact]
        public void AddNullAndGetNull()
        {
            // Arrange
            StorageService service = new StorageService();


            // Assert
            Assert.Throws<KeyNotFoundException>(() => service.Add(string.Empty, "662354234"));
            Assert.Throws<KeyNotFoundException>(() => service.Get(string.Empty));
        }

        [Fact]
        public void GetNotInDictionary()
        {
            // Arrange
            StorageService service = new StorageService();

            // Act
            service.Add("dghskjdf", "662354234");

            // Assert
            Assert.Throws<KeyNotFoundException>(() => service.Get("444535"));
        }

        [Fact]
        public void GetAllKeysWithValues()
        {
            // Arrange
            StorageService service = new StorageService();

            // Act
            service.Add("dghskjdf", "662354234");
            service.Add("232 hjkhk", "111");
            service.Add("ghfg gh gf", "cnnmskksokodf");

            // Assert
            Assert.NotEmpty(service.GetAllKeys());
            Assert.Equal(3, service.GetAllKeys().Count());
            Assert.Equal("662354234", service.Get("dghskjdf"));
            Assert.Equal("111", service.Get("232 hjkhk"));
            Assert.Equal("cnnmskksokodf", service.Get("ghfg gh gf"));
            Assert.True(service.GetAllKeys().Contains("dghskjdf") && service.GetAllKeys().Contains("232 hjkhk") && service.GetAllKeys().Contains("ghfg gh gf"));
        }

        [Fact]
        public void GetAllKeysWithValuesAndOneWithoutValue()
        {
            // Arrange
            StorageService service = new StorageService();

            // Act
            service.Add("dghskjdf", "662354234");
            service.Add("232 hjkhk", string.Empty);
            service.Add("ghfg gh gf", "cnnmskksokodf");

            // Assert
            Assert.NotEmpty(service.GetAllKeys());
            Assert.Equal(2, service.GetAllKeys().Count());
            Assert.Equal("662354234", service.Get("dghskjdf"));
            Assert.Equal(string.Empty, service.Get("232 hjkhk"));
            Assert.Equal("cnnmskksokodf", service.Get("ghfg gh gf"));
            Assert.True(service.GetAllKeys().Contains("dghskjdf") && !service.GetAllKeys().Contains("232 hjkhk") && service.GetAllKeys().Contains("ghfg gh gf"));
        }

        [Fact]
        public void DeleteValueByExistedKey()
        {
            // Arrange
            StorageService service = new StorageService();

            // Act
            service.Add("dghskjdf", "662354234");
            service.Add("232 hjkhk", "111");
            service.Add("ghfg gh gf", "cnnmskksokodf");
            service.Delete("232 hjkhk");

            // Assert
            Assert.NotEmpty(service.GetAllKeys());
            Assert.Equal(2, service.GetAllKeys().Count());
            Assert.Equal("662354234", service.Get("dghskjdf"));
            Assert.Equal(string.Empty, service.Get("232 hjkhk"));
            Assert.Equal("cnnmskksokodf", service.Get("ghfg gh gf"));
            Assert.True(service.GetAllKeys().Contains("dghskjdf") && !service.GetAllKeys().Contains("232 hjkhk") && service.GetAllKeys().Contains("ghfg gh gf"));
        }

        [Fact]
        public void DeleteValueByNotExistedKey()
        {
            // Arrange
            StorageService service = new StorageService();

            // Act
            service.Add("dghskjdf", "662354234");
            service.Add("232 hjkhk", "111");
            service.Add("ghfg gh gf", "cnnmskksokodf");
            

            // Assert
            Assert.Throws<KeyNotFoundException>(() => service.Delete("43545nmghjg3"));
            Assert.NotEmpty(service.GetAllKeys());
            Assert.Equal(3, service.GetAllKeys().Count());
            Assert.Equal("662354234", service.Get("dghskjdf"));
            Assert.Equal("111", service.Get("232 hjkhk"));
            Assert.Equal("cnnmskksokodf", service.Get("ghfg gh gf"));
            Assert.True(service.GetAllKeys().Contains("dghskjdf") && service.GetAllKeys().Contains("232 hjkhk") && service.GetAllKeys().Contains("ghfg gh gf") && !service.GetAllKeys().Contains("43545nmghjg3"));
        }

    }
}
