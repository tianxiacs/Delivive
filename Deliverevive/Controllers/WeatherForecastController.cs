using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Deliverevive.Models;

namespace Deliverevive.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}


        [HttpGet]
        public IEnumerable<ResturantModel> Get()
        {
            string constr = @"data source=DESKTOP-2D7M32E;initial catalog=DelivereviveDB;persist security info=True;user id=sa;password=manager";
            List<ResturantModel> resturants = new List<ResturantModel>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM Resturant";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            resturants.Add(new ResturantModel
                            {
                                ResturantId = Convert.ToInt32(sdr["ResturantId"]),
                                ResturantName = sdr["ResturantName"].ToString(),
                                ResturantPhone = sdr["ResturantPhone"].ToString(),
                                ResturantAddress = sdr["ResturantAddress"].ToString(),
                                ResturantCity = sdr["ResturantCity"].ToString(),
                                ResturantProvince = sdr["ResturantProvince"].ToString(),
                                ResturantCountry = sdr["ResturantCountry"].ToString(),
                            });
                        }
                    }
                    con.Close();
                }
            }

            return resturants;
        }

        [HttpPost]
        [Route("api/WeatherForecast/ReadResturants")]
        public static List<ResturantModel> ReadResturants()
        {
            string constr = @"data source=;initial catalog=DESKTOP-2D7M32E;persist security info=True;user id=sa;password=manager";
            List<ResturantModel> resturants = new List<ResturantModel>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM Resturant";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            resturants.Add(new ResturantModel
                            {
                                ResturantId = Convert.ToInt32(sdr["FruitId"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            return resturants;
        }
    }
}
