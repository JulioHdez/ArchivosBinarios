using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    class ArchivosBinariosEmpleados
    {
        //declaracion de flujos
        BinaryWriter bw = null; //Flujo de salida - escritura de datos
        BinaryReader br = null; //flujo entrada - lectura de datos

        //Campos de la clase
        string Nombre, Direccion;
        long Telefono;
        int NumEmp, DiasTrabajados;
        float SalarioDiario;

        //Metodo de la clase
        public void CrearArchivo(string Archivo)
        {
            //variable local metodo
            char resp;
            try
            {
                //Creacion del  flujo para escribir datos al archivo
                bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                //Captura de datos
                do
                {
                    Console.Clear();

                    Console.Write("Numero del  Empleado: ");
                    NumEmp = Int32.Parse(Console.ReadLine());

                    Console.Write("Nombre del Empleado: ");
                    Nombre = Console.ReadLine();

                    Console.Write("Direccion del Empleado: ");
                    Direccion = Console.ReadLine();

                    Console.Write("Telefono del Empleado: ");
                    Telefono = Int64.Parse(Console.ReadLine());

                    Console.Write("Dias trabajados del Empleado: ");
                    DiasTrabajados = Int32.Parse(Console.ReadLine());

                    Console.Write("Salario Diario del Empleado: ");
                    SalarioDiario = Single.Parse(Console.ReadLine());

                    //escribe los datos al archivo
                    bw.Write(NumEmp);
                    bw.Write(Nombre);
                    bw.Write(Direccion);
                    bw.Write(Telefono);
                    bw.Write(DiasTrabajados);
                    bw.Write(SalarioDiario);

                    Console.Write("\n\n¿Deseas Almacenar otro registro (s/n)?");
                    resp = Char.Parse(Console.ReadLine());
                } while ((resp == 's') || (resp == 'S'));
            }
            catch (IOException e)
            {
                Console.WriteLine("\nError: {0}" , e.Message);
                Console.WriteLine("\nRuta: {0}", e.StackTrace);
            }
            finally
            {
                if (bw != null) bw.Close();
                //Cierra el flujo - escritura de datos y refresa al menu. 
                Console.ReadKey();
            }
        }
        
        public void MostrarArchivo(string Archivo)
        {
            try
            {
                //Verifica si existe el archivo
                if (File.Exists(Archivo))
                {
                    //Creacion flujo para leer datos del archivo
                    br = new BinaryReader(new FileStream(Archivo,FileMode.Open, FileAccess.Read));

                    //Despliegue de datos en pantalla
                    Console.Clear();
                    do
                    {
                        //Lectura de registros mientras no llegue a EndOfFile
                        NumEmp = br.ReadInt32();
                        Nombre = br.ReadString();
                        Direccion = br.ReadString();
                        Telefono = br.ReadInt64();
                        DiasTrabajados = br.ReadInt32();
                        SalarioDiario = br.ReadSingle();

                        //Muestra los datos
                        Console.WriteLine("Numero del Empleado: {0}", NumEmp);
                        Console.WriteLine("Nombre del Empleado: {0}", Nombre);
                        Console.WriteLine("Dirección del Empleado: {0}", Direccion);
                        Console.WriteLine("Telefono del Empleado: {0}", Telefono);
                        Console.WriteLine("Dias Trabajados del Empleado: {0}", DiasTrabajados);
                        Console.WriteLine("Salario Diario del Empleado: {0:C}", SalarioDiario);

                        Console.WriteLine("SUELDO TOTAL DEL EMPLEADO: {0:C}", (SalarioDiario * DiasTrabajados));
                        Console.WriteLine("\n");
                    } while (true);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n \n El Archivo " + Archivo + "No existe en el disco!");
                    Console.Write("\nPresione ENTER para continuar...");
                    Console.ReadKey();
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("\n\nFin del Listado de Empleados");
                Console.Write("\nPresione ENTER para continuar...");
                Console.ReadKey();
            }
            finally
            {
                if (br != null) br.Close(); //Cierre de Flujo
                Console.Write("\nPresione ENTER para terminar la lectura de datos y regresar al menu.");
                Console.ReadKey();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Declaracion variables auxiliares
            string Arch = null;
            int opcion;

            //Creacion del objeto
            ArchivosBinariosEmpleados A1 = new ArchivosBinariosEmpleados();

            //Menu de opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS ***");
                Console.WriteLine("1.- Creacion del un Archivo.");
                Console.WriteLine("2.- Lectura de un Archivo.");
                Console.WriteLine("3.- Salida del Programa.");
                Console.Write("Seleccione la opcion que desea: ");
                opcion = Int16.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        //Bloque de escritura
                        try
                        {
                            //Captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo a Crear: ");
                            Arch = Console.ReadLine();

                            //Verifica si existe el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl Archivo Existe!!, deseas sobreescribirlo (s/n)?");
                                resp = Char.Parse(Console.ReadLine());
                            }
                            if ((resp == 's') || (resp == 'S')) A1.CrearArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;

                    case 2:
                        //Bloque de Lectura
                        try
                        {
                            //Captura del nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo que deseas Leer");
                            Arch = Console.ReadLine();
                            A1.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;

                    case 3:
                        Console.Write("\nPresiona ENTER para salir del programa");
                        Console.ReadKey();
                        break;

                    default:
                        Console.Write("\nEsa Opcion no existe!!! Presiona ENTER para continuar...");
                        break;
                }
            } while (opcion != 3);
        }
    }
}
