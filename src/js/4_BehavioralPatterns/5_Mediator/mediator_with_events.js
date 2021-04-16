class Event
{
  constructor()
  {
    this.handlers = new Map();
    this.count = 0;
  }

  subscribe(handler)
  {
    this.handlers.set(++this.count, handler);
    return this.count;
  }

  unsubscribe(idx)
  {
    this.handlers.delete(idx);
  }

  fire(sender, args)
  {
    this.handlers.forEach(function (v, k)
    {
      v(sender, args);
    });
  }
}

class PlayerScoredEventArgs
{
  constructor(playerName, goalsScoredSoFar) {
    this.playerName = playerName;
    this.goalsScoredSoFar = goalsScoredSoFar;
  }

  print()
  {
    console.log(`${this.playerName} has scored `
      + `their ${this.goalsScoredSoFar} goal`);
  }
}

class Game {
  constructor() {
    this.events = new Event();
  }
}

class Player
{
  constructor(name, game)
  {
    this.name = name;
    this.game = game;
    this.goalsScored = 0;
  }

  score()
  {
    this.goalsScored++;
    let args = new PlayerScoredEventArgs(this.name, this.goalsScored);
    this.game.events.fire(this, args);
  }
}

class Coach
{
  constructor(game)
  {
    this.game = game;

    game.events.subscribe(function (sender, args)
    {
      if (args instanceof PlayerScoredEventArgs
        && args.goalsScoredSoFar < 3)
      {
        console.log(`coach says: well done, ${args.playerName}`);
      }
    });
  }
}

let game = new Game();
let player = new Player('Sam', game);
let coach = new Coach(game);

player.score();
player.score();
player.score();