using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies.InstanceSettings
{
	public enum InstanceSettingValueTypeDTOV1
	{
		// Indicates the default value type. By default it is text.
		Default = 0,

		// Indicates that the value should be treated as text.
		Text = 1,

		// Indicates that the value be treated as a whole number from -2147483648 to 2147483647.
		Integer32 = 2,

		// Indicates that the value should be treated as a whole number from -9223372036854775808 to 9223372036854775807.
		Integer64 = 3,

		// Indicates that the value should be treated as a whole number from 0 to 2147483647.
		NonnegativeInteger32 = 4,

		// Indicates that the value should be treated as a whole number from 0 to 9223372036854775807.
		NonnegativeInteger64 = 5,

		// Indicates that the value should be treated as a whole number from 1 to 2147483647.
		PositiveInteger32 = 6,

		// Indicates that the value should be treated as a whole number from 1 to 9223372036854775807.
		PositiveInteger64 = 7,

		// Indicates that the value should be treated as a Boolean with only True or False valid values.
		TrueFalse = 8,
	}
}
