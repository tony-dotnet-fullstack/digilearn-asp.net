using System.ComponentModel.DataAnnotations;
using CoreModule.Application.Course.Sections.AddSection;
using CoreModule.Facade.Course;
using Tony.Web.Infrastructure;
using Tony.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tony.Web.Pages.Profile.Teacher.Courses.Sections;


[ServiceFilter(typeof(TeacherActionFilter))]
[BindProperties]
public class AddModel : BaseRazor
{
    private ICourseFacade _courseFacade;

    public AddModel(ICourseFacade courseFacade)
    {
        _courseFacade = courseFacade;
    }


    [Display(Name = "Title")]
    [Required(ErrorMessage = "{0} Enter")]
    public string Title { get; set; }

    [Display(Name = " show")]
    [Required(ErrorMessage = "{0} Enter")]
    public int DisplayOrder { get; set; }


  
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(Guid courseId)
    {
        var result = await _courseFacade.AddSection(new AddCourseSectionCommand()
        {
            Title = Title,
            DisplayOrder = DisplayOrder,
            CourseId = courseId,
        });

        return RedirectAndShowAlert(result, RedirectToPage("Index", new { courseId }));
    }
}