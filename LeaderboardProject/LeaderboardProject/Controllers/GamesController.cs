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
    public class GamesController : ApiController
    {
        // GET: api/games
        public IEnumerable<games> Get()
        {
            return GamesRepository.GetGames();
        }

        // GET: api/games/5
        public games Get(int id)
        {
            return GamesRepository.GetGames().FirstOrDefault(s => s.Id == id);
        }
    }
}
