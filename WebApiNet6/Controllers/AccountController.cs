using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiNet6.Models;
using WebApiNet6.Repositories;

namespace WebApiNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var result = await _accountRepository.SignUpAsync(model);
            if(!result.Succeeded)
            {
                return Unauthorized();
            }
            return Ok(result.Succeeded);
        }
        [HttpPost]
        [Route("/SignIn")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var result = await _accountRepository.SignInAsync(model);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}
