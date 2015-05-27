[![Build status](https://ci.appveyor.com/api/projects/status/mo4b10d7fib2qt9b/branch/master?svg=true)](https://ci.appveyor.com/project/azabujuban/linqtv/branch/master)

# LINQ provider for accessing TheTVDB.com
[TheTVDB.com](TheTVDB.com) is a great source for information about TV shows. They also provide an API to access it. Unfortunately, while simple it's not convenient which together with my desire to try build a LINQ provider seemed like a perfect candidate.

The API provided by ttdb is very basic, therefore it's really only possible to specify the condition
on the name of the show.

## Getting started
In order to query the ttdb you will need to create an instance of the `TvdbQueryable` object.
This can be done by calling `TvdbQueryable<Show>.Create()`. The returned object can be reused.

For example, the simplest (and pretty much the only) query would be:
```C#
var showContext = TvdbQueryable<Show>.Create(apikey)

var shows = from show in showContext
    where show.SeriesName == "mad men"
    select show;
```
`shows` will be an `IEnumerable<Show>` and to actually make the query you will need to
call any of the standard LINQ extension methods that do so (`ToList()`, `ToArray()`, etc...).
The data will be fetched synchronously so you should probably use `Task.Run()` like so:
```C#
await Task.Run(() => shows.ToList())
```

The result will be a list of shows will all information pre-parsed. Each show contains
the list of episodes (also pre-parsed).

## async
Internally, all networking calls are asynchronous. For example, in order to query ttdb for
a show a call to a web service is made. The result is a list of show ids that match. In order
to get the show details another call to a web service must be made.

If more than one show matches the query the web calls to get the details of the show will be
made in parallel.

It seemed an overkill to add async support to the LINQ queries directly (similar to
the [Entity Framework](https://msdn.microsoft.com/en-us/data/jj819165.aspx)) so i decided that
simply actualizing the results on the thread pool and awaiting it is the way to go.

## Thread safety
The context is _not_ thread safe so you will need to keep a separate instance for each thread.

## Context reusability
The context is `disposable` so if you only need to use it once you can/should use the `using` statement. For example:
```C#
using (var tvddb = TvdbQueryable<Show>.Create(_apiKey))
{
    var hh = from s in tvddb
             where s.SeriesName == "the office"
             select s;

}
```

You _don't_ however need to dispose of the context all the time and can just keep it in a field and use when needed (as noted above it's not thread safe)

