package mediator

import "fmt"

type Person struct {
  Name string
  Room *ChatRoom
  chatLog []string
}

func NewPerson(name string) *Person {
  return &Person{Name: name}
}

func (p *Person) Receive(sender, message string) {
  s := fmt.Sprintf("%s: '%s'", sender, message)
  fmt.Printf("[%s's chat session] %s\n", p.Name, s)
  p.chatLog = append(p.chatLog, s)
}

func (p *Person) Say(message string) {
  p.Room.Broadcast(p.Name, message)
}

func (p *Person) PrivateMessage(who, message string) {
  p.Room.Message(p.Name, who, message)
}

type ChatRoom struct {
  people []*Person
}

func (c *ChatRoom) Broadcast(source, message string) {
  for _, p := range c.people {
    if p.Name != source {
      p.Receive(source, message)
    }
  }
}

func (c *ChatRoom) Join(p *Person) {
  joinMsg := p.Name + " joins the chat"
  c.Broadcast("Room", joinMsg)

  p.Room = c
  c.people = append(c.people, p)
}

func (c *ChatRoom) Message(src, dst, msg string) {
  for _, p := range c.people {
    if p.Name == dst {
      p.Receive(src, msg)
    }
  }
}

func main() {
  room := ChatRoom{}

  john := NewPerson("John")
  jane := NewPerson("Jane")

  room.Join(john)
  room.Join(jane)

  john.Say("hi room")
  jane.Say("oh, hey john")

  simon := NewPerson("Simon")
  room.Join(simon)
  simon.Say("hi everyone!")

  jane.PrivateMessage("Simon", "glad you could join us!")
}