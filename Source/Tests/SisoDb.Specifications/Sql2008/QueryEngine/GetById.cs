﻿using System;
using System.Collections.Generic;
using Machine.Specifications;
using SisoDb.Sql2008;
using SisoDb.Testing;

namespace SisoDb.Specifications.Sql2008.QueryEngine
{
    namespace GetById
    {
        [Subject(typeof(Sql2008UnitOfWork), "Get by Id (guid)")]
        public class when_set_with_guid_id_is_empty : SpecificationBase
        {
            Establish context = () => TestContext = TestContextFactory.Create(StorageProviders.Sql2008);

            Because of = () =>
                _fetchedStructure = TestContext.Database.FetchVia().GetById<QueryGuidItem>(Guid.Parse("ABF5FC75-1E74-4564-B55A-DB3594394BE3"));

            It should_not_fetch_any_structure =
                () => _fetchedStructure.ShouldBeNull();

            private static QueryGuidItem _fetchedStructure;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Get by Id (identity)")]
        public class when_set_with_identity_id_is_empty : SpecificationBase
        {
            Establish context = () => TestContext = TestContextFactory.Create(StorageProviders.Sql2008);

            Because of = () =>
                _fetchedStructure = TestContext.Database.FetchVia().GetById<QueryIdentityItem>(42);

            It should_not_fetch_any_structure =
                () => _fetchedStructure.ShouldBeNull();

            private static QueryIdentityItem _fetchedStructure;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Get by Id (big identity)")]
        public class when_set_with_big_identity_id_is_empty : SpecificationBase
        {
            Establish context = () => TestContext = TestContextFactory.Create(StorageProviders.Sql2008);

            Because of = () =>
                _fetchedStructure = TestContext.Database.FetchVia().GetById<QueryBigIdentityItem>(42);

            It should_not_fetch_any_structure =
                () => _fetchedStructure.ShouldBeNull();

            private static QueryBigIdentityItem _fetchedStructure;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Get by Id as Json (guid)")]
        public class when_json_set_with_guid_id_is_empty : SpecificationBase
        {
            Establish context = () => TestContext = TestContextFactory.Create(StorageProviders.Sql2008);

            Because of = () =>
                _fetchedStructure = TestContext.Database.FetchVia().GetByIdAsJson<QueryGuidItem>(Guid.Parse("ABF5FC75-1E74-4564-B55A-DB3594394BE3"));

            It should_not_fetch_any_structure =
                () => _fetchedStructure.ShouldBeNull();

            private static string _fetchedStructure;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Get by Id as Json (identity)")]
        public class when_json_set_with_identity_id_is_empty : SpecificationBase
        {
            Establish context = () => TestContext = TestContextFactory.Create(StorageProviders.Sql2008);

            Because of = () =>
                _fetchedStructure = TestContext.Database.FetchVia().GetByIdAsJson<QueryIdentityItem>(42);

            It should_not_fetch_any_structure =
                () => _fetchedStructure.ShouldBeNull();

            private static string _fetchedStructure;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Get by Id as Json (big identity)")]
        public class when_json_set_with_big_identity_id_is_empty : SpecificationBase
        {
            Establish context = () => TestContext = TestContextFactory.Create(StorageProviders.Sql2008);

            Because of = () =>
                _fetchedStructure = TestContext.Database.FetchVia().GetByIdAsJson<QueryBigIdentityItem>(42);

            It should_not_fetch_any_structure =
                () => _fetchedStructure.ShouldBeNull();

            private static string _fetchedStructure;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Get by Id (guid)")]
        public class when_set_with_guid_id_contains_four_items : SpecificationBase
        {
            Establish context = () =>
            {
                TestContext = TestContextFactory.Create(StorageProviders.Sql2008);
                _structures = TestContext.Database.UoW().InsertMany(QueryGuidItem.CreateFourItems<QueryGuidItem>());
            };

            Because of =
                () => _fetchedStructure = TestContext.Database.FetchVia().GetById<QueryGuidItem>(_structures[1].StructureId);

            It should_fetch_the_structure = 
                () => _fetchedStructure.ShouldBeValueEqualTo(_structures[1]);

            private static IList<QueryGuidItem> _structures;
            private static QueryGuidItem _fetchedStructure;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Get by Id (identity)")]
        public class when_set_with_identity_id_contains_four_items : SpecificationBase
        {
            Establish context = () =>
            {
                TestContext = TestContextFactory.Create(StorageProviders.Sql2008);
                _structures = TestContext.Database.UoW().InsertMany(QueryIdentityItem.CreateFourItems<QueryIdentityItem>());
            };

            Because of =
                () => _fetchedStructure = TestContext.Database.FetchVia().GetById<QueryIdentityItem>(_structures[1].StructureId);

            It should_fetch_the_structure =
                () => _fetchedStructure.ShouldBeValueEqualTo(_structures[1]);

            private static IList<QueryIdentityItem> _structures;
            private static QueryIdentityItem _fetchedStructure;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Get by Id (big identity)")]
        public class when_set_with_big_identity_id_contains_four_items : SpecificationBase
        {
            Establish context = () =>
            {
                TestContext = TestContextFactory.Create(StorageProviders.Sql2008);
                _structures = TestContext.Database.UoW().InsertMany(QueryBigIdentityItem.CreateFourItems<QueryBigIdentityItem>());
            };

            Because of =
                () => _fetchedStructure = TestContext.Database.FetchVia().GetById<QueryBigIdentityItem>(_structures[1].StructureId);

            It should_fetch_the_structure =
                () => _fetchedStructure.ShouldBeValueEqualTo(_structures[1]);

            private static IList<QueryBigIdentityItem> _structures;
            private static QueryBigIdentityItem _fetchedStructure;
        }
        
        [Subject(typeof(Sql2008UnitOfWork), "Get by Id as Json (guid)")]
        public class when_json_set_with_guid_id_contains_four_items : SpecificationBase
        {
            Establish context = () =>
            {
                TestContext = TestContextFactory.Create(StorageProviders.Sql2008);
                _structures = TestContext.Database.UoW().InsertMany(QueryGuidItem.CreateFourItems<QueryGuidItem>());
            };

            Because of =
                () => _fetchedStructure = TestContext.Database.FetchVia().GetByIdAsJson<QueryGuidItem>(_structures[1].StructureId);

            It should_fetch_the_structure =
                () => _fetchedStructure.ShouldEqual(_structures[1].AsJson());

            private static IList<QueryGuidItem> _structures;
            private static string _fetchedStructure;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Get by Id as Json (identity)")]
        public class when_json_set_with_identity_id_contains_four_items : SpecificationBase
        {
            Establish context = () =>
            {
                TestContext = TestContextFactory.Create(StorageProviders.Sql2008);
                _structures = TestContext.Database.UoW().InsertMany(QueryIdentityItem.CreateFourItems<QueryIdentityItem>());
            };

            Because of =
                () => _fetchedStructure = TestContext.Database.FetchVia().GetByIdAsJson<QueryIdentityItem>(_structures[1].StructureId);

            It should_fetch_the_structure =
                () => _fetchedStructure.ShouldEqual(_structures[1].AsJson());

            private static IList<QueryIdentityItem> _structures;
            private static string _fetchedStructure;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Get by Id as Json (big identity)")]
        public class when_json_set_with_big_identity_id_contains_four_items : SpecificationBase
        {
            Establish context = () =>
            {
                TestContext = TestContextFactory.Create(StorageProviders.Sql2008);
                _structures = TestContext.Database.UoW().InsertMany(QueryBigIdentityItem.CreateFourItems<QueryBigIdentityItem>());
            };

            Because of =
                () => _fetchedStructure = TestContext.Database.FetchVia().GetByIdAsJson<QueryBigIdentityItem>(_structures[1].StructureId);

            It should_fetch_the_structure =
                () => _fetchedStructure.ShouldEqual(_structures[1].AsJson());

            private static IList<QueryBigIdentityItem> _structures;
            private static string _fetchedStructure;
        }
    }
}