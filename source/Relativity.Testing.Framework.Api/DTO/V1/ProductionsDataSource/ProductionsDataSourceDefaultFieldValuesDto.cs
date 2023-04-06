using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.DTO
{
	internal class ProductionsDataSourceDefaultFieldValuesDto
	{
		public DefaultFieldValueDto<ChoiceFieldValueDto> UseImagePlaceholder { get; set; }

		public DefaultFieldValueDto<bool> BurnRedactions { get; set; }
	}
}
