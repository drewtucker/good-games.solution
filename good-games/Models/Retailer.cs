using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using GoodGamesApp;

namespace GoodGamesApp.Models
{
  public class Retailer
  {
    private string _name;
    private string _website;
    private int _id;

    public Retailer(string name, string website, int id = 0)
    {
      _name = name;
      _website = website;
      _id = id;
    }
    public override bool Equals(System.Object otherRetailer)
    {
      if (!(otherRetailer is Retailer))
      {
        return false;
      }
      else
      {
        Retailer newRetailer = (Retailer) otherRetailer;
        bool nameEquality = this.GetName().ToLower() == newRetailer.GetName().ToLower();
        return (nameEquality);
      }
    }
    //GETTERS
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public string GetName()
    {
      return _name;
    }

    public string GetWebsite()
    {
      return _website;
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

    public void SetWebsite(string newWebsite)
    {
      _website = newWebsite;
    }

    public void SetId(int newId)
    {
      _id = newId;
    }

    public static List<Retailer> GetAll()
    {
      List<Retailer> allRetailers = new List<Retailer>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM retailers;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string retailerName = rdr.GetString(1);
        string retailerWebsite = rdr.GetString(2);
        int retailerId = rdr.GetInt32(0);
        Retailer newRetailer = new Retailer(retailerName, retailerWebsite, retailerId);
        allRetailers.Add(newRetailer);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allRetailers;
    }

    public void AddGame(Game newGame)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO retailers_games (retailer_id, game_id) VALUES (@RetailerId, @GameId);";

      MySqlParameter retailer_id = new MySqlParameter("@RetailerId", _id);
      cmd.Parameters.Add(retailer_id);

      MySqlParameter game_id = new MySqlParameter("@GameId", newGame.GetId());
      cmd.Parameters.Add(game_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Game> GetGames()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT games.* FROM retailers
          JOIN retailers_games ON (retailers.id = retailers_games.retailer_id)
          JOIN games ON (retailers_games.game_id = games.id)
          WHERE retailers.id = @RetailerId;";

      MySqlParameter retailerIdParameter = new MySqlParameter("@RetailerId", _id);
      cmd.Parameters.Add(retailerIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Game> games = new List<Game>{};

      while(rdr.Read())
      {
        int gameId = rdr.GetInt32(0);
        string gameName = rdr.GetString(1);
        string gameGenre = rdr.GetString(2);
        string gameSystem = rdr.GetString(3);
        int gameReleaseYear = rdr.GetInt32(4);
        int gameRating = rdr.GetInt32(5);
        Game newGame = new Game(gameName, gameGenre, gameSystem, gameReleaseYear, gameRating, gameId);
        games.Add(newGame);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return games;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO retailers (name, website) VALUES (@name, @website);";
      MySqlParameter name = new MySqlParameter("@name", _name);
      cmd.Parameters.Add(name);
      MySqlParameter website = new MySqlParameter("@website", _website);
      cmd.Parameters.Add(website);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Retailer Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM retailers WHERE id = (@searchId);";

      MySqlParameter thisId = new MySqlParameter("@searchId", id);
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int retailerId = 0;
      string retailerName = "";
      string retailerWebsite = "";
      while(rdr.Read())
      {
        retailerId = rdr.GetInt32(0);
        retailerName = rdr.GetString(1);
        retailerWebsite = rdr.GetString(2);

      }
      Retailer newRetailer = new Retailer(retailerName, retailerWebsite, retailerId);
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return newRetailer;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM retailers WHERE id = @RetailerId; DELETE FROM retailers_games WHERE retailer_id = @RetailerId;";

      MySqlParameter retailerIdParameter = new MySqlParameter("@RetailerId", this.GetId());
      cmd.Parameters.Add(retailerIdParameter);

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
      cmd.CommandText = @"DELETE FROM retailers;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }


  }
}
