#include "Headers.hpp"
#include "Observer.hpp"
#include "SaferObservable.hpp"

class Person : public SaferObservable<Person>
{
  int age{0};
public:
  Person(){}
  Person(int age) : age(age) {}

  int get_age() const
  {
    return age;
  }

  void set_age(int age)
  {
    if (this->age == age) return;

    auto old_can_vote = get_can_vote();
    this->age = age;
    notify(*this, "age");

    if (old_can_vote != get_can_vote())
      notify(*this, "can_vote");
  }

  bool get_can_vote() const {
    return age >= 16;
  }
};

// observer & observable

struct ConsolePersonObserver
  : public Observer<Person>
{
private:
  void field_changed(Person &source, const std::string &field_name) override
  {
    cout << "Person's " << field_name << " has changed to ";
    if (field_name == "age") cout << source.get_age();
    if (field_name == "can_vote")
      cout << boolalpha << source.get_can_vote();
    cout << ".\n";
  }
};

struct TrafficAdministration : Observer<Person>
{
  void field_changed(Person &source, const std::string &field_name) override
  {
    if (field_name == "age")
    {
      if (source.get_age() < 17)
        cout << "Whoa there, you're not old enough to drive!\n";
      else
      {
        cout << "Oh, ok, we no longer care!\n";
        source.unsubscribe(*this);
      }
    }
  }
};

int main(int ac, char* av[])
{
  Person p;
  TrafficAdministration ta;
  p.subscribe(ta);

  p.set_age(15);
  p.set_age(16);
  try
  {
    p.set_age(17);
  }
  catch (const std::exception& e)
  {
    cout << "Oops, " << e.what() << "\n";
  }

  return 0;
}