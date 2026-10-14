using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caso_Empleado
{
    internal class Program
    {
        static int max_emp = 50;
        static string[] nombres_empl = new string[max_emp];
        static string[] correo_empl = new string[max_emp];
        static int contador_emp = 0;
        static string rutaArchivo = "empleados_datos.txt";
        static public void Title()
        {
            string titulo = "SISTEMA DE EMPLEADOS ARREGLOS Y CADENAS";
            int posicion = (Console.WindowWidth - titulo.Length) / 2;

            Console.SetCursorPosition(posicion, Console.CursorTop);
            Console.WriteLine("--------------------------------------");

            Console.SetCursorPosition(posicion, Console.CursorTop);
            Console.WriteLine(titulo);

            Console.SetCursorPosition(posicion, Console.CursorTop);
            Console.WriteLine("--------------------------------------");
        }

        static public void RegistrarEmpleado()
        {
            Console.WriteLine("*****REGISTRO DE EMPLEADOS*****");
            if (contador_emp >= max_emp)
            {
                Console.WriteLine("No se pueden registrar más empleados. Se ha alcanzado el límite máximo.");
                return;
            }

            Console.Write("Ingrese el nombre y apellido del empleado: ");
            string nombres = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(nombres))
            {
                Console.WriteLine("!!!!ERROR: El nombre no puede estar vacío...!!!!");
                return;
            }
            string nombres_Mayus = nombres.ToUpper();
            string[] partes = nombres_Mayus.Split(' ');
            string primer_nombre;
            if (partes.Length > 0)
                primer_nombre = partes[0];
            else
                primer_nombre = "Usuario";
            string primer_apellido;
            if (partes.Length > 0)
                primer_apellido = partes[1];
            else
                primer_apellido = "";
            string correo = $"{primer_nombre.ToLower()}.{primer_apellido.ToLower()}@upn.edu.pe";
            nombres_empl[contador_emp] = nombres_Mayus;
            correo_empl[contador_emp] = correo;
            contador_emp++;
        }
        static public void Buscar_empleado()
        {
            Console.WriteLine("*****BUSCAR EMPLEADO*****");
            Console.Write("************************");
            Console.WriteLine("\nIngrese el nombre del empleado a buscar: ");
            if (contador_emp == 0)
            {
                Console.WriteLine("No hay empleados registrados.");
                return;
            }
            string buscar_nombre = Console.ReadLine()?.Trim().ToUpper();
            if (string.IsNullOrEmpty(buscar_nombre))
            {
                Console.WriteLine("!!!!ERROR: El nombre no puede estar vacío...!!!!");
                return;
            }
            bool encontrado = false;

            for (int i = 0; i < contador_emp; i++)
            {
                if (nombres_empl[i].Contains(buscar_nombre))
                {
                    Console.WriteLine($"Nombre: {nombres_empl[i]} - Correo: {correo_empl[i]}");
                    encontrado = true;
                }
            }
            if (!encontrado)
            {
                Console.WriteLine("No se encontró ningún empleado con ese nombre.");
            }

        }
        static public void ordenar_alfabeticamente()
        {
            if (contador_emp <= 1)
            {
                Console.WriteLine("No hay suficientes empleados para ordenar.");
                return;
            }
            for (int i = 0; i < contador_emp - 1; i++)
            {
                for (int j = 0; j < contador_emp - 1 - i; j++)
                {
                    if (string.Compare(nombres_empl[j], nombres_empl[j + 1]) > 0)
                    {
                        string temp_nombre = nombres_empl[j];
                        nombres_empl[j] = nombres_empl[j + 1];
                        nombres_empl[j + 1] = temp_nombre;

                        string temp_correo = correo_empl[j];
                        correo_empl[j] = correo_empl[j + 1];
                        correo_empl[j + 1] = temp_correo;
                    }

                }
            }
        }
        static public void Mostrar_empleados()
        {
            Console.WriteLine("*****LISTA DE EMPLEADOS*****");
            Console.WriteLine("****************************");
            if (contador_emp == 0)
            {
                Console.WriteLine("No hay empleados registrados.");
                return;
            }
            Console.WriteLine("Nombres y apellidos\tCorreo corporativo");
            for (int i = 0; i < contador_emp; i++)
            {
                Console.WriteLine($"{nombres_empl[i]}\t\t{correo_empl[i]}");
            }
        }
        static public void Guardar_datos_archivo()
        {
            Console.WriteLine("*****GUARDAR DATOS EN ARCHIVO*****");
            Console.WriteLine("**********************************");
            if (contador_emp == 0)
            {
                Console.WriteLine("No hay empleados registrados para guardar.");
                return;
            }
            using (StreamWriter escribir = new StreamWriter(rutaArchivo))
            {
                for (int i = 0; i < contador_emp; i++)
                {
                    escribir.WriteLine($"{nombres_empl[i]},{correo_empl[i]}");
                }
            }
            Console.WriteLine($"{contador_emp} empleados guardados.\nDatos guardados en el archivo '{rutaArchivo}' correctamente.");
        }
        static public void Leer_datos_archivo()
        {
            Console.WriteLine("*****LEER DATOS DESDE ARCHIVO*****");
            Console.WriteLine("**********************************");
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine($"El archivo '{rutaArchivo}' no existe.");
                return;
            }
            using (StreamReader lector = new StreamReader(rutaArchivo))
            {
                string linea;
                while ((linea = lector.ReadLine()) != null)
                {
                    string[] partes = linea.Split(',');
                    if (partes.Length == 2)
                    {
                        Console.WriteLine($"Nombres: {partes[0]} Correo: {partes[1]}");
                    }
                }

            }
        }

        static public void Cargar_datos_desde_archivo()
        {
            Console.WriteLine("*****CARGAR DATOS DESDE ARCHIVO*****");
            Console.WriteLine("************************************");
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine($"El archivo '{rutaArchivo}' no existe.");
                return;
            }
            string[] lineas = File.ReadAllLines(rutaArchivo);
            contador_emp = 0;
            for (int i = 0; i < lineas.Length && i < max_emp; i++)
            {
                string[] partes = lineas[i].Split(',');
                if (partes.Length == 2)
                {
                    nombres_empl[contador_emp] = partes[0];
                    correo_empl[contador_emp] = partes[1];
                    contador_emp++;

                }

            }
            Console.WriteLine($"{contador_emp} empleados cargados desde el archivo '{rutaArchivo}' correctamente.");
        }
        static void Main(string[] args)
        {
            int opcion = 0;
            while (opcion != 8)
            {
                Console.Clear();
                Title();
                Console.WriteLine("1. Registrar empleado");
                Console.WriteLine("2. Buscar empleado");
                Console.WriteLine("3. Ordenar empleados alfabéticamente");
                Console.WriteLine("4. Mostrar empleados");
                Console.WriteLine("5. Guardar datos en archivo");
                Console.WriteLine("6. Leer datos desde archivo");
                Console.WriteLine("7. Cargar datos desde archivo");
                Console.WriteLine("8. Salir");
                Console.Write("Ingrese una opción: ");
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("!!!!ERROR: Ingrese una opción numerica...!!!!");
                    continue;
                }
                switch (opcion)
                {
                    case 1:
                        RegistrarEmpleado(); break;
                    case 2:
                        Buscar_empleado(); break;
                    case 3:
                        ordenar_alfabeticamente(); break;
                    case 4:
                        Mostrar_empleados(); break;
                    case 5:
                        Guardar_datos_archivo(); break;
                    case 6:
                        Leer_datos_archivo(); break;
                    case 7:
                        Cargar_datos_desde_archivo(); break;
                    case 8:
                        Console.WriteLine("Saliendo del programa..."); break;
                    default:
                        Console.WriteLine("!!!!ERROR: Opción inválida...!!!!"); break;
                }
                Console.ReadKey();
            }
        }
    }
}