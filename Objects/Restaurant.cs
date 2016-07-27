using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace DiningList
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private string _city;

    public Restaurant(string Name, string City, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _city = City;
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool nameEquality = (this.GetName() == newRestaurant.GetName() && this.GetCity() == newRestaurant.GetCity() && this.GetId() == newRestaurant.GetId() );
        return (nameEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetCity()
    {
      return _city;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }
    public void SetCity(string newCity)
    {
      _city = newCity;
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        string restaurantCity = rdr.GetString(2);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCity, restaurantId);
        allRestaurants.Add(newRestaurant);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allRestaurants;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, city) OUTPUT INSERTED.id VALUES (@RestaurantName, @RestaurantCity);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@RestaurantName";
      nameParameter.Value = this.GetName();

      SqlParameter cityParameter = new SqlParameter();
      cityParameter.ParameterName = "@RestaurantCity";
      cityParameter.Value = this.GetCity();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(cityParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
    public static Restaurant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @RestaurantId;", conn);
      SqlParameter RestaurantIdParameter = new SqlParameter();
      RestaurantIdParameter.ParameterName = "@RestaurantId";
      RestaurantIdParameter.Value = id.ToString();
      cmd.Parameters.Add(RestaurantIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundRestaurantId = 0;
      string foundRestaurantname = null;
      string foundRestaurantcity = null;
      while(rdr.Read())
      {
        foundRestaurantId = rdr.GetInt32(0);
        foundRestaurantname = rdr.GetString(1);
        foundRestaurantcity = rdr.GetString(2);
      }
      Restaurant foundRestaurant = new Restaurant(foundRestaurantname, foundRestaurantcity, foundRestaurantId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundRestaurant;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
