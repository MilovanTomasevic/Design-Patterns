import Foundation

// hl modules should not depend on low-level; both should depend on abstractions
// abstractions should not depend on details; details should depend on abstractions

enum Relationship
{
  case parent
  case child
  case sibling
}

class Person
{
  var name = ""
  // dob, etc
  init(_ name: String)
  {
    self.name = name
  }
}

protocol RelationshipBrowser
{
  func findAllChildrenOf(_ name: String) -> [Person]
}

class Relationships : RelationshipBrowser
{
  var relations = [(Person, Relationship, Person)]()

  func addParentAndChild(_ p: Person, _ c: Person)
  {
    relations.append((p, Relationship.parent, c))
    relations.append((c, Relationship.child, p))
  }

  func findAllChildrenOf(_ name: String) -> [Person]
  {
    return relations.filter({$0.name == name && $1 == Relationship.parent && $2 != nil})
                    .map({$2})
  }
}

class Research
{
  init(_ relationships: Relationships)
  {
    // high-level: find all of job's children
    let relations = relationships.relations
    for r in relations where r.0.name == "John" && r.1 == Relationship.parent {
      print("John has a child called \(r.2.name)")
    }
  }

  init(_ browser: RelationshipBrowser)
  {
    for p in browser.findAllChildrenOf("John")
    {
      print("John has a child called \(p.name)")
    }
  }
}

func main()
{
  let parent = Person("John")
  let child1 = Person("Chris")
  let child2 = Person("Matt")

  var relationships = Relationships()
  relationships.addParentAndChild(parent, child1)
  relationships.addParentAndChild(parent, child2)

  let _ = Research(relationships)
}

main()