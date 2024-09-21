namespace API_LIVRARIA.Controllers
{
    using API_LIVRARIA.Communication.Request;
    using Microsoft.AspNetCore.Mvc;
    using System.Numerics;

    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {

        private static List<Livros> listaDeLivros = new List<Livros>();

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetLivrosById([FromRoute] int id)
        {

            var livro = listaDeLivros.FirstOrDefault(l => l.Id == id);

            if (livro == null)
            {
                return NotFound("Livro não encontrado!");
            }

            return Ok(livro);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Livros), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateLivros([FromBody] Livros request)
        {

            if (listaDeLivros.Any(l => l.Id == request.Id))
            {
                return BadRequest("Livro Ja cadastrado");
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Nome do livro é obrigatório!");
            }

            var novoLivro = new Livros
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description
            };

            listaDeLivros.Add(novoLivro);


            return CreatedAtAction(nameof(GetLivrosById), new { id = novoLivro.Id }, novoLivro);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetAllLivros()
        {
            var listaLivrosAll = listaDeLivros;
            if (listaDeLivros.Count == 0)
            {
                return BadRequest("Não tem livro cadastrado!");
            }
            return Ok(listaLivrosAll);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteLivroPorId([FromRoute] int id)
        {
            var livro = listaDeLivros.FirstOrDefault(l => l.Id == id);

            if (livro == null)
            {
                return BadRequest("Livro nao encontrado");
            }
            listaDeLivros.Remove(livro);
            return NoContent();
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateLivro(int id, [FromBody] Livros request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Nome do livro é obrigatório!");
            }

            var livro = listaDeLivros.FirstOrDefault(l => l.Id == id);

            if (livro == null)
            {
                return NotFound("Livro não encontrado!");
            }

            livro.Name = request.Name;
            livro.Description = request.Description;

            return NoContent();
        }

    }
}
