using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ChoiceResolveByObjectFieldAndNameStrategy))]
	public class ChoiceResolveByObjectFieldAndNameStrategyFixture : ApiTestFixture
	{
		private IChoiceResolveByObjectFieldAndNameStrategy _sut;

		public ChoiceResolveByObjectFieldAndNameStrategyFixture()
		{
		}

		public ChoiceResolveByObjectFieldAndNameStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IChoiceResolveByObjectFieldAndNameStrategy>();
		}

		[Test]
		public void ResolveReference_WithNullObjectTypeName()
		{
			Assert.Throws<ArgumentNullException>(() =>
				_sut.ResolveReference(null, "Client Status", "Active"));
		}

		[Test]
		public void ResolveReference_WithNullFieldName()
		{
			Assert.Throws<ArgumentNullException>(() =>
				_sut.ResolveReference("Client", null, "Active"));
		}

		[Test]
		public void ResolveReference_WithNullChoiceName()
		{
			Assert.Throws<ArgumentNullException>(() =>
				_sut.ResolveReference("Client", "Client Status", null));
		}

		[Test]
		public void ResolveReference_Missing()
		{
			Assert.Throws<ObjectNotFoundException>(() =>
				_sut.ResolveReference("Client", "Client Status", "Missing"));
		}

		[Test]
		public void ResolveReference_Existing()
		{
			var result = _sut.ResolveReference("Client", "Client Status", "Active");

			result.ArtifactID.Should().BePositive();
		}
	}
}
