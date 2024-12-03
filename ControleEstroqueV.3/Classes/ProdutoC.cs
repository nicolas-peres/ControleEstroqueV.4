using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ControleEstroqueV._3.Classes
{
    public class ProdutoC
    {
        private MySqlConnection Conexao = new MySqlConnection("Server=localhost;Database=controleestoque;Uid=root;Password=;");
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Decimal Preco { get; set; }

        public object[] Linha()
        {
            return new object[] { Id, Nome, Descricao, Preco, };
        }


        public void Inserir()
        {
            Conexao.Open();
            string query = "INSERT INTO produto (Nome, Descricao, Preco) VALUES (@nome, @descricao, @preco)";
            MySqlCommand comando = new MySqlCommand(query, Conexao);

            MySqlParameter parametro1 = new MySqlParameter("@nome", Nome);
            MySqlParameter parametro2 = new MySqlParameter("@descricao", Descricao);
            MySqlParameter parametro3 = new MySqlParameter("@preco", Preco);


            comando.Parameters.Add(parametro1);
            comando.Parameters.Add(parametro2);
            comando.Parameters.Add(parametro3);

            comando.ExecuteNonQuery();
            Conexao.Close();
        }
        public DataTable ObterProdutos()
        {
            DataTable dataTable = new DataTable();

            try
            {
                Conexao.Open();
                string query = "SELECT Id, Nome, Descricao, Preco FROM produto";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, Conexao);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }

            return dataTable;
        }

        public DataTable PreencherGrid()
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT Id, Nome, Descricao, Preco, Estoque FROM produto ORDER BY Id DESC";
            Conexao.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, Conexao);
            try
            {
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao acessar os dados para preencher grid: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Conexao.Close();
            return dataTable;
        }
        public DataTable Pesquisar(string pesquisa)
        {
            DataTable dt = new DataTable();
            Conexao.Open();

            string query = string.IsNullOrEmpty(pesquisa)
                ? "SELECT Id, Nome, Descricao, Preco FROM produto ORDER BY Id DESC"
                : "SELECT Id, Nome, Descricao, Preco  FROM produto WHERE Nome LIKE @pesquisa ORDER BY Id DESC";

            MySqlCommand comando = new MySqlCommand(query, Conexao);
            if (!string.IsNullOrEmpty(pesquisa))
                comando.Parameters.Add(new MySqlParameter("@pesquisa", "%" + pesquisa + "%"));

            MySqlDataReader leitura = comando.ExecuteReader();

            foreach (var propriedade in typeof(ProdutoC).GetProperties())
            {
                dt.Columns.Add(propriedade.Name);
            }

            if (leitura.HasRows)
            {
                while (leitura.Read())
                {
                    ProdutoC p = new ProdutoC
                    {
                        Id = Convert.ToInt32(leitura["Id"]),
                        Nome = leitura["Nome"].ToString(),
                        Descricao = leitura["Descricao"].ToString(),
                        Preco = decimal.Parse(leitura["Preco"].ToString()),

                    };

                }
            }

            Conexao.Close();
            return dt;
        }

        public void Excluir()
        {
            string query = "Delete from produto WHERE  Id = @id";
            Conexao.Open();
            MySqlCommand comando = new MySqlCommand(query, Conexao);
            comando.Parameters.Add(new MySqlParameter("@id", Id));
            int resposta = comando.ExecuteNonQuery();
            if (resposta == 1)
            {
                MessageBox.Show("Produto Excluído com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao excluir", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void Editar()
        {
            string query = "update produto set Nome = @nome, Descricao = @descricao, Preco = @preco,   WHERE  Id = @id";
            Conexao.Open();
            MySqlCommand comando = new MySqlCommand(query, Conexao);
            comando.Parameters.Add(new MySqlParameter("@nome", Nome));
            comando.Parameters.Add(new MySqlParameter("@Descricao", Descricao));
            comando.Parameters.Add(new MySqlParameter("@preco", Preco));

            comando.Parameters.Add(new MySqlParameter("@id", Id));
            int resposta = comando.ExecuteNonQuery();
            if (resposta == 1)
            {
                MessageBox.Show("Produto Atualizado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao atualizar", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PesquisarPorId(int id)
        {
            DataTable dataTable = new DataTable();
            Conexao.Open();
            string query = "SELECT Id, Nome, Quantidade, Nome FROM produto Where Id = @id Order by Id desc";
            MySqlCommand Comando = new MySqlCommand(query, Conexao);
            Comando.Parameters.AddWithValue("@id", id);
            MySqlDataReader resultado = Comando.ExecuteReader();

            if (resultado.Read())
            {
                Id = resultado.GetInt32(0);
                Nome = resultado.GetString(1);

                Descricao = resultado.GetString(3);
                Preco = resultado.GetDecimal(4);
            }

            Conexao.Close();
        }
    }
}
