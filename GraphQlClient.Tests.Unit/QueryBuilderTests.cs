using Xunit;
using GraphQlClient.Query;
using GraphQlClient.Tests.Unit.Query;

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
            [Fact]
            public void QueryWithFunctionAndArgumentsIsPassed()
            {
                var queryBuilder = new QueryBuilder(new CamelCaseNamingStrategy());

                var wrapper = new QueryWrapper<HeroQueryArgumentQuery> {Query = new HeroQueryArgumentQuery()};
                var query = queryBuilder.CreateRetrieveQuery(wrapper);

                Assert.Equal("{query {human() {height,name}}}", query);
            }
        }
    }
}
