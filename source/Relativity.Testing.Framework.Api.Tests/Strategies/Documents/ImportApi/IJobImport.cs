using System.Collections;
using kCura.Relativity.DataReaderClient;

namespace Relativity.Testing.Framework.Api.Tests.Strategies.Documents.ImportApi
{
    public delegate void OnErrorEventHandler(IDictionary row);

    public delegate void OnMessageEventHandler(Status status);

    public interface IJobImport
    {
        void RegisterEventHandlers();

        void Execute();
    }
}
