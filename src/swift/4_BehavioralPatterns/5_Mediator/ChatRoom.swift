import Foundation
import XCTest

class Person
{
  var name: String
  var room: ChatRoom? = nil // not in a room
  private var chatLog = [String]()

  init(_ name: String)
  {
    self.name = name
  }

  func receive(sender: String, message: String)
  {
    let s = "\(sender): `\(message)`"
    print("[\(name)'s chat session] \(s)")
    chatLog.append(s)
  }

  func say(_ message: String)
  {
    room?.broadcast(sender: name, message: message)
  }

  func pm(to target: String, message: String)
  {
    room?.message(sender: name, destination: target, message: message)
  }
}

class ChatRoom
{
  private var people = [Person]()

  func broadcast(sender: String, message: String)
  {
    for p in people
    {
      if p.name != sender
      {
        p.receive(sender: sender, message: message)
      }
    }
  }

  func join(_ p: Person)
  {
    let joinMsg = "\(p.name) joins the chat"
    broadcast(sender: "room", message: joinMsg)
    p.room = self
    people.append(p)
  }

  func message(sender: String, destination: String, message: String)
  {
    people.first{ $0.name == destination}?.receive(sender: sender, message: message)
  }
}

func main()
{
  let room = ChatRoom()

  let john = Person("John")
  let jane = Person("Jane")

  room.join(john)
  room.join(jane)

  john.say("hi room")
  jane.say("oh, hey john")

  let simon = Person("Simon")
  room.join(simon)
  simon.say("hi everyone!")

  jane.pm(to: "Simon", message: "glad you could join us!")
}

main()