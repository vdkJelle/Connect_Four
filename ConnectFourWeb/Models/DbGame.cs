using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ConnectFourLibrary;
using Microsoft.EntityFrameworkCore;
using ConnectFourWeb.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectFourWeb.Models
{
    public class DbGame
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string GameSerialized { get; set; }
        //public string PlayerOneSerialized { get; set; }
        //public string PlayerTwoSerialized { get; set; }
        //public string PlayerTurnSerialized { get; set; }

        [JsonIgnore]
        public Game GameManager
        {
            get => JsonConvert.DeserializeObject<Game>(GameSerialized);
            set => GameSerialized = JsonConvert.SerializeObject(value);
        }

        //[NotMapped]
        public Player PlayerOne { get; set; }
        //{
        //    get => JsonConvert.DeserializeObject<Player>(PlayerOneSerialized);
        //    set => PlayerOneSerialized = JsonConvert.SerializeObject(value);
        //}

        //[NotMapped]
        public Player PlayerTwo { get; set; }
        //{
        //    get => JsonConvert.DeserializeObject<Player>(PlayerTwoSerialized);
        //    set => PlayerOneSerialized = JsonConvert.SerializeObject(value);
        //}

        //[NotMapped]
        public Player PlayerTurn { get; set; }
        //{
        //    get => JsonConvert.DeserializeObject<Player>(PlayerTurnSerialized);
        //    set => PlayerTurnSerialized = JsonConvert.SerializeObject(value);
        //}
    }
}
