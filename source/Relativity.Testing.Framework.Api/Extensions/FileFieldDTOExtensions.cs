using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Extensions
{
	/// <summary>
	/// Provides a set of extension methods for <see cref="FileFieldDTO"/>.
	/// </summary>
	internal static class FileFieldDTOExtensions
	{
		/// <summary>
		/// Validates FileFieldDTO FileName property(not null and not empty or whitespace).
		/// </summary>
		/// <param name="fileFieldDto">The FileFieldDTO to chceck.</param>
		/// <exception cref="ArgumentException">The fileFieldDto FileName is invalid.</exception>
		public static void ValidateFileName(this FileFieldDTO fileFieldDto)
		{
			if (string.IsNullOrWhiteSpace(fileFieldDto.FileName))
			{
				throw new ArgumentException("File Field DTO must contain not null and not empty FileName.");
			}
		}

		/// <summary>
		/// Validates FileFieldDTO Field property (not null and Field Artifact ID or Name is filled)
		/// and ObjectRef property (not null and with positive Artifact ID).
		/// </summary>
		/// <param name="fileFieldDto">The FileFieldDTO to chceck.</param>
		/// <exception cref="ArgumentException">The fileFieldDto Field or ObjectRef is invalid.</exception>
		public static void ValidateFieldAndObjectRef(this FileFieldDTO fileFieldDto)
		{
			if (!fileFieldDto.IsFileFieldValid())
			{
				throw new ArgumentException("File Field DTO must contain File with Artifact ID or Name.");
			}

			if (!fileFieldDto.IsObjectRefValid())
			{
				throw new ArgumentException("File Field DTO must contain ObjectRef with Artifact ID");
			}
		}

		private static bool IsFileFieldValid(this FileFieldDTO fileFieldDto)
		{
			return fileFieldDto.Field != null && (fileFieldDto.Field.ArtifactID > 0 || !string.IsNullOrWhiteSpace(fileFieldDto.Field.Name));
		}

		private static bool IsObjectRefValid(this FileFieldDTO fileFieldDto)
		{
			return fileFieldDto.ObjectRef != null && fileFieldDto.ObjectRef.ArtifactID > 0;
		}

		/// <summary>
		/// Validates FileFieldDTO Field (not null).
		/// </summary>
		/// <param name="fileFieldDto">The FileFieldDTO to chceck.</param>
		/// <exception cref="ArgumentException">The fileFieldDto is null.</exception>
		public static void ValidateFileFieldDTO(this FileFieldDTO fileFieldDto)
		{
			if (fileFieldDto == null)
			{
				throw new ArgumentException("File Field DTO cannot be null.");
			}
		}

		/// <summary>
		/// Validates FileFieldDTO FileStream property (not null and supports writing).
		/// </summary>
		/// <param name="fileFieldDto">The FileFieldDTO to chceck.</param>
		/// <exception cref="ArgumentException">The fileFieldDto FileStream is invalid.</exception>
		public static void ValidatFileStream(this FileFieldDTO fileFieldDto)
		{
			if (!fileFieldDto.IsFileStreamValid())
			{
				throw new ArgumentException("File Field DTO must contain FileStream that supports writing.");
			}
		}

		private static bool IsFileStreamValid(this FileFieldDTO fileFieldDto)
		{
			return fileFieldDto.FileStream != null && fileFieldDto.FileStream.CanWrite;
		}
	}
}
