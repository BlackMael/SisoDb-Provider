using System;
using NUnit.Framework;
using SisoDb.Querying;
using SisoDb.Querying.Lambdas.Converters.Sql;
using SisoDb.Querying.Lambdas.Parsers;
using SisoDb.Querying.Sql;
using SisoDb.Tests.UnitTests.TestFactories;

namespace SisoDb.Tests.UnitTests.Querying.Sql
{
    [TestFixture]
    public class SqlQueryGeneratorTests : UnitTestBase
    {
        [Test]
        public void Generate_WithWhere_GeneratesCorrectSql()
        {
            var queryCommand = GetQueryCommand<MyClass>(q => q.Where(i => i.Int1 == 42));
            var generator = GetQueryGenerator();

            var sqlQuery = generator.GenerateQuery(queryCommand);

            Assert.AreEqual(
                "select s.Json from [dbo].[MyClassStructure] as s "
                + "inner join [dbo].[MyClassIndexes] as si on si.[StructureId] = s.[StructureId] where (si.[MemberPath]='Int1' and si.[IntegerValue] = @p0);",
                sqlQuery.Sql);
        }

        [Test]
        public void Generate_WithSorting_GeneratesCorrectSql()
        {
            var queryCommand = GetQueryCommand<MyClass>(q => q.SortBy(i => i.Int1));
            var generator = GetQueryGenerator();

            var sqlQuery = generator.GenerateQuery(queryCommand);

            Assert.AreEqual(
                "select s.Json from [dbo].[MyClassStructure] as s " +
                "inner join [dbo].[MyClassIndexes] as si on si.[StructureId] = s.[StructureId] " + 
                "left join [dbo].[MyClassIndexes] as siSort1 on siSort1.[StructureId] = s.[StructureId] and siSort1.[MemberPath]='Int1' " +
                "order by siSort1.[IntegerValue] Asc;",
                sqlQuery.Sql);
        }

        [Test]
        public void Generate_WithWhereAndSorting_GeneratesCorrectSql()
        {
            var queryCommand = GetQueryCommand<MyClass>(q =>
            {
                q.Where(i => i.Int1 == 42);
                q.SortBy(i => i.Int1);
            });
            var generator = GetQueryGenerator();

            var sqlQuery = generator.GenerateQuery(queryCommand);

            Assert.AreEqual(
                "select s.Json from [dbo].[MyClassStructure] as s " + 
                "inner join [dbo].[MyClassIndexes] as si on si.[StructureId] = s.[StructureId] " + 
                "left join [dbo].[MyClassIndexes] as siSort1 on siSort1.[StructureId] = s.[StructureId] and siSort1.[MemberPath]='Int1' " +
                "where (si.[MemberPath]='Int1' and si.[IntegerValue] = @p0) " +
                "order by siSort1.[IntegerValue] Asc;",
                sqlQuery.Sql);
        }

        [Test]
        public void Generate_WhenTakeWithOutWhereAndSorting_GeneratesCorrectSql()
        {
            var queryCommand = GetQueryCommand<MyClass>(q => q.Take(11));
            var generator = GetQueryGenerator();

            var sqlQuery = generator.GenerateQuery(queryCommand);

            Assert.AreEqual(
                "select top(11) s.Json from [dbo].[MyClassStructure] as s " +
                "inner join [dbo].[MyClassIndexes] as si on si.[StructureId] = s.[StructureId];",
                sqlQuery.Sql);
        }

        [Test]
        public void Generate_WithTakeAndSorting_GeneratesCorrectSql()
        {
            var queryCommand = GetQueryCommand<MyClass>(q =>
            {
                q.SortBy(i => i.Int1);
                q.Take(11);
            });
            var generator = GetQueryGenerator();

            var sqlQuery = generator.GenerateQuery(queryCommand);

            Assert.AreEqual(
                "select top(11) s.Json from [dbo].[MyClassStructure] as s " +
                "inner join [dbo].[MyClassIndexes] as si on si.[StructureId] = s.[StructureId] " +
                "left join [dbo].[MyClassIndexes] as siSort1 on siSort1.[StructureId] = s.[StructureId] and siSort1.[MemberPath]='Int1' " +
                "order by siSort1.[IntegerValue] Asc;",
                sqlQuery.Sql);
        }

        [Test]
        public void Generate_WithTakeAndWhereAndSorting_GeneratesCorrectSql()
        {
            var queryCommand = GetQueryCommand<MyClass>(q =>
            {
                q.Where(i => i.Int1 == 42);
                q.SortBy(i => i.Int1);
                q.Take(11);
            });
            var generator = GetQueryGenerator();

            var sqlQuery = generator.GenerateQuery(queryCommand);

            Assert.AreEqual(
                "select top(11) s.Json from [dbo].[MyClassStructure] as s " +
                "inner join [dbo].[MyClassIndexes] as si on si.[StructureId] = s.[StructureId] " +
                "left join [dbo].[MyClassIndexes] as siSort1 on siSort1.[StructureId] = s.[StructureId] and siSort1.[MemberPath]='Int1' " +
                "where (si.[MemberPath]='Int1' and si.[IntegerValue] = @p0) " +
                "order by siSort1.[IntegerValue] Asc;",
                sqlQuery.Sql);
        }

        [Test]
        public void Generate_WithPagingAndWhereAndSorting_GeneratesCorrectSql()
        {
            var queryCommand = GetQueryCommand<MyClass>(q =>
            {
                q.Where(i => i.Int1 == 42);
                q.SortBy(i => i.Int1);
                q.Page(0, 10);
            });
            var generator = GetQueryGenerator();

            var sqlQuery = generator.GenerateQuery(queryCommand);

            Assert.AreEqual(
                "with pagedRs as (select s.Json,row_number() over ( order by siSort1.[IntegerValue] Asc) RowNum " +
                "from [dbo].[MyClassStructure] as s " +
                "inner join [dbo].[MyClassIndexes] as si on si.[StructureId] = s.[StructureId] " +
                "left join [dbo].[MyClassIndexes] as siSort1 on siSort1.[StructureId] = s.[StructureId] and siSort1.[MemberPath]='Int1' " +
                "where (si.[MemberPath]='Int1' and si.[IntegerValue] = @p0))select Json from pagedRs where RowNum between @pagingFrom and @pagingTo;",
                sqlQuery.Sql);

            //const string expectedSql =
            //    "with pagedRs as (select s.Json,row_number() over ( order by si.[Int1] Desc) RowNum "
            //    + "from [dbo].[MyClassStructure] as s inner join [dbo].[MyClassIndexes] as si on si.StructureId = s.StructureId "
            //    + "where si.[Int1] = 42)select Json from pagedRs where RowNum between @pagingFrom and @pagingTo;";

            Assert.AreEqual("@p0", sqlQuery.Parameters[0].Name);
            Assert.AreEqual(42, sqlQuery.Parameters[0].Value);
            Assert.AreEqual("@pagingFrom", sqlQuery.Parameters[1].Name);
            Assert.AreEqual(1, sqlQuery.Parameters[1].Value);
            Assert.AreEqual("@pagingTo", sqlQuery.Parameters[2].Name);
            Assert.AreEqual(10, sqlQuery.Parameters[2].Value);
        }

        private static IQueryCommand GetQueryCommand<T>(Action<IQueryCommandBuilder<T>> commandInitializer) where T : class
        {
            var schema = StructureSchemaTestFactory.Stub<T>();//new StructureSchemas(new StructureTypeFactory(), new AutoSchemaBuilder()).GetSchema<T>();
            var builder = new QueryCommandBuilder<T>(schema, new WhereParser(), new SortingParser(), new IncludeParser());
            
            commandInitializer(builder);

            return builder.Command;
        }

        private static SqlQueryGenerator GetQueryGenerator()
        {
            return new SqlQueryGenerator(
                new LambdaToSqlWhereConverter(), 
                new LambdaToSqlSortingConverter(), 
                new LambdaToSqlIncludeConverter());
        }

        private class MyClass
        {
            public int Int1 { get; set; }
        }
    }
}