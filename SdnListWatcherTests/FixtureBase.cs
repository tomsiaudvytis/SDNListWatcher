﻿using Moq;

namespace SdnListWatcherTests
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using AutoFixture.Kernel;

    public abstract class FixtureBase<TSystemUnderTest> : FixtureBase
    {
        protected TSystemUnderTest Sut { get; set; }

        protected FixtureBase() =>
            Fixture.Customize<TSystemUnderTest>(x => x.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));

        protected TSystemUnderTest CreateSut() => Fixture.Create<TSystemUnderTest>();
    }

    public abstract class FixtureBase
    {
        protected IFixture Fixture { get; set; }

        protected FixtureBase()
        {
            Fixture = new Fixture();
            Fixture.Customize(new AutoMoqCustomization());
        }

        protected Mock<T> FreezeMock<T>() where T : class
        {
            return Fixture.Freeze<Mock<T>>();
        }
    }
}