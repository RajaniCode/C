namespace WebBackend.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebBackend.Models.FeatureContext>
    {
        private static string iconPath = AppDomain.CurrentDomain.BaseDirectory + @"..\WebAssets\Icons\";
        private static string imagePath = AppDomain.CurrentDomain.BaseDirectory + @"..\WebAssets\Details\";
        private static string featureList = AppDomain.CurrentDomain.BaseDirectory + @"..\WebAssets\FeatureList.xml";

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebBackend.Models.FeatureContext context)
        {
            using (XmlReader reader = XmlReader.Create(featureList))
            {
                reader.MoveToContent();
                XDocument doc = (XDocument)XDocument.Load(reader);

                foreach (XElement element in doc.Root.Elements("Feature"))
                {
                    WebBackend.Models.Feature feature = new WebBackend.Models.Feature
                    {
                        Name = element.Element("Name").Value,
                        Icon = ReadFile(iconPath, element.Element("Icon").Value),
                        Version = element.Element("Version").Value,
                        Rating = int.Parse(element.Element("Rating").Value),
                        Description = element.Element("Description").Value,
                        Image = ReadFile(imagePath, element.Element("Image").Value),
                    };

                    context.Features.AddOrUpdate(f => f.Name, feature);
                }
            }
        }

        private byte[] ReadFile(string path, string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                return null;
            }

            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Open))
            {
                byte[] contents = new byte[stream.Length];
                stream.Read(contents, 0, (int)stream.Length);
                return contents;
            }
        }
    }
}
