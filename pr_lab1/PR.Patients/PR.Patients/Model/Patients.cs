using System;
using System.Text.Json.Serialization;

namespace PR.Patients.Model
{
    public class Patients
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        [JsonPropertyName("age")]
        public int Age { get; set; }
        [JsonPropertyName("testDate")]
        public DateTime TestDate { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }

    }
}
