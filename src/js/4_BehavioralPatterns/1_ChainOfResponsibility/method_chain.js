class Creature {
  constructor(name, attack, defense) {
    this.name = name;
    this.attack = attack;
    this.defense = defense;
  }

  toString() {
    return `${this.name} (${this.attack}/${this.defense})`;
  }
}

class CreatureModifier
{
  constructor(creature)
  {
    this.creature = creature;
    this.next = null;
  }

  add(modifier)
  {
    if (this.next) this.next.add(modifier);
    else this.next = modifier;
  }

  handle()
  {
    if (this.next) this.next.handle();
  }
}

class NoBonusesModifier extends CreatureModifier
{
  constructor(creature)
  {
    super(creature);
  }

  handle()
  {
    console.log('No bonuses for you!');
  }
}

class DoubleAttackModifier extends CreatureModifier
{
  constructor(creature)
  {
    super(creature);
  }

  handle()
  {
    console.log(`Doubling ${this.creature.name}'s attack`);
    this.creature.attack *= 2;
    super.handle();
  }
}

class IncreaseDefenseModifier extends CreatureModifier
{
  constructor(creature)
  {
    super(creature);
  }

  handle() {
    if (this.creature.attack <= 2)
    {
      console.log(`Increasing ${this.creature.name}'s defense`);
      this.creature.defense++;
    }
    super.handle();
  }
}

let goblin = new Creature('Goblin', 1, 1);
console.log(goblin.toString());

let root = new CreatureModifier(goblin);

//root.add(new NoBonusesModifier(goblin));

root.add(new DoubleAttackModifier(goblin));
//root.add(new DoubleAttackModifier(goblin));

root.add(new IncreaseDefenseModifier(goblin));

// eventually...
root.handle();
console.log(goblin.toString());