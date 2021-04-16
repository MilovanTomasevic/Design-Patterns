import Foundation
import XCTest

protocol Stuff {}
class Foo : Stuff {}
class Bar : Stuff {}

func f(_ foo: Foo) {}
func f(_ bar: Bar) {}

func main()
{
  let s: Stuff = Foo()

  // this doesn't work
  // f(s)

  // dynamic in ObjC compatibility
}

main()