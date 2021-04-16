class ChiefExecutiveOfficer
{
  get name() { return ChiefExecutiveOfficer._name; }
  set name(value) { ChiefExecutiveOfficer._name = value; }

  get age() { return ChiefExecutiveOfficer._age; }
  set age(value) { ChiefExecutiveOfficer._age = value; }

  toString()
  {
    return `CEO's name is ${this.name} ` +
      `and he is ${this.age} years old.`;
  }
}
ChiefExecutiveOfficer._age = undefined;
ChiefExecutiveOfficer._name = undefined;

let ceo = new ChiefExecutiveOfficer();
ceo.name = 'Adam Smith';
ceo.age = 55;

let ceo2 = new ChiefExecutiveOfficer();
ceo2.name = 'John Gold';
ceo2.age = 66;

console.log(ceo.toString());
console.log(ceo2.toString());