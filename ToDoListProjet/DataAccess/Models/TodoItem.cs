﻿using System;
using ServiceStack.OrmLite;
using ServiceStack.DataAnnotations;
using ToDoListProjet.DataAccess;

namespace ToDoListProjet.DataAccess.Models
{
    [Alias("todo_list")]
    public class TodoItem
    {
        [PrimaryKey]
        [AutoIncrement]

        public int id { get; set; }

        public string Description { get; set; } = string.Empty;

        [References(typeof(Category))]
        public int CategoryId { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndStae { get; set; } = DateTime.Today.AddDays(1);

        public bool Done { get; set; } = false;


        public Category GetCategory()
            => DbContext.GetInstance().SingleById<Category>(this.CategoryId);


        public string GetStatus()
        {
            if (this.Done) return "Terminé";


            if (this.EndStae >= DateTime.Now)
            {
                return "En cours";
            }
            else
            {
                return "Past due";
            }
        }
    }
}
