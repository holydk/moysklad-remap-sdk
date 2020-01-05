using System;
using System.Collections.Generic;
using Confetti.MoySklad.Remap.Client;
using FluentAssertions;
using NUnit.Framework;

namespace Confetti.MoySklad.Remap.UnitTests.Client
{
    internal class TestExpandMetaEntity
    {
        [AllowExpand]
        [Parameter("entity_one_level")]
        public L1TestExpandMetaEntity L1Entity { get; set; }

        [Parameter("entity_one_level")]
        public L1TestExpandMetaEntity NotAllowedExpandMember { get; set; }
    }

    internal class L1TestExpandMetaEntity
    {
        [AllowExpand]
        [Parameter("entity_two_level")]
        public L2TestExpandMetaEntity L2Entity { get; set; }
    }

    internal class L2TestExpandMetaEntity
    {

    }

    public class ExpandParameterBuilderTests
    {
        [Test]
        public void With_should_add_Expander()
        {
            var expanders = new List<string>();
            var subject = new ExpandParameterBuilder<TestExpandMetaEntity>(expanders);

            subject.With(p => p.L1Entity.L2Entity);

            expanders.Should().HaveCount(1);
            expanders[0].Should().Be("entity_one_level.entity_two_level");
        }

        [Test]
        public void Double_With_should_add_Expander_with_two_properties()
        {
            var expanders = new List<string>();
            var subject = new ExpandParameterBuilder<TestExpandMetaEntity>(expanders);

            subject.With(p => p.L1Entity).And.With(p => p.L1Entity.L2Entity);

            expanders.Should().HaveCount(2);
            expanders[0].Should().Be("entity_one_level");
            expanders[1].Should().Be("entity_one_level.entity_two_level");
        }

        [Test]
        public void If_expand_is_not_allowed_then_By_should_throw_api_exception()
        {
            var expanders = new List<string>();
            var subject = new ExpandParameterBuilder<TestExpandMetaEntity>(expanders);

            Action action = () => subject.With(p => p.NotAllowedExpandMember);
            
            action.Should().Throw<ApiException>();
        }
    }
}