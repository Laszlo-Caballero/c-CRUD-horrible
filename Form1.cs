using System.Data;
using System.Net;
using System.Web;
using MySql.Data.MySqlClient;

namespace sqlwin
{
    public partial class Form1 : Form
    {
        string nombre, apellido;
        MySqlConnection conexion = new MySqlConnection("server=localhost; user id=root; password=jade2314; database=datoswf");
        DataTable datos = new DataTable();
        public Form1()
        {
            InitializeComponent();
            
            try
            {
             conexion.Open();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Close();
            }
        }
        
        public void Ver()
        {
            try
            {
                datos.Clear();
                string consulta = "SELECT * FROM datos";
                MySqlDataAdapter adaptador = new MySqlDataAdapter(consulta, conexion);
                adaptador.Fill(datos);
                dataGridView1.DataSource = datos;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text) )
            {
                MessageBox.Show("no puede ser nulo");
            }
            else
            {
                try
                {
                    string comandoEnviar = $"INSERT INTO datos(nombre, apellido) VALUES ('{nombre}', '{apellido}')";
                    MySqlCommand command = new MySqlCommand(comandoEnviar, conexion);
                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show("se enviaron");
                    Ver();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
                apellido = textBox2.Text ?? "no puede ser nulo";  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Ver();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 actualizar = new Form2();
            actualizar.FormClosed += actualizar_FromClosed;
            actualizar.ShowDialog();
        }
        private void actualizar_FromClosed(object sender, FormClosedEventArgs e)
        {
            Ver();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 eliminar = new Form3();
            eliminar.FormClosed += eliminar_FromClosed;
            eliminar.ShowDialog();
        }
        private void eliminar_FromClosed(Object sender, FormClosedEventArgs e)
        {
            Ver();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                nombre = textBox1.Text ?? "no puede ser nulo"; ;
        }
    }
}