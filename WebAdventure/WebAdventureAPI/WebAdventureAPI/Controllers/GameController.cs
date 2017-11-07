﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAdventureAPI.Models;
using WebAdventureAPI.Models.Dtos;
using WebAdventureAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebAdventureAPI.Models.Responses;

namespace WebAdventureAPI.Controllers
{
    [Route("api/games")]
    public class GameController : Controller
    {
        private IWARepository repo;
        private UserManager<WAUser> userManager;
        private GameResponses responses;

        public GameController(IWARepository repo, UserManager<WAUser> userManager)
        {
            this.repo = repo;
            this.userManager = userManager;
            responses = new GameResponses();
        }

        [HttpGet]
        public JsonResult GetAllGames()
        {
            var list = new List<GameDto>();
            foreach (var x in repo.GetAllGames())
            {
                list.Add(new GameDto
                {
                    Id = x.Id,
                    Author = userManager.Users.FirstOrDefault(a => a.Id == x.AuthorId).UserName,
                    Genre = repo.GetGenreById(x.GenreId).Descr,
                    Name = x.Name,
                    Descr = x.Descr
                });
            }

            return Json(list);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public IActionResult CreateGame([FromBody] GameDto gameDto)
        {
            try
            {
                var newGame = new Game
                {
                    Name = gameDto.Name,
                    Descr = gameDto.Descr,
                    AuthorId = gameDto.Author,
                    GenreId = repo.GetGenreByDescr(gameDto.Genre).Id,
                };

                if (gameDto.Id == 0)
                {
                    repo.AddGameToDb(newGame);

                    gameDto.Id = repo.GetGameId(newGame);

                    return StatusCode(201, responses.CreateRepsone(gameDto));
                }
                else
                {
                    newGame.Id = gameDto.Id;

                    repo.UpdateGame(newGame);

                    return StatusCode(204, responses.UpdateResponse(gameDto));
                }
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorResponse.ServerError);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        public IActionResult DeleteGame([FromBody] GameIdDto gameIdDto)
        {
            try
            {
                // get game
                // delete everything from every table
                // delete game
                // returhn success
                return null;
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorResponse.ServerError);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("author")]
        public JsonResult GetGameByAuthor([FromBody] AuthorDto authorDto)
        {
            var list = new List<GameDto>();
            foreach (var x in repo.GetGamesByAuthor(authorDto.Author))
            {
                list.Add(new GameDto
                {
                    Author = userManager.Users.FirstOrDefault(a => a.Id == x.AuthorId).UserName,
                    Genre = repo.GetGenreById(x.GenreId).Descr,
                    Name = x.Name,
                    Descr = x.Descr
                });
            }

            return Json(list);
        }

        private WAUser VerifyUserCredentials(string email, string passwordHash)
        {
            return userManager.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == passwordHash);
        }
    }
}
