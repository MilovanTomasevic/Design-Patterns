using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DotNetDesignPatternDemos.Annotations;

namespace DotNetDesignPatternDemos.Structural.Proxy
{
  // Model
  public class Person
  {
    public string FirstName;
    public string LastName;
  }
  
  // what if you need to update on changes?

  /// <summary>
  /// A wrapper around a <c>Person</c> that can be
  /// bound to UI controls.
  /// </summary>
  public class PersonViewModel 
    : INotifyPropertyChanged
  {
    private readonly Person person;

    public PersonViewModel(Person person)
    {
      this.person = person;
    }

    public string FirstName
    {
      get => person.FirstName;
      set
      {
        if (person.FirstName == value) return;
        person.FirstName = value;
        OnPropertyChanged();
      }
    }

    public string LastName
    {
      get => person.LastName;
      set
      {
        if (person.LastName == value) return;
        person.LastName = value;
        OnPropertyChanged();
      }
    }
    
    // Decorator functionality (augments original object)
    // Project two properties together into, e.g., an edit box.
    public string FullName
    {
      get => $"{FirstName} {LastName}".Trim();
      set
      {
        if (value == null)
        {
          FirstName = LastName = null;
          return;
        }
        var items = value.Split();
        if (items.Length > 0)
          FirstName = items[0]; // may cause npc
        if (items.Length > 1)
          LastName = items[1];
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged(
      [CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, 
        new PropertyChangedEventArgs(propertyName));
    }
  }
}