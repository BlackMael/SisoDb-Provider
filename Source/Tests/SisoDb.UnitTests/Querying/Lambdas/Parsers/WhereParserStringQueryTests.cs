using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using SisoDb.Querying.Lambdas.Nodes;
using SisoDb.Querying.Lambdas.Parsers;

namespace SisoDb.UnitTests.Querying.Lambdas.Parsers
{
    [TestFixture]
    public class WhereParserStringQueryTests : UnitTestBase
    {
        [Test]
        public void Parse_WhenToLower_ReturnsCorrectNodes()
        {
            var expression = Reflect<MyClass>.LambdaFrom(m => m.String1.ToLower() == "foo");

            var parser = new WhereParser();
            var parsedLambda = parser.Parse(expression);

            var listOfNodes = parsedLambda.Nodes.ToList();
            var memberNode = (ToLowerMemberNode)listOfNodes[0];
            var operatorNode = (OperatorNode)listOfNodes[1];
            var operandNode = (ValueNode)listOfNodes[2];
            Assert.AreEqual("String1", memberNode.Path);
            Assert.AreEqual("=", operatorNode.Operator.ToString());
            Assert.AreEqual("foo", operandNode.Value);
        }

        [Test]
        public void Parse_WhenToUpper_ReturnsCorrectNodes()
        {
            var expression = Reflect<MyClass>.LambdaFrom(m => m.String1.ToUpper() == "foo");

            var parser = new WhereParser();
            var parsedLambda = parser.Parse(expression);

            var listOfNodes = parsedLambda.Nodes.ToList();
            var memberNode = (ToUpperMemberNode)listOfNodes[0];
            var operatorNode = (OperatorNode)listOfNodes[1];
            var operandNode = (ValueNode)listOfNodes[2];
            Assert.AreEqual("String1", memberNode.Path);
            Assert.AreEqual("=", operatorNode.Operator.ToString());
            Assert.AreEqual("foo", operandNode.Value);
        }

		private class StartsWithQueryObject
		{
			public string Value { get; set; }

			public LambdaExpression CreateExpression()
			{
				return Reflect<MyClass>.LambdaFrom(m => m.String1.StartsWith(Value));	
			}
		}

		[Test]
		public void Parse_WhenStartsWith_AgainstValueOfProperty_ReturnsCorrectNodes()
		{
			var q = new StartsWithQueryObject{Value = "Foo"};
			var expression = q.CreateExpression();

			var parser = new WhereParser();
			var parsedLambda = parser.Parse(expression);

			var listOfNodes = parsedLambda.Nodes.ToList();
			var memberNode = (MemberNode)listOfNodes[0];
			var operatorNode = (OperatorNode)listOfNodes[1];
			var operandNode = (ValueNode)listOfNodes[2];
			Assert.AreEqual("String1", memberNode.Path);
			Assert.AreEqual("like", operatorNode.Operator.ToString());
			Assert.AreEqual("Foo%", operandNode.Value);
		}

        [Test]
        public void Parse_WhenStartsWith_ReturnsCorrectNodes()
        {
            var expression = Reflect<MyClass>.LambdaFrom(m => m.String1.StartsWith("Foo"));

            var parser = new WhereParser();
            var parsedLambda = parser.Parse(expression);

            var listOfNodes = parsedLambda.Nodes.ToList();
            var memberNode = (MemberNode)listOfNodes[0];
            var operatorNode = (OperatorNode)listOfNodes[1];
            var operandNode = (ValueNode)listOfNodes[2];
            Assert.AreEqual("String1", memberNode.Path);
            Assert.AreEqual("like", operatorNode.Operator.ToString());
            Assert.AreEqual("Foo%", operandNode.Value);
        }

        [Test]
        public void Parse_WhenEndsWith_ReturnsCorrectNodes()
        {
            var expression = Reflect<MyClass>.LambdaFrom(m => m.String1.EndsWith("bar"));

            var parser = new WhereParser();
            var parsedLambda = parser.Parse(expression);

            var listOfNodes = parsedLambda.Nodes.ToList();
            var memberNode = (MemberNode)listOfNodes[0];
            var operatorNode = (OperatorNode)listOfNodes[1];
            var operandNode = (ValueNode)listOfNodes[2];
            Assert.AreEqual("String1", memberNode.Path);
            Assert.AreEqual("like", operatorNode.Operator.ToString());
            Assert.AreEqual("%bar", operandNode.Value);
        }

        [Test]
        public void Parse_WhenQxToLower_ReturnsCorrectNodes()
        {
            var expression = Reflect<MyClass>.LambdaFrom(m => m.String1.QxToLower() == "foo");

            var parser = new WhereParser();
            var parsedLambda = parser.Parse(expression);

            var listOfNodes = parsedLambda.Nodes.ToList();
            var memberNode = (ToLowerMemberNode)listOfNodes[0];
            var operatorNode = (OperatorNode)listOfNodes[1];
            var operandNode = (ValueNode)listOfNodes[2];
            Assert.AreEqual("String1", memberNode.Path);
            Assert.AreEqual("=", operatorNode.Operator.ToString());
            Assert.AreEqual("foo", operandNode.Value);
        }

        [Test]
        public void Parse_WhenQxToUpper_ReturnsCorrectNodes()
        {
            var expression = Reflect<MyClass>.LambdaFrom(m => m.String1.QxToUpper() == "foo");

            var parser = new WhereParser();
            var parsedLambda = parser.Parse(expression);

            var listOfNodes = parsedLambda.Nodes.ToList();
            var memberNode = (ToUpperMemberNode)listOfNodes[0];
            var operatorNode = (OperatorNode)listOfNodes[1];
            var operandNode = (ValueNode)listOfNodes[2];
            Assert.AreEqual("String1", memberNode.Path);
            Assert.AreEqual("=", operatorNode.Operator.ToString());
            Assert.AreEqual("foo", operandNode.Value);
        }

        [Test]
        public void Parse_WhenQxStartsWith_ReturnsCorrectNodes()
        {
            var expression = Reflect<MyClass>.LambdaFrom(m => m.String1.QxStartsWith("Foo"));

            var parser = new WhereParser();
            var parsedLambda = parser.Parse(expression);

            var listOfNodes = parsedLambda.Nodes.ToList();
            var memberNode = (MemberNode)listOfNodes[0];
            var operatorNode = (OperatorNode)listOfNodes[1];
            var operandNode = (ValueNode)listOfNodes[2];
            Assert.AreEqual("String1", memberNode.Path);
            Assert.AreEqual("like", operatorNode.Operator.ToString());
            Assert.AreEqual("Foo%", operandNode.Value);
        }

        [Test]
        public void Parse_WhenQxEndsWith_ReturnsCorrectNodes()
        {
            var expression = Reflect<MyClass>.LambdaFrom(m => m.String1.QxEndsWith("bar"));

            var parser = new WhereParser();
            var parsedLambda = parser.Parse(expression);

            var listOfNodes = parsedLambda.Nodes.ToList();
            var memberNode = (MemberNode)listOfNodes[0];
            var operatorNode = (OperatorNode)listOfNodes[1];
            var operandNode = (ValueNode)listOfNodes[2];
            Assert.AreEqual("String1", memberNode.Path);
            Assert.AreEqual("like", operatorNode.Operator.ToString());
            Assert.AreEqual("%bar", operandNode.Value);
        }

        [Test]
        public void Parse_WhenQxLike_ReturnsCorrectNodes()
        {
            var expression = Reflect<MyClass>.LambdaFrom(m => m.String1.QxLike("Foo%bar"));

            var parser = new WhereParser();
            var parsedLambda = parser.Parse(expression);

            var listOfNodes = parsedLambda.Nodes.ToList();
            var memberNode = (MemberNode)listOfNodes[0];
            var operatorNode = (OperatorNode)listOfNodes[1];
            var operandNode = (ValueNode)listOfNodes[2];
            Assert.AreEqual("String1", memberNode.Path);
            Assert.AreEqual("like", operatorNode.Operator.ToString());
            Assert.AreEqual("Foo%bar", operandNode.Value);
        }

        [Test]
        public void Parse_WhenQxContains_ReturnsCorrectNodes()
        {
            var expression = Reflect<MyClass>.LambdaFrom(m => m.String1.QxContains("Foo"));

            var parser = new WhereParser();
            var parsedLambda = parser.Parse(expression);

            var listOfNodes = parsedLambda.Nodes.ToList();
            var memberNode = (MemberNode)listOfNodes[0];
            var operatorNode = (OperatorNode)listOfNodes[1];
            var operandNode = (ValueNode)listOfNodes[2];
            Assert.AreEqual("String1", memberNode.Path);
            Assert.AreEqual("like", operatorNode.Operator.ToString());
            Assert.AreEqual("%Foo%", operandNode.Value);
        }

		[Test]
		public void Parse_WhenStartsWithOnToStringOfNullable_ReturnsCorrectNodes()
		{
			var expression = Reflect<MyClass>.LambdaFrom(m => m.NullableInt.HasValue && m.NullableInt.ToString().StartsWith("42"));

			var parser = new WhereParser();
			var parsedLambda = parser.Parse(expression);

			var listOfNodes = parsedLambda.Nodes.ToList();
			Assert.AreEqual(7, listOfNodes.Count);

			var memberNode1 = (NullableMemberNode)listOfNodes[0];
			var isNotNode1 = (OperatorNode)listOfNodes[1];
			var nullNode1 = (NullNode)listOfNodes[2];
			var andNode = (OperatorNode)listOfNodes[3];
			var memberNode2 = (StartsWithMemberNode)listOfNodes[4];
			var likeNode = (OperatorNode)listOfNodes[5];
			var valueNode = (ValueNode)listOfNodes[6];

			Assert.AreEqual("NullableInt", memberNode1.Path);
			Assert.AreEqual(typeof(int), memberNode1.MemberType);
			Assert.AreEqual("is not", isNotNode1.Operator.ToString());
			Assert.AreEqual("null", nullNode1.ToString());
			Assert.AreEqual("and", andNode.ToString());
			Assert.AreEqual("NullableInt", memberNode2.Path);
			Assert.AreEqual(typeof(int), memberNode2.MemberType);
			Assert.AreEqual("like", likeNode.Operator.ToString());
			Assert.AreEqual("42%", valueNode.Value);
		}

		[Test]
		public void Parse_WhenQxStartsWithOnToStringOfNullable_ReturnsCorrectNodes()
		{
			var expression = Reflect<MyClass>.LambdaFrom(m => m.NullableInt.HasValue && m.NullableInt.ToString().QxStartsWith("42"));

			var parser = new WhereParser();
			var parsedLambda = parser.Parse(expression);

			var listOfNodes = parsedLambda.Nodes.ToList();
			Assert.AreEqual(7, listOfNodes.Count);

			var memberNode1 = (NullableMemberNode)listOfNodes[0];
			var isNotNode1 = (OperatorNode)listOfNodes[1];
			var nullNode1 = (NullNode)listOfNodes[2];
			var andNode = (OperatorNode)listOfNodes[3];
			var memberNode2 = (StartsWithMemberNode)listOfNodes[4];
			var likeNode = (OperatorNode)listOfNodes[5];
			var valueNode = (ValueNode)listOfNodes[6];

			Assert.AreEqual("NullableInt", memberNode1.Path);
			Assert.AreEqual(typeof(int), memberNode1.MemberType);
			Assert.AreEqual("is not", isNotNode1.Operator.ToString());
			Assert.AreEqual("null", nullNode1.ToString());
			Assert.AreEqual("and", andNode.ToString());
			Assert.AreEqual("NullableInt", memberNode2.Path);
			Assert.AreEqual(typeof(int), memberNode2.MemberType);
			Assert.AreEqual("like", likeNode.Operator.ToString());
			Assert.AreEqual("42%", valueNode.Value);
		}

		[Test]
		public void Parse_WhenStartsWithOnToStringOfNullableValue_ReturnsCorrectNodes()
		{
			var expression = Reflect<MyClass>.LambdaFrom(m => m.NullableInt.HasValue && m.NullableInt.Value.ToString().StartsWith("42"));

			var parser = new WhereParser();
			var parsedLambda = parser.Parse(expression);

			var listOfNodes = parsedLambda.Nodes.ToList();
			Assert.AreEqual(7, listOfNodes.Count);

			var memberNode1 = (NullableMemberNode)listOfNodes[0];
			var isNotNode1 = (OperatorNode)listOfNodes[1];
			var nullNode1 = (NullNode)listOfNodes[2];
			var andNode = (OperatorNode)listOfNodes[3];
			var memberNode2 = (StartsWithMemberNode)listOfNodes[4];
			var likeNode = (OperatorNode)listOfNodes[5];
			var valueNode = (ValueNode)listOfNodes[6];

			Assert.AreEqual("NullableInt", memberNode1.Path);
			Assert.AreEqual(typeof(int), memberNode1.MemberType);
			Assert.AreEqual("is not", isNotNode1.Operator.ToString());
			Assert.AreEqual("null", nullNode1.ToString());
			Assert.AreEqual("and", andNode.ToString());
			Assert.AreEqual("NullableInt", memberNode2.Path);
			Assert.AreEqual(typeof(int), memberNode2.MemberType);
			Assert.AreEqual("like", likeNode.Operator.ToString());
			Assert.AreEqual("42%", valueNode.Value);
		}

		[Test]
		public void Parse_WhenQxStartsWithOnToStringOfNullableValue_ReturnsCorrectNodes()
		{
			var expression = Reflect<MyClass>.LambdaFrom(m => m.NullableInt.HasValue && m.NullableInt.Value.ToString().QxStartsWith("42"));

			var parser = new WhereParser();
			var parsedLambda = parser.Parse(expression);

			var listOfNodes = parsedLambda.Nodes.ToList();
			Assert.AreEqual(7, listOfNodes.Count);

			var memberNode1 = (NullableMemberNode)listOfNodes[0];
			var isNotNode1 = (OperatorNode)listOfNodes[1];
			var nullNode1 = (NullNode)listOfNodes[2];
			var andNode = (OperatorNode)listOfNodes[3];
			var memberNode2 = (StartsWithMemberNode)listOfNodes[4];
			var likeNode = (OperatorNode)listOfNodes[5];
			var valueNode = (ValueNode)listOfNodes[6];

			Assert.AreEqual("NullableInt", memberNode1.Path);
			Assert.AreEqual(typeof(int), memberNode1.MemberType);
			Assert.AreEqual("is not", isNotNode1.Operator.ToString());
			Assert.AreEqual("null", nullNode1.ToString());
			Assert.AreEqual("and", andNode.ToString());
			Assert.AreEqual("NullableInt", memberNode2.Path);
			Assert.AreEqual(typeof(int), memberNode2.MemberType);
			Assert.AreEqual("like", likeNode.Operator.ToString());
			Assert.AreEqual("42%", valueNode.Value);
		}

		[Test]
		public void Parse_WhenEndsWithOnToStringOfNullable_ReturnsCorrectNodes()
		{
			var expression = Reflect<MyClass>.LambdaFrom(m => m.NullableInt.HasValue && m.NullableInt.ToString().EndsWith("42"));

			var parser = new WhereParser();
			var parsedLambda = parser.Parse(expression);

			var listOfNodes = parsedLambda.Nodes.ToList();
			Assert.AreEqual(7, listOfNodes.Count);

			var memberNode1 = (NullableMemberNode)listOfNodes[0];
			var isNotNode1 = (OperatorNode)listOfNodes[1];
			var nullNode1 = (NullNode)listOfNodes[2];
			var andNode = (OperatorNode)listOfNodes[3];
			var memberNode2 = (EndsWithMemberNode)listOfNodes[4];
			var likeNode = (OperatorNode)listOfNodes[5];
			var valueNode = (ValueNode)listOfNodes[6];

			Assert.AreEqual("NullableInt", memberNode1.Path);
			Assert.AreEqual(typeof(int), memberNode1.MemberType);
			Assert.AreEqual("is not", isNotNode1.Operator.ToString());
			Assert.AreEqual("null", nullNode1.ToString());
			Assert.AreEqual("and", andNode.ToString());
			Assert.AreEqual("NullableInt", memberNode2.Path);
			Assert.AreEqual(typeof(int), memberNode2.MemberType);
			Assert.AreEqual("like", likeNode.Operator.ToString());
			Assert.AreEqual("%42", valueNode.Value);
		}

		[Test]
		public void Parse_WhenQxEndsWithOnToStringOfNullable_ReturnsCorrectNodes()
		{
			var expression = Reflect<MyClass>.LambdaFrom(m => m.NullableInt.HasValue && m.NullableInt.ToString().QxEndsWith("42"));

			var parser = new WhereParser();
			var parsedLambda = parser.Parse(expression);

			var listOfNodes = parsedLambda.Nodes.ToList();
			Assert.AreEqual(7, listOfNodes.Count);

			var memberNode1 = (NullableMemberNode)listOfNodes[0];
			var isNotNode1 = (OperatorNode)listOfNodes[1];
			var nullNode1 = (NullNode)listOfNodes[2];
			var andNode = (OperatorNode)listOfNodes[3];
			var memberNode2 = (EndsWithMemberNode)listOfNodes[4];
			var likeNode = (OperatorNode)listOfNodes[5];
			var valueNode = (ValueNode)listOfNodes[6];

			Assert.AreEqual("NullableInt", memberNode1.Path);
			Assert.AreEqual(typeof(int), memberNode1.MemberType);
			Assert.AreEqual("is not", isNotNode1.Operator.ToString());
			Assert.AreEqual("null", nullNode1.ToString());
			Assert.AreEqual("and", andNode.ToString());
			Assert.AreEqual("NullableInt", memberNode2.Path);
			Assert.AreEqual(typeof(int), memberNode2.MemberType);
			Assert.AreEqual("like", likeNode.Operator.ToString());
			Assert.AreEqual("%42", valueNode.Value);
		}

		[Test]
		public void Parse_WhenEndsWithOnToStringOfNullableValue_ReturnsCorrectNodes()
		{
			var expression = Reflect<MyClass>.LambdaFrom(m => m.NullableInt.HasValue && m.NullableInt.Value.ToString().EndsWith("42"));

			var parser = new WhereParser();
			var parsedLambda = parser.Parse(expression);

			var listOfNodes = parsedLambda.Nodes.ToList();
			Assert.AreEqual(7, listOfNodes.Count);

			var memberNode1 = (NullableMemberNode)listOfNodes[0];
			var isNotNode1 = (OperatorNode)listOfNodes[1];
			var nullNode1 = (NullNode)listOfNodes[2];
			var andNode = (OperatorNode)listOfNodes[3];
			var memberNode2 = (EndsWithMemberNode)listOfNodes[4];
			var likeNode = (OperatorNode)listOfNodes[5];
			var valueNode = (ValueNode)listOfNodes[6];

			Assert.AreEqual("NullableInt", memberNode1.Path);
			Assert.AreEqual(typeof(int), memberNode1.MemberType);
			Assert.AreEqual("is not", isNotNode1.Operator.ToString());
			Assert.AreEqual("null", nullNode1.ToString());
			Assert.AreEqual("and", andNode.ToString());
			Assert.AreEqual("NullableInt", memberNode2.Path);
			Assert.AreEqual(typeof(int), memberNode2.MemberType);
			Assert.AreEqual("like", likeNode.Operator.ToString());
			Assert.AreEqual("%42", valueNode.Value);
		}

		[Test]
		public void Parse_WhenQxEndsWithOnToStringOfNullableValue_ReturnsCorrectNodes()
		{
			var expression = Reflect<MyClass>.LambdaFrom(m => m.NullableInt.HasValue && m.NullableInt.Value.ToString().QxEndsWith("42"));

			var parser = new WhereParser();
			var parsedLambda = parser.Parse(expression);

			var listOfNodes = parsedLambda.Nodes.ToList();
			Assert.AreEqual(7, listOfNodes.Count);

			var memberNode1 = (NullableMemberNode)listOfNodes[0];
			var isNotNode1 = (OperatorNode)listOfNodes[1];
			var nullNode1 = (NullNode)listOfNodes[2];
			var andNode = (OperatorNode)listOfNodes[3];
			var memberNode2 = (EndsWithMemberNode)listOfNodes[4];
			var likeNode = (OperatorNode)listOfNodes[5];
			var valueNode = (ValueNode)listOfNodes[6];

			Assert.AreEqual("NullableInt", memberNode1.Path);
			Assert.AreEqual(typeof(int), memberNode1.MemberType);
			Assert.AreEqual("is not", isNotNode1.Operator.ToString());
			Assert.AreEqual("null", nullNode1.ToString());
			Assert.AreEqual("and", andNode.ToString());
			Assert.AreEqual("NullableInt", memberNode2.Path);
			Assert.AreEqual(typeof(int), memberNode2.MemberType);
			Assert.AreEqual("like", likeNode.Operator.ToString());
			Assert.AreEqual("%42", valueNode.Value);
		}

        private class MyClass
        {
            public string String1 { get; set; }

			public int? NullableInt { get; set; }
        }
    }
}