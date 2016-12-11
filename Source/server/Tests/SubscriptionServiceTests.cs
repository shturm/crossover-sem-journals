using System;
using Autofac;
using Moq;
using CrossoverSemJournals.Domain.Entities;
using CrossoverSemJournals.Domain.ServiceInterfaces;
using NHibernate;
using NUnit.Framework;
using Autofac.Extras.Moq;
using CrossoverSemJournals.Domain.Infrastructure;
using System.Linq.Expressions;
using CrossoverSemJournals.Domain.Services;

namespace Tests
{

	[TestFixture]
	public class SubscriptionServiceTests
	{
		public ILifetimeScope Scope { get; private set; }
		public ISession Session { get; private set; }
		public ISubscriptionService SUT { get; private set; }

		[TestFixtureSetUp]
		public void Init()
		{
			Scope = TestUtils.GetAutofacScope ();
			Session = Scope.Resolve<ISession> ();
		}

		[SetUp]
		public void SetUp()
		{
			SUT = Scope.Resolve<ISubscriptionService> ();
		}


		[Test]
		[Category ("Unit")]
		public void CanSubscribeUserToJournal()
		{
			using (var mock = AutoMock.GetLoose ()) {
				// arrange
				var user = new User ();
				var journal = new Journal ();
				mock.Mock<IRepository<User>> ().Setup (x => x.Get (It.IsAny<Expression<Func<User, bool>>> ())).Returns (user);
				mock.Mock<IRepository<Journal>> ().Setup (x => x.Get (It.IsAny<Expression<Func<Journal, bool>>> ())).Returns (journal);
				//mock.Mock<IRepository<Subscription>> ().Setup (x=>x.Insert (It.IsAny<Subscription> ())).cal;

				var sut = mock.Create<SubscriptionService> ();

				// act
				sut.Subscribe (Guid.NewGuid (), 1);

				// assert
				mock.Mock<IRepository<Subscription>> ()
				    .Verify (x => x.Insert (It.Is<Subscription>(s=> s.Journal == journal && s.User == user)), Times.Once (), "Did not persist subscription");
				mock.Mock<IRepository<User>> ().Verify (x => x.Update (It.Is<User> (u => u == user)), Times.Once (), "Did not update user");

			}
		}

		[Test]
		[TestCase (true, false)]
		[TestCase (false, true)]
		[TestCase (false, false)]
		[Category ("Unit")]
		public void GivenInvalidUserOrJournal_DoesNotPersistSubscription (bool userValid, bool journaValid)
		{
			using (var mock = AutoMock.GetLoose ()) {
				// arrange
				var user = userValid ? new User () : null;
				var journal = journaValid ? new Journal () : null;

				mock.Mock<IRepository<User>> ().Setup (x => x.Get (It.IsAny<Expression<Func<User, bool>>> ())).Returns (user);
				mock.Mock<IRepository<Journal>> ().Setup (x => x.Get (It.IsAny<Expression<Func<Journal, bool>>> ())).Returns (journal);
				//mock.Mock<IRepository<Subscription>> ().Setup (x=>x.Insert (It.IsAny<Subscription> ())).cal;

				var sut = mock.Create<SubscriptionService> ();

				// act
				Assert.Throws<ArgumentException> (() => sut.Subscribe (Guid.NewGuid (), 1));

				// assert
				mock.Mock<IRepository<Subscription>> ()
				    .Verify (x => x.Insert (It.Is<Subscription> (s => s.Journal == journal && s.User == user)), Times.Never (), "Persist invalid subscription");
				mock.Mock<IRepository<User>> ().Verify (x => x.Update (It.Is<User> (u => u == user)), Times.Never (), "Update user with invalid ");

			}
		}
	}
}