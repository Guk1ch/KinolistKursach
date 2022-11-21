using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Core.DataBase;

namespace Core.Functions
{
	public class StatisticFunction
	{
        public static int CountViewedFilm(int UserID)
        {
            var coll = BdConnection.connection.Collection.Where(x => x.ID_User == UserID && x.Name == "Просмотрено").FirstOrDefault();
            var films = BdConnection.connection.Film_Collection.Where(x => x.ID_Collection == coll.ID).Count();
            return films;
        }
        public static int CountTimeViewedFilm(int UserID)
        {
            var coll = BdConnection.connection.Collection.Where(x => x.ID_User == UserID && x.Name == "Просмотрено").FirstOrDefault();
            var films = BdConnection.connection.Film_Collection.Where(x => x.ID_Collection == coll.ID).ToList();
            int time = 0;
            foreach (var i in films)
            {
                time = (int)(time + i.Film.Duration);
            }
            return time;
        }
    }
}
