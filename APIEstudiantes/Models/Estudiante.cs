namespace APIEstudiantes;

public class Estudiante
{
    public int ID { get; set; }
    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public DateOnly FechaNacimiento {get; set;}

    public string CorreoElectronico { get; set; }
}