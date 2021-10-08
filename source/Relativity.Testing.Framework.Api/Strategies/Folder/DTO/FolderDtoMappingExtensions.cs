using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal static class FolderDtoMappingExtensions
	{
		internal static Folder DoMappingToFolder(this FolderDto dto)
		{
			return new Folder
			{
				ArtifactID = dto.ArtifactID,
				Name = dto.Name,
				ParentFolder = dto.ParentFolder,
				AccessControlListIsInherited = dto.AccessControlListIsInherited,
				HasChildren = dto.HasChildren,
				Selected = dto.Selected,
				Permissions = dto.Permissions.DoMappingToFolderPermission(),
				Children = dto.Children,
				SystemCreatedOn = dto.SystemCreatedOn,
				SystemLastModifiedOn = dto.SystemLastModifiedOn
			};
		}

		internal static FolderPermission DoMappingToFolderPermission(this FolderPermissionDto dto)
		{
			return new FolderPermission
			{
				Add = dto.Add,
				Delete = dto.Delete,
				Edit = dto.Edit,
				Secure = dto.Secure
			};
		}
	}
}
