using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace AoE2Civs.Model
{
    public static class AoE2CivsAPI
    {
        private static string BASE_URL = "https://age-of-empires-2-api.herokuapp.com";
        //USE jsontoC#Converter!!
        public static IRestResponse GetCivs()
        {
            var client = new RestClient(BASE_URL);
            var request = new RestRequest($"/api/v1/civilizations", DataFormat.Json);
            return client.Get(request);
        }
    }
}
