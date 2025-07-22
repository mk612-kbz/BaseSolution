namespace CommonApi.application.Common
{
	public class ResultData<T>
	{
		public int TotalCount { get; set; }
		public IQueryable<T> Data { get; set; }
	}

	public class PaginationInfo
	{
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public string? SearchValue { get; set; }
		public bool IsOrderDescending { get; set; }
		public required string OrderColumn { get; set; }
	}
}
