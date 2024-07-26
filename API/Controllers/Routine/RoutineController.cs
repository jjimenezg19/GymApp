﻿using Microsoft.AspNetCore.Mvc;
using BL;
using Microsoft.AspNetCore.Cors;
using DTO;

namespace API.Controllers.Routines
{ 
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoutineController : Controller
    {
        [HttpPost]
        public IActionResult CreateRoutine([FromBody] Routine routine)
        {
            try
            {
                // Imprimir el valor recibido
                Console.WriteLine($"Received creationDate: {routine.creationDate}");

                // Validar que creationDate esté dentro del rango permitido
                if (routine.creationDate < new DateTime(1753, 1, 1) || routine.creationDate > new DateTime(9999, 12, 31))
                {
                    return Json(new { success = false, message = "The date must be between 1/1/1753 and 12/31/9999." });
                }

                RoutineManager manager = new RoutineManager();
                int routineId = manager.CreateRoutine(routine);

                // Redirigir a la vista para agregar ejercicios a la rutina creada
                return Json(new { success = true, message = "Routine created successfully", routineId = routineId });
            }
            catch (Exception ex)
            {
                // Manejar el error y devolver una respuesta JSON con un mensaje de error
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public Routine GetRoutine(int id)
        {
            RoutineManager manager = new RoutineManager();
            Routine routine = manager.GetRoutineById(id);

            return routine;
        }
        [HttpGet]
        public List<Routine> GetAllRoutines()
        {
            RoutineManager manager = new RoutineManager();
            List<Routine> routineList = manager.GetAllRoutines();

            return routineList;
        }

        [HttpPost]
        public IActionResult AddExerciseToRoutine([FromBody] RoutineExercise routineExercise)
        {
            try
            {
                RoutineManager manager = new RoutineManager();
                manager.AddExerciseToRoutine(routineExercise);

                return Json(new { success = true, message = "Exercise added to routine successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
