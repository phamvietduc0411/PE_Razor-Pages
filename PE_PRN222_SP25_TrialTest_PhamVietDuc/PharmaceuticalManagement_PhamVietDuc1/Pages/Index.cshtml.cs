using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Service.Services;
using System.ComponentModel.DataAnnotations;

namespace PharmaceuticalManagement_PhamVietDuc1.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly AccountService _service;

    [BindProperty]
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string EmailAddress { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string MemberPassword { get; set; }

    public string ErrorMessage { get; set; }

    public IndexModel(ILogger<IndexModel> logger, AccountService service)
    {
        _logger = logger;
        _service = service;
        ErrorMessage = string.Empty;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var account = await _service.Login(EmailAddress, MemberPassword);

        if (account == null)
        {
            ErrorMessage = "Email or Password incorrect";
            return Page();
        }

        if (account.Role != 2 && account.Role !=3)
        {
            ErrorMessage = "You don't have permission";
            return Page();
        }
        //set session
        HttpContext.Session.SetString("UserEmail", account.EmailAddress);
        HttpContext.Session.SetInt32("UserRole", account.Role ?? 0);


        return RedirectToPage("/Pharmaceutical/Index");
    }

}