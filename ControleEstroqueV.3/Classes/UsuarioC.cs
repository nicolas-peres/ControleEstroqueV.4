﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ControleEstroqueV._3.Classes
{
    public class UsuarioC
    {
        private MySqlConnection Conexao = new MySqlConnection("Server=localhost;Database=controleestoque;Uid=root;Password=;");
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }

        public void Inserir()
        {
            try
            {
                Conexao.Open();
                string query = "INSERT INTO usuarios (`Nome`, `Login`, `Senha`, `Ativo`) " +
                               "VALUES (@nome, @login, @senha, @ativo)";
                MySqlCommand comando = new MySqlCommand(query, Conexao);

                comando.Parameters.Add(new MySqlParameter("@nome", Nome));
                comando.Parameters.Add(new MySqlParameter("@login", Login));
                comando.Parameters.Add(new MySqlParameter("@senha", Senha));
                comando.Parameters.Add(new MySqlParameter("@ativo", Ativo));

                comando.ExecuteNonQuery();
                MessageBox.Show("Usuário inserido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inserir usuário: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }


        public DataTable PreencherGrid()
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT Id, Nome, Login, Ativo FROM usuarios order by Id desc";
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
            DataTable dataTable = new DataTable();
            Conexao.Open();
            string query = "";
            if (string.IsNullOrEmpty(pesquisa))
            {
                query = "SELECT Id, Nome, Login, Ativo,  FROM usuarios order by Id desc";
            }
            else
            {
                query = "SELECT Id,Nome, Login, Ativo  FROM usuarios Where Login like '%" + pesquisa + "%' Order by Id desc";
            }

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

        public void Editar()
        {
            string query = "update usuarios set Nome = @nome, Login = @login, Senha = @senha, Ativo = @ativo WHERE  Id = @id";
            Conexao.Open();
            MySqlCommand comando = new MySqlCommand(query, Conexao);
            comando.Parameters.Add(new MySqlParameter("@nome", Nome));
            comando.Parameters.Add(new MySqlParameter("@login", Login));
            comando.Parameters.Add(new MySqlParameter("@senha", Senha));
            comando.Parameters.Add(new MySqlParameter("@ativo", Ativo));
            comando.Parameters.Add(new MySqlParameter("@id", Id));
            int resposta = comando.ExecuteNonQuery();
            if (resposta == 1)
            {
                MessageBox.Show("Usuário Atualizado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao atualizar", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Excluir()
        {
            string query = "Delete from usuarios WHERE  Id = @id";
            Conexao.Open();
            MySqlCommand comando = new MySqlCommand(query, Conexao);
            comando.Parameters.Add(new MySqlParameter("@id", Id));
            int resposta = comando.ExecuteNonQuery();
            if (resposta == 1)
            {
                MessageBox.Show("Usuário Excluído com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao excluir", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PesquisarPorId(int id)
        {
            DataTable dataTable = new DataTable();
            Conexao.Open();
            string query = "SELECT Id, Nome, Login, Senha, Ativo Nome FROM usuarios Where Id = @id Order by Id desc";
            MySqlCommand Comando = new MySqlCommand(query, Conexao);
            Comando.Parameters.AddWithValue("@id", id);
            MySqlDataReader resultado = Comando.ExecuteReader();

            if (resultado.Read())
            {
                Id = resultado.GetInt32(0);
                Login = resultado.GetString(1);
                Senha = resultado.GetString(2);
                Ativo = resultado.GetBoolean(3);
            }

            Conexao.Close();
        }
    }


}
