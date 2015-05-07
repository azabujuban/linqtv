#LINQ provider for accessing TheTVDB.com
[TheTVDB.com](TheTVDB.com) is a great source for information about TV shows. They also provide an API to access it. Unfortunately, while simple it's not convenient which together with my desire to try build a LINQ provider seemed like a perfect candidate.

For example, you can use the following query syntax to get all episodes for `mad men`

```c#
using (var tvdb = new LinqTVDB.Context(_apiKey))
{
    var madMen = from show in tvdb.Shows("mad men")
              select show;
}
```

You could be more specific and choose episodes which were aired after a specific date. For example:
```c#
using (var tvdb = new LinqTVDB.Context(_apiKey))
{
    var madMen = from show in tvdb.Shows("man men")
              from episode in show.Episodes
              where episode.AirDate > DateTime.Parse("Apr-27-2015")
              select show;
}
```


## Model


### Show


### Episode

