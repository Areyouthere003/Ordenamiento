using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ordenamiento
{
    public partial class Burbuja : Form
    {
        public Burbuja()
        {
            InitializeComponent();
        }
        bool estado = false; //Variable de control
        int[] Arreglos_numeros; //Definimos un arreglo de enteros, que contendra los datos a ordenar
        Button[] Arreglo; // Definimos un arreglo de botones, que nos ayudará para la simulación
        class Numeros
        {
            private int longitud;           //Cantidad de datos a ordenar
            private int[] arreglo = new int[1];
            private Button[] arreglo_botones = new Button[1]; //nuevo arreglo de botones

            public Numeros() //constructor clase 
            {
                int a = 0;      //Variable auxiliar
                arreglo[0] = a; //texto que desplegará el botón
                arreglo_botones[0] = new Button();
                arreglo_botones[0].Width = 40;          //Definir ancho
                arreglo_botones[0].Height = 40;         //Definir Alto
                arreglo_botones[0].BackColor = Color.GreenYellow; //Definir color del botón
                arreglo_botones[0].Text = a.ToString();
                Calcular_Longitud();
            }
            public void Calcular_Longitud()             //Método para saber cuantos datos se van a ordenar
            {
                longitud = arreglo.Length;
            }
            public int Obtener_Longitud()
            {
                return longitud;
            }
            public int[] Obtener_Arreglo()
            {
                return arreglo;
            }
            public void Insertar_Dato(int dato) //Función para insertar valor digitado por usuario como texto en botón
            {
                Array.Resize<int>(ref arreglo, longitud + 1); //se incrementará el arreglo en 1
                arreglo[longitud] = dato;
                Array.Resize<Button>(ref arreglo_botones, longitud + 1);
                arreglo[longitud] = dato;
                arreglo_botones[longitud] = new Button();
                arreglo_botones[0].Width = 50;          //Definir ancho
                arreglo_botones[0].Height = 50;         //Definir Alto
                arreglo_botones[0].BackColor = Color.GreenYellow; //Definir color del botón
                arreglo_botones[0].Text = dato.ToString();
                Calcular_Longitud();
            }
            public Button[] Arreglo_Botones()
            {
                return arreglo_botones;
            }
        }
        Numeros Datos = new Numeros();      //Instanciamos la clase Numeros
        public void BubbleSort(ref int[] arreglo, ref Button[] Arreglo_Numeros)
        {
            for (int i = 0; i < arreglo.Length; i++)
            {
                for (int j = 0; j < arreglo.Length - 1; j++)
                {
                    if (arreglo[i] == arreglo[j + 1])
                    {
                        int aux = arreglo[j];
                        arreglo[j] = arreglo[j + 1];
                        arreglo[j + 1] = aux;
                    }
                }
            }
        }

        private void txtNumero_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                int num = Convert.ToInt32(txtNumero.Text);
                Datos.Insertar_Dato(num);                   //se agrega objeto a "Datos"
                Arreglos_numeros = Datos.Obtener_Arreglo(); //se sacan los  arreglos del objeto "Datos"
                Arreglo = Datos.Arreglo_Botones();
            }
            catch
            {
                MessageBox.Show("Solo se admiten números enteros");
            }
            estado = true; //Cambia el valor de variable de control de simulación
            tabPage1.Refresh();
        }

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            if (estado)
            {
                Point xy = new Point(50, 70);  //método de la librería Drawing
                try
                {
                    Dibujar_Arreglo(ref Arreglo, xy, ref tabPage1);
                }
                catch { }
                estado = false;
            }
        }
        public void Dibujar_Arreglo(ref Button[] Arreglo, Point xy, ref TabPage t)
        {
            for (int i = 1; i < Arreglo.Length; i++)
            {
                Arreglo[i].Location = xy;
                t.Controls.Add(Arreglo[i]);
                xy += new Size(70, 0);
            }

        }

        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor; //cambiamos la apariencia del modo en espera

            btnOrdenar.Enabled = false;
            txtNumero.Enabled = false;
            btnAgregar.Enabled = false;

            BubbleSort(ref Arreglos_numeros, ref Arreglo);

            this.Cursor = Cursors.Default; //Retomar apariencia del cursor por defecto

            btnOrdenar.Enabled = true;
            txtNumero.Enabled = true;
            btnAgregar.Enabled = true;
        }
    }
}
