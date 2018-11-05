using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Defines a wrapper for the Spotify Web Tracks API.
    /// </summary>
    public interface ITracksApi
    {
        #region GetTrack

        /// <summary>
        /// Get Spotify catalog information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 <see cref="SpotifyCountryCode"/> or the
        /// string `from_token`. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>Task of Track</returns>
        Task<Track> GetTrack(string trackId, string market = null);

        /// <summary>
        /// Get Spotify catalog information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 <see cref="SpotifyCountryCode"/> or the
        /// string `from_token`. Provide this parameter if you want to apply Track Relinking.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        Task<T> GetTrack<T>(string trackId, string market = null);

        #endregion

        #region GetTracks

        /// <summary>
        /// Get Spotify catalog information for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="trackIds">An array of the Spotify IDs for the tracks. Maximum: 50 IDs.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 <see cref="SpotifyCountryCode"/> or the
        /// string `from_token`. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>Task of Track[]</returns>
        Task<Track[]> GetTracks(string[] trackIds, string market = null);

        /// <summary>
        /// Get Spotify catalog information for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="trackIds">An array of the Spotify IDs for the tracks. Maximum: 50 IDs.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 <see cref="SpotifyCountryCode"/> or the
        /// string `from_token`. Provide this parameter if you want to apply Track Relinking.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        Task<T> GetTracks<T>(string[] trackIds, string market = null);

        #endregion

        #region GetTrackAudioAnalysis

        /// <summary>
        /// Get a detailed audio analysis for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <returns>Task of <see cref="TrackAudioAnalysis" /></returns>
        Task<TrackAudioAnalysis> GetTrackAudioAnalysis(string trackId);

        /// <summary>
        /// Get a detailed audio analysis for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        Task<T> GetTrackAudioAnalysis<T>(string trackId);

        #endregion

        #region GetTrackAudioFeatures

        /// <summary>
        /// Get audio feature information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <returns>Task of <see cref="TrackAudioFeatures" /></returns>
        Task<TrackAudioFeatures> GetTrackAudioFeatures(string trackId);

        /// <summary>
        /// Get audio feature information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        Task<T> GetTrackAudioFeatures<T>(string trackId);

        #endregion

        #region GetTracksAudioFeatures

        /// <summary>
        /// Get audio features for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="trackIds">An array of the Spotify IDs for the tracks. Maximum: 100 IDs.</param>
        /// <returns>Task of <see cref="TrackAudioFeatures[]" /></returns>
        Task<TrackAudioFeatures[]> GetTracksAudioFeatures(string[] trackIds);

        /// <summary>
        /// Get audio features for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="trackIds">An array of the Spotify IDs for the tracks. Maximum: 100 IDs.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        Task<T> GetTracksAudioFeatures<T>(string[] trackIds);

        #endregion
    }
}