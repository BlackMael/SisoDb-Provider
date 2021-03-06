using System.Collections.Generic;
using PineCone.Structures.Schemas;
using SisoDb.Querying;
using SisoDb.Querying.Lambdas;

namespace SisoDb
{
	public interface IQuery
	{
		IStructureSchema StructureSchema { get; }
		int? TakeNumOfStructures { get; set; }
		Paging Paging { get; set; }
		IParsedLambda Where { get; set; }
		IParsedLambda Sortings { get; set; }
		IList<IParsedLambda> Includes { get; set; }

		bool IsEmpty { get; }
		bool HasTakeNumOfStructures { get; }
		bool HasPaging { get; }
		bool HasWhere { get; }
		bool HasSortings { get; }
		bool HasIncludes { get; }
	}
}