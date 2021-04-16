package visitor

import (
  "fmt"
  "strings"
)

type Expression interface {
  // nothing here!
}

type DoubleExpression struct {
  value float64
}

type AdditionExpression struct {
  left, right Expression
}

func Print(e Expression, sb *strings.Builder) {
  if de, ok := e.(*DoubleExpression); ok {
    sb.WriteString(fmt.Sprintf("%g", de.value))
  } else if ae, ok := e.(*AdditionExpression); ok {
    sb.WriteString("(")
    Print(ae.left, sb)
    sb.WriteString("+")
    Print(ae.right, sb)
    sb.WriteString(")")
  }

  // breaks OCP
  // will work incorrectly on missing case
}

func main() {
  // 1+(2+3)
  e := &AdditionExpression{
    &DoubleExpression{1},
    &AdditionExpression{
      left:  &DoubleExpression{2},
      right: &DoubleExpression{3},
    },
  }
  sb := strings.Builder{}
  Print(e, &sb)
  fmt.Println(sb.String())
}