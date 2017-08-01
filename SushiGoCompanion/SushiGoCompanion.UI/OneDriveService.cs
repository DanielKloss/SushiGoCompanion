using Microsoft.OneDrive.Sdk;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Microsoft.OneDrive.Sdk.Authentication;

namespace SushiGoCompanion.UI
{
    public class OneDriveService
    {
        private OneDriveClient _client;

        public async Task Authenticate()
        {
            MsaAuthenticationProvider msaAuthenticationProvider = new OnlineIdAuthenticationProvider((new string[] { "onedrive.readwrite" }));
            await msaAuthenticationProvider.AuthenticateUserAsync();
            _client = new OneDriveClient("https://api.onedrive.com/v1.0", msaAuthenticationProvider);
        }

        public async Task Backup()
        {
            await Authenticate();

            StorageFile file = await ApplicationData.Current.LocalFolder.TryGetItemAsync("{{DatabaseName.sql}}") as StorageFile;

            using (var stream = await file.OpenStreamForReadAsync())
            {
                var result = await _client.Drive.Root.ItemWithPath("{{/GameNameScorecard/GameName}}").Content.Request().PutAsync<Item>(stream);
            }
        }

        public async Task Restore()
        {
            await Authenticate();

            var stream = await _client.Drive.Root.ItemWithPath("{{/GameNameScorecard/GameName}}").Content.Request().GetAsync();

            using (stream)
            {
                var destination = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync("{{DatabaseName.sql}}", CreationCollisionOption.ReplaceExisting);
                stream.CopyTo(destination);
            }
        }
    }
}
