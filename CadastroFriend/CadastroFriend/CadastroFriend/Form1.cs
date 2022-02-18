using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace CadastroFriend
{
    public partial class cadFesta : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\CadastroFriend\\CadastroFriend\\CadastroFriend\\DbFesta.mdf;Integrated Security=True");
        public cadFesta()
        {
            InitializeComponent();
        }

        public void LoadDGV()
        {
            String str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\CadastroFriend\\CadastroFriend\\CadastroFriend\\DbFesta.mdf;Integrated Security=True";
            String query = "SELECT * FROM Festa";
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable Festa = new DataTable();
            da.Fill(Festa);
            dgvFesta.DataSource = Festa;
            con.Close();
        }

        private void cadFesta_Load(object sender, EventArgs e)
        {
            LoadDGV();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Cadastrar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Nome", SqlDbType.NChar).Value = txtNome.Text.Trim();
            cmd.Parameters.AddWithValue("@nascimento", SqlDbType.Date).Value = txtData_Nas.Text.Trim();
            cmd.Parameters.AddWithValue("@cidade", SqlDbType.NChar).Value = txtCidade.Text.Trim();
            cmd.Parameters.AddWithValue("@celular", SqlDbType.NChar).Value = txtCelular.Text.Trim();
            cmd.ExecuteNonQuery();
            LoadDGV();
            MessageBox.Show("Pessoa cadastrada com sucesso", "Cadastro de Pessoas");
            txtNome.Text = "";
            txtData_Nas.Text = "";
            txtCidade.Text = "";
            txtCelular.Text = "";
            con.Close();
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Localizar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text.Trim();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtNome.Text = dr["nome"].ToString();
                    txtData_Nas.Text = dr["nascimento"].ToString();
                    txtCidade.Text = dr["Cidade"].ToString();
                    txtCelular.Text = dr["celular"].ToString();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Nenhum registro encontrado!");
                    con.Close();
                }
            }
            finally
            {

            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtData_Nas.Text = "";
            txtCidade.Text = "";
            txtCelular.Text = "";   
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Excluir", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text.Trim();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                LoadDGV();
                MessageBox.Show("Pessoa deletada com sucesso!", "Cadastro de Pessoas");
                txtNome.Text = "";
                txtData_Nas.Text = "";
                txtCidade.Text = "";
                txtCelular.Text = "";
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Atualizar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text.Trim();
                cmd.Parameters.AddWithValue("@nascimento", SqlDbType.NChar).Value = txtData_Nas.Text.Trim();
                cmd.Parameters.AddWithValue("@cidade", SqlDbType.NChar).Value = txtCidade.Text.Trim();
                cmd.Parameters.AddWithValue("@celular", SqlDbType.NChar).Value = txtCelular.Text.Trim();
                cmd.ExecuteNonQuery();
                LoadDGV();
                MessageBox.Show("Pessoa atualizada com sucesso!", "Cadastro de Pessoas");
                txtNome.Text = "";
                txtData_Nas.Text = "";
                txtCidade.Text = "";
                txtCelular.Text = "";
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}
