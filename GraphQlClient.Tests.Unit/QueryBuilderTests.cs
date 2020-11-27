using Xunit;
using System.Threading.Tasks;

namespace GraphQlClient.Tests.Unit
{
    public class QueryBuilderTests
    {
        [Fact]
        public async Task Test1()
        {
            var client = new GraphQlClient("http://localhost:5000/graphql", new Serializer());
            var response = await client.Query(new HeroQuery());
        }
    }

    public class TestGraphQlClient : GraphQlClient
    {
        public string Query { get; private set; }


        public TestGraphQlClient(string uri, ISerializer serializer) 
            : base(uri, serializer)
        {
        }

        protected override Task SendQuery(string content)
        {
            Query = content;
            return Task.CompletedTask;
        }
    }

    public class HeroQuery
    {
        public string Name { get; set; }
    }

    public class Serializer : ISerializer
    {

    }
}
