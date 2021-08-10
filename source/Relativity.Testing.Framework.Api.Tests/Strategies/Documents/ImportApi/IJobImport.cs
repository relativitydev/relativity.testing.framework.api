using System.Collections;
using kCura.Relativity.DataReaderClient;

namespace Relativity.Testing.Framework.Api.Tests.Strategies.Documents.ImportApi
{
    public delegate void OnErrorEventHandler(IDictionary row);

    public delegate void OnMessageEventHandler(Status status);

    public interface IJobImport
    {
        event IImportNotifier.OnCompleteEventHandler OnComplete;

        event IImportNotifier.OnFatalExceptionEventHandler OnFatalException;

        event IImportNotifier.OnProgressEventHandler OnProgress;

        event IImportNotifier.OnProcessProgressEventHandler OnProcessProgress;

        event OnErrorEventHandler OnError;

        event OnMessageEventHandler OnMessage;

        void RegisterEventHandlers();

        void Execute();
    }
}
