namespace TrybeGames;

public class TrybeGamesDatabase
{
    public List<Game> Games = new List<Game>();

    public List<GameStudio> GameStudios = new List<GameStudio>();

    public List<Player> Players = new List<Player>();

    public int PlayerId { get; set; } = 1;
    public int StudioId { get; set; } = 1;
    public int GameId { get; set; } = 1;

    public void AddPlayer(string playerName)
    {

        Player player = new();
        player.Id = PlayerId;
        player.Name = playerName;
        PlayerId++;
        Players.Add(player);
    }

    public void AddStudio(string studioName)
    {
        GameStudio studio = new();
        studio.Id = StudioId;
        StudioId++;
        studio.Name = studioName;
        GameStudios.Add(studio);

    }
    public void AddGame(string gameName, DateTime date, GameType gameType)
    {
        Game game = new();
        game.Id = GameId;
        game.Name = gameName;
        game.ReleaseDate = date;
        game.GameType = gameType;
        Games.Add(game);
        GameId++;

    }


    // 4. Crie a funcionalidade de buscar jogos desenvolvidos por um estúdio de jogos
    public List<Game> GetGamesDevelopedBy(GameStudio gameStudio)
    {
        // implementar
        var list = from game in Games
                   where game.DeveloperStudio == gameStudio.Id
                   select game;
        return list.ToList();
    }

    // 5. Crie a funcionalidade de buscar jogos jogados por uma pessoa jogadora
    public List<Game> GetGamesPlayedBy(Player player)
    {
        // Implementar
        var list = from game in Games
                   from played in player.GamesOwned
                   where played == game.Id
                   select game;
        return list.ToList();

    }

    // 6. Crie a funcionalidade de buscar jogos comprados por uma pessoa jogadora
    public List<Game> GetGamesOwnedBy(Player playerEntry)
    {
        // Implementar
        var list = from game in Games
                   from played in playerEntry.GamesOwned
                   where played == game.Id
                   select game;

        return list.ToList();
    }


    // 7. Crie a funcionalidade de buscar todos os jogos junto do nome do estúdio desenvolvedor
    public List<GameWithStudio> GetGamesWithStudio()
    {
        // Implementar
        var list = from game in Games
                   from studio in GameStudios
                   where studio.Id == game.DeveloperStudio
                   select new GameWithStudio
                   {
                       GameName = game.Name,
                       NumberOfPlayers = game.Players.Count,
                       StudioName = studio.Name
                   };

        return list.ToList();
    }

    // 8. Crie a funcionalidade de buscar todos os diferentes Tipos de jogos dentre os jogos cadastrados
    public List<GameType> GetGameTypes()
    {
        // Implementar
        var list = from game in Games
                   select game.GameType;



        return list.Distinct().ToList();
    }

    // 9. Crie a funcionalidade de buscar todos os estúdios de jogos junto dos seus jogos desenvolvidos com suas pessoas jogadoras
    public List<StudioGamesPlayers> GetStudiosWithGamesAndPlayers()
    {
        // Implementar

        var list = from studio in GameStudios
                   select new StudioGamesPlayers
                   {
                       GameStudioName = studio.Name,
                       Games = (from game in Games
                                where game.DeveloperStudio == studio.Id
                                select new GamePlayer
                                {
                                    GameName = game.Name,
                                    Players = (from player in Players
                                               from played in game.Players
                                               where player.Id == played
                                               select player).ToList()
                                }).ToList()
                   };
        return list.ToList();

    }

}
