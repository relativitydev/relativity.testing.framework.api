using System.Linq;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal static class DataTableExtensions
	{
		public static System.Data.DataTable CsvToDataTable(System.IO.Stream stream)
		{
			System.Data.DataTable dt = new System.Data.DataTable();

			using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
			{
				System.Text.StringBuilder lineBuilder = new System.Text.StringBuilder();

				string line;
				int quoteCount = 0;
				do
				{
					line = reader.ReadLine();

					if (lineBuilder.Length > 0)
					{
						lineBuilder.AppendLine();
					}

					lineBuilder.Append(line);

					if (!string.IsNullOrEmpty(line))
					{
						quoteCount += line.Count(x => x == '\"');
					}
				}
				while (!reader.EndOfStream && quoteCount % 2 == 1);

				string[] columns = SplitCsvString(lineBuilder.ToString());

				foreach (string column in columns)
					dt.Columns.Add(column);

				while (!reader.EndOfStream)
				{
					lineBuilder.Clear();
					quoteCount = 0;

					do
					{
						line = reader.ReadLine();
						if (lineBuilder.Length > 0)
						{
							lineBuilder.AppendLine();
						}

						lineBuilder.Append(line);
						if (!string.IsNullOrEmpty(line))
						{
							quoteCount += line.Count(x => x == '\"');
						}
					}
					while (!reader.EndOfStream && quoteCount % 2 == 1);

					string[] values = SplitCsvString(lineBuilder.ToString());
					dt.LoadDataRow(values, true);
				}
			}

			return dt;
		}

		private static string[] SplitCsvString(string input)
		{
			const string CSV_REGEX = ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))";

			if (!string.IsNullOrEmpty(input))
			{
				string[] values = System.Text.RegularExpressions.Regex.Split(input, CSV_REGEX);

				for (int i = 0; i <= values.Length - 1; i++)
				{
					if (!string.IsNullOrEmpty(values[i]))
					{
						values[i] = TrimIfQuoted(values[i]).Replace("\"\"", "\"");
					}
				}

				return values;
			}
			else
			{
				return new string[] { };
			}
		}

		private static string TrimIfQuoted(string input)
		{
			return !string.IsNullOrEmpty(input) && input.First() == '\"' && input.Last() == '\"' ? input.Substring(1, input.Length - 2) : input;
		}
	}
}
