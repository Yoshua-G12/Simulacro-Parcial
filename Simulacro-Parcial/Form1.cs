using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Simulacro_Parcial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Alumnos> alumnos = new List<Alumnos>();
        List<Talleres> talleres = new List<Talleres>();
        List<Inscripciones> inscripciones = new List<Inscripciones>();

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void GuardarInscripciones()
        {
            try
            {
                // Serializar la lista de inscripciones a JSON
                string json = JsonConvert.SerializeObject(inscripciones, Formatting.Indented);

                // Guardar el archivo en la ruta especificada
                File.WriteAllText("../../Inscripcion.json", json);

                MessageBox.Show("Inscripción guardada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar las inscripciones: {ex.Message}");
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            // Verificar que se haya seleccionado un estudiante y un taller
            if (cmbnnombreestudiante.SelectedItem is Alumnos alumnoSeleccionado && cmbnombredeltaller.SelectedItem is Talleres tallerSeleccionado)
            {
                // Crear una nueva inscripción
                Inscripciones nuevaInscripcion = new Inscripciones
                {
                    nombre_estudiante = alumnoSeleccionado.Nombre, // Tomar el nombre del alumno seleccionado   
                    nombre_taller = tallerSeleccionado.Nombre_Taller, // Tomar el nombre del taller seleccionado
                    costo_taller = tallerSeleccionado.Costo_Taller.ToString(), // Tomar el costo del taller seleccionado
                    DPI_Estudiante = alumnoSeleccionado.DPI, // Tomar el DPI del alumno seleccionado
                    Codigo_Taller = tallerSeleccionado.Codigo_Taller, // Tomar el código del taller seleccionado
                    fecha_inscripcion = DateTime.Now // Fecha actual
                };

                // Agregar la inscripción a la lista de inscripciones
                inscripciones.Add(nuevaInscripcion);

                // Guardar la lista de inscripciones en el archivo JSON
                GuardarInscripciones();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un estudiante y un taller.");
            }
        }

        private void btnactualizar_Click(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void cmbnnombreestudiante_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Asegurarse de que se haya seleccionado un item
            if (cmbnnombreestudiante.SelectedItem is Alumnos alumnoSeleccionado)
            {
                // Obtener el nombre del alumno seleccionado
                string nombre = alumnoSeleccionado.Nombre;

                // Buscar al alumno en la lista
                Alumnos alumno = alumnos.FirstOrDefault(a => a.Nombre == nombre);
                if (alumno != null)
                {
                    // Llenar los campos con los datos del alumno
                    txtdpiestudiante.Text = alumno.DPI;
                    txtdireccionestudiante.Text = alumno.Direccion;
                }
                else
                {
                    // Limpiar los campos si no se encuentra el alumno
                    txtdireccionestudiante.Text = "";
                    txtdpiestudiante.Text = "";
                }
            }
        }
        private void Mostrar()
        {
            Inscripciones_Archivo inscripcionesArchivo = new Inscripciones_Archivo();
            inscripciones = inscripcionesArchivo.Leer("../../Inscripcion.json");

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = inscripciones;
            dataGridView1.Refresh();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Leer los datos de los estudiantes desde el archivo JSON
            Alumnos_Archivos alumnosArchivo = new Alumnos_Archivos();
            alumnos = alumnosArchivo.Leer("../../Estudiante.json");

            // Verificar si hay alumnos
            if (alumnos != null && alumnos.Any())
            {
                // Llenar el ComboBox con los nombres de los alumnos
                cmbnnombreestudiante.DataSource = alumnos;
                cmbnnombreestudiante.DisplayMember = "Nombre";  // Asegúrate de que "Nombre" es el nombre de la propiedad
                cmbnnombreestudiante.ValueMember = "DPI";  // Puedes usar "DPI" u otra propiedad como ValueMember
            }

            // Leer los datos de los talleres
            Talleres_Archivo talleresArchivo = new Talleres_Archivo();
            talleres = talleresArchivo.Leer("../../taller.json");
            if (alumnos != null && alumnos.Any())
            {
                // Llenar el ComboBox con los nombres de los alumnos
                cmbnombredeltaller.DataSource = talleres;
                cmbnombredeltaller.DisplayMember = "Nombre_Taller";  // Asegúrate de que "Nombre" es el nombre de la propiedad
                cmbnombredeltaller.ValueMember = "Codigo_Taller";  // Puedes usar "DPI" u otra propiedad como ValueMember
            }
        }

        private void cmbnombredeltaller_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Asegurarse de que se haya seleccionado un item
            if (cmbnombredeltaller.SelectedItem is Talleres tallerSeleccionado)
            {
                // Obtener el nombre del alumno seleccionado
                string nombret = tallerSeleccionado.Nombre_Taller;

                // Buscar al alumno en la lista
                Talleres talleress = talleres.FirstOrDefault(b => b.Nombre_Taller == nombret);
                if (talleres != null)
                {
                    // Llenar los campos con los datos del alumno
                    txtcodigotaller.Text = talleress.Codigo_Taller;
                    txtcostotaller.Text = talleress.Costo_Taller.ToString();
                }
                else
                {
                    // Limpiar los campos si no se encuentra el alumno
                    txtcostotaller.Text = "";
                    txtcodigotaller.Text = "";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Ordenar las inscripciones por el nombre del taller alfabéticamente
                var inscripcionesOrdenadas = inscripciones
                    .OrderBy(i => talleres.FirstOrDefault(t => t.Codigo_Taller == i.Codigo_Taller)?.Nombre_Taller)
                    .ToList();

                // Asignar la lista ordenada al DataGridView
                dataGridView1.DataSource = null; // Limpiar cualquier dato previo
                dataGridView1.DataSource = inscripcionesOrdenadas;

                MessageBox.Show("Datos ordenados alfabéticamente por el nombre del taller.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ordenar los datos: {ex.Message}");
            }
        }
    }

}
