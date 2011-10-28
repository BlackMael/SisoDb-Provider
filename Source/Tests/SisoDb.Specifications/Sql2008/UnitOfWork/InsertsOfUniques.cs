﻿using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using PineCone.Annotations;
using SisoDb.Sql2008;
using SisoDb.Testing;
using SisoDb.Testing.Steps;

namespace SisoDb.Specifications.Sql2008.UnitOfWork
{
    namespace InsertsOfUniquesPerType
    {
        [Subject(typeof(Sql2008UnitOfWork), "Insert (unique per type)")]
        public class when_inserting_one_unique_per_type_guid_entity : SpecificationBase
        {
            Establish context = () => TestContext = TestContextFactory.Create(StorageProviders.Sql2008);

            Because of = () => _structure = TestContext.Database.UoW().Insert(new VehicleWithGuidId { VehRegNo = "ABC123" });

            It should_have_one_inserted_vehicle = () => TestContext.Database.should_have_X_num_of_items<VehicleWithGuidId>(1);

            It should_get_inserted = () => TestContext.Database.should_have_identical_structures(_structure);

            private static VehicleWithGuidId _structure;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Insert (unique per type)")]
        public class when_inserting_two_unique_per_type_guid_entities : SpecificationBase
        {
            Establish context = () => TestContext = TestContextFactory.Create(StorageProviders.Sql2008);

            Because of = () => _structures = TestContext.Database.UoW().InsertMany(new[]
            {
                new VehicleWithGuidId { VehRegNo = "ABC123" }, 
                new VehicleWithGuidId { VehRegNo = "ABC321" }
            });

            It should_have_two_inserted_vehicles = () => TestContext.Database.should_have_X_num_of_items<VehicleWithGuidId>(2);

            It should_get_inserted = () => TestContext.Database.should_have_identical_structures<VehicleWithGuidId>(_structures.ToArray());

            private static IList<VehicleWithGuidId> _structures;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Insert (unique per type)")]
        public class when_inserting_two_non_unique_per_type_guid_entities : SpecificationBase
        {
            Establish context = () =>
            {
                TestContext = TestContextFactory.Create(StorageProviders.Sql2008);

                _orgStructure = TestContext.Database.UoW().Insert(new VehicleWithGuidId { VehRegNo = "ABC123" });
            };

            Because of =
                () => CaughtException = Catch.Exception(() => TestContext.Database.UoW().Insert(new VehicleWithGuidId { VehRegNo = "ABC123" }));


            It should_have_failed = () =>
            {
                CaughtException.ShouldNotBeNull();
                CaughtException.Message.StartsWith("Cannot insert duplicate key row in object 'dbo.VehicleUniques' with unique index 'UQ_VehicleUniques'.");
            };

            It should_have_one_inserted_vehicle = () => TestContext.Database.should_have_X_num_of_items<VehicleWithGuidId>(1);

            It should_have_inserted_first = () => TestContext.Database.should_have_identical_structures<VehicleWithGuidId>(new[] { _orgStructure });

            private static VehicleWithGuidId _orgStructure;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Insert (unique per type)")]
        public class when_inserting_one_unique_per_type_identity_entity : SpecificationBase
        {
            Establish context = () => TestContext = TestContextFactory.Create(StorageProviders.Sql2008);

            Because of = () => _structure = TestContext.Database.UoW().Insert(new VehicleWithIdentityId { VehRegNo = "ABC123" });

            It should_have_one_inserted_vehicle = () => TestContext.Database.should_have_X_num_of_items<VehicleWithIdentityId>(1);

            It should_get_inserted = () => TestContext.Database.should_have_identical_structures(_structure);

            private static VehicleWithIdentityId _structure;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Insert (unique per type)")]
        public class when_inserting_two_unique_per_type_identity_entities : SpecificationBase
        {
            Establish context = () => TestContext = TestContextFactory.Create(StorageProviders.Sql2008);

            Because of = () => _structures = TestContext.Database.UoW().InsertMany(new[]
            {
                new VehicleWithIdentityId { VehRegNo = "ABC123" }, 
                new VehicleWithIdentityId { VehRegNo = "ABC321" }
            });

            It should_have_two_inserted_vehicles = () => TestContext.Database.should_have_X_num_of_items<VehicleWithIdentityId>(2);

            It should_get_inserted = () => TestContext.Database.should_have_identical_structures<VehicleWithIdentityId>(_structures.ToArray());

            private static IList<VehicleWithIdentityId> _structures;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Insert (unique per type)")]
        public class when_inserting_two_non_unique_per_type_identity_entities : SpecificationBase
        {
            Establish context = () =>
            {
                TestContext = TestContextFactory.Create(StorageProviders.Sql2008);

                _orgStructure = TestContext.Database.UoW().Insert(new VehicleWithIdentityId { VehRegNo = "ABC123" });
            };

            Because of =
                () => CaughtException = Catch.Exception(() => TestContext.Database.UoW().Insert(new VehicleWithIdentityId { VehRegNo = "ABC123" }));


            It should_have_failed = () =>
            {
                CaughtException.ShouldNotBeNull();
                CaughtException.Message.StartsWith("Cannot insert duplicate key row in object 'dbo.VehicleUniques' with unique index 'UQ_VehicleUniques'.");
            };

            It should_have_one_inserted_vehicle = () => TestContext.Database.should_have_X_num_of_items<VehicleWithIdentityId>(1);

            It should_have_inserted_first = () => TestContext.Database.should_have_identical_structures<VehicleWithIdentityId>(new[] { _orgStructure });

            private static VehicleWithIdentityId _orgStructure;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Insert (unique per type)")]
        public class when_inserting_one_unique_per_type_big_identity_entity : SpecificationBase
        {
            Establish context = () => TestContext = TestContextFactory.Create(StorageProviders.Sql2008);

            Because of = () => _structure = TestContext.Database.UoW().Insert(new VehicleWithBigIdentityId { VehRegNo = "ABC123" });

            It should_have_one_inserted_vehicle = () => TestContext.Database.should_have_X_num_of_items<VehicleWithBigIdentityId>(1);

            It should_get_inserted = () => TestContext.Database.should_have_identical_structures(_structure);

            private static VehicleWithBigIdentityId _structure;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Insert (unique per type)")]
        public class when_inserting_two_unique_per_type_big_identity_entities : SpecificationBase
        {
            Establish context = () => TestContext = TestContextFactory.Create(StorageProviders.Sql2008);

            Because of = () => _structures = TestContext.Database.UoW().InsertMany(new[]
            {
                new VehicleWithBigIdentityId { VehRegNo = "ABC123" }, 
                new VehicleWithBigIdentityId { VehRegNo = "ABC321" }
            });

            It should_have_two_inserted_vehicles = () => TestContext.Database.should_have_X_num_of_items<VehicleWithBigIdentityId>(2);

            It should_get_inserted = () => TestContext.Database.should_have_identical_structures<VehicleWithBigIdentityId>(_structures.ToArray());

            private static IList<VehicleWithBigIdentityId> _structures;
        }

        [Subject(typeof(Sql2008UnitOfWork), "Insert (unique per type)")]
        public class when_inserting_two_non_unique_per_type_big_identity_entities : SpecificationBase
        {
            Establish context = () =>
            {
                TestContext = TestContextFactory.Create(StorageProviders.Sql2008);

                _orgStructure = TestContext.Database.UoW().Insert(new VehicleWithBigIdentityId { VehRegNo = "ABC123" });
            };

            Because of =
                () => CaughtException = Catch.Exception(() => TestContext.Database.UoW().Insert(new VehicleWithBigIdentityId { VehRegNo = "ABC123" }));


            It should_have_failed = () =>
            {
                CaughtException.ShouldNotBeNull();
                CaughtException.Message.StartsWith("Cannot insert duplicate key row in object 'dbo.VehicleUniques' with unique index 'UQ_VehicleUniques'.");
            };

            It should_have_one_inserted_vehicle = () => TestContext.Database.should_have_X_num_of_items<VehicleWithBigIdentityId>(1);

            It should_have_inserted_first = () => TestContext.Database.should_have_identical_structures<VehicleWithBigIdentityId>(new[] { _orgStructure });

            private static VehicleWithBigIdentityId _orgStructure;
        }

        public class VehicleWithGuidId : VehicleBase<Guid>
        {
        }

        public class VehicleWithIdentityId : VehicleBase<int>
        {
        }

        public class VehicleWithBigIdentityId : VehicleBase<long>
        {
        }

        public class VehicleBase<TId>
        {
            public TId StructureId { get; set; }

            [Unique(UniqueModes.PerType)]
            public string VehRegNo { get; set; }
        }
    }
}