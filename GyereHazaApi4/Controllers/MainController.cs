using GyereHazaApi4.Authentication;
using GyereHazaApi4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GyereHazaApi4.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext;

        public MainController(ApplicationDbContext context)
        {
            _dbcontext = context;
        }
        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("Account")]
        public async Task<IActionResult> AccountSetting([FromBody] Owner owner)
        {
            Models.Owner own = new();
            own = await _dbcontext.Owners.FindAsync(owner.Id);
            if (own != null)
            {
                own.Name = owner.Name;
                own.PhoneNumber = owner.PhoneNumber;
                own.Area= owner.Area;
                _dbcontext.Owners.Update(own);
            }
            else
            {
                _dbcontext.Add(owner);
            }
            _dbcontext.SaveChanges();
            return Ok();
        }
        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("Pet")]
        public async Task<IActionResult> PetSetting([FromBody] Pet pet)
        {
            if (pet.Id != Guid.Empty)
            {
                Models.Pet petmodel = new();
                petmodel = await _dbcontext.Pets.FindAsync(pet.Id);
                if (petmodel != null)
                {
                    petmodel.Id = Guid.NewGuid();
                    petmodel.Name = pet.Name;
                    petmodel.OwnerId = pet.OwnerId;
                    petmodel.TypeOfAnimal = pet.TypeOfAnimal;
                    petmodel.Color = pet.Color;
                    petmodel.Breed= pet.Breed;
                    petmodel.Sex= pet.Sex;
                    petmodel.Issues = pet.Issues;
                    petmodel.Other= pet.Other;
                    petmodel.is_Missing= pet.is_Missing;
                    _dbcontext.Pets.Update(petmodel);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                pet.Id = Guid.NewGuid();
                _dbcontext.Pets.Add(pet);
            }
            _dbcontext.SaveChanges();
            return Ok();
        }
        [HttpGet("GetPets/{OwnerId}")]
        public async Task<ActionResult<List<Pet>>> GetPets(Guid OwnerId)
        {
            List<Pet> PetList = await _dbcontext.Pets.Where(x => x.OwnerId == OwnerId).ToListAsync();
            return PetList;
        }

    }
}
