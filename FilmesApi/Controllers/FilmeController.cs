using Filmes.Models;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.Controllers;

[ApiController]
[Route("[controller]")]

public class FilmeController : ControllerBase
{

    private static List<Filme> filmes = new List<Filme>();
    private static int id = 1;
  
    [HttpPost]
    public IActionResult AdicionarFilme([FromBody] Filme filme) 
    {
        filme.Id = id++;
        filmes.Add(filme);
       return CreatedAtAction(nameof(RecuperaFilmePorId),
            new {id = filme.Id, filme});
       
    }

    [HttpGet]

    public IEnumerable<Filme> RecuperarFilmes([FromQuery]int skip = 0,
    [FromQuery]int take =50)
    {
        return filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id) 
    {
      var filme = filmes.FirstOrDefault(filme => filme.Id == id);
        if (filmes is null) return NotFound();
        return Ok(filme);
    }
}
