using System;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Xunit;
using System.Threading.Tasks;
using System.Net;

namespace DictionaryService.Tests
{
    public class StartupTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly Uri _baseUri = new Uri("http://localhost:5000");
        public StartupTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetRequestToRoot()
        {
            // Arrange
            var response = await _client.GetAsync(_baseUri);
            response.EnsureSuccessStatusCode();
            // Act
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(string.Empty, responseString);
        }

        [Fact]
        public async Task PostRequestWithAllParameters()
        {
            // Arrange
            var response = await _client.PostAsync(new Uri(_baseUri, "/111"), new StringContent("asdfsdfasdf"));
            response.EnsureSuccessStatusCode();
            // Act
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal("asdfsdfasdf", responseString);
        }

        [Fact]
        public async Task PostRequestWithNoId()
        {
            // Arrange
            var response = await _client.PostAsync(_baseUri, new StringContent("asdfsdfasdf"));
            // Act
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(string.Empty, responseString);
        }

        [Fact]
        public async Task PostRequestWithNoValue()
        {
            // Arrange
            var response = await _client.PostAsync(new Uri(_baseUri, "/111"), new StringContent(string.Empty));
            response.EnsureSuccessStatusCode();
            // Act
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(string.Empty, responseString);
        }

        [Fact]
        public async Task PostRequestWithNoIdAndNoValue()
        {
            // Arrange
            var response = await _client.PostAsync(_baseUri, new StringContent(string.Empty));
            // Act
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(string.Empty, responseString);
        }

        [Fact]
        public async Task GetRequestToRootAfterAddRecords()
        {
            // Arrange
            var response = await _client.PostAsync(new Uri(_baseUri, "/111"), new StringContent("asdfsdfasdf"));
            response.EnsureSuccessStatusCode();
            response = await _client.PostAsync(new Uri(_baseUri, "/222"), new StringContent("tuyzcxvvrevasvdfg"));
            response.EnsureSuccessStatusCode();
            response = await _client.GetAsync(_baseUri);
            response.EnsureSuccessStatusCode();
            // Act
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal("111" + Environment.NewLine + "222", responseString);
        }

        [Fact]
        public async Task GetRequestToRecordAfterAdd()
        {
            // Arrange
            var response = await _client.PostAsync(new Uri(_baseUri, "/111"), new StringContent("asdfsdfasdf"));
            response.EnsureSuccessStatusCode();
            response = await _client.GetAsync(new Uri(_baseUri, "111"));
            // Act
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal("asdfsdfasdf", responseString);
        }

        [Fact]
        public async Task GetRequestToRecordNotAdded()
        {
            // Arrange
            var response = await _client.GetAsync(new Uri(_baseUri, "111"));
            // Act
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(string.Empty, responseString);
        }

        [Fact]
        public async Task DeleteRequestToNotExistedRecord()
        {
            // Arrange
            var response = await _client.DeleteAsync(new Uri(_baseUri, "111"));
            // Act
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(string.Empty, responseString);
        }

        [Fact]
        public async Task DeleteRequestToExistedRecord()
        {
            // Arrange
            var response = await _client.PostAsync(new Uri(_baseUri, "/111"), new StringContent("asdfsdfasdf"));
            response.EnsureSuccessStatusCode();
            response = await _client.DeleteAsync(new Uri(_baseUri, "111"));
            // Act
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("111", responseString);
        }

        [Fact]
        public async Task DeleteRequestToRoot()
        {
            // Arrange            
            var response = await _client.DeleteAsync(_baseUri);
            // Act
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(string.Empty, responseString);
        }
    }
}
