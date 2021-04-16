package state

import (
  "bufio"
  "fmt"
  "os"
  "strings"
)

type State int

const (
  Locked State = iota
  Failed
  Unlocked
)

func main() {
  code := "1234"
  state := Locked
  entry := strings.Builder{}

  for {
    switch state {
    case Locked:
      // only reads input when you press Return
      r, _, _ := bufio.NewReader(os.Stdin).ReadRune()
      entry.WriteRune(r)

      if entry.String() == code {
        state = Unlocked
        break
      }

      if strings.Index(code, entry.String()) != 0 {
        // code is wrong
        state = Failed
      }
    case Failed:
      fmt.Println("FAILED")
      entry.Reset()
      state = Locked
    case Unlocked:
      fmt.Println("UNLOCKED")
      return
    }
  }
}