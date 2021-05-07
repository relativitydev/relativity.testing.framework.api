using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ChoiceGetAllByObjectFieldStrategy))]
	public class ChoiceGetAllByObjectFieldStrategyFixture : ApiTestFixture
	{
		private IChoiceGetAllByObjectFieldStrategy _sut;

		public ChoiceGetAllByObjectFieldStrategyFixture()
		{
		}

		public ChoiceGetAllByObjectFieldStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IChoiceGetAllByObjectFieldStrategy>();
		}

		[Test]
		public void GetAll_WithNullObjectTypeName()
		{
			Assert.Throws<ArgumentNullException>(() =>
				_sut.GetAll(-1, null, "Client Status"));
		}

		[Test]
		public void GetAll_WithNullFieldName()
		{
			Assert.Throws<ArgumentNullException>(() =>
				_sut.GetAll(-1, "Client", null));
		}

		[Test]
		public void GetAll_Missing()
		{
			var result = _sut.GetAll(-1, "Client", "Missing Field");

			result.Should().BeEmpty();
		}

		[Test]
		public void GetAll_Existing()
		{
			var result = _sut.GetAll(-1, "Client", "Client Status");

			result.Should().NotBeEmpty();

			foreach (Choice choice in result)
			{
				choice.ArtifactID.Should().BePositive();
				choice.Name.Should().NotBeNullOrWhiteSpace();
				choice.Order.Should().BeGreaterOrEqualTo(0);
				choice.ObjectType.Name.Should().Be("Client");
				choice.Field.Name.Should().Be("Client Status");
				choice.Active.Should().BeTrue();
			}
		}
	}
}
