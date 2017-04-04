using System.Threading.Tasks;
using System.Web.Http;
using Api.Identity;
using Api.Models;

namespace Api.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        [Route]
        [HttpGet]
        public IHttpActionResult GetAccount()
        {
            return Ok(new { result = "success" });
        }

        [Route]
        [HttpPost]
        public async Task<IHttpActionResult> CreateAccount(CreateAccountModel model)
        {
            var userStore = new ApplicationUserStore();

            var usermanager = new ApplicationUserManager(userStore);

            var user = new ApplicationUser
            {
                UserName = model.Email
            };

            var result = await usermanager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}