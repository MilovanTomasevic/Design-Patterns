import Foundation

class Property<T: Equatable>
{
  private var _value: T

  public var value: T {
    get {
      return _value
    }
    set(value) {
      if (value == _value) {
        return
      }
      print("Setting value to \(value)")
      _value = value
    }
  }

  init(_ value: T)
  {
    self._value = value
  }
}

extension Property: Equatable {}

// compare the values of two properties
func ==<T>(lhs: Property<T>, rhs: Property<T>) -> Bool
{
  return lhs.value == rhs.value
}

class Creature
{
  // start with public
  private let _agility = Property<Int>(0)

  // no implicit conversions, so...
  var agility: Int {
    get { return _agility.value }
    set(value) { _agility.value = value }
  }
}

func main()
{
  let c = Creature()
  c.agility = 10

  print(c.agility)
}

main()