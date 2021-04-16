import Foundation

class Creature : CustomStringConvertible
{
  var name: String
  var attack: Int
  var defense: Int

  init(name: String, attack: Int, defense: Int)
  {
    self.name = name
    self.attack = attack
    self.defense = defense
  }

  var description : String
  {
    return "Name: \(name), A = \(attack), D = \(defense)"
  }
}

class CreatureModifier
{
  let creature: Creature
  var next: CreatureModifier?

  init(creature: Creature)
  {
    self.creature = creature
  }

  func add(_ cm: CreatureModifier)
  {
    if (next != nil) {
      next!.add(cm)
    } else {
      next = cm
    }
  }

  func handle() {
    next?.handle()
  }
}

class DoubleAttackModifier : CreatureModifier
{
  override func handle()
  {
    print("Doubling \(creature.name)'s attack")
    creature.attack *= 2
    super.handle()
  }
}

class IncreaseDefenseModifier : CreatureModifier
{
  override func handle()
  {
    print("Increasing \(creature.name)'s defense")
    creature.defense += 3;
    super.handle()
  }
}

class NoBonusesModifier : CreatureModifier
{
  override func handle()
  {
    // nothing here
    print("No bonuses for you!")
    // we don't call super.handle()
  }
}

func main()
{
  let goblin = Creature(name: "Goblin", attack: 2, defense: 2)
  print(goblin)

  let root = CreatureModifier(creature: goblin)

  // later
  root.add(NoBonusesModifier(creature: goblin))

  print("Let's double the goblin's attack")
  root.add(DoubleAttackModifier(creature: goblin))

  print("Let's increase goblin's defense")
  root.add(IncreaseDefenseModifier(creature: goblin))

  root.handle()
  print(goblin)
}

main()