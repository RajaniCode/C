using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

using System.Net.Http;
using Newtonsoft.Json;
using System.IO;


namespace DotnetCatalog.DataModel
{
    public class FeatureDataItem : DotnetCatalog.Common.BindableBase
    {
        private int _id = 0;
        public int Id
        {
            get { return this._id; }
            set { this.SetProperty(ref this._id, value); }
        }

        private string _name = string.Empty;
        public string Name
        {
            get { return this._name; }
            set { this.SetProperty(ref this._name, value); }
        }

        private byte[] _icon = null;
        public byte[] Icon
        {
            get { return this._icon; }
            set { this.SetProperty(ref this._icon, value); }
        }

        private string _version = null;
        public string Version
        {
            get { return this._version; }
            set { this.SetProperty(ref this._version, value); }
        }

        private int _rating = 0;
        public int Rating
        {
            get { return this._rating; }
            set { this.SetProperty(ref this._rating, value); }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set { this.SetProperty(ref this._description, value); }
        }

        private byte[] _image = null;
        public byte[] Image
        {
            get { return this._image; }
            set { this.SetProperty(ref this._image, value); }
        }

        public async void GetMoreInfoAsync()
        {
            using (var client = new WebBackendClient())
            {
                FeatureDataItem feature = await client.Download<FeatureDataItem>("/features/" + Id);

                Version = feature.Version;
                Rating = feature.Rating;
                Description = feature.Description;
                Image = feature.Image;
            }
        }
    }

    public class FeatureDataSource
    {
        private static FeatureDataSource _sampleDataSource = new FeatureDataSource();

        private ObservableCollection<FeatureDataItem> _items = new ObservableCollection<FeatureDataItem>();
        public ObservableCollection<FeatureDataItem> Items
        {
            get { return this._items; }
        }

        public FeatureDataSource()
        {
            this.Initialize();
        }

        public static IEnumerable<FeatureDataItem> Get()
        {
            return _sampleDataSource.Items;
        }

        public static FeatureDataItem Get(int id)
        {
            return _sampleDataSource.Items.FirstOrDefault(i => i.Id == id);
        }

        private async void Initialize()
        {
            using (var client = new WebBackendClient())
            {
                List<int> list = await client.Download<List<int>>("/featuressummary");

                foreach (var i in list)
                {
                    FeatureDataItem feature = await client.Download<FeatureDataItem>("/featuressummary/" + i);
                    _items.Add(feature);
                }
            }
        }
    }
}
