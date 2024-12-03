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
    public partial class FrmProdutos : Form
    {
        public FrmProdutos()
        {
            InitializeComponent();
        }

        
        private void btnCadastrarP_Click(object sender, EventArgs e)
        {
            FrmCadastrarProduto cadastrar = new FrmCadastrarProduto();
            cadastrar.FormClosed += Fechou_Produto_FormClosed;

            // Abre o formulário de cadastro como modal
            cadastrar.ShowDialog();
        }

        private void Fechou_Produto_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProdutoC u = new ProdutoC();
            dataGridView1.DataSource = u.PreencherGrid();
        }
    }
}
