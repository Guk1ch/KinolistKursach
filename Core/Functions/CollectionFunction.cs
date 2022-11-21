using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Core.DataBase;

namespace Core.Functions
{
	public class CollectionFunction
	{
		public static ObservableCollection<Collection> collections { get; set; }
		public static ObservableCollection<Film_Collection> films { get; set; }
		public static Collection collectionForDelete { get; set; }
		public static Film_Collection Film_CollectionForDelete { get; set; }
		public static ObservableCollection<Collection> GetCollection(int idUser)
		{
			return collections = new ObservableCollection<Collection>((BdConnection.connection.Collection.Where(userCollectiom => userCollectiom.ID_User == idUser && userCollectiom.IsDeleted != true)).ToList());
		}
		public static ObservableCollection<Collection> GetFriendCollection(int idUser)
		{
			return collections = new ObservableCollection<Collection>((BdConnection.connection.Collection.Where(userCollectiom => userCollectiom.ID_User == idUser && userCollectiom.IsDeleted != true && userCollectiom.Inkognito != true)).ToList());
		}
		public static ObservableCollection<Film_Collection> GetFilmInCollection(int idColl)
		{
			return films = new ObservableCollection<Film_Collection>((BdConnection.connection.Film_Collection.Where(a => a.ID_Collection == idColl)).ToList());
		}
		public static bool NewCollection(string nameCollection, int userID)
		{
			Collection newCollection = new Collection();
			if (UniqueCollection(userID, nameCollection))
			{
				newCollection.ID_User = userID;
				newCollection.Name = nameCollection;
				BdConnection.connection.Collection.Add(newCollection);
				BdConnection.connection.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}
		public static void EditCollectionName(int idColl, string newName)
		{
			Collection editcollection = BdConnection.connection.Collection.Where(userCollectiom => userCollectiom.ID == idColl).FirstOrDefault();
			editcollection.Name = newName;
			BdConnection.connection.SaveChanges();
		}
		public static bool UniqueCollection(int idUser, string name)
		{
			bool uniq = true;
			var unique = GetCollection(idUser);
			foreach (var i in unique)
			{
				if (i.Name == name)
				{
					uniq = false;
				}
			}
			return uniq;
		}
		public static void ViewedFilm(int idUser, int idFilm)
		{
			var FilmColl = new Film_Collection();
			var allColl = CollectionFunction.GetCollection(idUser);
			int viewedCollection = 0;
			foreach (var i in allColl)
			{
				if (i.Name == "Просмотрено")
				{
					viewedCollection = i.ID;
				}
			}
			FilmColl.ID_Collection = viewedCollection;
			FilmColl.ID_Film = idFilm;
			FilmColl.Date = DateTime.Now;
			Film_Collection UnigueFilmCollection = BdConnection.connection.Film_Collection.Where(a => a.ID_Collection == FilmColl.ID_Collection && a.ID_Film == FilmColl.ID_Film).FirstOrDefault();
			if (UnigueFilmCollection == null)
			{
				BdConnection.connection.Film_Collection.Add(FilmColl);
				BdConnection.connection.SaveChanges();
			}
		}
		public static void UnviewedFilm(int idUser, int idFilm)
		{
			var FilmColl = new Film_Collection();
			var allColl = CollectionFunction.GetCollection(idUser);
			int viewedCollection = 0;
			foreach (var i in allColl)
			{
				if (i.Name == "Просмотрено")
				{
					viewedCollection = i.ID;
				}
			}
			FilmColl.ID_Collection = viewedCollection;
			FilmColl.ID_Film = idFilm;
			Film_Collection DeletedFilmCollection = BdConnection.connection.Film_Collection.Where(a => a.ID_Collection == FilmColl.ID_Collection && a.ID_Film == FilmColl.ID_Film).FirstOrDefault();
			BdConnection.connection.Film_Collection.Remove(DeletedFilmCollection);
			BdConnection.connection.SaveChanges();
		}
		public static bool Viewed(int idUser, int idFilm)
		{
			var FilmColl = new Film_Collection();
			var allColl = CollectionFunction.GetCollection(idUser);
			int viewedCollection = 0;
			foreach (var i in allColl)
			{
				if (i.Name == "Просмотрено")
				{
					viewedCollection = i.ID;
				}
			}
			FilmColl.ID_Collection = viewedCollection;
			FilmColl.ID_Film = idFilm;
			Film_Collection DeletedFilmCollection = BdConnection.connection.Film_Collection.Where(a => a.ID_Collection == FilmColl.ID_Collection && a.ID_Film == FilmColl.ID_Film).FirstOrDefault();
			if (DeletedFilmCollection != null)
			{
				return true;
			}
			else
			{
				return false;
			}

		}
		public static void DeletedCollection(int IDCollection)
		{
			collectionForDelete = BdConnection.connection.Collection.Where(userCollectiom => userCollectiom.ID == IDCollection).FirstOrDefault();
			collectionForDelete.IsDeleted = true;
			BdConnection.connection.SaveChanges();
		}
		public static void DeletedFilmInCollection(int IDColl, int IDFilm)
		{
			Film_CollectionForDelete = BdConnection.connection.Film_Collection.Where(a => a.ID_Collection == IDColl && a.ID_Film == IDFilm).FirstOrDefault();
			BdConnection.connection.Film_Collection.Remove(Film_CollectionForDelete);
			BdConnection.connection.SaveChanges();
		}
		public static void InkognitoCollection(int IDColl)
		{
			Collection inckognitoCollection = BdConnection.connection.Collection.Where(a => a.ID == IDColl).FirstOrDefault();
			inckognitoCollection.Inkognito = true;
			BdConnection.connection.SaveChanges();
		}
		public static void NotInkognitoCollection(int IDColl)
		{
			Collection inckognitoCollection = BdConnection.connection.Collection.Where(a => a.ID == IDColl).FirstOrDefault();
			inckognitoCollection.Inkognito = false;
			BdConnection.connection.SaveChanges();
		}
	}
}
