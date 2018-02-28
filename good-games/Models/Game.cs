using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using GoodGamesApp;

namespace GoodGamesApp.Models
{
  public class Game
  {
    private string _name;
    private string _genre;
    private string _system;
    private int _releaseYear;
    private int _rating;
    private int _id;

    public Game (string name, string genre, string system, int releaseYear, int rating, int Id = 0)
    {
      _name = name;
      _genre = genre;
      _system = system;
      _releaseYear = releaseYear;
      _rating = rating;
      _id = Id;
    }

    public override bool Equals(System.Object otherGame)
    {
      if(!(otherGame is Game))
      {
        return false;
      }
      else
      {
        Game newGame = (Game) otherGame;
        bool nameEquality = this.GetName().ToLower() == newGame.GetName().ToLower();

        return (nameEquality);
      }
    }

    //GETTERS

    public string GetName()
    {
      return _name;
    }

    public string GetGenre()
    {
      return _genre;
    }

    public string GetSystem()
    {
      return _system;
    }

    public int GetReleaseYear()
    {
      return _releaseYear;
    }

    public int GetRating()
    {
      return _rating;
    }

    public int GetId()
    {
      return _id;
    }

    //SETTERS

    public void SetName(string newName)
    {
      _name = newName;
    }

    public void SetGenre(string newGenre)
    {
      _genre = newGenre;
    }

    public void SetSystem(string newSystem)
    {
      _system = newSystem;
    }

    public void SetReleaseYear(int newReleaseYear)
    {
      _releaseYear = newReleaseYear;
    }

    public void SetRating(int newRating)
    {
      _rating = newRating;
    }

    public void SetId(int newId)
    {
      _id = newId;
    }

    public static List<Game> GetAll()
    {
      List<Game> allGames = new List<Game>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM games;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string gameName = rdr.GetString(1);
        string gameGenre = rdr.GetString(2);
        string gameSystem = rdr.GetString(3);
        int gameReleaseYear = rdr.GetInt32(4);
        int gameRating = rdr.GetInt32(5);
        int gameId = rdr.GetInt32(0);
        Game newGame = new Game(gameName, gameGenre, gameSystem, gameReleaseYear, gameRating, gameId);
        allGames.Add(newGame);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allGames;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO games (name, genre, system, release_year, rating) VALUES (@name, @genre, @system, @release_year, @rating);";

      MySqlParameter name = new MySqlParameter("@name", _name);
      cmd.Parameters.Add(name);
      MySqlParameter genre = new MySqlParameter("@genre", _genre);
      cmd.Parameters.Add(genre);
      MySqlParameter system = new MySqlParameter("@system", _system);
      cmd.Parameters.Add(system);
      MySqlParameter release_year = new MySqlParameter("@release_year", _releaseYear);
      cmd.Parameters.Add(release_year);
      MySqlParameter rating = new MySqlParameter("@rating", _rating);
      cmd.Parameters.Add(rating);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Game Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM games WHERE id = (@searchId);";

      MySqlParameter thisId = new MySqlParameter("@searchId", id);
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int gameId = 0;
      string gameName = "";
      string gameGenre = "";
      string gameSystem = "";
      int gameReleaseYear = 0;
      int gameRating = 0;

      while(rdr.Read())
      {
        gameId = rdr.GetInt32(0);
        gameName = rdr.GetString(1);
        gameGenre = rdr.GetString(2);
        gameSystem = rdr.GetString(3);
        gameReleaseYear = rdr.GetInt32(4);
        gameRating = rdr.GetInt32(5);
      }
      Game newGame = new Game(gameName, gameGenre, gameSystem, gameReleaseYear, gameRating, gameId);
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return newGame;
    }


    public void AddRetailer(Retailer newRetailer)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO retailers_games (retailer_id, game_id) VALUES (@RetailerId, @GameId);";

      MySqlParameter retailer_id = new MySqlParameter("@RetailerId", newRetailer.GetId());
      cmd.Parameters.Add(retailer_id);

      MySqlParameter game_id = new MySqlParameter("@GameId", _id);
      cmd.Parameters.Add(game_id);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Retailer> GetRetailers()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT retailers.* FROM games
          JOIN retailers_games ON (games.id = retailers_games.game_id)
          JOIN retailers ON (retailers_games.retailer_id = retailers.id)
          WHERE games.id = @GameId;";

      MySqlParameter gameIdParameter = new MySqlParameter("@GameId", _id);
      cmd.Parameters.Add(gameIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Retailer> retailers = new List<Retailer>{};

      while(rdr.Read())
      {
        int retailerId = rdr.GetInt32(0);
        string retailerName = rdr.GetString(1);
        string retailerWebsite = rdr.GetString(2);
        Retailer newRetailer = new Retailer(retailerName, retailerWebsite, retailerId);
        retailers.Add(newRetailer);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return retailers;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM games WHERE id = @GameId; DELETE FROM retailers_games WHERE game_id = @GameId;";

      MySqlParameter gameIdParameter = new MySqlParameter("@GameId", this.GetId());
      cmd.Parameters.Add(gameIdParameter);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM games;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
