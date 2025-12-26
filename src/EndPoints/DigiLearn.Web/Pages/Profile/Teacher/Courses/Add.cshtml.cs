using System.ComponentModel.DataAnnotations;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using CoreModule.Application.Course.Create;
using CoreModule.Domain.Course.Enums;
using CoreModule.Facade.Course;
using CoreModule.Facade.Teacher;
using Tony.Web.Infrastructure;
using Tony.Web.Infrastructure.RazorUtils;
using Tony.Web.Infrastructure.Utils.CustomValidation.IFormFile;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tony.Web.Pages.Profile.Teacher.Courses;


[ServiceFilter(typeof(TeacherActionFilter))]
[BindProperties]
public class AddModel : BaseRazor
{
    private readonly ICourseFacade _courseFacade;
    private readonly ITeacherFacade _teacherFacade;
    public AddModel(ICourseFacade courseFacade, ITeacherFacade teacherFacade)
    {
        _courseFacade = courseFacade;
        _teacherFacade = teacherFacade;
    }


    [Display(Name = "")]
    [Required(ErrorMessage = "{0} Enter")]
    public Guid CategoryId { get; set; }


    [Display(Name = "branch")]
    [Required(ErrorMessage = "{0} Enter")]
    public Guid SubCategoryId { get; set; }

    [Display(Name = "Title course")]
    [Required(ErrorMessage = "{0} Enter")]
    public string Title { get; set; }


    [Display(Name = "Title  course")]
    [Required(ErrorMessage = "{0} Enter")]
    public string Slug { get; set; }


    [Display(Name = "discription")]
    [Required(ErrorMessage = "{0} Enter")]
    [UIHint("Ckeditor4")]
    public string Description { get; set; }


    [Display(Name = "picture course")]
    [Required(ErrorMessage = "{0} Enter")]
    [FileImage(ErrorMessage = "picture name")]
    public IFormFile ImageFile { get; set; }

    [Display(Name = " course")]
    [FileType("mp4", ErrorMessage = "")]
    public IFormFile? VideoFile { get; set; }


    [Display(Name = "Price (0=Free(")]
    [Required(ErrorMessage = "{0} Enter")]
    public int Price { get; set; }

    [Display(Name = "سطح course")]
    [Required(ErrorMessage = "{0} Enter")]
    public CourseLevel CourseLevel { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var teacher = await _teacherFacade.GetByUserId(User.GetUserId());

        var result = await _courseFacade.Create(new CreateCourseCommand()
        {
            Status = CourseActionStatus.Pending,
            TeacherId = teacher!.Id,
            Slug = Slug.ToSlug(),
            Title = Title,
            ImageFile = ImageFile,
            VideoFile = VideoFile,
            CourseLevel = CourseLevel,
            CategoryId = CategoryId,
            Description = Description,
            SeoData = new SeoData(Title, Title, Title, null),
            Price = Price,
            SubCategoryId = SubCategoryId
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}