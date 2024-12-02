﻿using ControleEstroqueV._3.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControleEstroqueV._3
{
    public partial class PrincipalFunci : Form
    {
        public PrincipalFunci()
        {
            InitializeComponent();
        }

        private void cadastrosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void funcionariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCadastrarUsuario u = new FrmCadastrarUsuario();
            u.ShowDialog();
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCadastrarProduto s = new FrmCadastrarProduto();
            s.ShowDialog();
        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Comprados co = new Comprados();
            //co.ShowDialog();
        }
    }
}
