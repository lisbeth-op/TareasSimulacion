using System;

namespace SimuladorAlumnos
{
    class Program
    {
        const float ProbabilidadAprobados1 = 0.21f; // a1%
        const float ProbabilidadAbandono1 = 0.08f; // b1%
        const float ProbabilidadReprobados1 = 1.0f - ProbabilidadAprobados1 - ProbabilidadAbandono1; // c1%

        const float ProbabilidadAprobados2 = 0.06f; // ai%
        const float ProbabilidadAbandono2 = 0.10f; // bi%
        const float ProbabilidadReprobados2 = 1.0f - ProbabilidadAprobados2 - ProbabilidadAbandono2; // ci%

        static void Main(string[] args)
        {
            var random = new Random();
            int opcion;
            int TiempoSimular = 0, AlumnosXAula = 0, AlumnosEntrantesPorAnio = 0;
            int TotalReprobados, TotalAprobados, TotalAbandonos, TotalInscritos, TotalGraduados = 0;
            int MayorCantidadAulasPrevistas = 0, AulasPrevistas = 0;

            do
            {
                Console.Clear();
                ImprimirMenu();

                opcion = GetIntegerInput();

                switch (opcion)
                {
                    case 1: /* SIMULAR */
                        {
                            TotalReprobados = 0;
                            TotalAprobados = 0;
                            TotalAbandonos = 0;
                            TotalInscritos = 0;
                            TotalGraduados = 0;

                            Console.Write("Digite la cantidad de años a simular: ");
                            TiempoSimular = GetIntegerInput();

                            Console.Write("Digite la cantidad de alumnos que podrá tener un aula: ");
                            AlumnosXAula = GetIntegerInput();

                            Console.Write("Digite la cantidad de alumnos que entran a primer año por año: ");
                            AlumnosEntrantesPorAnio = GetIntegerInput();

                            while (TiempoSimular < 1)
                            {
                                Console.Write("El tiempo digitado no es válido.\nPor favor digite un número mayor a cero: ");
                                TiempoSimular = GetIntegerInput();
                            }

                            int[] estudiantesPorAnio = new int[6]; // Índice 0 no se usa, de 1 a 5 para años 1 a 5

                            for (int i = 1; i <= TiempoSimular; i++)
                            {
                                Console.WriteLine($"\n\n|-------------------------------------------- SIMULACION DEL AÑO {i} ---------------------------------------------------|");

                                estudiantesPorAnio[1] += AlumnosEntrantesPorAnio; // Añadir nuevos estudiantes al primer año
                                Console.WriteLine($"Se inscribieron {AlumnosEntrantesPorAnio} estudiantes en el primer año.");

                                for (int año = 5; año >= 1; año--)
                                {
                                    int estudiantesIniciales = estudiantesPorAnio[año];
                                    int AlumnosReprobados = 0, AlumnosAprobados = 0, AlumnosAbandonaron = 0;

                                    for (int estudiante = 1; estudiante <= estudiantesIniciales; estudiante++)
                                    {
                                        double output = random.NextDouble();
                                        if (año == 1) // Primer año
                                        {
                                            if (output <= ProbabilidadReprobados1)
                                            {
                                                AlumnosReprobados++;
                                            }
                                            else if (output > ProbabilidadReprobados1 && output <= ProbabilidadReprobados1 + ProbabilidadAbandono1)
                                            {
                                                AlumnosAbandonaron++;
                                            }
                                            else
                                            {
                                                AlumnosAprobados++;
                                            }
                                        }
                                        else // Años posteriores
                                        {
                                            if (output <= ProbabilidadReprobados2)
                                            {
                                                AlumnosReprobados++;
                                            }
                                            else if (output > ProbabilidadReprobados2 && output <= ProbabilidadReprobados2 + ProbabilidadAbandono2)
                                            {
                                                AlumnosAbandonaron++;
                                            }
                                            else
                                            {
                                                AlumnosAprobados++;
                                            }
                                        }
                                    }

                                    if (año == 5)
                                    {
                                        TotalGraduados += AlumnosAprobados; // Graduados al finalizar el quinto año
                                    }

                                    estudiantesPorAnio[año] = AlumnosReprobados; // Reprobados repiten el año
                                    if (año < 5)
                                    {
                                        estudiantesPorAnio[año + 1] += AlumnosAprobados; // Aprobados pasan al siguiente año
                                    }

                                    TotalAbandonos += AlumnosAbandonaron;
                                    TotalAprobados += AlumnosAprobados;
                                    TotalReprobados += AlumnosReprobados;

                                    Console.WriteLine($"Año {año}: {estudiantesIniciales} estudiantes iniciales, {AlumnosAprobados} aprobaron, {AlumnosReprobados} repitieron, {AlumnosAbandonaron} abandonaron.");
                                }

                                TotalInscritos += AlumnosEntrantesPorAnio;

                                for (int año = 1; año <= 5; año++)
                                {
                                    AulasPrevistas = (int)Math.Ceiling((double)estudiantesPorAnio[año] / AlumnosXAula);
                                    if (AulasPrevistas > MayorCantidadAulasPrevistas)
                                    {
                                        MayorCantidadAulasPrevistas = AulasPrevistas;
                                    }
                                    Console.WriteLine($"Año {año}: {estudiantesPorAnio[año]} estudiantes, Aulas previstas: {AulasPrevistas}");
                                }
                            }

                            Console.WriteLine("\n\n|=============================================  ESTADISTICAS GENERALES ================================================|");
                            Console.WriteLine($"Años simulados    : {TiempoSimular}");
                            Console.WriteLine($"Aulas previstas   : {MayorCantidadAulasPrevistas}");
                            Console.WriteLine($"Total inscritos   : {TotalInscritos}");
                            Console.WriteLine($"Total reprobados  : {TotalReprobados}");
                            Console.WriteLine($"Total abandonos   : {TotalAbandonos}");
                            Console.WriteLine($"Total aprobados   : {TotalAprobados}");
                            Console.WriteLine($"Total graduados   : {TotalGraduados}");
                        }
                        break;

                    case 0:
                        Console.WriteLine("Saliendo del programa...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 0);
        }

        static void ImprimirMenu()
        {
            Console.WriteLine("1. Simular");
            Console.WriteLine("0. Salir");
        }

        static int GetIntegerInput()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Entrada no válida. Por favor, ingrese un número entero.");
            }
            return result;
        }
    }
}
