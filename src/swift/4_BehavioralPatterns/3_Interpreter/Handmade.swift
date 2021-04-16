import Foundation

// allow the use of an indexer
extension String
{
  subscript (i: Int) -> Character
  {
    return self[index(startIndex, offsetBy: i)]
  }

  var isNumber : Bool
  {
    get {
      return !self.isEmpty && self.rangeOfCharacter(from: CharacterSet.decimalDigits.inverted) == nil
    }
  }
}

protocol Element
{
  var value: Int { get }
}

class Integer : Element
{
  var value: Int
  init(_ value: Int){
    self.value = value
  }
}

class BinaryOperation : Element
{
  enum OpType
  {
    case addition
    case subtraction
  }

  var opType: OpType
  var left: Element
  var right: Element

  init()
  {
    // dummy values
    opType = OpType.addition
    left = Integer(0)
    right = Integer(0)
  }

  init(left: Element, right: Element, opType: OpType)
  {
    self.left = left
    self.right = right
    self.opType = opType
  }

  var value: Int {
    switch (opType)
    {
      case OpType.addition:
        return left.value + right.value
      case OpType.subtraction:
        return left.value - right.value
    }
  }
}

struct Token : CustomStringConvertible
{
  enum TokenType
  {
    case integer
    case plus
    case minus
    case lparen
    case rparen
  }

  var tokenType: TokenType
  var text: String

  init(_ tokenType: TokenType, _ text: String)
  {
    self.tokenType = tokenType
    self.text = text
  }

  var description: String
  {
    return "`\(text)`"
  }
}


func lex(_ input: String) -> [Token]
{
  var result = [Token]()

  var i = 0 // need a mutable iterable variable
  while i < input.characters.count
  {
    switch (input[i])
    {
      case "+":
        result.append(Token(Token.TokenType.plus, "+"))
      case "-":
        result.append(Token(Token.TokenType.minus, "-"))
      case "(":
        result.append(Token(Token.TokenType.lparen, "("))
      case ")":
        result.append(Token(Token.TokenType.rparen, ")"))
      default:
        var s = String(input[i])
        for j in (i+1)..<input.characters.count
        {
          if String(input[j]).isNumber
          {
            s.append(input[j])
            i += 1
          }
          else
          {
            result.append(Token(Token.TokenType.integer, s))
            break
          }
        }
    }
    i += 1
  }
  return result
}

func parse(_ tokens: [Token]) -> Element
{
  let result = BinaryOperation()
  var haveLHS = false

  var i = 0
  while i < tokens.count
  {
    let token = tokens[i]
    switch (token.tokenType)
    {
      case Token.TokenType.integer:
        let integer = Integer(Int(token.text)!)
        if !haveLHS
        {
          result.left = integer
          haveLHS = true
        }
        else
        {
          result.right = integer
        }
      case Token.TokenType.plus:
        result.opType = BinaryOperation.OpType.addition
      case Token.TokenType.minus:
        result.opType = BinaryOperation.OpType.subtraction
      case Token.TokenType.lparen:
        var j = i
        while j < tokens.count
        {
          if tokens[j].tokenType == Token.TokenType.rparen
          {
            break
          }
          j += 1
        }
        // process subexpression without opening (
        let subexpression = tokens[(i+1)..<j]
        let element = parse(Array(subexpression))
        if !haveLHS
        {
          result.left = element
          haveLHS = true;
        }
        else {
          result.right = element;
        }
        i = j // advance
      default: break
    }
    i += 1
  }
  return result
}


func main()
{
  let input = "(13+4)-(12+1)"
  let tokens = lex(input)
  print(tokens.map { $0.description }.joined(separator: "\t"))

  let parsed = parse(tokens)
  print("\(input) = \(parsed.value)")
}

main()