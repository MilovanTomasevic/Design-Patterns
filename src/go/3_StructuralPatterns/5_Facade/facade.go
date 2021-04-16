package facade

type Buffer struct {
  width, height int
  buffer []rune
}

func NewBuffer(width, height int) *Buffer {
  return &Buffer { width, height,
    make([]rune, width*height)}
}

func (b *Buffer) At(index int) rune {
  return b.buffer[index]
}

type Viewport struct {
  buffer *Buffer
  offset int
}

func NewViewport(buffer *Buffer) *Viewport {
  return &Viewport{buffer: buffer}
}

func (v *Viewport) GetCharacterAt(index int) rune {
  return v.buffer.At(v.offset + index)
}

// a facade over buffers and viewports
type Console struct {
  buffers []*Buffer
  viewports []*Viewport
  offset int
}

func NewConsole() *Console {
  b := NewBuffer(10, 10)
  v := NewViewport(b)
  return &Console{[]*Buffer{b}, []*Viewport{v}, 0}
}

func (c *Console) GetCharacterAt(index int) rune {
  return c.viewports[0].GetCharacterAt(index)
}

func main() {
  c := NewConsole()
  u := c.GetCharacterAt(1)
}