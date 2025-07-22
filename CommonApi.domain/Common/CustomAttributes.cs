namespace CommonApi.domain.Common
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SearchableAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class ExtractPropertyAttribute : Attribute
	{
	}
}
