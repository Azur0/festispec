using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Festispec.DesktopApplication.DesktopControllers
{
    public class OfflineController
    {
        public async Task SaveToFile<T>(string filename, T dataToSave)
        {
            var offlineObject = new OfflineObject<T>();
            offlineObject.savedTime = DateTime.Now;
            offlineObject.value = dataToSave;

            string output = JsonConvert.SerializeObject(offlineObject);

            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.CreateFileAsync(filename + ".json", CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(file, output);
        }

        public async Task<OfflineObject<T>> GetOfflineData<T>(string filename)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.GetFileAsync(filename + ".json");
            string content = await FileIO.ReadTextAsync(file);
            var offlineObject = JsonConvert.DeserializeObject<OfflineObject<T>>(content);
            return offlineObject;
        }
    }

    public class OfflineObject<T>
    {
        public DateTime savedTime;
        public T value;
    }
}
