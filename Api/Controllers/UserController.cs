using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Api.Responses;
using AutoMapper;
using Core.CustomEntities;
using Core.DTOs;
using Core.Interfaces;
using Core.Entities;
using Core.QueryFilters;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserQueryFilter filters)
        {
            var users = await _userService.GetUsers(filters);
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            var pagedData = new PagedData
            {
                TotalCount = users.TotalCount,
                PageSize = users.PageSize,
                CurrentPage = users.CurrentPage,
                TotalPages = users.TotalPages,
                HasNextPage = users.HasNextPage,
                HasPreviousPage = users.HasPreviousPage
            };

            var response = new ApiResponse<IEnumerable<UserDto>>(usersDto)
            {
                PagedData = pagedData
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(long id)
        {
            var user = await _userService.GetUser(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> SetUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userService.InsertUser(user);
            userDto = _mapper.Map<UserDto>(user);
            var response = new ApiResponse<UserDto>(userDto);
            return Ok(response);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var result= await _userService.UpdateUser(user);
            var response = new ApiResponse<bool>(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var result = await _userService.DeleteUser(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}
