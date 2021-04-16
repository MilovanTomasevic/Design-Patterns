package main

import (
  "fmt"
  "strings"
)

const (
  indentSize = 2
)

type HtmlElement struct {
  name, text string
  elements []HtmlElement
}

func (e *HtmlElement) String() string {
  return e.string(0)
}

func (e *HtmlElement) string(indent int) string {
  sb := strings.Builder{}
  i := strings.Repeat(" ", indentSize * indent)
  sb.WriteString(fmt.Sprintf("%s<%s>\n",
    i, e.name))
  if len(e.text) > 0 {
    sb.WriteString(strings.Repeat(" ",
      indentSize * (indent + 1)))
    sb.WriteString(e.text)
    sb.WriteString("\n")
  }

  for _, el := range e.elements {
    sb.WriteString(el.string(indent+1))
  }
  sb.WriteString(fmt.Sprintf("%s</%s>\n",
    i, e.name))
  return sb.String()
}

type HtmlBuilder struct {
  rootName string
  root HtmlElement
}

func NewHtmlBuilder(rootName string) *HtmlBuilder {
  b := HtmlBuilder{rootName,
    HtmlElement{rootName, "", []HtmlElement{}}}
  return &b
}

func (b *HtmlBuilder) String() string {
  return b.root.String()
}

func (b *HtmlBuilder) AddChild(
  childName, childText string) {
  e := HtmlElement{childName, childText, []HtmlElement{}}
  b.root.elements = append(b.root.elements, e)
}

func (b *HtmlBuilder) AddChildFluent(
  childName, childText string) *HtmlBuilder {
  e := HtmlElement{childName, childText, []HtmlElement{}}
  b.root.elements = append(b.root.elements, e)
  return b
}

func main() {
  hello := "hello"
  sb := strings.Builder{}
  sb.WriteString("<p>")
  sb.WriteString(hello)
  sb.WriteString("</p>")
  fmt.Printf("%s\n", sb.String())

  words := []string{"hello", "world"}
  sb.Reset()
  // <ul><li>...</li><li>...</li><li>...</li></ul>'
  sb.WriteString("<ul>")
  for _, v := range words {
    sb.WriteString("<li>")
    sb.WriteString(v)
    sb.WriteString("</li>")
  }
  sb.WriteString("</ul>")
  fmt.Println(sb.String())

  b := NewHtmlBuilder("ul")
  b.AddChildFluent("li", "hello").
    AddChildFluent("li", "world")
  fmt.Println(b.String())
}