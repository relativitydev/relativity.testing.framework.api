using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	public class OcrProfileCreateStrategy : CreateWorkspaceEntityStrategy<OcrProfile>
	{
		private readonly IObjectService _objectService;
		private readonly IChoiceService _choiceService;

		public OcrProfileCreateStrategy(
			IObjectService objectService,
			IChoiceService choiceService)
		{
			_objectService = objectService;
			_choiceService = choiceService;
		}

		protected override OcrProfile DoCreate(int workspaceId, OcrProfile entity)
		{
			Validate(workspaceId, entity);
			return _objectService.Create(workspaceId, entity);
		}

		private void Validate(int workspaceId, OcrProfile entity)
		{
			const string ocrProfileObjectTypeName = "OCR Profile";

			if (entity.Languages == null)
			{
				var language = _choiceService.GetAll(workspaceId, ocrProfileObjectTypeName, "Languages").FirstOrDefault(x => x.Name == "English");

				if (language == null)
				{
					language = _choiceService.GetAll(workspaceId, ocrProfileObjectTypeName, "Language").FirstOrDefault(x => x.Name == "English");
				}

				entity.Languages = new List<NamedArtifact> { language };
			}
			else
			{
				var languages = new List<NamedArtifact>();

				foreach (var language in entity.Languages)
				{
					if (language.ArtifactID != 0)
					{
						languages.Add(language);
						continue;
					}

					var lang = _choiceService.GetAll(workspaceId, ocrProfileObjectTypeName, "Languages").FirstOrDefault(x => x.Name == language.Name);

					if (lang == null)
					{
						lang = _choiceService.GetAll(workspaceId, ocrProfileObjectTypeName, "Language").FirstOrDefault(x => x.Name == language.Name);
					}

					languages.Add(lang);
				}

				entity.Languages = languages;
			}

			if (entity.Accuracy == null)
			{
				var accuracy = _choiceService.GetAll(workspaceId, ocrProfileObjectTypeName, "Accuracy").First(x => x.Name == "Low (Fastest Speed)");

				entity.Accuracy = accuracy;
			}
			else
			{
				if (entity.Accuracy.ArtifactID == 0)
				{
					var accuracy = _choiceService.GetAll(workspaceId, ocrProfileObjectTypeName, "Accuracy").First(x => x.Name == entity.Accuracy.Name);

					entity.Accuracy = accuracy;
				}
			}

			if (entity.Name == null)
			{
				entity.Name = Randomizer.GetString("entity {0}");
			}
		}
	}
}
