using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SisoDb.TestUtils;

namespace SisoDb.Tests.IntegrationTests.Providers.SqlProvider.UnitOfWork.Deletes
{
    [TestFixture]
    public class SqlUnitOfWorkDeleteByIdsTests : SqlIntegrationTestBase
    {
        protected override void OnTestFinalize()
        {
            DropStructureSet<IdentityItem>();
            DropStructureSet<GuidItem>();
        }

        [Test]
        public void DeleteByIds_ForIdentities_WhenMatchingSubsetOfAllRows_OnlySubsetIsDeleted()
        {
            var items = new List<IdentityItem>
                        {
                            new IdentityItem{SortOrder = 1, Value = "A"},
                            new IdentityItem{SortOrder = 2, Value = "B"},
                            new IdentityItem{SortOrder = 3, Value = "C"},
                            new IdentityItem{SortOrder = 4, Value = "D"},
                        };

            IList<IdentityItem> refetched;

            using (var uow = Database.CreateUnitOfWork())
            {
                uow.InsertMany(items);
                uow.Commit();

                uow.DeleteByIds<IdentityItem>(new[] { 2, 3 });
                uow.Commit();

                refetched = uow.GetAll<IdentityItem>().ToList();
            }

            Assert.AreEqual(2, refetched.Count);
            CustomAssert.AreValueEqual(items[0], refetched[0]);
            CustomAssert.AreValueEqual(items[3], refetched[1]);
        }

        [Test]
        public void DeleteByIds_ForIdentities_WhenSpecifyingItemThatDoesNotExist_OnlyRealSubsetIsDeleted()
        {
            var nonExistingId = 99;
            var items = new List<IdentityItem>
                        {
                            new IdentityItem{SortOrder = 1, Value = "A"},
                            new IdentityItem{SortOrder = 2, Value = "B"},
                            new IdentityItem{SortOrder = 3, Value = "C"},
                            new IdentityItem{SortOrder = 4, Value = "D"},
                        };

            IList<IdentityItem> refetched;

            using (var uow = Database.CreateUnitOfWork())
            {
                uow.InsertMany(items);
                uow.Commit();

                uow.DeleteByIds<IdentityItem>(new[] { 2, 3, nonExistingId });
                uow.Commit();

                refetched = uow.GetAll<IdentityItem>().ToList();
            }

            Assert.AreEqual(2, refetched.Count);
            CustomAssert.AreValueEqual(items[0], refetched[0]);
            CustomAssert.AreValueEqual(items[3], refetched[1]);
        }

        [Test]
        public void DeleteByIds_ForGuids_WhenMatchingSubsetOfAllRows_OnlySubsetIsDeleted()
        {
            var items = new List<GuidItem>
                        {
                            new GuidItem{SortOrder = 1, Value = "A"},
                            new GuidItem{SortOrder = 2, Value = "B"},
                            new GuidItem{SortOrder = 3, Value = "C"},
                            new GuidItem{SortOrder = 4, Value = "D"},
                        };

            IList<GuidItem> refetched;

            using (var uow = Database.CreateUnitOfWork())
            {
                uow.InsertMany(items);
                uow.Commit();

                uow.DeleteByIds<GuidItem>(new[] { items[1].SisoId, items[2].SisoId });
                uow.Commit();

                refetched = uow.GetAll<GuidItem>(q => q.SortBy(i => i.SortOrder)).ToList();
            }

            Assert.AreEqual(2, refetched.Count);
            CustomAssert.AreValueEqual(items[0], refetched[0]);
            CustomAssert.AreValueEqual(items[3], refetched[1]);
        }

        [Test]
        public void DeleteByIds_ForGuids_WhenSpecifyingItemThatDoesNotExist_OnlySubsetIsDeleted()
        {
            var nonExistingId = Guid.Empty;
            var items = new List<GuidItem>
                        {
                            new GuidItem{SortOrder = 1, Value = "A"},
                            new GuidItem{SortOrder = 2, Value = "B"},
                            new GuidItem{SortOrder = 3, Value = "C"},
                            new GuidItem{SortOrder = 4, Value = "D"},
                        };

            IList<GuidItem> refetched;

            using (var uow = Database.CreateUnitOfWork())
            {
                uow.InsertMany(items);
                uow.Commit();

                uow.DeleteByIds<GuidItem>(new[] { items[1].SisoId, items[2].SisoId, nonExistingId });
                uow.Commit();

                refetched = uow.GetAll<GuidItem>(q => q.SortBy(i => i.SortOrder)).ToList();
            }

            Assert.AreEqual(2, refetched.Count);
            CustomAssert.AreValueEqual(items[0], refetched[0]);
            CustomAssert.AreValueEqual(items[3], refetched[1]);
        }

        private class IdentityItem
        {
            public int SisoId { get; set; }

            public int SortOrder { get; set; }

            public string Value { get; set; }
        }

        private class GuidItem
        {
            public Guid SisoId { get; set; }

            public int SortOrder { get; set; }

            public string Value { get; set; }
        }
    }
}