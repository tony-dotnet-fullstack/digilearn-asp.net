using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using BlogModule.Services;
using BlogModule.Services.DTOs.Command;
using BlogModule.Services.DTOs.Query;
using Common.Application;
using Tony.Web.Infrastructure;
using Tony.Web.Infrastructure.RazorUtils;
using Tony.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserModule.Core.Services;

namespace Tony.Web.Areas.Admin.Pages.Blog.Posts;

public class IndexModel : BaseRazorFilter<BlogPostFilterParams>
{
    private IBlogService _blogService;
    private IUserFacade _userFacade;
    private IRenderViewToString _renderViewToString;

    public IndexModel(IBlogService blogService, IUserFacade userFacade, IRenderViewToString renderViewToString)
    {
        _blogService = blogService;
        _userFacade = userFacade;
        _renderViewToString = renderViewToString;
    }


    public List<BlogCategoryDto> Categories { get; set; }
    public BlogPostFilterResult FilterResult { get; set; }



    [Display(Name = "Title")]
    [Required(ErrorMessage = "{0} Enter")]
    [BindProperty]
    public string Title { get; set; }

    [Display(Name = "name Author")]
    [Required(ErrorMessage = "{0} Enter")]
    [BindProperty]
    public string OwnerName { get; set; }

    [Display(Name = "discription")]
    [Required(ErrorMessage = "{0} Enter")]
    [UIHint("Ckeditor4")]
    [BindProperty]
    public string Description { get; set; }

    [Display(Name = "slug")]
    [Required(ErrorMessage = "{0} Enter")]
    [BindProperty]
    public string Slug { get; set; }

    [Display(Name = "picture article")]
    [Required(ErrorMessage = "{0} Enter")]
    [BindProperty]
    public IFormFile ImageFile { get; set; }


    [Display(Name = "دسته بندی")]
    [Required(ErrorMessage = "{0} Enter")]
    [BindProperty]
    public Guid CategoryId { get; set; }

    public async Task OnGet()
    {
        Categories = await _blogService.GetAllCategories();
        FilterResult = await _blogService.GetPostsByFilter(FilterParams);

        var user = await _userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
        if (user != null)
        {
            OwnerName = user.Name + " " + user.Family;
        }
    }

    public async Task<IActionResult> OnGetShowEditPage(Guid id)
    {
        return await AjaxTryCatch(async () =>
        {
            var post = await _blogService.GetPostById(id);
            if (post == null)
                return OperationResult<string>.NotFound();

            var categories = await _blogService.GetAllCategories();

            var viewModel = new EditPostViewModel
            {
                Categories = categories,
                Id = post.Id,
                CategoryId = post.CategoryId,
                Title = post.Title,
                UserId = post.UserId,
                OwnerName = post.OwnerName,
                Description = post.Description,
                Slug = post.Slug,
            };
            var view = await _renderViewToString.RenderToStringAsync("_Edit", viewModel, PageContext);
            return OperationResult<string>.Success(view);
        });
    }
    public async Task<IActionResult> OnPostEdit(EditPostViewModel viewModel)
    {
        return await AjaxTryCatch(async () => await _blogService.EditPost(new EditPostCommand()
        {
            Title = viewModel.Title,
            Slug = viewModel.Slug,
            CategoryId = viewModel.CategoryId,
            Description = viewModel.Description,
            Id = viewModel.Id,
            ImageFile = viewModel.ImageFile,
            OwnerName = viewModel.OwnerName,
        }));
    }
    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        return await AjaxTryCatch(() => _blogService.DeletePost(id));
    }
    public async Task<IActionResult> OnPost()
    {
        var result = await _blogService.CreatePost(new CreatePostCommand()
        {
            UserId = User.GetUserId(),
            OwnerName = OwnerName,
            CategoryId = CategoryId,
            Description = Description,
            Slug = Slug,
            Title = Title,
            ImageFile = ImageFile
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}