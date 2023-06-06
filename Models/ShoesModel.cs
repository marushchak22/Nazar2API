using System.Collections.Generic;
using Newtonsoft.Json;

namespace shoesAPI.Models
{


    public class SneakersModel
    {

        public string shoeName { get; set; }
        public string brand { get; set; }
        public string colorway { get; set; }
        public string styleID { get; set; }
        public string thumbnail { get; set; }
        public lowestResellPrice? lowestResellPrice { get; set; }

    }

    public class lowestResellPrice
    {
        public string stockX { get; set; }
        public string goat { get; set; }
        public string flightClub { get; set; }
    }
}




