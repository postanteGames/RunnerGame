using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    WebClient client = new WebClient();
    public class Player_Settings
    {
        public float speed { get; set; }
        public float para { get; set; }
    }
    public List<Player_Settings> GetPlayerData()
    {
        try
        {
            var result = client.DownloadString("http://93.190.8.28:3003/player_settings");

            var obj = JsonConvert.DeserializeObject<List<Player_Settings>>(result);


            return obj;
        }
        catch
        {
            return null;
        }
    }
}
