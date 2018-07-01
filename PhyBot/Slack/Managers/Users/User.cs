using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyBot.Slack.Managers.Users
{
    /*
          {
              "id": "W012A3CDE",
              "team_id": "T012AB3C4",
              "name": "spengler",
              "deleted": false,
              "color": "9f69e7",
              "real_name": "Egon Spengler",
              "tz": "America/Los_Angeles",
              "tz_label": "Pacific Daylight Time",
              "tz_offset": -25200,
              "profile": {
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
              "is_admin": true,
              "is_owner": false,
              "is_primary_owner": false,
              "is_restricted": false,
              "is_ultra_restricted": false,
              "is_bot": false,
              "is_stranger": false,
              "updated": 1502138686,
              "is_app_user": false,
              "has_2fa": false,
              "locale": "en-US"
              }
          }
      */

    public class User
    {
        public string Id { get; set; }
        public string Team_Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public string Colour { get; set; }
        public string Real_Name { get; set; }
        public string TZ { get; set; }
        public string TZ_Label { get; set; }
        public string TZ_OffSet { get; set; }
        public Profile Profile { get; set; }
        public bool Is_Admin { get; set; }
        public bool Is_Owner { get; set; }
        public bool Is_Primary_Owner { get; set; }
        public bool Is_Restricted { get; set; }
        public bool Is_Ultra_Restricted { get; set; }
        public bool Is_Bot { get; set; }
        public bool Is_Stranger { get; set; }
        public string Updated { get; set; }
        public bool Is_App_User { get; set; }
        public bool Has_2fa { get; set; }
        public string Locale { get; set; }
    }
}