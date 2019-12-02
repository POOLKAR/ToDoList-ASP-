using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList_Jefremov_.Models
{
	public class ToDo
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Date { get; set; }
		public string Description { get; set; }
		public bool isDone { get; set; }
		public virtual ApplicationUser User { get; set; }
	}
}