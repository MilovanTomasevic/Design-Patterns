using System;
using System.Runtime.Serialization;
using System.Text;
using static System.Console;

namespace DotNetDesignPatternDemos.Structural.AdapterDecorator
{
  public class MyStringBuilder
  {
    StringBuilder sb = new StringBuilder();

    //=============================================

    public static implicit operator MyStringBuilder(string s)
    {
      var msb = new MyStringBuilder();
      msb.sb.Append(s);
      return msb;
    }

    public static MyStringBuilder operator +(MyStringBuilder msb, string s)
    {
      msb.Append(s);
      return msb;
    }

    public override string ToString()
    {
      return sb.ToString();
    }

    //=============================================

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      ((ISerializable) sb).GetObjectData(info, context);
    }

    public int EnsureCapacity(int capacity)
    {
      return sb.EnsureCapacity(capacity);
    }

    public string ToString(int startIndex, int length)
    {
      return sb.ToString(startIndex, length);
    }

    public StringBuilder Clear()
    {
      return sb.Clear();
    }

    public StringBuilder Append(char value, int repeatCount)
    {
      return sb.Append(value, repeatCount);
    }

    public StringBuilder Append(char[] value, int startIndex, int charCount)
    {
      return sb.Append(value, startIndex, charCount);
    }

    public StringBuilder Append(string value)
    {
      return sb.Append(value);
    }

    public StringBuilder Append(string value, int startIndex, int count)
    {
      return sb.Append(value, startIndex, count);
    }

    public StringBuilder AppendLine()
    {
      return sb.AppendLine();
    }

    public StringBuilder AppendLine(string value)
    {
      return sb.AppendLine(value);
    }

    public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
    {
      sb.CopyTo(sourceIndex, destination, destinationIndex, count);
    }

    public StringBuilder Insert(int index, string value, int count)
    {
      return sb.Insert(index, value, count);
    }

    public StringBuilder Remove(int startIndex, int length)
    {
      return sb.Remove(startIndex, length);
    }

    public StringBuilder Append(bool value)
    {
      return sb.Append(value);
    }

    public StringBuilder Append(sbyte value)
    {
      return sb.Append(value);
    }

    public StringBuilder Append(byte value)
    {
      return sb.Append(value);
    }

    public StringBuilder Append(char value)
    {
      return sb.Append(value);
    }

    public StringBuilder Append(short value)
    {
      return sb.Append(value);
    }

    public StringBuilder Append(int value)
    {
      return sb.Append(value);
    }

    public StringBuilder Append(long value)
    {
      return sb.Append(value);
    }

    public StringBuilder Append(float value)
    {
      return sb.Append(value);
    }

    public StringBuilder Append(double value)
    {
      return sb.Append(value);
    }

    public StringBuilder Append(decimal value)
    {
      return sb.Append(value);
    }

    public StringBuilder Append(ushort value)
    {
      return sb.Append(value);
    }

    public StringBuilder Append(uint value)
    {
      return sb.Append(value);
    }

    public StringBuilder Append(ulong value)
    {
      return sb.Append(value);
    }

    public StringBuilder Append(object value)
    {
      return sb.Append(value);
    }

    public StringBuilder Append(char[] value)
    {
      return sb.Append(value);
    }

    public StringBuilder Insert(int index, string value)
    {
      return sb.Insert(index, value);
    }

    public StringBuilder Insert(int index, bool value)
    {
      return sb.Insert(index, value);
    }

    public StringBuilder Insert(int index, sbyte value)
    {
      return sb.Insert(index, value);
    }

    public StringBuilder Insert(int index, byte value)
    {
      return sb.Insert(index, value);
    }

    public StringBuilder Insert(int index, short value)
    {
      return sb.Insert(index, value);
    }

    public StringBuilder Insert(int index, char value)
    {
      return sb.Insert(index, value);
    }

    public StringBuilder Insert(int index, char[] value)
    {
      return sb.Insert(index, value);
    }

    public StringBuilder Insert(int index, char[] value, int startIndex, int charCount)
    {
      return sb.Insert(index, value, startIndex, charCount);
    }

    public StringBuilder Insert(int index, int value)
    {
      return sb.Insert(index, value);
    }

    public StringBuilder Insert(int index, long value)
    {
      return sb.Insert(index, value);
    }

    public StringBuilder Insert(int index, float value)
    {
      return sb.Insert(index, value);
    }

    public StringBuilder Insert(int index, double value)
    {
      return sb.Insert(index, value);
    }

    public StringBuilder Insert(int index, decimal value)
    {
      return sb.Insert(index, value);
    }

    public StringBuilder Insert(int index, ushort value)
    {
      return sb.Insert(index, value);
    }

    public StringBuilder Insert(int index, uint value)
    {
      return sb.Insert(index, value);
    }

    public StringBuilder Insert(int index, ulong value)
    {
      return sb.Insert(index, value);
    }

    public StringBuilder Insert(int index, object value)
    {
      return sb.Insert(index, value);
    }

    public StringBuilder AppendFormat(string format, object arg0)
    {
      return sb.AppendFormat(format, arg0);
    }

    public StringBuilder AppendFormat(string format, object arg0, object arg1)
    {
      return sb.AppendFormat(format, arg0, arg1);
    }

    public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
    {
      return sb.AppendFormat(format, arg0, arg1, arg2);
    }

    public StringBuilder AppendFormat(string format, params object[] args)
    {
      return sb.AppendFormat(format, args);
    }

    public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0)
    {
      return sb.AppendFormat(provider, format, arg0);
    }

    public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0,
      object arg1)
    {
      return sb.AppendFormat(provider, format, arg0, arg1);
    }

    public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0,
      object arg1, object arg2)
    {
      return sb.AppendFormat(provider, format, arg0, arg1, arg2);
    }

    public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
    {
      return sb.AppendFormat(provider, format, args);
    }

    public StringBuilder Replace(string oldValue, string newValue)
    {
      return sb.Replace(oldValue, newValue);
    }

    public bool Equals(StringBuilder sb)
    {
      return this.sb.Equals(sb);
    }

    public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count)
    {
      return sb.Replace(oldValue, newValue, startIndex, count);
    }

    public StringBuilder Replace(char oldChar, char newChar)
    {
      return sb.Replace(oldChar, newChar);
    }

    public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
    {
      return sb.Replace(oldChar, newChar, startIndex, count);
    }

    public int Capacity
    {
      get => sb.Capacity;
      set => sb.Capacity = value;
    }

    public int MaxCapacity => sb.MaxCapacity;

    public int Length
    {
      get => sb.Length;
      set => sb.Length = value;
    }

    public char this[int index]
    {
      get => sb[index];
      set => sb[index] = value;
    }
  }

  public class Demo
  {
    static void Main(string[] args)
    {
      MyStringBuilder s = "hello ";
      s += "world"; // will work even without op+ in MyStringBuilder
                    // why? you figure it out!
      WriteLine(s);
    }
  }
}