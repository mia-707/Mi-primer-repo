using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionUnidad2
{      
    // Creamos la clase Empleado
    public class Empleado
    {
        // Atributos de la clase Empleado
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Posicion { get; set; }
        public decimal SalarioPorHora { get; set; }
        public int HorasTrabajadasMes { get; set; }

        // Constructor de la clase Empleado
        public Empleado(int id, string nombre, string posicion, decimal salarioPorHora)
        {
            ID = id;
            Nombre = nombre;
            Posicion = posicion;
            SalarioPorHora = salarioPorHora;
            HorasTrabajadasMes = 0; // Dejamos por defecto que las horas trabajadas son cero
        }

        // Método para calcular el salario mensual
        public decimal CalcularSalarioMensual()
        {
            return SalarioPorHora * HorasTrabajadasMes;
        }

        // Método para mostrar los detalles del empleado
        public void MostrarDetalles()
        {
            Console.WriteLine($"");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"ID: {ID}");
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Posición: {Posicion}");
            Console.WriteLine($"Salario por Hora: {SalarioPorHora:C0}");//Le puse C0 para que no hayan decimales
            Console.WriteLine($"Horas Trabajadas: {HorasTrabajadasMes}");
            Console.WriteLine($"Salario Mensual: {CalcularSalarioMensual():C0}");//Aqui lo mismo con el C0
            Console.WriteLine("-------------------------------------------");
        }
    }

    // Creamos la clase Empresa, que es donde almacenaremos a los empleados.
    public class Empresa
    {
        // Lista de empleados
        private List<Empleado> empleados = new List<Empleado>();

        // Método para agregar un nuevo empleado
        public void AgregarEmpleado(Empleado empleado)
        {
            // Verificar si ya existe un empleado con el mismo ID
            if (empleados.Any(e => e.ID == empleado.ID))
            {
                Console.WriteLine("Error: Ya existe un empleado con el mismo ID.");
            }
            else
            {
                empleados.Add(empleado);
                Console.WriteLine("Empleado agregado exitosamente.");
            }
        }

        // Método para editar un empleado existente
        public void EditarEmpleado(int id, string nuevoNombre, string nuevaPosicion, decimal nuevoSalarioPorHora)
        {
            Empleado empleado = empleados.Find(e => e.ID == id);
            if (empleado != null)
            {
                empleado.Nombre = nuevoNombre;
                empleado.Posicion = nuevaPosicion;
                empleado.SalarioPorHora = nuevoSalarioPorHora;
                Console.WriteLine("Empleado editado exitosamente.");
            }
            else
            {
                Console.WriteLine("Empleado no encontrado.");
            }
        }

        // Método para eliminar un empleado
        public void EliminarEmpleado(int id)
        {
            Empleado empleado = empleados.Find(e => e.ID == id);
            if (empleado != null)
            {
                empleados.Remove(empleado);
                Console.WriteLine("Empleado eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("Empleado no encontrado.");
            }
        }

        // Método para listar todos los empleados
        public void ListarEmpleados()
        {
            if (empleados.Count == 0)
            {
                Console.WriteLine("No hay empleados registrados.");
            }
            else
            {
                foreach (Empleado empleado in empleados)
                {
                    empleado.MostrarDetalles();
                }
            }
        }

        // Método para registrar las horas trabajadas de un empleado
        public void RegistrarHorasTrabajadas(int id, int horas)
        {
            Empleado empleado = empleados.Find(e => e.ID == id);
            if (empleado != null)
            {
                if (horas < 0)
                {
                    Console.WriteLine("Las horas trabajadas no pueden ser negativas.");
                }
                else
                {
                    empleado.HorasTrabajadasMes = horas;
                    Console.WriteLine("Horas registradas exitosamente.");
                }
            }
            else
            {
                Console.WriteLine("Empleado no encontrado.");
            }
        }

        // Método para verificar si un empleado existe por ID, Esto es para evitar el agregar un nuevo empleado con un ID ya existente.
        public bool ExisteEmpleado(int id)
        {
            return empleados.Any(e => e.ID == id);
        }
    }
    internal class Program
    {
        static Empresa empresa = new Empresa();

        static void Main(string[] args)
        {
            int opcion;
            // Mostramos el menu en pantalla para seleccionar una opcion, usando el do / while, donde el while hará que cerremos el programa con la opcion 6
            do
            {
                MostrarMenu();
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        AgregarEmpleado();
                        break;
                    case 2:
                        EditarEmpleado();
                        break;
                    case 3:
                        EliminarEmpleado();
                        break;
                    case 4:
                        ListarEmpleados();
                        break;
                    case 5:
                        RegistrarHorasTrabajadas();
                        break;
                    case 6:
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }

            } while (opcion != 6);
        }

        // Mostramos las opciones del menu
        static void MostrarMenu()
        {
            Console.WriteLine("\n--- Menú de Opciones ---");
            Console.WriteLine("1. Agregar Empleado");
            Console.WriteLine("2. Editar Empleado");
            Console.WriteLine("3. Eliminar Empleado");
            Console.WriteLine("4. Listar Empleados");
            Console.WriteLine("5. Registrar Horas Trabajadas");
            Console.WriteLine("6. Salir");
            Console.Write("Elija una opción: ");
        }

        // Opcion de agregar empleado
        static void AgregarEmpleado()
        {
            Console.Write("Ingrese el ID del empleado: ");
            int id = Convert.ToInt32(Console.ReadLine());

            // Verificar si el ID ya existe
            if (empresa.ExisteEmpleado(id))
            {
                Console.WriteLine("Error: Ya existe un empleado con el mismo ID. Intente con un ID diferente.");
                return; // Regresamos al menú si el ID ya existe.
            }

            Console.Write("Ingrese el nombre del empleado: ");
            string nombre = Console.ReadLine();

            Console.Write("Ingrese la posición del empleado: ");
            string posicion = Console.ReadLine();

            decimal salarioPorHora;
            do
            {
                Console.Write("Ingrese el salario por hora del empleado: ");
                salarioPorHora = Convert.ToDecimal(Console.ReadLine());

                if (salarioPorHora < 0)
                {
                    Console.WriteLine("Error: El salario por hora no puede ser negativo. Por favor, ingrese un valor válido.");
                }

            } while (salarioPorHora < 0);

            Empleado empleado = new Empleado(id, nombre, posicion, salarioPorHora);
            empresa.AgregarEmpleado(empleado);
        }

        //Opcion de editar empleado
        static void EditarEmpleado()
        {
            Console.Write("Ingrese el ID del empleado a editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ingrese el nuevo nombre del empleado: ");
            string nombre = Console.ReadLine();

            Console.Write("Ingrese la nueva posición del empleado: ");
            string posicion = Console.ReadLine();

            decimal salarioPorHora;
            do
            {
                Console.Write("Ingrese el nuevo salario por hora del empleado: ");
                salarioPorHora = Convert.ToDecimal(Console.ReadLine());

                if (salarioPorHora < 0)
                {
                    Console.WriteLine("Error: El salario por hora no puede ser negativo. Por favor, ingrese un valor válido.");
                }

            } while (salarioPorHora < 0);

            empresa.EditarEmpleado(id, nombre, posicion, salarioPorHora);
        }

        // Opcion de eliminar empleado
        static void EliminarEmpleado()
        {
            Console.Write("Ingrese el ID del empleado a eliminar: ");
            int id = Convert.ToInt32(Console.ReadLine());
            empresa.EliminarEmpleado(id);
        }

        // Opcion de listar empleados
        static void ListarEmpleados()
        {
            empresa.ListarEmpleados();
        }

        //Opcion de registrar las horas trabajadas del empleado
        static void RegistrarHorasTrabajadas()
        {
            Console.Write("Ingrese el ID del empleado: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Ingrese las horas trabajadas en el mes: ");
            int horas = Convert.ToInt32(Console.ReadLine());

            empresa.RegistrarHorasTrabajadas(id, horas);
        }
    }
}
// Un comentario, fin del programa