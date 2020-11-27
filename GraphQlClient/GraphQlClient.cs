using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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


        private static string CreateQuery<T>(T query) =>
            new StringBuilder()
                .Append('{')
                .Append(DoThing(query))
                .Append('}')
                .ToString();

        private static string DoThing<T>(T thing)
        {
            var sb = new StringBuilder();

            var properties = thing.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var propValue = prop.GetValue(thing, null);
                var type = prop.PropertyType;

                var name = GetCamelCasePropertyName<T>(prop);

                if (type.IsValueType)
                {

                    sb.Append($"{name},");
                }
                else if (type == typeof(string))
                {
                    sb.Append($"{name},");
                }
                else if (type.IsClass)
                {
                    var typedPropertyVal = Convert.ChangeType(propValue, prop.PropertyType);
                    sb.Append($"{name} {{ {DoThing(typedPropertyVal)} }}");
                }
            }

            return sb.ToString();
        }

        private static string GetCamelCasePropertyName<T>(PropertyInfo prop)
        {
            return char.ToLower(prop.Name[0]) + string.Join("", prop.Name.Skip(1));
        }

        protected virtual async Task SendQuery(string content)
        {
            var response = await _httpClient.PostAsync(_httpClient.BaseAddress,
                new StringContent(content));
        }
    }

    internal class QueryWrapper<T>
    {
        public T Query { get; set; }
    }

    public interface ISerializer
    {

    }
}
