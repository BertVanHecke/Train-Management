using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreinRittenApplicatie_VanHeckeBert.API.Models;
using TreinRittenApplicatie_VanHeckeBert.API.ViewModels;

namespace TreinRittenApplicatie_VanHeckeBert.API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        //Get api/Station
        [HttpGet]
        public async Task<IEnumerable<UserViewModel>> Get()
        {
            var users = await _userManager.Users.ToListAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }
    }
}
