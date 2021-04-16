package com.activemesa.decorator;

import java.io.UnsupportedEncodingException;
import java.nio.charset.Charset;
import java.util.Locale;
import java.util.stream.IntStream;

class MagicString
{
  private String string;

  public MagicString(String string)
  {
    this.string = string;
  }

  public long getNumberOfVowels()
  {
    return string.chars()
      .mapToObj(c -> (char)c)
      .filter(c -> "aeiou".contains(c.toString()))
      .count();
  }

  @Override
  public String toString()
  {
    return string;
  }

  // delegated members

  public int length()
  {
    return string.length();
  }
  public boolean isEmpty()
  {
    return string.isEmpty();
  }
  public char charAt(int index)
  {
    return string.charAt(index);
  }
  public int codePointAt(int index)
  {
    return string.codePointAt(index);
  }
  public int codePointBefore(int index)
  {
    return string.codePointBefore(index);
  }
  public int codePointCount(int beginIndex, int endIndex)
  {
    return string.codePointCount(beginIndex, endIndex);
  }
  public int offsetByCodePoints(int index, int codePointOffset)
  {
    return string.offsetByCodePoints(index, codePointOffset);
  }
  public void getChars(int srcBegin, int srcEnd, char[] dst, int dstBegin)
  {
    string.getChars(srcBegin, srcEnd, dst, dstBegin);
  }
  @Deprecated(
    since = "1.1"
  )
  public void getBytes(int srcBegin, int srcEnd, byte[] dst, int dstBegin)
  {
    string.getBytes(srcBegin, srcEnd, dst, dstBegin);
  }
  public byte[] getBytes(String charsetName) throws UnsupportedEncodingException
  {
    return string.getBytes(charsetName);
  }
  public byte[] getBytes(Charset charset)
  {
    return string.getBytes(charset);
  }
  public byte[] getBytes()
  {
    return string.getBytes();
  }
  public boolean contentEquals(StringBuffer sb)
  {
    return string.contentEquals(sb);
  }
  public boolean contentEquals(CharSequence cs)
  {
    return string.contentEquals(cs);
  }
  public boolean equalsIgnoreCase(String anotherString)
  {
    return string.equalsIgnoreCase(anotherString);
  }
  public int compareTo(String anotherString)
  {
    return string.compareTo(anotherString);
  }
  public int compareToIgnoreCase(String str)
  {
    return string.compareToIgnoreCase(str);
  }
  public boolean regionMatches(int toffset, String other, int ooffset, int len)
  {
    return string.regionMatches(toffset, other, ooffset, len);
  }
  public boolean regionMatches(boolean ignoreCase, int toffset, String other, int ooffset, int len)
  {
    return string.regionMatches(ignoreCase, toffset, other, ooffset, len);
  }
  public boolean startsWith(String prefix, int toffset)
  {
    return string.startsWith(prefix, toffset);
  }
  public boolean startsWith(String prefix)
  {
    return string.startsWith(prefix);
  }
  public boolean endsWith(String suffix)
  {
    return string.endsWith(suffix);
  }
  public int indexOf(int ch)
  {
    return string.indexOf(ch);
  }
  public int indexOf(int ch, int fromIndex)
  {
    return string.indexOf(ch, fromIndex);
  }
  public int lastIndexOf(int ch)
  {
    return string.lastIndexOf(ch);
  }
  public int lastIndexOf(int ch, int fromIndex)
  {
    return string.lastIndexOf(ch, fromIndex);
  }
  public int indexOf(String str)
  {
    return string.indexOf(str);
  }
  public int indexOf(String str, int fromIndex)
  {
    return string.indexOf(str, fromIndex);
  }
  public int lastIndexOf(String str)
  {
    return string.lastIndexOf(str);
  }
  public int lastIndexOf(String str, int fromIndex)
  {
    return string.lastIndexOf(str, fromIndex);
  }
  public String substring(int beginIndex)
  {
    return string.substring(beginIndex);
  }
  public String substring(int beginIndex, int endIndex)
  {
    return string.substring(beginIndex, endIndex);
  }
  public CharSequence subSequence(int beginIndex, int endIndex)
  {
    return string.subSequence(beginIndex, endIndex);
  }
  public String concat(String str)
  {
    return string.concat(str);
  }
  public String replace(char oldChar, char newChar)
  {
    return string.replace(oldChar, newChar);
  }
  public boolean matches(String regex)
  {
    return string.matches(regex);
  }
  public boolean contains(CharSequence s)
  {
    return string.contains(s);
  }
  public String replaceFirst(String regex, String replacement)
  {
    return string.replaceFirst(regex, replacement);
  }
  public String replaceAll(String regex, String replacement)
  {
    return string.replaceAll(regex, replacement);
  }
  public String replace(CharSequence target, CharSequence replacement)
  {
    return string.replace(target, replacement);
  }
  public String[] split(String regex, int limit)
  {
    return string.split(regex, limit);
  }
  public String[] split(String regex)
  {
    return string.split(regex);
  }
  public static String join(CharSequence delimiter, CharSequence... elements)
  {
    return String.join(delimiter, elements);
  }
  public static String join(CharSequence delimiter, Iterable<? extends CharSequence> elements)
  {
    return String.join(delimiter, elements);
  }
  public String toLowerCase(Locale locale)
  {
    return string.toLowerCase(locale);
  }
  public String toLowerCase()
  {
    return string.toLowerCase();
  }
  public String toUpperCase(Locale locale)
  {
    return string.toUpperCase(locale);
  }
  public String toUpperCase()
  {
    return string.toUpperCase();
  }
  public String trim()
  {
    return string.trim();
  }
  public IntStream chars()
  {
    return string.chars();
  }
  public IntStream codePoints()
  {
    return string.codePoints();
  }
  public char[] toCharArray()
  {
    return string.toCharArray();
  }
  public static String format(String format, Object... args)
  {
    return String.format(format, args);
  }
  public static String format(Locale l, String format, Object... args)
  {
    return String.format(l, format, args);
  }
  public static String valueOf(Object obj)
  {
    return String.valueOf(obj);
  }
  public static String valueOf(char[] data)
  {
    return String.valueOf(data);
  }
  public static String valueOf(char[] data, int offset, int count)
  {
    return String.valueOf(data, offset, count);
  }
  public static String copyValueOf(char[] data, int offset, int count)
  {
    return String.copyValueOf(data, offset, count);
  }
  public static String copyValueOf(char[] data)
  {
    return String.copyValueOf(data);
  }
  public static String valueOf(boolean b)
  {
    return String.valueOf(b);
  }
  public static String valueOf(char c)
  {
    return String.valueOf(c);
  }
  public static String valueOf(int i)
  {
    return String.valueOf(i);
  }
  public static String valueOf(long l)
  {
    return String.valueOf(l);
  }
  public static String valueOf(float f)
  {
    return String.valueOf(f);
  }
  public static String valueOf(double d)
  {
    return String.valueOf(d);
  }
  public String intern()
  {
    return string.intern();
  }
}

class Demo
{
  public static void main(String [] args)
  {
    MagicString s = new MagicString("hello");
    System.out.println(s + " has "
      + s.getNumberOfVowels() + " vowels");
  }
}