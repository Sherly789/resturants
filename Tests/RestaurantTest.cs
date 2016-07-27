using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DiningList
{
  public class DiningTest : IDisposable
  {
    public void Dispose()
    {
      Restaurant.DeleteAll();
    }

//TESTS
    public DiningTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=fine_dining_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test1_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test2_Equal_ReturnsTrueIfRestaurantsAreTheSame()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant("Burger King", "Seattle");
      Restaurant secondRestaurant = new Restaurant("Burger King", "Seattle");

      //Assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Burger King", "Seattle");

      // Act
      testRestaurant.Save();
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      Assert.Equal(testList, result);
    }

  }
}
