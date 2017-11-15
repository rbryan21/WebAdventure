﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdventureAPI.Models.Dtos;

namespace WebAdventureAPI.Models.Responses
{
    public class GameResponses : Response
    {
        public List<GameDto> Games { get; set; }

        public GameResponses CreateResponse(GameDto game)
        {
            return new GameResponses
            {
                StatusText = "New game successfully created!",
                StatusCode = 201,
                Status = true,
                Games = new List<GameDto>
                {
                    game
                }
            };
        }

        public GameResponses UpdateResponse(GameDto game)
        {
            return new GameResponses
            {
                StatusText = "Game successfully updated!",
                StatusCode = 200,
                Status = true,
                Games = new List<GameDto>
                {
                    game
                }
            };
        }

        public GameResponses AuthorsGamesFound(List<GameDto> games)
        {
            return new GameResponses
            {
                StatusText = "Author's games successfully found!",
                StatusCode = 200,
                Status = true,
                Games = games
            };
        }

        public GameResponses DeletGameResponse() => new GameResponses
        {
            Status = true,
            StatusCode = 201,
            StatusText = "Game Deleted"
        };
    }
}
