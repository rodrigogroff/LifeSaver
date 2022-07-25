
namespace Master.Entity.Domain.Config.Folder
{
    public class DtoConfigFolderGet
    {
        public long id { get; set; }

        public long? fkFolder { get; set; }

        public string date { get; set; }

        public bool income { get; set; }

        public string name { get; set; }
    }
}
