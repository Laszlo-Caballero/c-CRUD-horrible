using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sqlwin
{
    public partial class Form3 : Form
    {
        int id;
        string nombre, auxid;
        MySqlConnection conexion = new MySqlConnection("server=localhost; user id=root; password=jade2314; database=datoswf");

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            auxid = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            nombre= textBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string consulta = "";
            if (string.IsNullOrEmpty(nombre) && string.IsNullOrEmpty(auxid))
            {
                MessageBox.Show("Al menos uno tiene que tener datos");
                return;
            } else if (!int.TryParse(auxid, out id) && auxid != null )
            {
                MessageBox.Show("El ID no puede ser una letra");
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(nombre))
                {
                    consulta = $"DELETE FROM datos WHERE id = {id}";
                }else if (string.IsNullOrEmpty(auxid))
                {
                    consulta = $"DELETE FROM datos WHERE nombre = '{nombre}'";
                }
                else
                {
                    consulta = $"DELETE FROM datos WHERE id = {id} AND nombre ='{nombre}'";
                }
            }

            try
            {
                MySqlCommand peticion = new MySqlCommand(consulta, conexion);
                int eliminar = peticion.ExecuteNonQuery();
                MessageBox.Show("Se elimino el dato");
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Form3()
        {
            InitializeComponent();
            conexion.Open();
        }
    }
}
