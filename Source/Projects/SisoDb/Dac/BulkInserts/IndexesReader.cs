﻿using System.Collections.Generic;
using PineCone.Structures;
using SisoDb.DbSchema;

namespace SisoDb.Dac.BulkInserts
{
    public class IndexesReader : SingleResultReaderBase<IStructureIndex[]>
    {
        public IndexesReader(IndexStorageSchema storageSchema, IEnumerable<IStructureIndex[]> items)
            : base(storageSchema, items)
        {
        }

        public override object GetValue(int ordinal)
        {
            return ordinal != 0
                ? Enumerator.Current[ordinal - 1].Value
                : Enumerator.Current[0].StructureId.Value;
        }
    }
}