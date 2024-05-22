using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using System.Collections.Generic;
namespace WebApplication1.ViewModels
{
	public class PredmetViewModel
	{
		public IList<Predmet> Predmeti { get; set; }
		public string SearchStringI { get; set; }
		public string SearchStringP { get; set; }
	}
}