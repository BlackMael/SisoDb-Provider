using System;
using System.Collections.Generic;
using PineCone.Structures;
using SisoDb.DbSchema;

namespace SisoDb.Dac.BulkInserts
{
	public class ValueTypeIndexesReader : IndexesReader 
	{
		public ValueTypeIndexesReader(IndexStorageSchema storageSchema, IEnumerable<IStructureIndex> items) 
			: base(storageSchema, items) 
		{}

		public override object GetValue(int ordinal)
		{
			var schemaField = StorageSchema[ordinal];

			if (schemaField.Name == IndexStorageSchema.Fields.StructureId.Name)
				return Enumerator.Current.StructureId.Value;

			if (schemaField.Name == IndexStorageSchema.Fields.MemberPath.Name)
				return Enumerator.Current.Path;

			if (schemaField.Name == IndexStorageSchema.Fields.Value.Name)
				return Enumerator.Current.Value;

			if (schemaField.Name == IndexStorageSchema.Fields.StringValue.Name)
				return SisoEnvironment.StringConverter.AsString(Enumerator.Current.Value);

			throw new NotSupportedException();
		}
	}
}