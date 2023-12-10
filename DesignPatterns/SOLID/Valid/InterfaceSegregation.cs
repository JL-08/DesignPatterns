using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SOLID.Valid
{
    // Interface Segregation Principle
    // - A class should not be forced to implement interfaces it does not use
    public class InterfaceSegregation
    {
    }

    public class Document
    {

    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public interface IPrinter
    {
        void Print(Document d);
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }

    public interface IMultiFunctionDevice : IScanner, IPrinter
    {
        // additional methods
    }

    class MultiFunctionMachine : IMultiFunctionDevice
    {
        private IPrinter printer;
        private IScanner scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            this.printer = printer;
            this.scanner = scanner;
        }

        // delegate calls to the calls of IPrinter and IScanner
        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scanner.Scan(d);
        }
    }
}
