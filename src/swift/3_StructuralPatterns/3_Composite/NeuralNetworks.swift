import Foundation

class Neuron : Sequence
{
  var inputs = [Neuron]()
  var outputs = [Neuron]()

  func makeIterator() -> IndexingIterator<Array<Neuron>>
  {
    return IndexingIterator(_elements: [self])
  }

  func connectTo(_ other: Neuron)
  {
    outputs.append(other)
    other.inputs.append(self)
  }
}

class NeuronLayer : Sequence
{
  private var neurons: [Neuron]

  init(count: Int)
  {
    neurons = [Neuron](repeating: Neuron(), count: count)
  }

  func makeIterator() -> IndexingIterator<Array<Neuron>>
  {
    return IndexingIterator(_elements: neurons)
  }
}

extension Sequence
{
  func connectTo<Seq: Sequence>(_ other: Seq)
    where Seq.Iterator.Element == Neuron,
    Self.Iterator.Element == Neuron
  {
    for from in self
    {
      for to in other
      {
        from.outputs.append(to)
        to.inputs.append(from)
      }
    }
  }
}

func main()
{
  let neuron1 = Neuron()
  let neuron2 = Neuron()
  let layer1 = NeuronLayer(count: 10)
  let layer2 = NeuronLayer(count: 20)

  neuron1.connectTo(neuron2)
  neuron1.connectTo(layer1)
  layer2.connectTo(neuron1)
  layer1.connectTo(layer2)
}

main()