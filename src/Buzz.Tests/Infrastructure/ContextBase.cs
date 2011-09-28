using System;
using System.Diagnostics;
using System.Reflection;
using Moq;
using Xunit.Sdk;

namespace Buzz.Tests.Infrastructure
{
    public abstract class ContextBase<TSystemUnderTest> : ISpecification where TSystemUnderTest : class
    {
        protected AutoMockContainer Container { get; private set; }

        protected TSystemUnderTest Sut { get; private set; }

        public void Initialize()
        {
            Container = CreateAutoMocker();
            Sut = CreateSut();
            Given();
            InvokeWhen();
        }

        protected virtual TSystemUnderTest CreateSut()
        {
            return Container.Create<TSystemUnderTest>();
        }

        protected virtual void Given()
        {
        }

        private void InvokeWhen()
        {
            var timeThis = new TimeThisAssertion();
            timeThis.Start(GetType().GetMethod("When", BindingFlags.Instance | BindingFlags.NonPublic));
            When();
            timeThis.Stop();
        }

        protected abstract void When();

        protected virtual void AfterEachObservation()
        {
        }

        public void Cleanup()
        {
            AfterEachObservation();
        }

        private AutoMockContainer CreateAutoMocker()
        {
            return new AutoMockContainer(new MockRepository(MockBehavior.Loose));
        }

        protected void RegisterDependency<TDependency>(TDependency dependency)
        {
            Container.Register(dependency);
        }

        protected Mock<TDependency> MockFor<TDependency>() where TDependency : class
        {
            return GetMock<TDependency>();
        }

        protected Mock<TDependency> GetMock<TDependency>() where TDependency : class
        {
            return Container.GetMock<TDependency>() ?? new Mock<TDependency>(MockBehavior.Loose);
        }

        protected TDependency GetDependency<TDependency>() where TDependency : class
        {
            return GetMock<TDependency>().Object;
        }
    }

    public abstract class ContextBase : ISpecification
    {
        public void Initialize()
        {
            Given();
            InvokeWhen();
        }

        protected virtual void Given()
        {
        }

        private void InvokeWhen()
        {
            var timeThis = new TimeThisAssertion();
            timeThis.Start(GetType().GetMethod("When", BindingFlags.Instance | BindingFlags.NonPublic));
            When();
            timeThis.Stop();
        }

        protected abstract void When();

        protected virtual void AfterEachObservation()
        {
        }

        public void Cleanup()
        {
            AfterEachObservation();
        }
    }

    internal class TimeThisAssertion
    {
        private TimeThisAttribute _timeThis;
        private Stopwatch _sw;

        public void Start(MemberInfo info)
        {
            if (!ShouldTime(info)) return;
            _sw = new Stopwatch();
            _sw.Start();
        }

        private bool ShouldTime(MemberInfo info)
        {
            _timeThis = (info == null ? null : Attribute.GetCustomAttribute(info, typeof(TimeThisAttribute))) as TimeThisAttribute;
            return _timeThis != null;
        }       

        public void Stop()
        {
            if (_timeThis == null) return;
            _sw.Stop();
            AssertFailingTime();
            Console.WriteLine(string.Format("{0}: {1}", _timeThis.Message, _sw.Elapsed));
        }

        private void AssertFailingTime()
        {
            if (_timeThis.FailingTime != null)
            {
                TimeSpan failingTime;
                if (TimeSpan.TryParse(_timeThis.FailingTime, out failingTime) && _sw.Elapsed > failingTime)
                {
                    throw new AssertException(string.Format("Expected test to run under {0}, but took {1}",
                                                            failingTime, _sw.Elapsed));
                }
            }
        }
    }
}