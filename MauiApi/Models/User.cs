using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiApi.Models
{
    public class User
    {
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        [JsonPropertyName("nickname")]
        public string NickName { get; set; }
        // Класс описывающий таблицу users
        [JsonPropertyName("id")]
        public ulong Id { get; set; }
        [JsonPropertyName("surname")]
        public string Surname { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("patronymic")]

        public string? Patronymic {  get; set; }
        [JsonPropertyName("sex")]

        public int Sex { get; set; }
        [JsonPropertyName("birthday")]

        public DateTime Birthday {  get; set; }
        [JsonPropertyName("email")]

        public string Email { get; set; }
        [JsonPropertyName("password")]

        public string? Password { get; set; } = null;
        [JsonPropertyName("phone")]

        public string? Phone { get; set; }
        [JsonPropertyName("api_token")]

        public string? ApiToken { get; set; }
        [JsonPropertyName("role_id")]

        public ulong RoleId { get; set; }
        [JsonPropertyName("created_at")]

        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("updated_at")]

        public DateTime UpdatedAt { get; set; }
        

    }
}
