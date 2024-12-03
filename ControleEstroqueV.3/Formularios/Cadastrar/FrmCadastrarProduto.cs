using ControleEstroqueV._3.Classes;
using ControleEstroqueV._3.Dao;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControleEstroqueV._3.Formularios
{
    public partial class FrmCadastrarProduto : Form
    {
        ProdutoDao dao = new ProdutoDao();
        int LinhaSelecionada;
        DataTable dados;
        public FrmCadastrarProduto()
        {
            InitializeComponent();
            dados = new DataTable();
            foreach (var atributos in typeof(ProdutoC).GetProperties())
            {
                dados.Columns.Add(atributos.Name);
            }

            dados = dao.ObterProdutos();

            dtGridProduto.DataSource = dados;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            ProdutoC p = new ProdutoC();
            p.Nome = txtNome.Text;
            p.Descricao = txtDescricao.Text;
            p.Preco = Convert.ToDecimal(txtPreco.Text);
            p.Inserir();
            MessageBox.Show("Sucesso", "Cadastrado com sucesso");
            Close();
        }

        private void LimparCampos()
        {
            txtPreco.Text = "";
            txtDescricao.Text = "";
            txtNome.Text = "";
            numQtd.Value = 0;
            numId.Value = 0;
        }

        private void FrmCadastrarProduto_Load(object sender, EventArgs e)
        {

        }

        private void dtGridProduto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }
    }
}
