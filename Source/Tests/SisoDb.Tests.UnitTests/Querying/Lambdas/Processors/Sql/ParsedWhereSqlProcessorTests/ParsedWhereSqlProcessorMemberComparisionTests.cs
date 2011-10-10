﻿using NUnit.Framework;
using SisoDb.Querying.Lambdas.Processors.Sql;

namespace SisoDb.Tests.UnitTests.Querying.Lambdas.Processors.Sql.ParsedWhereSqlProcessorTests
{
    [TestFixture]
    public class ParsedWhereSqlProcessorMemberComparisionTests : ParsedWhereSqlProcessorTestBase
    {
        [Test]
        public void Process_WhenMemberOfSameType_GeneratesCorrectSqlQuery()
        {
            var parsedLambda = CreateParsedLambda<MyItem>(i => i.Int1 == i.Int2);

            var processor = new ParsedWhereSqlProcessor(new MemberPathGeneratorFake());
            var query = processor.Process(parsedLambda);

            const string expectedSql = "si.[Int1] = si.[Int2]";
            Assert.AreEqual(expectedSql, query.Sql);
        }

        [Test]
        public void Process_WhenMemberOfSameType_ExtractsCorrectParameters()
        {
            var parsedLambda = CreateParsedLambda<MyItem>(i => i.Int1 == i.Int2);

            var processor = new ParsedWhereSqlProcessor(new MemberPathGeneratorFake());
            var query = processor.Process(parsedLambda);

            Assert.AreEqual(0, query.Parameters.Count);
        }
    }
}