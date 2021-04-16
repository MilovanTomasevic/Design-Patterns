package chainofresponsibility

import "fmt"

type Creature struct {
  Name string
  Attack, Defense int
}

func (c *Creature) String() string {
  return fmt.Sprintf("%s (%d/%d)",
    c.Name, c.Attack, c.Defense)
}

func NewCreature(name string, attack int, defense int) *Creature {
  return &Creature{Name: name, Attack: attack, Defense: defense}
}

type Modifier interface {
  Add(m Modifier)
  Handle()
}

type CreatureModifier struct {
  creature *Creature
  next Modifier // singly linked list
}

func (c *CreatureModifier) Add(m Modifier) {
  if c.next != nil {
    c.next.Add(m)
  } else { c.next = m }
}

func (c *CreatureModifier) Handle() {
  if c.next != nil {
    c.next.Handle()
  }
}

func NewCreatureModifier(creature *Creature) *CreatureModifier {
  return &CreatureModifier{creature: creature}
}

type DoubleAttackModifier struct {
  CreatureModifier
}

func NewDoubleAttackModifier(
  c *Creature) *DoubleAttackModifier {
  return &DoubleAttackModifier{CreatureModifier{
    creature: c }}
}

type IncreasedDefenseModifier struct {
  CreatureModifier
}

func NewIncreasedDefenseModifier(
  c *Creature) *IncreasedDefenseModifier {
  return &IncreasedDefenseModifier{CreatureModifier{
    creature: c }}
}

func (i *IncreasedDefenseModifier) Handle() {
  if i.creature.Attack <= 2 {
    fmt.Println("Increasing",
      i.creature.Name, "\b's defense")
    i.creature.Defense++
  }
  i.CreatureModifier.Handle()
}

func (d *DoubleAttackModifier) Handle() {
  fmt.Println("Doubling", d.creature.Name,
    "attack...")
  d.creature.Attack *= 2
  d.CreatureModifier.Handle()
}

type NoBonusesModifier struct {
  CreatureModifier
}

func NewNoBonusesModifier(
  c *Creature) *NoBonusesModifier {
  return &NoBonusesModifier{CreatureModifier{
    creature: c }}
}

func (n *NoBonusesModifier) Handle() {
  // nothing here!
}

func main() {
  goblin := NewCreature("Goblin", 1, 1)
  fmt.Println(goblin.String())

  root := NewCreatureModifier(goblin)

  //root.Add(NewNoBonusesModifier(goblin))

  root.Add(NewDoubleAttackModifier(goblin))
  root.Add(NewIncreasedDefenseModifier(goblin))
  root.Add(NewDoubleAttackModifier(goblin))



  // eventually process the entire chain
  root.Handle()
  fmt.Println(goblin.String())
}