using System;
using System.Data;
using ToDoListProjet.DataAccess.Models;
using ServiceStack.OrmLite;

namespace ToDoListProjet.DataAccess
{
    class DbContext
    {
        private static IDbConnection _db;
        public static Exception Exception;

        public static IDbConnection GetInstance()
        {
            var dbFactory = new OrmLiteConnectionFactory(
                "Data Source=Database/todo.db;Version=3;",
                SqliteDialect.Provider
                );

            try
            {
                if(_db == null)
                {
                    _db = dbFactory.Open();
                    Migrate();
                }
                if (_db.State == ConnectionState.Broken || _db.State == ConnectionState.Closed)
                    _db = dbFactory.Open();

                return _db;
            }
            catch (Exception erreur)
            {
                Exception = erreur;
                return null;
            }
        }

        private static void Migrate()
        {
            var db = GetInstance();

            if(db.CreateTableIfNotExists<Category>())
            {
                db.Save(new Category() { CategoryName = "Projet" });
                db.Save(new Category() { CategoryName = "Travail" });
                db.Save(new Category() { CategoryName = "Personnel" });
            }

            if (db.CreateTableIfNotExists<TodoItem>())
            {
                db.Save(new TodoItem()
                {
                    CategoryId = 1,
                    Description = "Cocher la tâche pour qu'elle soit accomplie"
                });

                db.Save(new TodoItem()
                {
                    CategoryId = 1,
                    Description = "Filtrer les dates du calendrier"
                });

                db.Save(new TodoItem()
                {
                    CategoryId = 1,
                    Description = "Regoupre des tâches à l'aide des catégories"
                });
            }
        }
    }
}
