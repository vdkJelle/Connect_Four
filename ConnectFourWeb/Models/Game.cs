using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ConnectFourLibrary;

namespace ConnectFourWeb.Models
{
    public class Game
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string SerializedGame { get; set; }
        [JsonIgnore]
        public Game GameManager
        {
            get => JsonConvert.DeserializeObject<Game>(SerializedGame);
            set => SerializedGame = JsonConvert.SerializeObject(value); 
        }
    }
}
