using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Simulacro_Parcial
{
    internal class Alumnos_Archivos
    {
        public void Guardar(string archivo, List<Alumnos> alumnos)
        {
            string json = JsonConvert.SerializeObject(alumnos);
            System.IO.File.WriteAllText(archivo, json);

        }
        public List<Alumnos> Leer(string archivo)
        {
            List<Alumnos> lista = new List<Alumnos>();
            StreamReader jsonStream = File.OpenText(archivo);
            string json = jsonStream.ReadToEnd();
            jsonStream.Close();

            lista = JsonConvert.DeserializeObject<List<Alumnos>>(json);
            return lista;
        }
    }
}
