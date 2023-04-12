using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPTest
{
    public sealed class ExampleDiagnosticObserver : IObserver<DiagnosticListener>
    {
        void IObserver<DiagnosticListener>.OnNext(DiagnosticListener diagnosticListener)
        {
            Console.WriteLine(diagnosticListener.Name);
        }

        void IObserver<DiagnosticListener>.OnError(Exception error)
        { }

        void IObserver<DiagnosticListener>.OnCompleted()
        { }
    }
}
