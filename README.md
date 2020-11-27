# Graphql.Client
Static dotnet c# graphql client 


---
 Currently under HEAVY DEVELOPENT 
---
---


# What is this?
This is a c# client for any graphql endpoint

# How will it work
The contract in still in active development but I'm hoping that it will look something like:

Retrieve:

```
var client = new GraphQlClient("http://localhost:5000/graphql", new Serializer());
client.Retrieve(new HeroQuery());

public class HeroQuery
{
    public string Name { get; set; }
}

```

And this will send the following graphql request:

```
{
    hero {
        name
    }
}
```

Obviously there are many challenges to overcome here such as "What about argumnts" right now this is what I'm thinking:

```

var client = new GraphQlClient("http://localhost:5000/graphql", new Serializer());
client.Retrieve(new HeroQuery { Id = "1000"});

public class HeroQuery
{
    [GraphqlFunction(functionName: "human")]
    public Human Human { get; set; }

    [GraphqlArgument(functionName: "human")]
    public string Id { get; set; }
}

public class Human 
{
    public string Name { get; set; }
    public string Height { get; set; }
}
```

Which will produce the graphql query:
```
{
  human(id: "1000") {
    name
    height
  }
}
```

And of course have a something like:

```
public class HeroQuery
{
    [GraphqlProperty("notName")]
    public string Name { get; set; }
}
```

Produces:

```
{
    hero {
        notName
    }
}
```

# Why not use *insert other library here*
Most other graphql clients for dotnet seem to expect you to create queries via strings, I want to manipulte as few strings as possible so I'm sticking it in a library soI cna use it in my own personal projects

# What will this support

Heres a list of things that graphql can do and if my library supports it:


Type of query | Is Supported
--|--|
Retrieving | :x:
Retrieving with arguments | :x:
Mutations | :x:
Subscriptions | :x:

Hopefully it will eventually support all of these things