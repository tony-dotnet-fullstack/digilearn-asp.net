using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using CoreModule.Application.Category.Edit;
using CoreModule.Facade.Category;
using Tony.Web.Infrastructure.RazorUtils;

namespace Tony.Web.Areas.Admin.Pages.Courses.Categories
{
    [BindProperties]
    public class EditModel : BaseRazor
    {
        private ICourseCategoryFacade _categoryFacade;

        public EditModel(ICourseCategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} Enter")]
        public string Title { get; set; }

        [Display(Name = "Title انگلیسی")]
        [Required(ErrorMessage = "{0} Enter")]
        public string Slug { get; set; }
        public async Task<IActionResult> OnGet(Guid id)
        {
            var category = await _categoryFacade.GetById(id);
            if (category == null)
                return RedirectToPage("Index");


            Title = category.Title;
            Slug = category.Slug;
            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            var result = await _categoryFacade.Edit(new EditCategoryCommand()
            {
                Slug = Slug,
                Title = Title,
                Id = id
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
