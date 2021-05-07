using System;
using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class WorkspaceService : IWorkspaceService
	{
		private readonly ICreateStrategy<Workspace> _createStrategy;

		private readonly IDeleteByIdStrategy<Workspace> _deleteByIdStrategy;

		private readonly IGetByIdStrategy<Workspace> _getByIdStrategy;

		private readonly IGetByNameStrategy<Workspace> _getByNameStrategy;

		private readonly IGetAllStrategy<Workspace> _getAllStrategy;

		private readonly IGetAllByClientNameStrategy<Workspace> _getAllByClientNameStrategy;

		private readonly IExistsByIdStrategy<Workspace> _existsByIdStrategy;

		private readonly IDocumentsImportGeneratedStrategy _documentsImportGeneratedStrategy;

		public WorkspaceService(
			ICreateStrategy<Workspace> createStrategy,
			IDeleteByIdStrategy<Workspace> deleteByIdStrategy,
			IGetByIdStrategy<Workspace> getByIdStrategy,
			IGetByNameStrategy<Workspace> getByNameStrategy,
			IGetAllStrategy<Workspace> getAllStrategy,
			IGetAllByClientNameStrategy<Workspace> getAllByClientNameStrategy,
			IExistsByIdStrategy<Workspace> existsByIdStrategy,
			IDocumentsImportGeneratedStrategy documentsImportGeneratedStrategy)
		{
			_createStrategy = createStrategy;
			_deleteByIdStrategy = deleteByIdStrategy;
			_getByIdStrategy = getByIdStrategy;
			_getByNameStrategy = getByNameStrategy;
			_getAllStrategy = getAllStrategy;
			_getAllByClientNameStrategy = getAllByClientNameStrategy;
			_existsByIdStrategy = existsByIdStrategy;
			_documentsImportGeneratedStrategy = documentsImportGeneratedStrategy;
		}

		public Workspace Create(Workspace entity)
			=> _createStrategy.Create(entity);

		public Workspace CreateWithDocs(Workspace entity, int numberOfDocuments = 10)
		{
			Workspace workspaceCreated = _createStrategy.Create(entity);

			if (numberOfDocuments < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(numberOfDocuments), "Documents count must be positive.");
			}

			_documentsImportGeneratedStrategy.Import(workspaceCreated.ArtifactID, numberOfDocuments);

			return workspaceCreated;
		}

		public void Delete(int id)
			=> _deleteByIdStrategy.Delete(id);

		public Workspace Get(int id)
			=> _getByIdStrategy.Get(id);

		public Workspace Get(string name)
			=> _getByNameStrategy.Get(name);

		public Workspace[] GetAll()
			=> _getAllStrategy.GetAll();

		public IEnumerable<Workspace> GetAllByClientName(string clientName)
			=> _getAllByClientNameStrategy.GetAllByClientName(clientName);

		public bool Exists(int id)
			=> _existsByIdStrategy.Exists(id);
	}
}
