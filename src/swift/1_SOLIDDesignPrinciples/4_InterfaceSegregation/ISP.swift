import Foundation

class Document
{

}


protocol Machine
{
  func print(d: Document)
  func scan(d: Document)
  func fax(d: Document)  
}

class MFP : Machine
{
  func print(d: Document)
  //
}

enum NoRequiredFunctionality : Error
{
  case doesNotFax
}

class OldFashionedPrinter: Machine
{
  func print(d: Document)
  {
    // ok
  }
  func fax(d: Document)
  {
    throw NoRequiredFunctionality.doesNotFax
  }
}

protocol Printer
{
  func print(d: Document)
}

protocol Scanner
{
  func scan(d: Document)
}

protocol Fax
{
  func fax(d: Document)
}

class OrdinaryPrinter : Printer
{
  func print(d: Document)
  {
    // ok
  }
}

class Photocopier : Printer, Scanner
{
  func print(d: Document)
  {
    // ok
  }

  func scan(d: Document)
  {

  }
}

protocol MultiFunctionDevice : Printer, Scanner, Fax
{
}

class MultiFunctionMachine : MultiFunctionDevice
{
  let printer: Printer
  let scanner: Scanner

  init(printer: Printer, scanner: Scanner)
  {
    self.printer = printer
    self.scanner = scanner
  }

  func print(d: Document)
  {
    printer.print(d) // Decorator
  }
}