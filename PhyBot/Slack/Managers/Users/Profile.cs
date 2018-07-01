using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyBot.Slack.Managers.Users
{
    /*
       {
        "avatar_hash": "ge3b51ca72de",
        "status_text": "Print is dead",
        "status_emoji": ":books:",
        "real_name": "Egon Spengler",
        "display_name": "spengler",
        "real_name_normalized": "Egon Spengler",
        "display_name_normalized": "spengler",
        "email": "spengler@ghostbusters.example.com",
        "image_24": "https://.../avatar/e3b51ca72dee4ef87916ae2b9240df50.jpg",
        "image_32": "https://.../avatar/e3b51ca72dee4ef87916ae2b9240df50.jpg",
        "image_48": "https://.../avatar/e3b51ca72dee4ef87916ae2b9240df50.jpg",
        "image_72": "https://.../avatar/e3b51ca72dee4ef87916ae2b9240df50.jpg",
        "image_192": "https://.../avatar/e3b51ca72dee4ef87916ae2b9240df50.jpg",
        "image_512": "https://.../avatar/e3b51ca72dee4ef87916ae2b9240df50.jpg",
        "team": "T012AB3C4"
    },
    */

    public class Profile
    {
        public string Avatar_Hash { get; set; }
        public string Status_Text { get; set; }
        public string Status_Emoji { get; set; }
        public string Real_Name { get; set; }
        public string Display_Name { get; set; }
        public string Real_Name_Normalized { get; set; }
        public string Display_Name_Normalized { get; set; }
        public string Email { get; set; }
        public string Team { get; set; }
    }
}
