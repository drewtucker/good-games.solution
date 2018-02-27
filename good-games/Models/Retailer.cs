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

  public Retailer (string name, string website, int id = 0);
  {
    _name = name;
    _website = website;
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
      return this.GetId().Equals(newRetailer.GetId());
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
    cmd.CommandText = @"DELETE FROM retailers WHERE id = @searchId;";

    MySqlParameter searchId = new MySqlParameter();
    searchId.ParameterName = "@searchId";
    searchId.Value = _id;
    cmd.Parameters.Add(searchId);

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
