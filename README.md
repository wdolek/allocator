## Benchmarkator: benchmarks and stuff

This project is playground for various benchmarks. Creating benchmark is much easier than reading IL and
_guessing_ performance characteristics. Be aware that IL is just one part of puzzle, keep in mind there's
JITter as well and other factors, such as CPU caching, GC, ...

If you can explain benchmark results/you can link to documentation, feel free to submit an issue or PR with
explanation/link.

Only results are listed - interpretation of results is up to the readers themselves.

### List of benchmarks:

Not all benchmarks may be listed, check source directly.

- [Get Assembly Version](src/Benchmarkator/Assemblinator/GetAssemblyVersion.md):
  demonstrating how impactful it is to get custom attribute over and over
- [Bitmap access](src/Benchmarkator/Bitmap/Bitmap.md):
  determining the fastest approach for implementing bitmap
- [Division by `n`](src/Benchmarkator/Division/DivisibleByTwo.md):
  comparing modulo with logical AND when dividing by even number
- [Formatting string while rendering just part of it](src/Benchmarkator/Stringator/StringFormatSubstring.md):
  comparing approaches to format string in combination of `string.Substring`
- [Using lambdas](src/Benchmarkator/Lambdinator/LambdaUsage.md):
  comparing different ways how to write lambdas/anonymous functions
- [Throw or return](src/Benchmarkator/Exceptions/ThrowOrReturn.md):
  demonstrating price of throwing an exception

#### JSON

- [JSON Deserialization](src/Benchmarkator.Json/Deserialization/JsonPayloadDeserialization.md):
  observing memory allocation by `StreamReader` buffer

#### Collection benchmarks

- [Array access](src/Benchmarkator.Collections/Iteration/ArrayIteration.md):
  determining the fastest way to access array item (and way of iteration)
- [Collection Contains ...](src/Benchmarkator.Collections/Contains/ImmutableCollectionContains.md):
  comparing `corefx` immutable collections with collections from `LanguageExt.Core`
- [Collection Create](src/Benchmarkator.Collections/Create/CreateCtor.md):
  comparing `corefx` immutable collections instantiation/creation with `LanguageExt.Core` (`ctor`, `.Create`)
- [Collection Lookup](src/Benchmarkator.Collections/Lookup/ValueLookup.md):
  benchmark of lookup of structured value (e.g. `Id`), comparing array, `List<T>` and `Dictionary<TKey, TValue>`
- [`.ToArray` vs `.ToList`](src/Benchmarkator.Collections/ToCollection/ToCollection.md):
  bemchmark comparing performance of `.ToArray()` and `.ToList()`
- [`Collection ToDictionary`](src/Benchmarkator.Collections/ToDictionary/ToDictionary.md):
  benchmark of creating dictionary out of collection using LINQ and simple implementation

#### MongoDB

- [System.Text.Json.JsonDocument serialization](src/Benchmarkator.MongoDb/JsonDocumentSerialization.md)
  benchmark of serialization of `System.Text.Json.JsonDocument`
- [System.Text.Json.JsonDocument to MongoDB.Bson.BsonDocument](src/Benchmarkator.MongoDb/JsonDocumentToBsonDocument.md)
  benchmark of conversion of JSON to BSON document

### Running benchmarks

```
dotnet run -c Release --project src/Benchmarkator -f net6.0
```

... so running benchmarks related to collections is done using command:

```
dotnet run -c Release --project src/Benchmarkator -f net6.0 --filter Benchmarkator.Collections*
```

More about running benchmarks: [BenchmarkDotNet | How to use console arguments](https://benchmarkdotnet.org/articles/guides/console-args.html).
