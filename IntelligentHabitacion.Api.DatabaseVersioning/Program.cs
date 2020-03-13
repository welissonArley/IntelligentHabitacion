using IntelligentHabitacion.Api.Repository;
using IntelligentHabitacion.Api.Repository.DataBaseVersions;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System;
using System.Configuration;

namespace IntelligentHabitacion.Api.DatabaseVersioning
{
    public static class Program
    {
        public static void Main()
        {
            try
            {
                Console.WriteLine("Starting process to update Database ....");

                string typeDataBase = ConfigurationManager.AppSettings["TipoBancoDeDadosUtilizado"];
                var connectionString = ConfigurationManager.ConnectionStrings[typeDataBase + "_DB"].ConnectionString;

                var updateController = new UpdateController(new DatabaseType(typeDataBase, connectionString));

                var databaseVersion = updateController.DataBaseVersion();
                var avaliableVersion = LastVersionCode();

                Console.WriteLine("Version of Database: [" + databaseVersion + "]");
                Console.WriteLine("The version avaliable is: [" + avaliableVersion + "]");

                if (databaseVersion < avaliableVersion)
                {
                    Console.WriteLine("Running update ...");

                    updateController.UpdateRepository();

                    Console.WriteLine("Finish update.");

                    Console.WriteLine(Environment.NewLine);
                }
                else
                    Console.WriteLine("There is not any version to update the Database.");
            }
            catch (IntelligentHabitacionException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch
            {
                Console.WriteLine("There is one erro when tryed update the Database.");
            }
        }

        private static long LastVersionCode()
        {
            var enumSize = Enum.GetValues(typeof(EnumVersions)).Length;
            var lastVersionCode = (int)Enum.GetValues(typeof(EnumVersions)).GetValue(enumSize - 1);

            return lastVersionCode;
        }
    }
}
