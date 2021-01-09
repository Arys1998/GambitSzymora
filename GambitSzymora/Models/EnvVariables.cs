using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GambitSzymora.Models
{
    class EnvVariables
    {
        //Rozdzielczosc
       private static void setEnvForResolution()
        {
            Environment.SetEnvironmentVariable("Height_1000", "1000");
            Environment.SetEnvironmentVariable("Width_1200", "1200");

            Environment.SetEnvironmentVariable("Height_600", "600");
            Environment.SetEnvironmentVariable("Width_800", "800");

            Environment.SetEnvironmentVariable("Height_400", "600");
            Environment.SetEnvironmentVariable("Width_600", "600");
        }


        //Paleta Kolorow
        private static void setEnvForColors()
        {
            Environment.SetEnvironmentVariable("Burlywood", "BurlyWood");
            Environment.SetEnvironmentVariable("DarkKhaki", "DarkKhaki");
            Environment.SetEnvironmentVariable("DodgerBlue", "DodgerBlue");
        }

        //Czas Gry
        private static void setTimer()
        {
            Environment.SetEnvironmentVariable("5 minutes", "5");
            Environment.SetEnvironmentVariable("10 minutes", "10");
            Environment.SetEnvironmentVariable("15 minutes", "15");
        }

        //Wybor Tekstur
    }
}
