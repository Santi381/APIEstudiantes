using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Authorization;

namespace APIEstudiantes.Controllers;

[ApiController]
[Route("api/estudiantes")]
[Authorize]

public class EstudianteController : ControllerBase
{
    static Dictionary<int, Estudiante> estudiantes = new Dictionary<int, Estudiante>();

    #region GET
    //GET api/estudiantes
    [HttpGet]
    [Authorize("read:estudiantes")]
    public IEnumerable<Estudiante> Get()
    {
        return new List<Estudiante>(estudiantes.Values);
    }

    //GET api/estudiantes/{id}
    [HttpGet("{idEstudiante}")]
    [Authorize("read:estudiantes")]
    public ActionResult<Estudiante> Get(int idEstudiante)
    {
        Estudiante encontrado;
        estudiantes.TryGetValue(idEstudiante, out encontrado);
        if(encontrado == null)
        {
            return NotFound("No se encotró un estudiante con ese ID");
        } else
        {
            return encontrado;
        }
        
        
    }

    #endregion
    #region POST
    //POST api/estudiantes
    [HttpPost]
    [Authorize("write:estudiantes")]
    public IActionResult Post([FromBody] Estudiante estudiante) //El formato de fecha en Postman es "yyyy-MM-dd"
    {
        Estudiante encontrado;
        estudiantes.TryGetValue(estudiante.ID, out encontrado);
        if (encontrado == null)
        {
            estudiantes.Add(estudiante.ID, estudiante);
            return Ok("Estudiante creado");
        } else
        {
            return BadRequest("La solicitud es incorrecta");
        }
    }

    #endregion
    #region DELETE
    //DELETE api/estudiantes/{id}
    [HttpDelete("{idEstudiante}")]
    [Authorize("write:estudiantes")]
    public IActionResult Delete(int idEstudiante)
    {
        bool respuesta = estudiantes.Remove(idEstudiante);
        if(respuesta == true)
        {
            return Ok("Operación exitosa");
        } else
        {
            return NotFound("No se encontró un estudiante con ese ID");
        }

         
    }

    #endregion
    #region PUT
    //PUT api/estudiantes/{id}
    [HttpPut("{idEstudiante}")]
    [Authorize("write:estudiantes")]
    public IActionResult Put(int idEstudiante, [FromBody] Estudiante estudianteActualizado)
    {
        if (!estudiantes.ContainsKey(idEstudiante))
        {
            return NotFound("No se encontró un estudiante con ese ID");
        }

        estudiantes[idEstudiante].Nombre = estudianteActualizado.Nombre;
        estudiantes[idEstudiante].Apellido = estudianteActualizado.Apellido;
        estudiantes[idEstudiante].FechaNacimiento = estudianteActualizado.FechaNacimiento;
        estudiantes[idEstudiante].CorreoElectronico = estudianteActualizado.CorreoElectronico;

        return Ok("Opearación exitosa");
    }

    #endregion
}