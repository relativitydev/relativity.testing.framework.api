﻿using System;
using System.Collections.Generic;
using Relativity.Testing.Framework.Api.DTO;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies.Layouts.DTO
{
	internal class LayoutDTOV1
	{
		public NamedArtifactWithGuids ObjectIdentifier { get; set; }

		public SecuredValueDTO ObjectType { get; set; }

		public string Order { get; set; }

		public bool OverwriteProtection { get; set; }

		public bool AllowCopyFromPrevious { get; set; }

		public LayoutRelativityApplicationsDTOV1 RelativityApplications { get; set; }

		public DateTime CreatedOn { get; set; }

		public SecuredValueDTO CreatedBy { get; set; }

		public SecuredValueDTO LastModifiedBy { get; set; }

		public DateTime LastModifiedOn { get; set; }

		public string Keywords { get; set; }

		public string Notes { get; set; }

		public Meta Meta { get; set; }

		public List<LayoutActionDTOV1> Actions { get; set; }

		public SecuredValueDTO Owner { get; set; }
	}
}
