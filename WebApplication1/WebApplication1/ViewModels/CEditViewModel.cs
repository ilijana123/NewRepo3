using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using WebApplication1.Models;
namespace WebApplication1.ViewModels
{
    public class CEditViewModel
    {
        public Casovi Casovi { get; set; }
        public IEnumerable<int>? SelectedStudenti { get; set; }
        public IEnumerable<SelectListItem>? StudentList { get; set; }
    }
}