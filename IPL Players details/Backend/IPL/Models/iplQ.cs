using System.ComponentModel.DataAnnotations;

namespace IPL.Models
{
    public class iplQ
    {
        public string player_name { get; set; }

        [Required]
        public int team_id { get; set; }
        [Required]
        public string role { get; set; }

        public int age { get; set; }
        public int matches_played { get; set; }
    }

    public class Q2
    {
        public int match_id { get; set; }
        [Required]
        public string team1_name { get; set; }
        public string team2_name { get; set; }
        public string venue { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime match_date { get; set; }
        [Required]

        public long total_fan_engagements { get; set; }
    }


    public class Q3
    {
        public string player_name { get; set; }
        public int matches_played { get; set; }
        public long total_fan_engagements { get; set; }
    }


    public class Q4
    {
        public int match_id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime match_date { get; set; }
        [Required]

        public string venue {get ; set; }

        public string team1_name { get; set; }
        public string team2_name { get; set; }
    }

 }
