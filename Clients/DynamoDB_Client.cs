using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime.Internal.Transform;
using shoesAPI.Extensions;
using shoesAPI.Models;
using shoesAPI;
using shoesAPI.Models;
using Microsoft.VisualBasic;

namespace shoesAPI.Clients
{
    public class DynamoDB_Client : IDynamoDB_Client
    {
        public string _tableName;
        private readonly IAmazonDynamoDB _dynamoDB;


        public DynamoDB_Client(IAmazonDynamoDB dynamoDB)
        {
            _dynamoDB = dynamoDB;
            _tableName = ConstantsDB.TableName;
        }


        public async Task<ModelShoesDB> GetDataByShoes(string shoeName)
        {
            var item = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
        {
            {"shoeName", new AttributeValue { S = shoeName} }
        }
            };

            var response = await _dynamoDB.GetItemAsync(item);

            if (response.Item == null || !response.IsItemSet)
                return null;

            var result = new ModelShoesDB
            {
                shoeName = response.Item["shoeName"].S,
                brand = response.Item["brand"].S,
                colorway = response.Item["colorway"].S,
                styleID = response.Item["styleID"].S,
                thumbnail = response.Item["thumbnail"].S,
                stockX = response.Item["stockX"].S,
                goat = response.Item["goat"].S,
                flightClub = response.Item["flightClub"].S
            };

            return result;
        }




        //public async Task<bool> PostDataToDB(modelsforPost dataDB)
        //{
        //    var request = new PutItemRequest
        //    {
        //        TableName = _tableName,
        //        Item = new Dictionary<string, AttributeValue>
        //        {

        //            {"shoeName", new AttributeValue {S = dataDB.shoeName} },
        //            {"brand", new AttributeValue {S = dataDB.brand} },
        //            {"colorway", new AttributeValue {S = dataDB.colorway} },
        //            {"styleID", new AttributeValue {S = dataDB.styleID} },
        //            {"thumbnail", new AttributeValue {S = dataDB.thumbnail} },
        //            {"stockX", new AttributeValue {S = dataDB.stockX} },
        //            {"goat", new AttributeValue {S = dataDB.goat} },
        //            {"flightClub", new AttributeValue {S = dataDB.flightClub} }
        //        }
        //    };

        //    try
        //    {
        //        var response = await _dynamoDB.PutItemAsync(request);
        //        return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        //    }

        //    catch (Exception e)
        //    {
        //        Console.WriteLine("error" + e);
        //        return false;
        //    }

        //}



        //public async Task DeleteDataFromDBByShoeName(string shoeName)
        //{
        //    var request = new DeleteItemRequest
        //    {
        //        TableName = _tableName,
        //        Key = new Dictionary<string, AttributeValue>
        //    {
        //        {"shoeName", new AttributeValue {S = shoeName}}
        //    }
        //    };

        //    try
        //    {
        //        var response = await _dynamoDB.DeleteItemAsync(request);

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("error" + e);

        //    }
        //}


        //public async Task<bool> UpdateDataInDB(string shoeName, modelsforPost updatedData)
        //{
        //    var request = new UpdateItemRequest
        //    {
        //        TableName = _tableName,
        //        Key = new Dictionary<string, AttributeValue>
        //{
        //    {"shoeName", new AttributeValue {S = shoeName}}
        //},
        //        ExpressionAttributeValues = new Dictionary<string, AttributeValue>
        //{
        //    {":brand", new AttributeValue {S = updatedData.brand}},
        //    {":colorway", new AttributeValue {S = updatedData.colorway}},
        //    {":styleID", new AttributeValue {S = updatedData.styleID}},
        //    {":thumbnail", new AttributeValue {S = updatedData.thumbnail}},
        //    {":stockX", new AttributeValue {S = updatedData.stockX}},
        //    {":goat", new AttributeValue {S = updatedData.goat}},
        //    {":flightClub", new AttributeValue {S = updatedData.flightClub}}
        //},
        //        UpdateExpression = "SET brand = :brand, colorway = :colorway, styleID = :styleID, thumbnail = :thumbnail, stockX = :stockX, goat = :goat, flightClub = :flightClub",
        //        ReturnValues = ReturnValue.ALL_NEW
        //    };

        //    try
        //    {
        //        var response = await _dynamoDB.UpdateItemAsync(request);
        //        return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Error updating record: " + e);
        //        return false;
        //    }
        //}



    }
}

