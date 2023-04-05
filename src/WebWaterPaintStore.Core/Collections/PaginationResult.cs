using WebWaterPaintStore.Core.Contracts;
using WebWaterPaintStore.Core.DTO;

namespace WebWaterPaintStore.Core.Collections;

public class PaginationResult<T>
{

	public IEnumerable<T> Items { get; set; }

	public PagingMetadata Metadata { get; set; }

	public PaginationResult(IPagedList<T> pagedList)
	{
		Items = pagedList;
		Metadata = new PagingMetadata(pagedList);
	}

}