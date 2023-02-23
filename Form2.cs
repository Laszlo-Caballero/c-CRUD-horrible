using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Crypto.Tls;
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
    public partial class Form2 : Form
    {
        int id;
        string nombre, apellido, auxid;
        MySqlConnection conexion = new MySqlConnection("server=localhost; user id=root; password=jade2314; database=datoswf");
        DataTable datosNew = new DataTable();
        public Form2()
        {
            conexion.Open();
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            nombre = textBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("el id es necesario para actualizar los datos");
                return;
            }else if (!int.TryParse(auxid, out id )){
                MessageBox.Show("el id no puede ser una letra");
                return;
            }
            string consulta = "";
            if (string.IsNullOrEmpty(textBox2.Text) && string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("tiene que actualizar al menos alguno de los dos datos");
                return;
            } else if (string.IsNullOrEmpty(textBox2.Text))
            {
                consulta = $"UPDATE datos SET apellido = '{apellido}' WHERE id = {id}";
            } else if (string.IsNullOrEmpty(textBox3.Text))
            {
                consulta = $"UPDATE datos SET nombre = '{nombre}' WHERE id = {id}";
            }
            else
            {
                consulta = $"UPDATE datos SET nombre = '{nombre}', apellido = '{apellido}' WHERE id = {id}";
            }
            try
            {
                MySqlCommand peticion = new MySqlCommand(consulta, conexion);
                int actualizado = peticion.ExecuteNonQuery();
                MessageBox.Show("se actualizaron los datos");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string consultaEnviar = "SELECT * FROM datos";
            MySqlDataAdapter adaptador = new MySqlDataAdapter(consultaEnviar, conexion);
            
            adaptador.Fill(datosNew);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           auxid = textBox1.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            apellido = textBox3.Text;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public DataTable datos
        {
            get { return datosNew; }
        }
    }
}
