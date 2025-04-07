using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Simulacro_Parcial
{
    internal class Inscripciones_Archivo
    {
        public void Guardar(string archivo, List<Inscripciones> inscripciones)
        {
            string json = JsonConvert.SerializeObject(inscripciones);
            System.IO.File.WriteAllText(archivo, json);

        }
        public List<Inscripciones> Leer(string archivo)
        {
            List<Inscripciones> lista = new List<Inscripciones>();
            StreamReader jsonStream = File.OpenText(archivo);
            string json = jsonStream.ReadToEnd();
            jsonStream.Close();

            lista = JsonConvert.DeserializeObject<List<Inscripciones>>(json);
            return lista;
        }
    }
}
