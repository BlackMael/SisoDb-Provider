﻿using System;
using System.Data.SqlServerCe;
using System.IO;
using System.Web;
using NCore;
using SisoDb.Resources;

namespace SisoDb.SqlCe4
{
    [Serializable]
    public class SqlCe4ConnectionInfo : SisoConnectionInfo
    {
        private string _serverPath, _filePath, _dbName;
        private IConnectionString _serverConnectionString;

        public string ServerPath
        {
            get { return _serverPath; }
        }

        public string FilePath
        {
            get { return _filePath; }
        }

        public override string DbName
        {
            get { return _dbName; }
        }

        public override IConnectionString ServerConnectionString
        {
            get { return _serverConnectionString; }
        }

        public SqlCe4ConnectionInfo(string connectionStringOrName)
            : this(GetConnectionString(connectionStringOrName))
        { }

        public SqlCe4ConnectionInfo(IConnectionString connectionString)
            : base(connectionString)
        {
            if (ProviderType != StorageProviders.SqlCe4)
                throw new SisoDbException(ExceptionMessages.ConnectionInfo_UnsupportedProviderSpecified.Inject(ProviderType, StorageProviders.SqlCe4));

            InitializeDbNameAndServerConnectionString();
        }

        private void InitializeDbNameAndServerConnectionString()
        {
            var cnStringBuilder = new SqlCeConnectionStringBuilder(ConnectionString.PlainString);

            _filePath = cnStringBuilder.DataSource;

            const string dataDirectorySwitch = "|DataDirectory|";
            if(_filePath.StartsWith(dataDirectorySwitch, StringComparison.InvariantCultureIgnoreCase))
            {
                _filePath = _filePath.Substring(dataDirectorySwitch.Length);
                if (HttpContext.Current != null)
                    _filePath = Path.Combine(HttpContext.Current.Server.MapPath("App_Data"), _filePath);
            }

            _dbName = FilePath.Contains(Path.DirectorySeparatorChar.ToString())
                ? Path.GetFileNameWithoutExtension(FilePath)
                : FilePath;

            if (string.IsNullOrWhiteSpace(_dbName))
                throw new SisoDbException(ExceptionMessages.ConnectionInfo_MissingName);

            _serverPath = Path.GetDirectoryName(_filePath);

            cnStringBuilder.DataSource = _serverPath;

            _serverConnectionString = ConnectionString.ReplacePlain(cnStringBuilder.ConnectionString);
        }
    }
}