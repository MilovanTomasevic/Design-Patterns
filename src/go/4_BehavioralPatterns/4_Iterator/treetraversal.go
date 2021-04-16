package iterator

import "fmt"

type Node struct {
  Value int
  left, right, parent *Node
}

func NewNode(value int, left *Node, right *Node) *Node {
  n := &Node{Value: value, left: left, right: right}
  left.parent = n
  right.parent = n
  return n
}

func NewTerminalNode(value int) *Node {
  return &Node{Value:value}
}

type InOrderIterator struct {
  Current *Node
  root *Node
  returnedStart bool
}

func NewInOrderIterator(root *Node) *InOrderIterator {
  i := &InOrderIterator{
    Current:       root,
    root:          root,
    returnedStart: false,
  }
  // move to the leftmost element
  for ;i.Current.left != nil; {
    i.Current = i.Current.left
  }
  return i
}

func (i *InOrderIterator) Reset() {
  i.Current = i.root
  i.returnedStart = false
}

func (i *InOrderIterator) MoveNext() bool {
  if i.Current == nil { return false }
  if !i.returnedStart {
    i.returnedStart = true
    return true // can use first element
  }

  if i.Current.right != nil {
    i.Current = i.Current.right
    for ;i.Current.left != nil; {
      i.Current = i.Current.left
    }
    return true
  } else {
    p := i.Current.parent
    for ;p != nil && i.Current == p.right; {
      i.Current = p
      p = p.parent
    }
    i.Current = p
    return i.Current != nil
  }
}

type BinaryTree struct {
  root *Node
}

func NewBinaryTree(root *Node) *BinaryTree {
  return &BinaryTree{root: root}
}

func (b *BinaryTree) InOrder() *InOrderIterator {
  return NewInOrderIterator(b.root)
}

func main() {
  //   1
  //  / \
  // 2   3

  // in-order:  213
  // preorder:  123
  // postorder: 231

  root := NewNode(1,
    NewTerminalNode(2),
    NewTerminalNode(3))
  it := NewInOrderIterator(root)

  for ;it.MoveNext(); {
    fmt.Printf("%d,", it.Current.Value)
  }
  fmt.Println("\b")

  t := NewBinaryTree(root)
  for i := t.InOrder(); i.MoveNext(); {
    fmt.Printf("%d,", i.Current.Value)
  }
  fmt.Println("\b")
}