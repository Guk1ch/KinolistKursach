using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Core.DataBase;

namespace Core.Functions
{
	public class FilmFunction
	{
		public static ObservableCollection<Film> films { get; set; }
		public static ObservableCollection<Film> GetFilm()
		{
			return new ObservableCollection<Film>(BdConnection.connection.Film.ToList());
		}
        public static Film GetFilmInfo(int id)
        {
            Film selectedFilm = BdConnection.connection.Film.Where(filmId => filmId.ID == id).FirstOrDefault();
            Film film = new Film()
            {
                ID = selectedFilm.ID,
                Name = selectedFilm.Name,
                Poster = null,
                Description = selectedFilm.Description,
                Date_Issue = selectedFilm.Date_Issue,
                Duration = selectedFilm.Duration
            };
            return film;
        }
        public static ObservableCollection<Film> SearchFilm(string name)
        {
            return films = new ObservableCollection<Film>(BdConnection.connection.Film.Where(a => a.Name.Contains(name)).ToList());
        }
    }
}
