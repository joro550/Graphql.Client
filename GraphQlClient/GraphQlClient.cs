using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GraphQlClient.Query;

[assembly:InternalsVisibleTo("GraphQlClient.Tests.Unit")]

namespace GraphQlClient
{
    public class GraphQlClient
    {
        private readonly HttpClient _httpClient;
        private readonly ISerializer _serializer;
        private readonly QueryBuilder _queryBuilder;

        public GraphQlClient(string uri, ISerializer serializer)
            : this(new Uri(uri), serializer)
        {
        }

        private GraphQlClient(Uri uri, ISerializer serializer)
        {
            _serializer = serializer;
            _httpClient = new HttpClient { BaseAddress = uri };
            _queryBuilder = new QueryBuilder(new CamelCaseNamingStrategy());
        }

        public async Task<T> Query<T>(T query) where T : new()
        {
            var wrapper = new QueryWrapper<T> { Query = query };
            var queryToSend = _queryBuilder.CreateRetrieveQuery(wrapper);
            await SendQuery(queryToSend);
            return new T();
        }

        protected virtual async Task SendQuery(string content)
        {
            var response = await _httpClient.PostAsync(_httpClient.BaseAddress,
                new StringContent(content));
        }
    }
}
