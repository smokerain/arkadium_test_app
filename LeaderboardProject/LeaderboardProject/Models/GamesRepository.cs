using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LeaderboardProject.EntityModels;

namespace LeaderboardProject.Models
{
    public class GamesRepository
    {
        // GET: api/GamesRepository
            private static LeaderboardDatabaseEntities _database;
            private static LeaderboardDatabaseEntities Database
            {
                get { return _database ?? (_database = new LeaderboardDatabaseEntities()); }
            }

            /// <summary>
            /// Gets the Games.
            /// </summary>
            /// <returns>IEnumerable Leaderboard List</returns>
            public static IEnumerable<games> GetGames()
            {
                var query = Database.games;
                return query.ToList();
            }
        }
    }
