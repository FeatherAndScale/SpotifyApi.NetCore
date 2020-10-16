using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using SpotifyApi.NetCore.Tests.Mocks;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class PlayerApiTests
    {
        private static IConfiguration _config = TestsHelper.GetLocalConfig();
        private static HttpClient _http = new HttpClient();

        [TestMethod]
        public async Task PlayTracks_AccessToken_PutInvokedWithAccessToken()
        {
            // arrange
            const string token = "abc123";
            const string trackUri = "spotify:track:7ouMYWpwJ422jRcDASZB7P";

            var http = new MockHttpClient();
            http.SetupSendAsync();
            var service = new Mock<PlayerApi>(http.HttpClient, token) { CallBase = true };

            // act
            await service.Object.PlayTracks(trackUri, token);

            // assert
            service.Verify(s => s.Put(It.IsAny<Uri>(), It.IsAny<object>(), token));
        }

        [TestMethod]
        public async Task PlayAlbum_AccessToken_PutInvokedWithAccessToken()
        {
            // arrange
            const string token = "abc123";
            const string uri = "spotify:album:FooBar1234567";

            var http = new MockHttpClient();
            http.SetupSendAsync();
            var service = new Mock<PlayerApi>(http.HttpClient, token) { CallBase = true };

            // act
            await service.Object.PlayAlbum(uri, token);

            // assert
            service.Verify(s => s.Put(It.IsAny<Uri>(), It.IsAny<object>(), token));
        }

        [TestMethod]
        public async Task PlayArtist_AccessToken_PutInvokedWithAccessToken()
        {
            // arrange
            const string token = "abc123";
            const string id = "0TnOYISbd1XYRBk9myaseg";

            var http = new MockHttpClient();
            http.SetupSendAsync();
            var service = new Mock<PlayerApi>(http.HttpClient, token) { CallBase = true };

            // act
            await service.Object.PlayArtist(id, token);

            // assert
            service.Verify(s => s.Put(It.IsAny<Uri>(), It.IsAny<object>(), token));
        }

        [TestMethod]
        public async Task PlayPlaylist_AccessToken_PutInvokedWithAccessToken()
        {
            // arrange
            const string token = "abc123";
            const string uri = "spotify:playlist:FooBar1234567";

            var http = new MockHttpClient();
            http.SetupSendAsync();
            var service = new Mock<PlayerApi>(http.HttpClient, token) { CallBase = true };

            // act
            await service.Object.PlayPlaylist(uri, token);

            // assert
            service.Verify(s => s.Put(It.IsAny<Uri>(), It.IsAny<object>(), token));
        }

        [TestMethod]
        public async Task Play_AccessToken_PutInvokedWithAccessToken()
        {
            // arrange
            const string token = "abc123";

            var http = new MockHttpClient();
            http.SetupSendAsync();
            var service = new Mock<PlayerApi>(http.HttpClient, token) { CallBase = true };

            // act
            await service.Object.Play(token);

            // assert
            service.Verify(s => s.Put(It.IsAny<Uri>(), It.IsAny<object>(), token));
        }

        [TestMethod]
        public async Task GetDevices_AccessToken_PutInvokedWithAccessToken()
        {
            // arrange
            const string token = "abc123";
            string json = JsonConvert.SerializeObject(new { devices = new Device[] { new Device() } });
            var http = new MockHttpClient();
            http.SetupSendAsync(json);
            var service = new Mock<PlayerApi>(http.HttpClient, token) { CallBase = true };

            // act
            await service.Object.GetDevices<dynamic>(token);

            // assert
            service.Verify(s => s.GetModelFromProperty<dynamic>(It.IsAny<Uri>(), It.IsAny<string>(), token));
        }

        [TestMethod]
        public async Task GetCurrentPlaybackInfo_AccessToken_GetJObjectInvokedWithAccessToken()
        {
            // arrange
            const string token = "abc123";
            string json = JsonConvert.SerializeObject(new CurrentTrackPlaybackContext());
            var http = new MockHttpClient();
            http.SetupSendAsync(json);
            var service = new Mock<PlayerApi>(http.HttpClient, token) { CallBase = true };

            // act
            await service.Object.GetCurrentPlaybackInfo(token);

            // assert
            service.Verify(s => s.GetJObject(It.IsAny<Uri>(), token));
        }

        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GetCurrentPlaybackInfo_NoToken_ThrowsInvalidOperationException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);

            // act
            await player.GetCurrentPlaybackInfo();
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task Seek_1ms_NoException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);
            string accessToken = await TestsHelper.GetUserAccessToken();
            await player.PlayAlbum("5mAPk4qeNqVLtNydaWbWlf", accessToken);

            // act
            await player.Seek(1, accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task Shuffle_OnOff_NoException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);
            string accessToken = await TestsHelper.GetUserAccessToken();
            await player.PlayAlbum("5mAPk4qeNqVLtNydaWbWlf", accessToken);

            // act
            await player.Shuffle(true, accessToken: accessToken);
            await player.Shuffle(false, accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task Volume_100_NoException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);
            string accessToken = await TestsHelper.GetUserAccessToken();
            await player.PlayAlbum("5mAPk4qeNqVLtNydaWbWlf", accessToken);

            // act
            await player.Volume(100, accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task Repeat_ContextOff_NoException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);
            string accessToken = await TestsHelper.GetUserAccessToken();
            await player.PlayAlbum("5mAPk4qeNqVLtNydaWbWlf", accessToken);

            // act
            await player.Repeat(RepeatStates.Context, accessToken: accessToken);
            await player.Repeat(RepeatStates.Off, accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task Pause_UserAccessToken_NoException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);
            string accessToken = await TestsHelper.GetUserAccessToken();
            await player.PlayAlbum("5mAPk4qeNqVLtNydaWbWlf", accessToken);

            // act
            await player.Pause(accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task SkipNext_UserAccessToken_NoException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);
            string accessToken = await TestsHelper.GetUserAccessToken();
            await player.PlayAlbum("5mAPk4qeNqVLtNydaWbWlf", accessToken);

            // act
            await player.SkipNext(accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task SkipPrevious_AfterSkipNext_NoException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);
            string accessToken = await TestsHelper.GetUserAccessToken();
            await player.PlayAlbum("5mAPk4qeNqVLtNydaWbWlf", accessToken);
            await player.SkipNext(accessToken: accessToken);

            // act
            await player.SkipPrevious(accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task GetRecentlyPlayedTracks_UserAccessToken_NotNull()
        {
            // arrange
            var http = new HttpClient();
            IPlayerApi player = new PlayerApi(http);
            string accessToken = await TestsHelper.GetUserAccessToken();

            // act
            var history = await player.GetRecentlyPlayedTracks(accessToken: accessToken);

            // assert
            Assert.IsNotNull(history);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task GetRecentlyPlayedTracks_GetNextPage_ItemsAny()
        {
            // arrange
            var http = new HttpClient();
            IPlayerApi player = new PlayerApi(http);
            string accessToken = await TestsHelper.GetUserAccessToken();

            // act
            var history = await player.GetRecentlyPlayedTracks(accessToken: accessToken);
            var moreHistory = await player.GetRecentlyPlayedTracks(before: history.Cursors.Before, accessToken: accessToken);

            // assert
            Assert.IsTrue(moreHistory.Items.Any());
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task GetCurrentlyPlayingTrack_UserAccessToken_NoException()
        {
            // arrange
            var http = new HttpClient();
            IPlayerApi player = new PlayerApi(http);
            string accessToken = await TestsHelper.GetUserAccessToken();

            // act
            var context = await player.GetCurrentlyPlayingTrack(
                additionalTypes: new[] { "episode" },
                accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task TransferPlayback_Device0Id_NoException()
        {
            // arrange
            var http = new HttpClient();
            IPlayerApi player = new PlayerApi(http);
            string accessToken = await TestsHelper.GetUserAccessToken();
            var devices = await player.GetDevices(accessToken: accessToken);

            // act
            if (devices.Any()) await player.TransferPlayback(devices[0].Id, accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task TransferPlayback_GetDevice1IdPlayTrue_NoException()
        {
            // arrange
            var http = new HttpClient();
            IPlayerApi player = new PlayerApi(http);
            string accessToken = await TestsHelper.GetUserAccessToken();
            var devices = await player.GetDevices(accessToken: accessToken);

            // act
            if (devices.Any()) await player.TransferPlayback(devices.Last().Id, play: true, accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task PlayTracks_2Tracks_NoException()
        {
            // act
            await new PlayerApi(new HttpClient())
                .PlayTracks(
                    new [] { "1vNNfTgHsrpOXeiaXQBlH7", "48rukEbJkzt2yUxAudbcZw" }, 
                    accessToken: await TestsHelper.GetUserAccessToken());
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task PlayAlbum_AlbumId_NoException()
        {
            // act
            await new PlayerApi(new HttpClient())
                .PlayAlbum(
                    "5mAPk4qeNqVLtNydaWbWlf",
                    accessToken: await TestsHelper.GetUserAccessToken());
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task PlayAlbumOffset_Track3_NoException()
        {
            // act
            await new PlayerApi(new HttpClient())
                .PlayAlbumOffset(
                    "5mAPk4qeNqVLtNydaWbWlf",
                    2,
                    accessToken: await TestsHelper.GetUserAccessToken());
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task PlayAlbumOffset_TrackId_NoException()
        {
            // act
            await new PlayerApi(new HttpClient())
                .PlayAlbumOffset(
                    "5mAPk4qeNqVLtNydaWbWlf",
                    "48rukEbJkzt2yUxAudbcZw",
                    accessToken: await TestsHelper.GetUserAccessToken());
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task PlayArtist_ArtistId_NoException()
        {
            // act
            await new PlayerApi(new HttpClient())
                .PlayArtist(
                    "6FXMGgJwohJLUSr5nVlf9X",
                    accessToken: await TestsHelper.GetUserAccessToken());
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task PlayArtistOffset_Track2_NoException()
        {
            // act
            await new PlayerApi(new HttpClient())
                .PlayAlbumOffset(
                    "6FXMGgJwohJLUSr5nVlf9X",
                    1,
                    accessToken: await TestsHelper.GetUserAccessToken());
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task PlayArtistOffset_TrackId_NoException()
        {
            // act
            await new PlayerApi(new HttpClient())
                .PlayAlbumOffset(
                    "6FXMGgJwohJLUSr5nVlf9X",
                    "1vNNfTgHsrpOXeiaXQBlH7",
                    accessToken: await TestsHelper.GetUserAccessToken());
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task PlayPlaylist_PlaylistId_NoException()
        {
            // act
            await new PlayerApi(new HttpClient())
                .PlayPlaylist(
                    "37i9dQZF1DZ06evO3WBS4o",
                    accessToken: await TestsHelper.GetUserAccessToken());
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task PlayPlaylistOffset_Track2_NoException()
        {
            // act
            await new PlayerApi(new HttpClient())
                .PlayAlbumOffset(
                    "37i9dQZF1DZ06evO3WBS4o",
                    1,
                    accessToken: await TestsHelper.GetUserAccessToken());
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task PlayPlaylistOffset_TrackId_NoException()
        {
            // act
            await new PlayerApi(new HttpClient())
                .PlayAlbumOffset(
                    "37i9dQZF1DZ06evO3WBS4o",
                    "2BndJYJQ17UcEeUFJP5JmY",
                    accessToken: await TestsHelper.GetUserAccessToken());
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task Play_Paused_NoException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);
            string accessToken = await TestsHelper.GetUserAccessToken();
            await player.Pause(accessToken: accessToken);

            // act
            await player.Play(accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task GetDevices_AccessToken_ResultIsNotNull()
        {
            // act
            var result = await new PlayerApi(new HttpClient())
                .GetDevices(accessToken: await TestsHelper.GetUserAccessToken());

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task GetCurrentPlaybackInfo_AccessToken_NoException()
        {
            // act
            var result = await new PlayerApi(new HttpClient())
                .GetCurrentPlaybackInfo(accessToken: await TestsHelper.GetUserAccessToken());
        }


        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task AddToQueue_ItemUri_NoException()
        {
            // act
            await new PlayerApi(new HttpClient())
                .AddToQueue(
                    "spotify:track:6jmVOhtad54Xpx35gMB3qY",
                    accessToken: await TestsHelper.GetUserAccessToken());
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task AddToQueue_ItemUri_Empty_ArgumentNullException()
        {
            // act
            await new PlayerApi(new HttpClient())
                .AddToQueue(
                    null,
                    accessToken: await TestsHelper.GetUserAccessToken());
        }

        

    }

    class BearerTokenStore
    {
        public string GetToken(string user)
        {
            return "foo";
        }
    }
}