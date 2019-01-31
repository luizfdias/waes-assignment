namespace Waes.Diff.Infrastructure
{
    public class StorageSettings
    {
        private string _connectionString = string.Empty;

        public string Database { get; set; }               

        public string ConnectionString
        {
            get
            {
                if (IsContained && Development)
                {
                    return Container;
                }

                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        public string Container { get; set; }

        public bool IsContained { get; set; }

        public bool Development { get; set; }

    }
}
