using PhyBot.Slack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyBot.Slack.Http
{
    public class HttpCustomResponse
    {
        public string Body { get; set; }
        public SuccessResponse SuccessResponse { get; set; }

        public HttpCustomResponse(string body, SuccessResponse successResponse)
        {
            Body = body;
            SuccessResponse = successResponse;
        }
    }
}
