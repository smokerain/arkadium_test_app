using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LeaderboardProject.EntityModels;
using LeaderboardProject.Models;

namespace LeaderboardProject.Controllers
{
    public class LeaderboardController : ApiController
    {
        // GET: api/Leaderboard
        public IEnumerable<scores> Get()
        {
            return LeaderboardRepository.GetLeaderboard();
        }

        // GET: api/Leaderboard/5
        public IEnumerable<scores> Get(int id)
        {
            return LeaderboardRepository.GetLeaderboard(id);
        }
    }
}
