﻿using System;
using System.Linq.Expressions;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class BatchService : IBatchService
	{
		private readonly IGetWorkspaceEntityByIdStrategy<Batch> _getWorkspaceEntityByIdStrategy;
		private readonly IGetAllWorkspaceEntitiesStrategy<Batch> _getAllWorkspaceEntitiesStrategy;
		private readonly IBatchQueryStrategy _batchQueryStrategy;
		private readonly IBatchAssignToUserStrategy _batchAssignToUserStrategy;
		private readonly IBatchCheckinStrategy _batchCheckinStrategy;
		private readonly IBatchCheckoutStrategy _batchCheckoutStrategy;

		public BatchService(
			IGetWorkspaceEntityByIdStrategy<Batch> getWorkspaceEntityByIdStrategy,
			IGetAllWorkspaceEntitiesStrategy<Batch> getAllWorkspaceEntitiesStrategy,
			IBatchQueryStrategy batchQueryStrategy,
			IBatchAssignToUserStrategy batchAssignToUserStrategy,
			IBatchCheckinStrategy batchCheckinStrategy,
			IBatchCheckoutStrategy batchCheckoutStrategy)
		{
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getAllWorkspaceEntitiesStrategy = getAllWorkspaceEntitiesStrategy;
			_batchQueryStrategy = batchQueryStrategy;
			_batchAssignToUserStrategy = batchAssignToUserStrategy;
			_batchCheckinStrategy = batchCheckinStrategy;
			_batchCheckoutStrategy = batchCheckoutStrategy;
		}

		public Batch Get(int workspaceId, int entityId)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceId, entityId);

		public Batch[] GetAll(int workspaceId)
			=> _getAllWorkspaceEntitiesStrategy.GetAll(workspaceId);

		public Batch[] Query(int workspaceId, Expression<Func<Batch, object>> wherePropertySelector, object whereValue)
			=> _batchQueryStrategy.Query(workspaceId, wherePropertySelector, whereValue);

		public void AssignToUser(int workspaceId, int batchId, int userId)
			=> _batchAssignToUserStrategy.AssignToUser(workspaceId, batchId, userId);

		public void Checkin(int workspaceId, int batchId, bool isCompleted)
			=> _batchCheckinStrategy.Checkin(workspaceId, batchId, isCompleted);

		public void Checkout(int workspaceId, int batchId, int userId)
			=> _batchCheckoutStrategy.Checkout(workspaceId, batchId, userId);
	}
}
