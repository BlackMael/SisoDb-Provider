﻿using NUnit.Framework;
using SisoDb.Dac;
using SisoDb.Querying;
using SisoDb.Querying.Lambdas.Processors.Sql;

namespace SisoDb.Tests.UnitTests.Querying.Lambdas.Processors.Sql.ParsedWhereSqlProcessorTests
{
    [TestFixture]
    public class ParsedWhereSqlProcessorHierarchyTests : ParsedWhereSqlProcessorTestBase
    {
        [Test]
        public void Process_WhenFirstLevel_GeneratesCorrectSqlQuery()
        {
            var parsedLambda = CreateParsedLambda<MyItem>(i => i.Int1 == 42);

            var processor = new ParsedWhereSqlProcessor(new MemberPathGeneratorFake());
            var query = processor.Process(parsedLambda);

            const string expectedSql = "si.[Int1] = @p0";
            Assert.AreEqual(expectedSql, query.Sql);
        }

        [Test]
        public void Process_WhenFirstLevel_ExtractsCorrectParameters()
        {
            var parsedLambda = CreateParsedLambda<MyItem>(i => i.Int1 == 42);

            var processor = new ParsedWhereSqlProcessor(new MemberPathGeneratorFake());
            var query = processor.Process(parsedLambda);

            var expectedParameters = new[] {new DacParameter("@p0", 42)};
            AssertQueryParameters(expectedParameters, query.Parameters);
        }

        [Test]
        public void Process_WhenNested_GeneratesCorrectSqlQuery()
        {
            var parsedLambda = CreateParsedLambda<MyItem>(i => i.NestedItem.Int1 == 42);

            var processor = new ParsedWhereSqlProcessor(new MemberPathGeneratorFake());
            var query = processor.Process(parsedLambda);

            const string expectedSql = "si.[NestedItem.Int1] = @p0";
            Assert.AreEqual(expectedSql, query.Sql);
        }

        [Test]
        public void Process_WhenNested_ExtractsCorrectParameters()
        {
            var parsedLambda = CreateParsedLambda<MyItem>(i => i.NestedItem.Int1 == 42);

            var processor = new ParsedWhereSqlProcessor(new MemberPathGeneratorFake());
            var query = processor.Process(parsedLambda);

            var expectedParameters = new[] { new DacParameter("@p0", 42) };
            AssertQueryParameters(expectedParameters, query.Parameters);
        }

        [Test]
        public void Process_WhenDeepNested_GeneratesCorrectSqlQuery()
        {
            var parsedLambda = CreateParsedLambda<MyItem>(i => i.NestedItem.SuperNestedItem.Int1 == 42);

            var processor = new ParsedWhereSqlProcessor(new MemberPathGeneratorFake());
            var query = processor.Process(parsedLambda);

            const string expectedSql = "si.[NestedItem.SuperNestedItem.Int1] = @p0";
            Assert.AreEqual(expectedSql, query.Sql);
        }

        [Test]
        public void Process_WhenDeepNested_ExtractsCorrectParameters()
        {
            var parsedLambda = CreateParsedLambda<MyItem>(i => i.NestedItem.SuperNestedItem.Int1 == 42);

            var processor = new ParsedWhereSqlProcessor(new MemberPathGeneratorFake());
            var query = processor.Process(parsedLambda);

            var expectedParameters = new[] { new DacParameter("@p0", 42) };
            AssertQueryParameters(expectedParameters, query.Parameters);
        }

        [Test]
        public void Process_WhenNestedMemberComparedToFieldValue_GeneratesCorrectSqlQuery()
        {
            var item = new MyItem {NestedItem = new MyNestedItem {Int1 = 42}};
            var parsedLambda = CreateParsedLambda<MyItem>(i => i.NestedItem.Int1 == item.NestedItem.Int1);

            var processor = new ParsedWhereSqlProcessor(new MemberPathGeneratorFake());
            var query = processor.Process(parsedLambda);

            const string expectedSql = "si.[NestedItem.Int1] = @p0";
            Assert.AreEqual(expectedSql, query.Sql);
        }

        [Test]
        public void Process_WhenNestedMemberComparedToFieldValue_ExtractsCorrectParameters()
        {
            var item = new MyItem { NestedItem = new MyNestedItem { Int1 = 42 } };
            var parsedLambda = CreateParsedLambda<MyItem>(i => i.NestedItem.Int1 == item.NestedItem.Int1);

            var processor = new ParsedWhereSqlProcessor(new MemberPathGeneratorFake());
            var query = processor.Process(parsedLambda);

            var expectedParameters = new[] { new DacParameter("@p0", 42) };
            AssertQueryParameters(expectedParameters, query.Parameters);
        }
    }
}