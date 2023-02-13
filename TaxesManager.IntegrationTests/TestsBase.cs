using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using AutoFixture;

namespace TaxesManager.IntegrationTests
{
    public class TestsBase
    {
        protected IServiceScopeFactory ServiceScopeFactory = null!;
        protected IFixture Fixture = null!;
        protected HttpClient ApplicationClient = null!;
        private WebApplicationFactory<Program> _applicationFactory = null!;        

        [OneTimeSetUp]
        public void RunBeforeApiTests()
        {
            _applicationFactory = new CustomWebApplicationFactory();
            ServiceScopeFactory = _applicationFactory.Services.GetRequiredService<IServiceScopeFactory>();
            ApplicationClient = _applicationFactory.CreateClient();
            Fixture = new Fixture();
        }

        protected async Task<HttpResponseMessage> InvokeHttp(object body, Func<StringContent, Task<HttpResponseMessage>> action)
        {
            var payload = JsonConvert.SerializeObject(body);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await action.Invoke(request);
            return response;
        }
    }
}