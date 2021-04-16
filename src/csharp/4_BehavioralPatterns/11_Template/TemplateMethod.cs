using static System.Console;

namespace DotNetDesignPatternDemos.Behavioral.TemplateMethod
{
  public abstract class Game
  {
    public void Run()
    {
      Start();
      while (!HaveWinner)
        TakeTurn();
      WriteLine($"Player {WinningPlayer} wins.");
    }

    protected abstract void Start();
    protected abstract bool HaveWinner { get; }
    protected abstract void TakeTurn();
    protected abstract int WinningPlayer { get; }

    protected int currentPlayer;
    protected readonly int numberOfPlayers;

    public Game(int numberOfPlayers)
    {
      this.numberOfPlayers = numberOfPlayers;
    }
  }

  // simulate a game of chess
  public class Chess : Game
  {
    public Chess() : base(2)
    {
    }

    protected override void Start()
    {
      WriteLine($"Starting a game of chess with {numberOfPlayers} players.");
    }

    protected override bool HaveWinner => turn == maxTurns;

    protected override void TakeTurn()
    {
      WriteLine($"Turn {turn++} taken by player {currentPlayer}.");
      currentPlayer = (currentPlayer + 1) % numberOfPlayers;
    }

    protected override int WinningPlayer => currentPlayer;

    private int maxTurns = 10;
    private int turn = 1;
  }

  public class Demo
  {
    static void Main(string[] args)
    {
      var chess = new Chess();
      chess.Run();
    }
  }
}