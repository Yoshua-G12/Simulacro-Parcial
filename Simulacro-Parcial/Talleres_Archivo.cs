using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Simulacro_Parcial
{
    internal class Talleres_Archivo
    {
        public void Guardar(string archivo, List<Talleres> talleres)
        {
            try
            {
                // Serializar la lista de talleres a formato JSON
                string json = JsonConvert.SerializeObject(talleres);

                // Guardar el contenido JSON en el archivo
                System.IO.File.WriteAllText(archivo, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el archivo: {ex.Message}");
            }
        }

        public List<Talleres> Leer(string archivo)
        {
            List<Talleres> lista = new List<Talleres>();

            try
            {
                // Verificar si el archivo existe
                if (File.Exists(archivo))
                {
                    // Usar 'using' para asegurar el cierre adecuado del archivo
                    using (StreamReader jsonStream = File.OpenText(archivo))
                    {
                        // Leer el contenido del archivo
                        string json = jsonStream.ReadToEnd();

                        // Deserializar el contenido JSON a una lista de Talleres
                        lista = JsonConvert.DeserializeObject<List<Talleres>>(json);
                    }
                }
                else
                {
                    MessageBox.Show($"El archivo {archivo} no se encuentra.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el archivo: {ex.Message}");
            }

            return lista;
        }

    }
}
