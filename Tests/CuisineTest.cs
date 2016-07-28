using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DiningList
{
  public class CuisineTest : IDisposable
  {
    public void Dispose()
    {
      Cuisine.DeleteAll();
    }

    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=fine_dining_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test1_CuisineEmptyFirst()
    {
      int result = Cuisine.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Cuisine firstCuisine = new Cuisine("Fast Food");
      Cuisine secondCuisine = new Cuisine("Fast Food");

      // Assert
      Assert.Equal(firstCuisine, secondCuisine);
    }
  }
}
