﻿using SpotifyApi.NetCore.Authorization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// An API Wrapper for the Spotify Web API Follow endpoints.
    /// </summary>
    public class FollowApi : SpotifyWebApi, IFollowApi
    {
        #region constructors

        public FollowApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : base(httpClient, accessTokenProvider)
        {
            SearchApi = new SearchApi(httpClient, accessTokenProvider);
        }

        public FollowApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken)
        {
            SearchApi = new SearchApi(httpClient, accessToken);
        }

        public FollowApi(HttpClient httpClient) : base(httpClient)
        {
            SearchApi = new SearchApi(httpClient);
        }

        #endregion
        protected internal virtual ISearchApi SearchApi { get; set; }

        #region CheckCurrentUserFollowsArtistsOrUsers

        /// <summary>
        /// Check if Current User Follows Artists
        /// </summary>
        /// <param name="ids">	Required. A comma-separated list of the artist or the user Spotify IDs to check. For example: ids=74ASZWbe4lXaubB36ztrGX,08td7MxkoHQkXnWAYD8d6Q. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        public async Task<bool[]> CheckCurrentUserFollowsArtists(
            string[] ids,
            string accessToken = null
            ) => await CheckCurrentUserFollowsArtists<bool[]>(ids, accessToken);

        /// <summary>
        /// Check if Current User Follows Artists
        /// </summary>
        /// <param name="ids">	Required. A comma-separated list of the artist or the user Spotify IDs to check. For example: ids=74ASZWbe4lXaubB36ztrGX,08td7MxkoHQkXnWAYD8d6Q. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        public async Task<T> CheckCurrentUserFollowsArtists<T>(
            string[] ids,
            string accessToken = null
            )
        {
            if (ids?.Length < 1 || ids?.Length > 50) throw new
                    ArgumentException("A minimum of 1 and a maximum of 50 ids can be sent.");

            var builder = new UriBuilder($"{BaseUrl}/me/following/contains");
            builder.AppendToQuery("type", "artist");
            builder.AppendToQueryAsCsv("ids", ids);
            return await GetModel<T>(builder.Uri, accessToken);
        }

        /// <summary>
        /// Check if Current User Follows Users
        /// </summary>
        /// <param name="ids">	Required. A comma-separated list of the artist or the user Spotify IDs to check. For example: ids=74ASZWbe4lXaubB36ztrGX,08td7MxkoHQkXnWAYD8d6Q. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        public async Task<bool[]> CheckCurrentUserFollowsUsers(
            string[] ids,
            string accessToken = null
            ) => await CheckCurrentUserFollowsUsers<bool[]>(ids, accessToken);

        /// <summary>
        /// Check if Current User Follows Users
        /// </summary>
        /// <param name="ids">	Required. A comma-separated list of the artist or the user Spotify IDs to check. For example: ids=74ASZWbe4lXaubB36ztrGX,08td7MxkoHQkXnWAYD8d6Q. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        public async Task<T> CheckCurrentUserFollowsUsers<T>(
            string[] ids,
            string accessToken = null
            )
        {
            if (ids?.Length < 1 || ids?.Length > 50) throw new
                    ArgumentException("A minimum of 1 and a maximum of 50 ids can be sent.");

            var builder = new UriBuilder($"{BaseUrl}/me/following/contains");
            builder.AppendToQuery("type", "user");
            builder.AppendToQueryAsCsv("ids", ids);
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion

        #region CheckCurrentUserFollowsPlaylist
        /// <summary>
        /// Check to see if one or more Spotify users are following a specified playlist.
        /// </summary>
        /// <param name="playlistId">Required. The Spotify ID of the playlist.</param>
        /// <param name="userIds">Required. A comma-separated list of Spotify User IDs ; the ids of the users that you want to check to see if they follow the playlist. Maximum: 5 ids.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-user-following-playlist/
        /// </remarks>
        public Task<bool[]> CheckUsersFollowPlaylist(
            string playlistId,
            string[] userIds,
            string accessToken = null
            ) => CheckUsersFollowPlaylist<bool[]>(playlistId, userIds, accessToken);

        /// <summary>
        /// Check to see if one or more Spotify users are following a specified playlist.
        /// </summary>
        /// <param name="playlistId">Required. The Spotify ID of the playlist.</param>
        /// <param name="userIds">Required. A comma-separated list of Spotify User IDs ; the ids of the users that you want to check to see if they follow the playlist. Maximum: 5 ids.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-user-following-playlist/
        /// </remarks>
        public async Task<T> CheckUsersFollowPlaylist<T>(
            string playlistId,
            string[] userIds,
            string accessToken = null
            )
        {
            if (playlistId?.Length < 1) throw new 
                    ArgumentException("A playlist id must be supplied.");

            if (userIds?.Length < 1 || userIds?.Length > 5) throw new
                    ArgumentException("A minimum of 1 and a maximum of 5 user ids can be sent.");

            var builder = new UriBuilder($"{BaseUrl}/playlists/{playlistId}/followers/contains");
            builder.AppendToQueryAsCsv("ids", userIds);
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion

        #region FollowArtistsOrUsers
        /// <summary>
        /// Add the current user as a follower of one or more artists Spotify IDs.
        /// </summary>
        /// <param name="artistIds">Required. A comma-separated list of the artist Spotify IDs.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/follow-artists-users/
        /// </remarks>
        public async Task FollowArtists(
            string[] artistIds,
            string accessToken = null
            )
        {
            if (artistIds?.Length < 1 && artistIds.Length > 50) throw new
                    ArgumentException("A minimum of 1 and a maximum of 50 artist ids can be sent.");

            var builder = new UriBuilder($"{BaseUrl}/me/following");
            builder.AppendToQuery("type", "artist");
            builder.AppendToQueryAsCsv("ids", artistIds);
            await Put(builder.Uri, null, accessToken);
        }

        /// <summary>
        /// Add the current user as a follower of one or more users Spotify IDs.
        /// </summary>
        /// <param name="userIds">Required. A comma-separated list of the user Spotify IDs.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/follow-artists-users/
        /// </remarks>
        public async Task FollowUsers(
            string[] userIds,
            string accessToken = null
            )
        {
            if (userIds?.Length < 1 && userIds.Length > 50) throw new 
                    ArgumentException("A minimum of 1 and a maximum of 50 user ids can be sent.");

            var builder = new UriBuilder($"{BaseUrl}/me/following");
            builder.AppendToQuery("type", "user");
            builder.AppendToQueryAsCsv("ids", userIds);
            await Put(builder.Uri, null, accessToken);
        }
        #endregion

        #region FollowPlaylist
        /// <summary>
        /// Add the current user as a follower of a playlist.
        /// </summary>
        /// <param name="playlistId">Required. The Spotify ID of the playlist. Any playlist can be followed, regardless of its public/private status, as long as you know its playlist ID.</param>
        /// <param name="isPublic">Optional. Defaults to true. If true the playlist will be included in user’s public playlists, if false it will remain private.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/follow-playlist/
        /// </remarks>
        public async Task FollowPlaylist(
            string playlistId,
            bool isPublic = true,
            string accessToken = null
            )
        {
            if (playlistId == null) throw new
                    ArgumentException("A valid playlist id has to be provided.");

            var builder = new UriBuilder($"{BaseUrl}/playlists/{playlistId}/followers");
            await Put(builder.Uri, isPublic, accessToken);
        }
        #endregion

        #region GetUsersFollowedArtists
        /// <summary>
        /// Get User's Followed Artists
        /// </summary>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="after">Optional. The last artist ID retrieved from the previous request.</param>
        /// <returns>A json string containing an artists object. The artists object in turn contains a cursor-based paging object of Artists.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/get-followed/
        /// </remarks>
        public async Task<object> GetUsersFollowedArtists(
            int limit = 20,
            string after = "",
            string accessToken = null
            ) => await GetUsersFollowedArtists<object>(limit, after, accessToken);

        /// <summary>
        /// Get User's Followed Artists
        /// </summary>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="after">Optional. The last artist ID retrieved from the previous request.</param>
        /// <returns>A json string containing an artists object. The artists object in turn contains a cursor-based paging object of Artists.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/get-followed/
        /// </remarks>
        public async Task<T> GetUsersFollowedArtists<T>(
            int limit = 20,
            string after = null,
            string accessToken = null
            )
        {
            if (limit < 1 || limit > 50) throw new
                    ArgumentException("The limit can be a minimum of 1 and a maximum of 50.");

            var builder = new UriBuilder($"{BaseUrl}/me/following");
            builder.AppendToQuery("type", "artist");
            builder.AppendToQuery("limit", limit);
            if (after?.Length > 0)
            {
                builder.AppendToQuery("after", after);
            }
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion

        #region UnfollowArtistsOrUsers
        /// <summary>
        /// Unfollow Artists
        /// </summary>
        /// <param name="artistIds">A comma-separated list of the artist Spotify IDs. A maximum of 50 IDs can be sent in one request. A minimum of 1 artist id is required.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/unfollow-artists-users/
        /// </remarks>
        public async Task UnfollowArtists(
            string[] artistIds = null,
            string accessToken = null
            )
        {
            if (artistIds?.Length < 1 || artistIds?.Length > 50) throw new
                    ArgumentException("The artist ids can be a minimum of 1 and a maximum of 50.");

            var builder = new UriBuilder($"{BaseUrl}/me/following");
            builder.AppendToQuery("type", "artist");
            builder.AppendToQueryAsCsv("ids", artistIds);
            await Delete(builder.Uri, accessToken);
        }

        /// <summary>
        /// Unfollow Users
        /// </summary>
        /// <param name="userIds">A comma-separated list of the user Spotify IDs. A maximum of 50 IDs can be sent in one request. A minimum of 1 user id is required. </param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/unfollow-artists-users/
        /// </remarks>
        public async Task UnfollowUsers(
            string[] userIds = null,
            string accessToken = null
            )
        {
            if (userIds?.Length < 1 || userIds?.Length > 50) throw new
                    ArgumentException("The user ids can be a minimum of 1 and a maximum of 50.");

            var builder = new UriBuilder($"{BaseUrl}/me/following");
            builder.AppendToQuery("type", "user");
            builder.AppendToQueryAsCsv("ids", userIds);
            await Delete(builder.Uri, accessToken);
        }
        #endregion

        #region UnfollowPlaylist
        /// <summary>
        /// Unfollow a Playlist
        /// </summary>
        /// <param name="playlistId">The Spotify ID of the playlist that is to be no longer followed.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/unfollow-playlist/
        /// </remarks>
        public async Task UnfollowPlaylist(
            string playlistId,
            string accessToken = null
            )
        {
            if (playlistId?.Length < 1) throw new
                    ArgumentException("A valid playlist id has to be supplied.");

            var builder = new UriBuilder($"{BaseUrl}/playlists/{playlistId}/followers");
            await Delete(builder.Uri, accessToken);
        }
        #endregion
    }
}
