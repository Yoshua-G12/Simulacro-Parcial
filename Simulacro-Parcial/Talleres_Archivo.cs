using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Simulacro_Parcial
{
    internal class Talleres_Archivo
    {
        public void Guardar(string archivo, List<Talleres> talleres)
        {
            string json = JsonConvert.SerializeObject(talleres);
            System.IO.File.WriteAllText(archivo, json);

        }
        public List<Talleres> Leer(string archivo)
        {
            List<Talleres> lista = new List<Talleres>();
            StreamReader jsonStream = File.OpenText(archivo);
            string json = jsonStream.ReadToEnd();
            jsonStream.Close();

            lista = JsonConvert.DeserializeObject<List<Talleres>>(json);
            return lista;
        }
    }
}
