﻿using SpotifyApi.NetCore.Models;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Defines a wrapper for the Spotify Web Playlists API.
    /// </summary>
    public interface IFollowApi
    {
        #region GetFollowingContains

        /// <summary>
        /// Check if Current User Follows Artists
        /// </summary>
        /// <param name="artistIds">Required. A comma-separated list of the artist Spotify IDs to check. A maximum of artist 50 IDs can be sent in one request.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        Task<bool[]> CheckCurrentUserFollowsArtists(
            string[] artistIds,
            string accessToken = null
            );

        /// <summary>
        /// Check if Current User Follows Artists
        /// </summary>
        /// <param name="artistIds">Required. A comma-separated list of the artist Spotify IDs to check. A maximum of artist 50 IDs can be sent in one request.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        Task<T> CheckCurrentUserFollowsArtists<T>(
            string[] artistIds,
            string accessToken = null
            );

        /// <summary>
        /// Check if Current User Follows Users
        /// </summary>
        /// <param name="userIds">Required. A comma-separated list of the user Spotify IDs. A maximum of 50 user IDs can be sent in one request. A minimum of 1 user id is required.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        Task<bool[]> CheckCurrentUserFollowsUsers(
            string[] userIds,
            string accessToken = null
            );

        /// <summary>
        /// Check if Current User Follows Users
        /// </summary>
        /// <param name="userIds">Required. A comma-separated list of the user Spotify IDs. A maximum of 50 user IDs can be sent in one request. A minimum of 1 user id is required.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        Task<T> CheckCurrentUserFollowsUsers<T>(
            string[] userIds,
            string accessToken = null
            );
        #endregion

        #region CheckCurrentUserFollowsPlaylist
        /// <summary>
        /// Check if Users Follow a Playlist
        /// </summary>
        /// <param name="playlistId">Required. The Spotify ID of the playlist.</param>
        /// <param name="userIds">Required. A comma-separated list of Spotify User IDs ; the ids of the users that you want to check to see if they follow the playlist. Minimum: 1 id. Maximum: 5 ids.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-user-following-playlist/
        /// </remarks>
        Task<bool[]> CheckUsersFollowPlaylist(
            string playlistId,
            string[] userIds,
            string accessToken = null
            );

        /// <summary>
        /// Check if Users Follow a Playlist
        /// </summary>
        /// <param name="playlistId">Required. The Spotify ID of the playlist.</param>
        /// <param name="userIds">Required. A comma-separated list of Spotify User IDs ; the ids of the users that you want to check to see if they follow the playlist. Minimum: 1 id. Maximum: 5 ids.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-user-following-playlist/
        /// </remarks>
        Task<T> CheckUsersFollowPlaylist<T>(
            string playlistId,
            string[] userIds,
            string accessToken = null
            );
        #endregion

        #region FollowArtistsOrUsers
        /// <summary>
        /// Follow Artists
        /// </summary>
        /// <param name="artistIds">Required. A comma-separated list of the artist Spotify IDs. A maximum of 50 artist IDs can be sent in one request. A minimum of 1 artist id is required.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/follow-artists-users/
        /// </remarks>
        Task FollowArtists(
            string[] artistIds,
            string accessToken = null
            );

        /// <summary>
        /// Follow Users
        /// </summary>
        /// <param name="userIds">Required. A comma-separated list of the user Spotify IDs. A maximum of 50 user IDs can be sent in one request. A minimum of 1 user id is required.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/follow-artists-users/
        /// </remarks>
        Task FollowUsers(
            string[] userIds,
            string accessToken = null
            );
        #endregion

        #region FollowPlaylist
        /// <summary>
        /// Follow a Playlist
        /// </summary>
        /// <param name="playlistId">Required. The Spotify ID of the playlist. Any playlist can be followed, regardless of its public/private status, as long as you know its playlist ID.</param>
        /// <param name="isPublic">Optional. Defaults to true. If true the playlist will be included in user’s public playlists, if false it will remain private.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/follow-playlist/
        /// </remarks>
        Task FollowPlaylist(
            string playlistId,
            bool isPublic = true,
            string accessToken = null
            );
        #endregion

        #region GetUsersFollowedArtists
        /// <summary>
        /// Get User's Followed Artists
        /// </summary>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="after">Optional. The last artist ID retrieved from the previous request.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedArtists"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/get-followed/
        /// </remarks>
        Task<PagedArtists> GetUsersFollowedArtists(
            int limit = 20,
            string after = "",
            string accessToken = null
            );

        /// <summary>
        /// Get User's Followed Artists
        /// </summary>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="after">Optional. The last artist ID retrieved from the previous request.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedArtists"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/get-followed/
        /// </remarks>
        Task<T> GetUsersFollowedArtists<T>(
            int limit = 20,
            string after = null,
            string accessToken = null
            );
        #endregion

        #region UnfollowArtistsOrUsers
        /// <summary>
        /// Unfollow Artists
        /// </summary>
        /// <param name="artistIds">A comma-separated list of the artist Spotify IDs. A maximum of 50 artist IDs can be sent in one request. A minimum of 1 artist id is required. </param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/unfollow-artists-users/
        /// </remarks>
        Task UnfollowArtists(
            string[] artistIds,
            string accessToken = null
            );

        /// <summary>
        /// Unfollow Users
        /// </summary>
        /// <param name="userIds">A comma-separated list of the user Spotify IDs. A maximum of 50 artist IDs can be sent in one request. A minimum of 1 artist id is required. </param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/unfollow-artists-users/
        /// </remarks>
        Task UnfollowUsers(
            string[] userIds = null,
            string accessToken = null
            );
        #endregion

        #region UnfollowPlaylist
        /// <summary>
        /// Unfollow a Playlist
        /// </summary>
        /// <param name="playlistId">The Spotify ID of the playlist that is to be no longer followed.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/unfollow-playlist/
        /// </remarks>
        Task UnfollowPlaylist(
            string playlistId,
            string accessToken = null
            );
        #endregion
    }
}
