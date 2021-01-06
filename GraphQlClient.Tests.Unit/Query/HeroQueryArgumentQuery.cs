using GraphQlClient.Attributes;
using GraphQlClient.Query;

namespace GraphQlClient.Tests.Unit.Query
{
    [GraphqlFunction("human")]
    public class HeroQueryArgumentQuery : IGraphqlFunction<Human>
    {
    }
}