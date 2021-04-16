import Foundation
import XCTest

class Creature : Sequence
{
  var stats = [Int](repeating: 0, count: 3)

  private let _strength = 0
  private let _agility = 1
  private let _intelligence = 2

  // start with var strength: Int = 0
  var strength: Int
  {
    get { return stats[_strength] }
    set(value) { stats[_strength] = value }
  }

  var agility: Int
  {
    get { return stats[_agility] }
    set(value) { stats[_agility] = value }
  }

  var intelligence: Int
  {
    get { return stats[_intelligence] }
    set(value) { stats[_intelligence] = value }
  }

  var averageStat: Int
  {
    return stats.reduce(0, +) / stats.count
  }

  subscript(index: Int) -> Int
  {
    get { return stats[index] }
    set(value) { stats[index] = value }
  }

  func makeIterator() -> IndexingIterator<Array<Int>>
  {
    return IndexingIterator(_elements: stats)
  }
}

func main()
{
  let c = Creature()
  c.strength = 10
  c.agility = 15
  c.intelligence = 11

  print("strength = \(c.strength)")
  print("average stat = \(c.averageStat)")

  for s in c // no need for 'in c.stats'
  {
    print(s)
  }
}

main()