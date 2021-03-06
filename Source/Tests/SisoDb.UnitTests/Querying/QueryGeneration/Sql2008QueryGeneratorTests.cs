using NUnit.Framework;
using SisoDb.Sql2008;
using SisoDb.Querying;

namespace SisoDb.UnitTests.Querying.QueryGeneration
{
    [TestFixture]
    public class Sql2008QueryGeneratorTests : QueryGeneratorTests
    {
        protected override IDbQueryGenerator GetQueryGenerator()
        {
            return new Sql2008QueryGenerator(new Sql2008Statements());
        }

        [Test]
        public override void GenerateQuery_WithWhere_GeneratesCorrectQuery()
        {
            var sqlQuery = On_GenerateQuery_WithWhere_GeneratesCorrectQuery();

            Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId] from [MyClassStructure] s inner join [MyClassIntegers] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'Int1' where (mem0.[Value] = @p0) group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId];",
                sqlQuery.Sql);

            Assert.AreEqual("@p0", sqlQuery.Parameters[0].Name);
            Assert.AreEqual(42, sqlQuery.Parameters[0].Value);
        }

		[Test]
		public override void GenerateQuery_WithWhereHavingImplicitBool_GeneratesCorrectQuery()
    	{
			var sqlQuery = On_GenerateQuery_WithWhereHavingImplicitBool_GeneratesCorrectQuery();

			Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId] from [MyClassStructure] s inner join [MyClassBooleans] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'Bool1' where (mem0.[Value] = @p0) group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId];",
				sqlQuery.Sql);

			Assert.AreEqual("@p0", sqlQuery.Parameters[0].Name);
			Assert.AreEqual(true, sqlQuery.Parameters[0].Value);
    	}

		[Test]
		public override void GenerateQuery_WithWhereHavingExplicitBool_GeneratesCorrectQuery()
		{
			var sqlQuery = On_GenerateQuery_WithWhereHavingExplicitBool_GeneratesCorrectQuery();

			Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId] from [MyClassStructure] s inner join [MyClassBooleans] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'Bool1' where (mem0.[Value] = @p0) group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId];",
				sqlQuery.Sql);

			Assert.AreEqual("@p0", sqlQuery.Parameters[0].Name);
			Assert.AreEqual(true, sqlQuery.Parameters[0].Value);
		}

		[Test]
    	public override void GenerateQuery_WithWhereUsingNullableIntIsNull_GeneratesCorrectQuery()
    	{
			var sqlQuery = On_GenerateQuery_WithWhereUsingNullableIntIsNull_GeneratesCorrectQuery();

			Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId] from [MyClassStructure] s inner join [MyClassIntegers] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'NullableInt1' where (mem0.[Value] is null) group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId];",
				sqlQuery.Sql);

			Assert.AreEqual(0, sqlQuery.Parameters.Count);
    	}

		[Test]
    	public override void GenerateQuery_WithWhereUsingNullableIntIsNotNull_GeneratesCorrectQuery()
    	{
			var sqlQuery = On_GenerateQuery_WithWhereUsingNullableIntIsNotNull_GeneratesCorrectQuery();

			Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId] from [MyClassStructure] s inner join [MyClassIntegers] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'NullableInt1' where (mem0.[Value] is not null) group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId];",
				sqlQuery.Sql);

			Assert.AreEqual(0, sqlQuery.Parameters.Count);
    	}

    	[Test]
    	public override void GenerateQuery_WithWhereUsingNullableIntHasValue_GeneratesCorrectQuery()
    	{
			var sqlQuery = On_GenerateQuery_WithWhereUsingNullableIntHasValue_GeneratesCorrectQuery();

			Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId] from [MyClassStructure] s inner join [MyClassIntegers] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'NullableInt1' where (mem0.[Value] is not null) group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId];",
				sqlQuery.Sql);

			Assert.AreEqual(0, sqlQuery.Parameters.Count);
    	}

		[Test]
    	public override void GenerateQuery_WithWhereUsingNullableIntHasValueFalse_GeneratesCorrectQuery()
    	{
			var sqlQuery = On_GenerateQuery_WithWhereUsingNullableIntHasValueFalse_GeneratesCorrectQuery();

			Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId] from [MyClassStructure] s inner join [MyClassIntegers] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'NullableInt1' where (mem0.[Value] is null) group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId];",
				sqlQuery.Sql);

			Assert.AreEqual(0, sqlQuery.Parameters.Count);
    	}

    	[Test]
    	public override void GenerateQuery_WithWhereUsingNegationOfNullableIntHasValue_GeneratesCorrectQuery()
    	{
			var sqlQuery = On_GenerateQuery_WithWhereUsingNegationOfNullableIntHasValue_GeneratesCorrectQuery();

			Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId] from [MyClassStructure] s inner join [MyClassIntegers] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'NullableInt1' where not (mem0.[Value] is not null) group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId];",
				sqlQuery.Sql);

			Assert.AreEqual(0, sqlQuery.Parameters.Count);
    	}

    	[Test]
    	public override void GenerateQuery_WithWhereContainingNullableIntComparedAgainstValue_GeneratesCorrectQuery()
    	{
			var sqlQuery = On_GenerateQuery_WithWhereContainingNullableIntComparedAgainstValue_GeneratesCorrectQuery();

			Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId] from [MyClassStructure] s inner join [MyClassIntegers] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'NullableInt1' where (mem0.[Value] = @p0) group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId];",
				sqlQuery.Sql);

			Assert.AreEqual("@p0", sqlQuery.Parameters[0].Name);
			Assert.AreEqual(42, sqlQuery.Parameters[0].Value);
    	}

		[Test]
    	public override void GenerateQuery_WithWhereContainingNullableIntValueComparedAgainstValue_GeneratesCorrectQuery()
    	{
			var sqlQuery = On_GenerateQuery_WithWhereContainingNullableIntValueComparedAgainstValue_GeneratesCorrectQuery();

			Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId] from [MyClassStructure] s inner join [MyClassIntegers] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'NullableInt1' where (mem0.[Value] = @p0) group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId];",
				sqlQuery.Sql);

			Assert.AreEqual("@p0", sqlQuery.Parameters[0].Name);
			Assert.AreEqual(42, sqlQuery.Parameters[0].Value);
    	}

    	[Test]
		public override void GenerateQuery_WithChainedWheres_GeneratesCorrectQuery()
		{
			var sqlQuery = On_GenerateQuery_WithChainedWheres_GeneratesCorrectQuery();

			Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId] from [MyClassStructure] s inner join [MyClassIntegers] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'Int1' where ((mem0.[Value] >= @p0) and (mem0.[Value] <= @p1)) group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId];",
				sqlQuery.Sql);

			Assert.AreEqual("@p0", sqlQuery.Parameters[0].Name);
			Assert.AreEqual(40, sqlQuery.Parameters[0].Value);

			Assert.AreEqual("@p1", sqlQuery.Parameters[1].Name);
			Assert.AreEqual(42, sqlQuery.Parameters[1].Value);
		}

        [Test]
        public override void GenerateQuery_WithSorting_GeneratesCorrectQuery()
        {
            var sqlQuery = On_GenerateQuery_WithSorting_GeneratesCorrectQuery();

            Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId], min(mem0.[Value]) mem0 from [MyClassStructure] s inner join [MyClassIntegers] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'Int1' group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId] order by mem0 Asc;",
                sqlQuery.Sql);
        }

        [Test]
        public override void GenerateQuery_WithWhereAndSorting_GeneratesCorrectQuery()
        {
            var sqlQuery = On_GenerateQuery_WithWhereAndSorting_GeneratesCorrectQuery();

            Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId], min(mem0.[Value]) mem0 from [MyClassStructure] s inner join [MyClassIntegers] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'Int1' where (mem0.[Value] = @p0) group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId] order by mem0 Asc;",
                sqlQuery.Sql);

            Assert.AreEqual("@p0", sqlQuery.Parameters[0].Name);
            Assert.AreEqual(42, sqlQuery.Parameters[0].Value);
        }

        [Test]
        public override void GenerateQuery_WithTake_GeneratesCorrectQuery()
        {
            var sqlQuery = On_GenerateQuery_WithTake_GeneratesCorrectQuery();

            Assert.AreEqual(
                "select top (11) s.[Json] from (select s.[StructureId] from [MyClassStructure] s group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId];",
                sqlQuery.Sql);
        }

        [Test]
        public override void GenerateQuery_WithTakeAndSorting_GeneratesCorrectQuery()
        {
            var sqlQuery = On_GenerateQuery_WithTakeAndSorting_GeneratesCorrectQuery();

            Assert.AreEqual(
				"select top (11) s.[Json] from (select s.[StructureId], min(mem0.[Value]) mem0 from [MyClassStructure] s inner join [MyClassIntegers] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'Int1' group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId] order by mem0 Asc;",
                sqlQuery.Sql);
        }

        [Test]
        public override void GenerateQuery_WithTakeAndWhereAndSorting_GeneratesCorrectQuery()
        {
            var sqlQuery = On_GenerateQuery_WithTakeAndWhereAndSorting_GeneratesCorrectQuery();

            Assert.AreEqual(
				"select top (11) s.[Json] from (select s.[StructureId], min(mem0.[Value]) mem0 from [MyClassStructure] s inner join [MyClassIntegers] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'Int1' where (mem0.[Value] = @p0) group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId] order by mem0 Asc;",
                sqlQuery.Sql);

            Assert.AreEqual("@p0", sqlQuery.Parameters[0].Name);
            Assert.AreEqual(42, sqlQuery.Parameters[0].Value);
        }

        [Test]
        public override void GenerateQuery_WithPagingAndWhereAndSorting_GeneratesCorrectQuery()
        {
            var sqlQuery = On_GenerateQuery_WithPagingAndWhereAndSorting_GeneratesCorrectQuery();

            Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId] , row_number() over (order by min(mem0.[Value]) Asc) RowNum from [MyClassStructure] s inner join [MyClassIntegers] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'Int1' where (mem0.[Value] = @p0) group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId] where rs.RowNum between @pagingFrom and @pagingTo;",
                sqlQuery.Sql);

            Assert.AreEqual("@p0", sqlQuery.Parameters[0].Name);
            Assert.AreEqual(42, sqlQuery.Parameters[0].Value);

            Assert.AreEqual("@pagingFrom", sqlQuery.Parameters[1].Name);
            Assert.AreEqual(1, sqlQuery.Parameters[1].Value);
            
            Assert.AreEqual("@pagingTo", sqlQuery.Parameters[2].Name);
            Assert.AreEqual(10, sqlQuery.Parameters[2].Value);
        }

        [Test]
        public override void GenerateQuery_WithExplicitSortingOnTwoDifferentMemberTypesAndSorting_GeneratesCorrectQuery()
        {
            var sqlQuery = On_GenerateQuery_WithExplicitSortingOnTwoDifferentMemberTypesAndSorting_GeneratesCorrectQuery();

            Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId], min(mem0.[Value]) mem0, min(mem1.[Value]) mem1 from [MyClassStructure] s inner join [MyClassIntegers] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'Int1' inner join [MyClassStrings] mem1 on mem1.[StructureId] = s.[StructureId] and mem1.[MemberPath] = 'String1' group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId] order by mem0 Asc, mem1 Desc;",
                sqlQuery.Sql);
        }

        [Test]
        public override void GenerateQuery_WithSortingOnTwoDifferentMemberOfSameType_GeneratesCorrectQuery()
        {
            var sqlQuery = On_GenerateQuery_WithSortingOnTwoDifferentMemberOfSameType_GeneratesCorrectQuery();

            Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId], min(mem0.[Value]) mem0, min(mem1.[Value]) mem1 from [MyClassStructure] s inner join [MyClassIntegers] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'Int1' inner join [MyClassIntegers] mem1 on mem1.[StructureId] = s.[StructureId] and mem1.[MemberPath] = 'Int2' group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId] order by mem0 Asc, mem1 Asc;",
                sqlQuery.Sql);
        }

		[Test]
    	public override void GenerateQuery_WithEnum_GeneratesCorrectQuery()
    	{
			var sqlQuery = On_GenerateQuery_WithEnum_GeneratesCorrectQuery();

			Assert.AreEqual(
				"select s.[Json] from (select s.[StructureId] from [MyClassStructure] s inner join [MyClassStrings] mem0 on mem0.[StructureId] = s.[StructureId] and mem0.[MemberPath] = 'MyEnum1' where ((mem0.[Value] = @p0) or (mem0.[Value] = @p1)) group by s.[StructureId]) rs inner join [MyClassStructure] s on s.[StructureId] = rs.[StructureId];",
				sqlQuery.Sql);

			Assert.AreEqual("@p0", sqlQuery.Parameters[0].Name);
			Assert.AreEqual("Value1", sqlQuery.Parameters[0].Value);
			Assert.AreEqual("@p1", sqlQuery.Parameters[1].Name);
			Assert.AreEqual("Value2", sqlQuery.Parameters[1].Value);
    	}
    }
}