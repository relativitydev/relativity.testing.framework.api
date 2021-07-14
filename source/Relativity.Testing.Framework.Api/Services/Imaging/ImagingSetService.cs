﻿using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ImagingSetService : IImagingSetService
	{
		private readonly IImagingSetCreateStrategy _imagingSetCreateStrategy;
		private readonly IImagingSetGetStrategy _imagingSetGetStrategy;
		private readonly IImagingSetStatusGetStrategy _imagingSetStatusGetStrategy;
		private readonly IImagingSetDeleteStrategy _imagingSetDeleteStrategy;

		public ImagingSetService(
			IImagingSetCreateStrategy imagingSetCreateStrategy,
			IImagingSetGetStrategy imagingSetGetStrategy,
			IImagingSetStatusGetStrategy imagingSetStatusGetStrategy,
			IImagingSetDeleteStrategy imagingSetDeleteStrategy)
		{
			_imagingSetCreateStrategy = imagingSetCreateStrategy;
			_imagingSetGetStrategy = imagingSetGetStrategy;
			_imagingSetStatusGetStrategy = imagingSetStatusGetStrategy;
			_imagingSetDeleteStrategy = imagingSetDeleteStrategy;
		}

		public ImagingSet Create(int workspaceId, ImagingSetRequest imagingSetRequest)
			=> _imagingSetCreateStrategy.Create(workspaceId, imagingSetRequest);

		public async Task<ImagingSet> CreateAsync(int workspaceId, ImagingSetRequest imagingSetRequest)
			=> await _imagingSetCreateStrategy.CreateAsync(workspaceId, imagingSetRequest).ConfigureAwait(false);

		public void Delete(int workspaceId, int imagingSetId)
			=> _imagingSetDeleteStrategy.Delete(workspaceId, imagingSetId);

		public async Task DeleteAsync(int workspaceId, int imagingSetId)
			=> await _imagingSetDeleteStrategy.DeleteAsync(workspaceId, imagingSetId).ConfigureAwait(false);

		public ImagingSet Get(int workspaceId, int imagingSetId)
			=> _imagingSetGetStrategy.Get(workspaceId, imagingSetId);

		public async Task<ImagingSet> GetAsync(int workspaceId, int imagingSetId)
			=> await _imagingSetGetStrategy.GetAsync(workspaceId, imagingSetId).ConfigureAwait(false);

		public ImagingSetDetailedStatus GetStatus(int workspaceId, int imagingSetId)
			=> _imagingSetStatusGetStrategy.Get(workspaceId, imagingSetId);

		public async Task<ImagingSetDetailedStatus> GetStatusAsync(int workspaceId, int imagingSetId)
			=> await _imagingSetStatusGetStrategy.GetAsync(workspaceId, imagingSetId).ConfigureAwait(false);
	}
}
