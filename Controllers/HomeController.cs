using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nazar2API.Models;
using shoesAPI.Clients;
using shoesAPI.Models;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.Configuration;
using shoesAPI.Extensions;
using static shoesAPI.Models.ModelShoesDB;

namespace Nazar2API.Controllers;

public class HomeController : Controller
{
    private readonly ShoesClient _shoesClient;
    private readonly ILogger<HomeController> _logger;
    private readonly IDynamoDB_Client _dynamoDB_Client;

    public HomeController(ILogger<HomeController> logger, ShoesClient shoesClient, IDynamoDB_Client dynamoDB_Client)
    {
        _logger = logger;
        _shoesClient = shoesClient;
        _dynamoDB_Client = dynamoDB_Client;
    }


    public async Task<IActionResult> Index(string shoesmodel)
    {

        if (string.IsNullOrEmpty(shoesmodel))
        {
            shoesmodel = "James Arizumi x Nike SB Dunk Low Pro «What the Dunk»";
        }

        var sneakersJson = await _shoesClient.GetShoesbyBrands(shoesmodel);



        var sneakers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SneakersModel>>(sneakersJson);

        var result = new List<SneakersModel>();

        foreach (var sneaker in sneakers)
        {
            var sneakersModel = new SneakersModel
            {
                shoeName = sneaker.shoeName,
                brand = sneaker.brand,
                colorway = sneaker.colorway,
                styleID = sneaker.styleID,
                thumbnail = sneaker.thumbnail,
                lowestResellPrice = new lowestResellPrice
                {
                    stockX = sneaker.lowestResellPrice?.stockX,
                    goat = sneaker.lowestResellPrice?.goat,
                    flightClub = sneaker.lowestResellPrice?.flightClub
                }

            };
            result.Add(sneakersModel);
        }

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Index2()
    {
        string shoes1 = "Nike Dunk Low Retro White Black Panda (2021)";
        string shoes2 = "ASICS Gel-1130 Cream Pure Silver (Women's)";
        string shoes3 = "Nike SB Dunk Low Pro Bart Simpson";

        var result1 = await _dynamoDB_Client.GetDataByShoes(shoes1);
        var result2 = await _dynamoDB_Client.GetDataByShoes(shoes2);
        var result3 = await _dynamoDB_Client.GetDataByShoes(shoes3);

        if (result1 == null && result2 == null && result3 == null)
            return NotFound("Кросівок не знайдено.");

        var shoesResponse = new List<ModelShoesDB>();

        if (result1 != null)
        {
            var shoes1Response = new ModelShoesDB
            {
                shoeName = result1.shoeName,
                brand = result1.brand,
                colorway = result1.colorway,
                styleID = result1.styleID,
                thumbnail = result1.thumbnail,
                stockX = result1.stockX,
                goat = result1.goat,
                flightClub = result1.flightClub
            };

            shoesResponse.Add(shoes1Response);
        }

        if (result2 != null)
        {
            var shoes2Response = new ModelShoesDB
            {
                shoeName = result2.shoeName,
                brand = result2.brand,
                colorway = result2.colorway,
                styleID = result2.styleID,
                thumbnail = result2.thumbnail,
                stockX = result2.stockX,
                goat = result2.goat,
                flightClub = result2.flightClub
            };

            shoesResponse.Add(shoes2Response);
        }

        if (result3 != null)
        {
            var shoes3Response = new ModelShoesDB
            {
                shoeName = result3.shoeName,
                brand = result3.brand,
                colorway = result3.colorway,
                styleID = result3.styleID,
                thumbnail = result3.thumbnail,
                stockX = result3.stockX,
                goat = result3.goat,
                flightClub = result3.flightClub
            };

            shoesResponse.Add(shoes3Response);
        }
        return View(shoesResponse);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
