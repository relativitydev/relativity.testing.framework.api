﻿using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface ICancelImagingJobStrategy
	{
		ImagingJobActionResponse Cancel(int workspaceId, long imagingJobId, ImagingJobRequest cancelImagingJobRequest = null);
	}
}
