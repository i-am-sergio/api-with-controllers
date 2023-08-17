using APIWithControllers.Models;
using APIWithControllers.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIWithControllers.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController() { }

    // This is an HTTP GET endpoint that returns a list of all pizzas.
    [HttpGet] // http://localhost:{PORT}/pizza
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

    // This function retrieves a pizza with a specific ID and returns it as an ActionResult.
    [HttpGet("{id}")] // http://localhost:{PORT}/pizza/id
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);
        if (pizza == null)
            return NotFound();
        return pizza;
    }

    // POST action
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }
    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();

        var existingPizza = PizzaService.Get(id);
        if (existingPizza is null)
            return NotFound();

        PizzaService.Update(pizza);

        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza is null)
            return NotFound();

        PizzaService.Delete(id);

        return NoContent();
    }
}