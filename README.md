# OpenSky Network API Wrapper

An easy-to-use, maintained C# .NET wrapper for the OpenSky Network REST API.

This wrapper includes many operations, including the following:

- Aircraft states, flights, and tracks
- Airport arrivals and departures
- Aircraft and airport registration information
- Aircraft information search

## Getting Started

To use this, run `dotnet add package OpenSky` in a .NET project.

Here is an example program once you've added the OpenSky package:

```csharp
using OpenSky;

OpenSkyClient client = new();

// This is an expensive call, cache the response when applicable
OpenSkyStates states = await client.GetStatesAsync();

foreach (var state in states.States)
{
    // Write each aircraft ICAO 24-bit transponder address
    Console.Write(state.Icao24 + " ");
}
Console.WriteLine();
```