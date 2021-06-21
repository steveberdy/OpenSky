using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using OpenSky.Entities;

namespace OpenSky
{
    /// <summary>
    /// A disposable client for the OpenSky Network
    /// </summary>
    public class OpenSkyClient : IDisposable
    {
        private const string baseUrl = "https://opensky-network.org/api/";
        private readonly HttpClient client;
        private readonly JsonSerializer serializer;
        private bool IsAuthorized => client.DefaultRequestHeaders.Authorization != null;

        /// <summary>
        /// Creates a new OpenSkyClient
        /// </summary>
        public OpenSkyClient()
        {
            client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }, true);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(baseUrl);
            serializer = new JsonSerializer();
        }

        /// <summary>
        /// Creates a new OpenSkyClient with authorization
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public OpenSkyClient(string username, string password) : this()
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException(nameof(username));
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
        }

        /// <summary>
        /// Gets states for an aircraft at a certain time.
        /// State can only be checked from within one hour
        /// </summary>
        /// <returns>
        /// Current aircraft state
        /// </returns>
        /// <param name="icao24">Aicraft ICAO-24 code</param>
        /// <param name="time">DateTime for recent aircraft states, default is now</param>
        /// <param name="token">Cancellation token</param>
        /// <exception cref="OpenSkyException"></exception>
        public Task<IOpenSkyStates> GetState(string icao24, DateTime time = default, CancellationToken token = default)
        {
            return GetStates(new[] { icao24 }, time, null, token);
        }

        /// <summary>
        /// Gets states for an array of aircraft at a certain time,
        /// with an optional parameter for a region to check in.
        /// States can only be checked from within one hour
        /// </summary>
        /// <returns>
        /// Current aircraft states
        /// </returns>
        /// <param name="icao24s">Aicraft ICAO-24 codes to get states for</param>
        /// <param name="time">DateTime for recent aircraft states, default is now</param>
        /// <param name="region">Optional boundary box region for states</param>
        /// <param name="token">Cancellation token</param>
        /// <exception cref="OpenSkyException"></exception>
        public Task<IOpenSkyStates> GetStates(IEnumerable<string> icao24s = null, DateTime time = default, OpenSkyRegion region = null, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>();
            if (time != default)
            {
                if (DateTime.UtcNow.Subtract(time).TotalMinutes > 60)
                {
                    throw new OpenSkyException("Cannot retrieve states over one hour in the past");
                }
                dict.Add("time", time.ToUnixTimestamp());
            }
            if (icao24s != null)
            {
                dict.Add("icao24", icao24s);
            }
            if (region != null)
            {
                dict.Add("lamin", region.MinLatitude);
                dict.Add("lomin", region.MinLongitude);
                dict.Add("lamax", region.MaxLatitude);
                dict.Add("lomax", region.MaxLongitude);
            }
            var query = "states/all" + Utils.CreateQuery(dict);

            return GetAsync<IOpenSkyStates>(query, token);
        }

        /// <summary>
        /// Gets states for aircraft tracked by your trackers at a given time.
        /// User may not be anonymous when using this method
        /// </summary>
        /// <returns>
        /// States for your tracked aircraft at a given time
        /// </returns>
        /// <param name="icao24s">Aircraft ICAO-24 codes to get states for</param>
        /// <param name="time">DateTime for the aircraft states, default is now</param>
        /// <param name="serials">Your tracker serials</param>
        /// <param name="token">Cancellation token</param>
        /// <exception cref="OpenSkyException"></exception>
        public Task<IOpenSkyStates> GetMyStates(IEnumerable<string> icao24s = null, DateTime time = default, int[] serials = null, CancellationToken token = default)
        {
            if (!IsAuthorized)
            {
                throw new OpenSkyException("Method not allowed for anonymous users");
            }

            var dict = new Dictionary<string, object>();
            if (time != default)
            {
                dict.Add("time", time.ToUnixTimestamp());
            }
            if (icao24s != null)
            {
                dict.Add("icao24", icao24s);
            }
            if (serials != null)
            {
                dict.Add("serials", serials);
            }
            var query = "states/own" + Utils.CreateQuery(dict);

            return GetAsync<IOpenSkyStates>(query, token);
        }

        /// <summary>
        /// Gets all tracked flights in a range that is 2 hours before
        /// the end time provided
        /// </summary>
        /// <returns>
        /// An array of flights for the time period
        /// </returns>
        /// <param name="end">DateTime for the end of the interval checked. The beginning will be set to 2 hours before this</param>
        /// <param name="token">Cancellation token</param>
        public Task<IOpenSkyFlight[]> GetFlights(DateTime end, CancellationToken token = default)
        {
            return GetFlights(end.Subtract(TimeSpan.FromHours(2)), end, token);
        }

        /// <summary>
        /// Gets flights within the time period given.
        /// The time interval may not be more than 2 hours in length
        /// </summary>
        /// <returns>
        /// An array of flights for the time period
        /// </returns>
        /// <param name="begin">DateTime for the beginning of the interval checked</param>
        /// <param name="end">DateTime for the end of the interval checked</param>
        /// <param name="token">Cancellation token</param>
        /// <exception cref="OpenSkyException"></exception>
        public Task<IOpenSkyFlight[]> GetFlights(DateTime begin, DateTime end, CancellationToken token = default)
        {
            if (begin == default || end == default || end.Subtract(begin).TotalMinutes > 120)
            {
                throw new OpenSkyException("Beginning and end time must be provided, and must be within 2 hours of each other");
            }

            var dict = new Dictionary<string, object>();
            dict.Add("begin", begin.ToUnixTimestamp());
            dict.Add("end", end.ToUnixTimestamp());
            var query = "flights/all" + Utils.CreateQuery(dict);

            return GetAsync<IOpenSkyFlight[]>(query, token);
        }

        /// <summary>
        /// Gets flights for an aircraft in a range that is 30 days before
        /// the end time provided
        /// </summary>
        /// <returns>
        /// An array of flights for for the aircraft provided
        /// </returns>
        /// <param name="icao24">Aircraft ICAO-24 code</param>
        /// <param name="end">DateTime for the end of the interval checked. The beginning will be set to 30 days before this</param>
        /// <param name="token">Cancellation token</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<IOpenSkyFlight[]> GetFlightsByAircraft(string icao24, DateTime end, CancellationToken token = default)
        {
            return GetFlightsByAircraft(icao24, end.Subtract(TimeSpan.FromDays(30)), end, token);
        }

        /// <summary>
        /// Gets flights for an aircraft within the time period given.
        /// The time interval may not be more than 30 days in length
        /// </summary>
        /// <returns>
        /// An array of flights for for the aircraft provided
        /// </returns>
        /// <param name="icao24">Aircraft ICAO-24 code</param>
        /// <param name="begin">DateTime for the beginning of the interval checked</param>
        /// <param name="end">DateTime for the end of the interval checked</param>
        /// <param name="token">Cancellation token</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="OpenSkyException"></exception>
        public Task<IOpenSkyFlight[]> GetFlightsByAircraft(string icao24, DateTime begin, DateTime end, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(icao24))
            {
                throw new ArgumentNullException(nameof(icao24));
            }
            if (begin == default || end == default || end.Subtract(begin).TotalDays > 30)
            {
                throw new OpenSkyException("Beginning and end time must be provided, and must be within 30 days of each other");
            }

            var dict = new Dictionary<string, object>();
            dict.Add("icao24", icao24);
            dict.Add("begin", begin.ToUnixTimestamp());
            dict.Add("end", end.ToUnixTimestamp());
            var query = "flights/aircraft" + Utils.CreateQuery(dict);

            return GetAsync<IOpenSkyFlight[]>(query, token);
        }

        /// <summary>
        /// Gets a flight track for an aircraft at a certain time.
        /// Tracks are frequently not available, but if they are,
        /// cannot be accessed from more than 30 days in the past
        /// </summary>
        /// <returns>
        /// The flight track for the aircraft
        /// </returns>
        /// <param name="icao24">Aicraft ICAO-24 code</param>
        /// <param name="time">DateTime for the flight track, default is now</param>
        /// <param name="token">Cancellation token</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="OpenSkyException"></exception>
        public Task<IOpenSkyTrack> GetTrackByAircraft(string icao24, DateTime time = default, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(icao24))
            {
                throw new ArgumentNullException(nameof(icao24));
            }
            if (DateTime.UtcNow.Subtract(time).TotalDays > 30)
            {
                throw new OpenSkyException("Cannot access flight tracks over 30 days in the past");
            }

            var dict = new Dictionary<string, object>();
            dict.Add("icao24", icao24);
            dict.Add("time", (time == default ? 0 : time.ToUnixTimestamp()));
            var query = "tracks/all" + Utils.CreateQuery(dict);

            return GetAsync<IOpenSkyTrack>(query, token);
        }

        /// <summary>
        /// Gets registration information for an aircraft
        /// </summary>
        /// <returns>
        /// Aircraft registration information
        /// </returns>
        /// <param name="icao24">Aircraft ICAO-24 code</param>
        /// <param name="token">Cancellation token</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<IOpenSkyRegistration> GetAircraftRegistration(string icao24, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(icao24))
            {
                throw new ArgumentNullException(nameof(icao24));
            }

            return GetAsync<IOpenSkyRegistration>($"metadata/aircraft/icao/{icao24}", token);
        }

        /// <summary>
        /// Gets results for an aircraft search by the search provided.
        /// The search is according to aircraft ICAO-24 codes
        /// </summary>
        /// <returns>
        /// Search results for aircraft matching the search term
        /// </returns>
        /// <param name="search">A search term for searching aircraft by their ICAO-24 codes</param>
        /// <param name="amount">The number of results to return</param>
        /// <param name="token">Cancellation token</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<IOpenSkySearch> GetAircraftSearch(string search, int amount = 50, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                throw new ArgumentNullException(nameof(search));
            }

            return GetAsync<IOpenSkySearch>($"metadata/aircraft/list?n={amount}&p=1&q={search}", token);
        }

        /// <summary>
        /// Gets arrivals for an airport in a range that is seven days before
        /// the end time provided
        /// </summary>
        /// <returns>
        /// An array of flights that are arrivals for the airport provided
        /// </returns>
        /// <param name="icao">ICAO code for the airport</param>
        /// <param name="end">DateTime for the end of the interval checked. The beginning will be set to seven days before this</param>
        /// <param name="token">Cancellation token</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<IOpenSkyFlight[]> GetAirportArrivals(string icao, DateTime end, CancellationToken token = default)
        {
            return GetAirportArrivals(icao, end.Subtract(TimeSpan.FromDays(7)), end, token);
        }

        /// <summary>
        /// Gets arrivals for an airport within the time period given.
        /// The time interval may not be more than seven days in length
        /// </summary>
        /// <returns>
        /// An array of flights that are arrivals for the airport provided
        /// </returns>
        /// <param name="icao">ICAO code for the airport</param>
        /// <param name="begin">DateTime for the beginning of the interval checked</param>
        /// <param name="end">DateTime for the end of the interval checked</param>
        /// <param name="token">Cancellation token</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="OpenSkyException"></exception>
        public Task<IOpenSkyFlight[]> GetAirportArrivals(string icao, DateTime begin, DateTime end, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(icao))
            {
                throw new ArgumentNullException(nameof(icao));
            }
            if (begin == default || end == default || end.Subtract(begin).TotalDays > 7)
            {
                throw new OpenSkyException("Beginning and end time must be provided, and must be within 7 days of each other");
            }

            var dict = new Dictionary<string, object>();
            dict.Add("airport", icao);
            dict.Add("begin", begin.ToUnixTimestamp());
            dict.Add("end", end.ToUnixTimestamp());
            var query = "flights/arrival" + Utils.CreateQuery(dict);

            return GetAsync<IOpenSkyFlight[]>(query, token);
        }

        /// <summary>
        /// Gets departures for an airport in a range that is seven days before
        /// the end time provided
        /// </summary>
        /// <returns>
        /// An array of flights that are departures for the airport provided
        /// </returns>
        /// <param name="icao">ICAO code for the airport</param>
        /// <param name="end">DateTime for the end of the interval checked. The beginning will be set to seven days before this</param>
        /// <param name="token">Cancellation token</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<IOpenSkyFlight[]> GetAirportDepartures(string icao, DateTime end, CancellationToken token = default)
        {
            return GetAirportDepartures(icao, end.Subtract(TimeSpan.FromDays(7)), end, token);
        }

        /// <summary>
        /// Gets departures for an airport within the time period given.
        /// The time interval may not be more than seven days in length
        /// </summary>
        /// <returns>
        /// An array of flights that are departures for the airport provided
        /// </returns>
        /// <param name="icao">ICAO code for the airport</param>
        /// <param name="begin">DateTime for the beginning of the interval checked</param>
        /// <param name="end">DateTime for the end of the interval checked</param>
        /// <param name="token">Cancellation token</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="OpenSkyException"></exception>
        public Task<IOpenSkyFlight[]> GetAirportDepartures(string icao, DateTime begin, DateTime end, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(icao))
            {
                throw new ArgumentNullException(nameof(icao));
            }
            if (begin == default || end == default || end.Subtract(begin).TotalDays > 7)
            {
                throw new OpenSkyException("Beginning and end time must be provided, and must be within 7 days of each other");
            }

            var dict = new Dictionary<string, object>();
            dict.Add("airport", icao);
            dict.Add("begin", begin.ToUnixTimestamp());
            dict.Add("end", end.ToUnixTimestamp());
            var query = "flights/departure" + Utils.CreateQuery(dict);

            return GetAsync<IOpenSkyFlight[]>(query, token);
        }

        /// <summary>
        /// Gets airport information for the airport icao provided
        /// </summary>
        /// <returns>
        /// Airport information and registration
        /// </returns>
        /// <param name="icao">ICAO code for the airport</param>
        /// <param name="token">Cancellation token</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<IOpenSkyAirport> GetAirportInfo(string icao, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(icao))
            {
                throw new ArgumentNullException(nameof(icao));
            }

            return GetAsync<IOpenSkyAirport>($"airports?icao={icao}", token);
        }

        /// <summary>
        /// Gets airports in the provided region along with their information
        /// </summary>
        /// <returns>
        /// An array of airports in the region
        /// </returns>
        /// <param name="region">A region with latitude and longitude restrictions</param>
        /// <param name="token">Cancellation token</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<IOpenSkyAirport[]> GetAirportsByRegion(OpenSkyRegion region, CancellationToken token = default)
        {
            if (region == null)
            {
                throw new ArgumentNullException(nameof(region));
            }

            var dict = new Dictionary<string, object>();
            dict.Add("lamin", region.MinLatitude);
            dict.Add("lomin", region.MinLongitude);
            dict.Add("lamax", region.MaxLatitude);
            dict.Add("lomax", region.MaxLongitude);
            var query = "airports/region" + Utils.CreateQuery(dict);

            return GetAsync<IOpenSkyAirport[]>(query, token);
        }

        /// <summary>
        /// Base method for async get requests
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns>
        /// An entity of type TResult
        /// </returns>
        /// <param name="uriPath"></param>
        /// <param name="token">Cancellation token</param>
        /// <exception cref="OpenSkyException"></exception>
        private async Task<TResult> GetAsync<TResult>(string uriPath, CancellationToken token = default)
        {
            var response = await client.GetAsync(uriPath, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var jtr = new JsonTextReader(
                    new StreamReader(await response.Content.ReadAsStreamAsync().ConfigureAwait(false)))
                { CloseInput = true })
                {
                    return serializer.Deserialize<TResult>(jtr);
                }
            }

            // If it's not a success, return default instead of throwing an error
            return default(TResult);
        }

        /// <summary>
        /// Disposes this OpenSkyClient
        /// </summary>
        public void Dispose()
        {
            client?.Dispose();
        }
    }
}