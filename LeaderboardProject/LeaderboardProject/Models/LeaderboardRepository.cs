using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LeaderboardProject.EntityModels;

namespace LeaderboardProject.Models
{
    public class LeaderboardRepository
    {
        private static LeaderboardDatabaseEntities _database;
        private static LeaderboardDatabaseEntities Database
        {
            get { return _database ?? (_database = new LeaderboardDatabaseEntities()); }
        }

        /// <summary>
        /// Получить все данные
        /// </summary>
        /// <returns>IEnumerable Leaderboard List</returns>
        public static IEnumerable<scores> GetLeaderboard()
        {
            var query = from scores in Database.scores select scores;
            return query.ToList();
        }


        /// <summary>
        /// Получить данные по определенному разрезу
        /// </summary>
        /// <returns>IEnumerable Leaderboard List</returns>
        public static IEnumerable<scores> GetLeaderboard(int i)
        {
            // равносильно ветке по ИД = 0, то есть все данные
            var query = from scores in Database.scores select scores;

            // разветвляем по типу отбора
            switch (i)
            {
                // данные за сегодня
                case 1:
                    // берем сегодняшнюю дату
                    DateTime now = DateTime.Today;
                    query = (from scores s
                               in Database.scores
                            where s.Day == now
                           select s);
                    break;

                // данные за неделю
                case 2:
                    // получаем начало недели
                    DateTime week_start = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                    //получаем конец недели
                    DateTime week_end = week_start.AddDays(7);
                    query = (from scores sc
                               in Database.scores
                             where sc.Day >= week_start
                                && sc.Day < week_end
                             select sc);
                    break;

                // данные за год
                case 3:
                    // получаем начало года
                    DateTime year_start = new DateTime(DateTime.Now.Year, 1, 1);
                    //получаем конец года
                    DateTime year_end = new DateTime(DateTime.Now.Year, 12, 31);
                    query = (from scores sc
                               in Database.scores
                             where sc.Day >= year_start
                                && sc.Day <= year_end
                             select sc);
                    break;
            }
          
            return query.ToList();
        }
    }
}