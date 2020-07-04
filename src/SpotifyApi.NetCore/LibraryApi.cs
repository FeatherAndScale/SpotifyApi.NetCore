﻿using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// An implementation of the API Wrapper for the Spotify Web API Library endpoints.
    /// </summary>
    public class LibraryApi : SpotifyWebApi, ILibraryApi
    {
        #region constructors
        public LibraryApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : base(httpClient, accessTokenProvider)
        {
        }

        public LibraryApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken)
        {
        }

        public LibraryApi(HttpClient httpClient) : base(httpClient)
        {
        }
        #endregion

        #region CheckUsersSavedAlbumsOrShowsOrTracks
        #region CheckUsersSavedAlbums
        /// <summary>
        /// Check if one or more albums is already saved in the current Spotify user’s ‘Your Music’ library.
        /// </summary>
        /// <param name="albumIds">Required. A comma-separated list of the Spotify IDs for the albums. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/check-users-saved-albums/
        /// </remarks>
        public Task<bool[]> CheckUsersSavedAlbums(
            string[] albumIds,
            string accessToken = null
            ) => CheckUsersSavedAlbumsOrShowsOrTracks<bool[]>("album", albumIds, accessToken);
        #endregion

        #region CheckUsersSavedShows
        /// <summary>
        /// Check if one or more shows is already saved in the current Spotify user’s library.
        /// </summary>
        /// <param name="showIds">Required. A comma-separated list of the Spotify IDs for the shows. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/check-users-saved-shows/
        /// </remarks>
        public Task<bool[]> CheckUsersSavedShows(
            string[] showIds,
            string accessToken = null
            ) => CheckUsersSavedAlbumsOrShowsOrTracks<bool[]>("show", showIds, accessToken);
        #endregion

        #region CheckUsersSavedTracks
        /// <summary>
        /// Check if one or more tracks is already saved in the current Spotify user’s ‘Your Music’ library.
        /// </summary>
        /// <param name="trackIds">Required. A comma-separated list of the Spotify IDs for the tracks. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/check-users-saved-tracks/
        /// </remarks>
        public Task<bool[]> CheckUsersSavedTracks(
            string[] trackIds,
            string accessToken = null
            ) => CheckUsersSavedAlbumsOrShowsOrTracks<bool[]>("track", trackIds, accessToken);
        #endregion

        /// <summary>
        /// Check if one or more albums or shows or tracks is already saved in the current Spotify user’s ‘Your Music’ library.
        /// </summary>
        /// <param name="type">Required. Value is either album or show or track.</param>
        /// <param name="albumOrShowOrTrackIds">Required. A comma-separated list of the Spotify IDs for the albums or shows or tracks. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/check-users-saved-albums/
        /// https://developer.spotify.com/documentation/web-api/reference/library/check-users-saved-shows/
        /// https://developer.spotify.com/documentation/web-api/reference/library/check-users-saved-tracks/
        /// </remarks>
        public async Task<T> CheckUsersSavedAlbumsOrShowsOrTracks<T>(
            string type,
            string[] albumOrShowOrTrackIds,
            string accessToken = null
            )
        {
            if (albumOrShowOrTrackIds?.Length < 1 || albumOrShowOrTrackIds?.Length > 50) throw new
                    ArgumentException($"A minimum of 1 and a maximum of 50 {type} ids can be sent.");

            var builder = new UriBuilder($"{BaseUrl}/me/{type}s/contains");
            builder.AppendToQueryAsCsv("ids", albumOrShowOrTrackIds);
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion

        #region GetCurrentUsersSavedAlbumsOrTracks
        #region GetCurrentUsersSavedAlbums
        /// <summary>
        /// Get a list of the albums saved in the current Spotify user’s ‘Your Music’ library.
        /// </summary>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token or the string from_token <see cref="SpotifyCountryCodes" />. Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedAlbums"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-albums/
        /// </remarks>
        public Task<PagedAlbums> GetCurrentUsersSavedAlbums(
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            ) => GetCurrentUsersSavedAlbumsOrTracks<PagedAlbums>("album", limit, offset, market, accessToken);
        #endregion

        #region GetUsersSavedTracks
        /// <summary>
        /// Get a list of the songs saved in the current Spotify user’s ‘Your Music’ library.
        /// </summary>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token or the string from_token <see cref="SpotifyCountryCodes" />. Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedTracks"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-tracks/
        /// </remarks>
        public Task<PagedTracks> GetUsersSavedTracks(
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            ) => GetCurrentUsersSavedAlbumsOrTracks<PagedTracks>("track", limit, offset, market, accessToken);
        #endregion

        /// <summary>
        /// Get a list of the albums saved in the current Spotify user’s ‘Your Music’ library.
        /// </summary>
        /// <param name="type">Required. Value is either album or track.</param>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token or the string from_token <see cref="SpotifyCountryCodes" />. Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedAlbums"/> or <see cref="PagedTracks"/> object depending on the param type value.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-albums/
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-tracks/
        /// </remarks>
        public async Task<T> GetCurrentUsersSavedAlbumsOrTracks<T>(
            string type,
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            )
        {
            if(type != "album" && type != "track") throw new
                    ArgumentException("The type value can be one of either album or track.");
            if (limit < 1 || limit > 50) throw new
                    ArgumentException($"The limit can be a minimum of 1 and a maximum of 50 {type} IDs.");
            if(offset < 0) throw new
                    ArgumentException("The offset must be an integer value greater than 0.");

            var builder = new UriBuilder($"{BaseUrl}/me/{type}s");
            builder.AppendToQuery("limit", limit);
            builder.AppendToQuery("offset", offset);
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("market", market);
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion

        #region GetUsersSavedShows
        /// <summary>
        /// Get a list of shows saved in the current Spotify user’s library. Optional parameters can be used to limit the number of shows returned.
        /// </summary>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedShows"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-shows/
        /// </remarks>
        public Task<PagedShows> GetUsersSavedShows(
            int limit = 20,
            int offset = 0,
            string accessToken = null
            ) => GetUsersSavedShows<PagedShows>(limit, offset, accessToken);

        /// <summary>
        /// Get a list of shows saved in the current Spotify user’s library. Optional parameters can be used to limit the number of shows returned.
        /// </summary>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedShows"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-shows/
        /// </remarks>
        public async Task<T> GetUsersSavedShows<T>(
            int limit = 20,
            int offset = 0,
            string accessToken = null
            )
        {
            if (limit < 1 || limit > 50) throw new
                    ArgumentException("The limit can be a minimum of 1 and a maximum of 50.");
            if (offset < 0) throw new
                     ArgumentException("The offset must be an integer value greater than 0.");

            var builder = new UriBuilder($"{BaseUrl}/me/shows");
            builder.AppendToQuery("limit", limit);
            builder.AppendToQuery("offset", offset);
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion

        #region RemoveAlbumsOrTracksForCurrentUser
        #region RemoveAlbumsForCurrentUser
        /// <summary>
        /// Remove one or more albums from the current user’s ‘Your Music’ library.
        /// </summary>
        /// <param name="albumIds">A comma-separated list of the album Spotify IDs. A maximum of 50 album IDs can be sent in one request. A minimum of 1 album id is required. </param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/remove-albums-user/
        /// </remarks>
        public async Task RemoveAlbumsForCurrentUser(
            string[] albumIds,
            string accessToken = null
            ) => await RemoveAlbumsOrTracksForCurrentUser("album", albumIds, accessToken);
        #endregion

        #region RemoveUsersSavedTracks
        /// <summary>
        /// Remove one or more tracks from the current user’s ‘Your Music’ library.
        /// </summary>
        /// <param name="trackIds">Required. A comma-separated list of the track Spotify IDs. A maximum of 50 track IDs can be sent in one request. A minimum of 1 track id is required.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/remove-tracks-user/
        /// </remarks>
        public async Task RemoveUsersSavedTracks(
            string[] trackIds,
            string accessToken = null
            ) => await RemoveAlbumsOrTracksForCurrentUser("track", trackIds, accessToken);
        #endregion

        /// <summary>
        /// Remove one or more albums from the current user’s ‘Your Music’ library.
        /// </summary>
        /// <param name="type">Required. Value is either album or track.</param>
        /// <param name="albumIds">A comma-separated list of the album Spotify IDs. A maximum of 50 album IDs can be sent in one request. A minimum of 1 album id is required. </param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/remove-albums-user/
        /// https://developer.spotify.com/documentation/web-api/reference/library/remove-tracks-user/
        /// </remarks>
        internal async Task RemoveAlbumsOrTracksForCurrentUser(
            string type,
            string[] albumOrTrackIds,
            string accessToken = null
            )
        {
            if (type != "album" && type != "track") throw new
                     ArgumentException("The type value can be one of either album or track.");
            if (albumOrTrackIds?.Length < 1 || albumOrTrackIds?.Length > 50) throw new
                    ArgumentException($"The {type} ids can be a minimum of 1 and a maximum of 50.");

            var builder = new UriBuilder($"{BaseUrl}/me/{type}s");
            builder.AppendToQueryAsCsv("ids", albumOrTrackIds);
            await Delete(builder.Uri, accessToken);
        }
        #endregion

        #region RemoveUsersSavedShows
        /// <summary>
        /// Delete one or more shows from current Spotify user’s library.
        /// </summary>
        /// <param name="showIds">Required. A comma-separated list of the show Spotify IDs. A maximum of 50 show IDs can be sent in one request. A minimum of 1 show id is required.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token <see cref="SpotifyCountryCodes" />. If a country code is specified, only shows that are available in that market will be removed. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/remove-shows-user/
        /// </remarks>
        public async Task RemoveUsersSavedShows(
            string[] showIds,
            string market = null,
            string accessToken = null
            )
        {
            if (showIds?.Length < 1 || showIds?.Length > 50) throw new
                    ArgumentException("The show ids can be a minimum of 1 and a maximum of 50.");

            var builder = new UriBuilder($"{BaseUrl}/me/shows");
            builder.AppendToQueryAsCsv("ids", showIds);
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("market", market);
            await Delete(builder.Uri, accessToken);
        }
        #endregion

        #region SaveAlbumsOrShowsOrTracksForCurrentUser
        #region SaveAlbumsForCurrentUser
        /// <summary>
        /// Save one or more albums to the current user’s ‘Your Music’ library.
        /// </summary>
        /// <param name="albumIds">Required. A comma-separated list of the album Spotify IDs. A maximum of 50 album IDs can be sent in one request. A minimum of 1 album id is required.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/save-albums-user/
        /// </remarks>
        public async Task SaveAlbumsForCurrentUser(
            string[] albumIds,
            string accessToken = null
            ) => await SaveAlbumsOrShowsOrTracksForCurrentUser("album", albumIds, accessToken);
        #endregion

        #region SaveShowsForCurrentUser
        /// <summary>
        /// Save one or more shows to current Spotify user’s library.
        /// </summary>
        /// <param name="showIds">Required. A comma-separated list of the show Spotify IDs. A maximum of 50 show IDs can be sent in one request. A minimum of 1 show id is required.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/save-shows-user/
        /// </remarks>
        public async Task SaveShowsForCurrentUser(
            string[] showIds,
            string accessToken = null
            ) => await SaveAlbumsOrShowsOrTracksForCurrentUser("show", showIds, accessToken);
        #endregion

        #region SaveTracksForUser
        /// <summary>
        /// Save one or more tracks to the current user’s ‘Your Music’ library.
        /// </summary>
        /// <param name="trackIds">Required. A comma-separated list of the track Spotify IDs. A maximum of 50 track IDs can be sent in one request. A minimum of 1 track id is required.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/save-tracks-user/
        /// </remarks>
        public async Task SaveTracksForUser(
            string[] trackIds,
            string accessToken = null
            ) => await SaveAlbumsOrShowsOrTracksForCurrentUser("track", trackIds, accessToken);
        #endregion

        /// <summary>
        /// Save one or more albums to the current user’s ‘Your Music’ library.
        /// </summary>
        /// <param name="type">Required. Value is either album or show or track.</param>
        /// <param name="albumIds">Required. A comma-separated list of the album Spotify IDs. A maximum of 50 album IDs can be sent in one request. A minimum of 1 album id is required.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/save-albums-user/
        /// </remarks>
        internal async Task SaveAlbumsOrShowsOrTracksForCurrentUser(
            string type,
            string[] albumOrShowOrTrackIds,
            string accessToken = null
            )
        {
            if (type != "album" && type != "show" && type != "track") throw new
                     ArgumentException("The type value can be one of either album or show or track.");
            if (albumOrShowOrTrackIds?.Length < 1 && albumOrShowOrTrackIds.Length > 50) throw new
                    ArgumentException($"A minimum of 1 and a maximum of 50 {type} ids can be sent.");

            var builder = new UriBuilder($"{BaseUrl}/me/{type}s");
            await Put(builder.Uri, new { ids = albumOrShowOrTrackIds }, accessToken);
        }
        #endregion
    }
}
