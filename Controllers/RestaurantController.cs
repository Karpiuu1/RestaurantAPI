﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute] int id) 
        {

            _restaurantService.Update(id, dto);
       
            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);

             return NoContent();
        
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        
        public ActionResult CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
            
            var id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll([FromQuery] string searchPhrase) 
        {
            var restaurantDtos = _restaurantService.GetAll(searchPhrase);
            return Ok(restaurantDtos);
        }

       // [HttpGet]
      //  [Authorize(Policy ="CreatedAtLeast2Restaurants")]
      //  public ActionResult<IEnumerable<RestaurantDto>> GetAll()
      // {
       //     var restaurantsDtos = _restaurantService.GetAll();

      //      return Ok(restaurantsDtos);
      //  }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);

            return Ok(restaurant);
        }
    }
}
