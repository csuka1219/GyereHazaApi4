using GyereHazaApi4.Authentication;
using GyereHazaApi4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GyereHazaApi4.Controllers
{
    //[Authorize]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        [Inject]
        public ApplicationDbContext dbContext { get; set; }
        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("Account")]
        public async Task<IActionResult> AccountSetting()
        {
            Models.Owner owners = new();
            //using var context = new ApplicationDbContext();
            return Ok();
        }
    }
}
