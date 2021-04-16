class Integer
{
  constructor(value)
  {
    this.value = value;
  }
}

let Operation = Object.freeze({
  addition: 0,
  subtraction: 1
});

class BinaryOperation
{
  constructor()
  {
    this.type = null;
    this.left = null;
    this.right = null;
  }

  get value()
  {
    switch (this.type)
    {
      case Operation.addition:
        return this.left.value + this.right.value;
      case Operation.subtraction:
        return this.left.value - this.right.value;
    }
    return 0;
  }
}

let TokenType = Object.freeze({
  integer: 0,
  plus: 1,
  minus: 2,
  lparen: 3,
  rparen: 4
});

class Token
{
  constructor(type, text)
  {
    this.type = type;
    this.text = text;
  }

  toString()
  {
    return `\`${this.text}\``;
  }
}

function lex(input)
{
  let result = [];

  for (let i = 0; i < input.length; ++i)
  {
    switch (input[i])
    {
      case '+':
        result.push(new Token(TokenType.plus, '+'));
        break;
      case '-':
        result.push(new Token(TokenType.minus, '-'));
        break;
      case '(':
        result.push(new Token(TokenType.lparen, '('));
        break;
      case ')':
        result.push(new Token(TokenType.rparen, ')'));
        break;
      default:
        let buffer = [input[i]];
        for (let j = i+1; j < input.length; ++j)
        {
          if ('0123456789'.includes(input[j]))
          {
            buffer.push(input[j]);
            ++i;
          } else {
            result.push(new Token(TokenType.integer,
              buffer.join('')));
            break;
          }
        }
        break;
    }
  }

  return result;
}

function parse(tokens)
{
  let result = new BinaryOperation();
  let haveLHS = false;

  for (let i = 0; i < tokens.length; ++i) {
    let token = tokens[i];

    switch (token.type) {
      case TokenType.integer:
        let integer = new Integer(parseInt(token.text));
        if (!haveLHS) {
          result.left = integer;
          haveLHS = true;
        } else {
          result.right = integer;
        }
        break;
      case TokenType.plus:
        result.type = Operation.addition;
        break;
      case TokenType.minus:
        result.type = Operation.subtraction;
        break;
      case TokenType.lparen:
        let j = i;
        for (; j < tokens.length; ++j)
          if (tokens[j].type === TokenType.rparen)
            break; // found it!
        // process subexpression
        let subexpression = tokens.slice(i + 1, j);
        let element = parse(subexpression);
        if (!haveLHS) {
          result.left = element;
          haveLHS = true;
        } else result.right = element;
        i = j; // advance
        break;
    }
  }
  return result;
}

let input = "(13+4)-(12+1)";
let tokens = lex(input);
console.log(tokens.join('  '));

let parsed = parse(tokens);
console.log(`${input} = ${parsed.value}`);