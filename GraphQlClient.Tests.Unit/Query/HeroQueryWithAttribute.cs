using GraphQlClient.Attributes;

namespace GraphQlClient.Tests.Unit.Query
{
    public class HeroQueryWithAttribute
    {
        [GraphqlPropertyName("something")]
        public Hero Hero { get; }
            = new();
    }
}