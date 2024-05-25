using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public class UserRoleAssignmentService
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserRoleAssignmentService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task AssignAdminRoleAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            await _userManager.AddToRoleAsync(user, "Admin");
        }
    }
}