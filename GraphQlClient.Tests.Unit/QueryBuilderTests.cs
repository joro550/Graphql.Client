using Xunit;
using GraphQlClient.Query;
using GraphQlClient.Attributes;

namespace GraphQlClient.Tests.Unit
{
    public class QueryBuilderTests
    {
        public class SimpleFieldsQueryIsReturnedWhen
        {
            [Fact]
            public void SimpleQueryObjectIsSent()
            {
                var queryBuilder = new QueryBuilder(new CamelCaseNamingStrategy());

                var wrapper = new QueryWrapper<HeroQuery> {Query = new HeroQuery()};
                var query = queryBuilder.CreateRetrieveQuery(wrapper);

                Assert.Equal("{query {hero {name}}}", query);
            }
            
            [Fact]
            public void SimpleQueryObjectWithPropertyNameAttributeIsSent()
            {
                var queryBuilder = new QueryBuilder(new CamelCaseNamingStrategy());

                var wrapper = new QueryWrapper<HeroQueryWithAttribute> {Query = new HeroQueryWithAttribute()};
                var query = queryBuilder.CreateRetrieveQuery(wrapper);

                Assert.Equal("{query {something {name}}}", query);
            }
        }

        public class ArgumentBasedQueryIsReturnedWhen
        {
            public void QueryWithFunctionAndArgumentsIsPassed()
            {
                var queryBuilder = new QueryBuilder(new CamelCaseNamingStrategy());

                var wrapper = new QueryWrapper<HeroQuery> {Query = new HeroQuery()};
                var query = queryBuilder.CreateRetrieveQuery(wrapper);

                Assert.Equal("{query {human(name: \"1000\") {name}}}", query);
                
            }
        }
    }

    
    public class HeroQueryFunction
    {
        [GraphqlArgument]
        public Human Human { get; set; }
        
        [GraphqlArgumentParameter(ArgumentName = nameof(Human))]
        public string Id { get; set; }
        
    }

    public class Human
    {
        public string Height { get; set; }
        public string Name { get; set; }
    }

    public class HeroQueryWithAttribute
    {
        [GraphqlPropertyName("something")]
        public Hero Hero { get; }
            = new();
    }

    public class HeroQuery
    {
        public Hero Hero { get; }
            = new();
    }

    public class Hero
    {
        public string Name { get; set; }
    }

    public class Serializer : ISerializer
    {

    }
}
