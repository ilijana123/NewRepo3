using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using WebApplication1.Models;
namespace WebApplication1.ViewModels
{
    public class SEditViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<int>? SelectedCasovi { get; set; }
        public IEnumerable<SelectListItem>? CasoviList { get; set; }
    }
}