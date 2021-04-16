import Foundation

class User
{
  var fullName: String

  init(_ fullName: String)
  {
    self.fullName = fullName
  }

  var charCount: Int
  {
    return fullName.utf8.count
  }
}

class User2
{
  static var strings = [String]()
  private var names = [Int]()

  init(_ fullName: String)
  {
    func getOrAdd(_ s: String) -> Int
    {
      if let idx = type(of: self).strings.index(of: s)
      {
        return idx
      }
      else
      {
        type(of: self).strings.append(s)
        return type(of: self).strings.count + 1
      }
    }
    names = fullName.components(separatedBy: " ").map { getOrAdd($0) }
  }

  static var charCount: Int
  {
    return strings.map{ $0.utf8.count }.reduce(0, +)
  }
}

func main()
{
  let user1 = User("John Smith")
  let user2 = User("Jane Smith")
  let user3 = User("Jane Doe")
  // "Smith" and "Jane" allocated twice, eat 2x memory

  let totalChars = user1.charCount + user2.charCount + user3.charCount
  print("Total number of chars used: \(totalChars)")

  let user4 = User2("John Smith")
  let user5 = User2("Jane Smith")
  let user6 = User2("Jane Doe")
  print("Total number of chars used: \(User2.charCount)")
}

main()