class Node
{
  constructor(value, left=null, right=null)
  {
    this.value = value;
    this.left = left;
    this.right = right;

    this.parent = null;

    if (left)
      left.parent = this;
    if (right)
      right.parent = this;
  }

  * _traverse (current)
  {
    yield current;
    if (current.left)
    {
      for (let left of this._traverse(current.left))
        yield left;
    }
    if (current.right)
    {
      for (let right of this._traverse(current.right))
        yield right;
    }
  }

  * preorder()
  {
    // return all the node *values* (not the nodes)
    for (let node of this._traverse(this))
      yield node.value;
  }
}

describe('iterator', function()
{
  it('does preorder traversal', function()
  {
    let node = new Node('a',
      new Node('b',
        new Node('c'),
        new Node('d')),
      new Node('e'));


    let values = [];
    for (let value of node.preorder())
      values.push(value);

    expect(values.join('')).toEqual('abcde');
  });
});