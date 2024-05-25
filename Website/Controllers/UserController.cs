using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class UserController : Controller
{
    private readonly UserRoleAssignmentService _userRoleAssignmentService;

    public UserController(UserRoleAssignmentService userRoleAssignmentService)
    {
        _userRoleAssignmentService = userRoleAssignmentService;
    }

    public async Task<IActionResult> AssignAdminRole(string userId)
    {
        await _userRoleAssignmentService.AssignAdminRoleAsync(userId);
        return RedirectToAction("Index", "Home"); // Redirect to the home page or another page
    }
}