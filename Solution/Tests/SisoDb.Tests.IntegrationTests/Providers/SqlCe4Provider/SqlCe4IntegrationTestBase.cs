﻿using NUnit.Framework;
using SisoDb.Core;
using SisoDb.Providers.Sql2008Provider;

namespace SisoDb.Tests.IntegrationTests.Providers.SqlCe4Provider
{
    [TestFixture]
    public abstract class SqlCe4IntegrationTestBase : IntegrationTestBase
    {
        protected SqlCe4IntegrationTestBase()
            : base(LocalConstants.ConnectionStringNameForSqlCe4)
        {
        }

        protected string GetStructureTableName<T>() where T : class
        {
            return Database.StructureSchemas.GetSchema(TypeFor<T>.Type).GetStructureTableName();
        }

        protected string GetIndexesTableName<T>() where T : class
        {
            return Database.StructureSchemas.GetSchema(TypeFor<T>.Type).GetIndexesTableName();
        }

        protected string GetUniquesTableName<T>() where T : class
        {
            return Database.StructureSchemas.GetSchema(TypeFor<T>.Type).GetUniquesTableName();
        }
    }
}