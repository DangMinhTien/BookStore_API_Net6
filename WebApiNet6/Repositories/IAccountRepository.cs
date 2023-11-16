using Microsoft.AspNetCore.Identity;
using WebApiNet6.Models;

namespace WebApiNet6.Repositories
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
    }
}
