using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CamacaNegocio.Bll;

namespace CamadaApresentacao.Ui
{
    public partial class FrmCategoria : Form
    {
        private bool eNovo = false;
        private bool eEditar = false;


        public FrmCategoria()
        {
            InitializeComponent();
            this.ttMensagem.SetToolTip(this.txtNome, "Insira o nome da Categoria");
        }

        //Metodo mostra mensagem de confirmacão
        private void MensagemOk(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comércio", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Metodo mostra mensagem de erro
        private void MensagemErro(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comércio", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Metodo limpar campo
        private void LimparCampo()
        {
            this.txtNome.Text = string.Empty;
            this.txtIdCategoria.Text = string.Empty;
            this.txtDescricao.Text = string.Empty;
        }

        //metodo habilitar text box 
        private void Habilitar(bool valor)
        {
            this.txtNome.ReadOnly = !valor;
            this.txtIdCategoria.ReadOnly = !valor;
            this.txtDescricao.ReadOnly = !valor;
        }

        //metodo habilitar Botão
        private void Botoes()
        {
            if (this.eNovo || this.eEditar) 
            {
                this.Habilitar(true);
                this.btnNovo.Enabled = false;
                this.btnSalvar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.btnNovo.Enabled = true;
                this.btnSalvar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }
        }

        //metodo ocultar as colunas do dataGrid
        private void OcultarColunas()
        {
            this.dataLista.Columns[0].Visible = false;
            //this.dataLista.Columns[1].Visible = false;
        }

        //metodo mostra as colunas do dataGrid
        private void Mostra()
        {
            this.dataLista.DataSource = nCategoria.Mostra();
            this.OcultarColunas();
            lblTotal.Text = "Total de registros : " + Convert.ToString(dataLista.Rows.Count);
        }

         //Busca pelo nome
         private void BuscarNome()
         {
            this.dataLista.DataSource = nCategoria.BuscarNome(this.txtBuscar.Text);
            this.OcultarColunas();
            lblTotal.Text = "Total de registros : " + Convert.ToString(dataLista.Rows.Count);
         }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostra();
            this.Habilitar(false);
            this.Botoes();
        }
        
        
        //-------------------------------------------------------------//


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            this.eNovo = true;
            this.eEditar = false;
            this.Botoes();
            this.LimparCampo();
            this.Habilitar(true);
            this.txtNome.Focus();
            this.txtIdCategoria.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string rsp = "";
                if(txtNome.Text == string.Empty)
                {
                    this.MensagemErro("Preencha os campos obrigatórios ");
                    errorIcone.SetError(txtNome, "Insira o nome");
                }
                else
                {
                    if (this.eNovo)
                    {
                        rsp = nCategoria.Inserir(txtNome.Text.Trim().ToUpper(), this.txtDescricao.Text.Trim());
                    }
                    else
                    {
                        rsp = nCategoria.Editar(Convert.ToInt32(txtIdCategoria.Text), txtNome.Text.Trim().ToUpper(), this.txtIdCategoria.Text.Trim());
                    }
                    if (rsp.Equals("ok"))
                    {
                        if (this.eNovo)
                        {
                            this.MensagemOk("Registro salvo com sucesso");
                        }
                        else
                        {
                            this.MensagemOk("Registro editado com sucesso");
                        }
                    }
                    else
                    {
                        this.MensagemErro(rsp);
                    }

                    this.eNovo = false;
                    this.eEditar = false;
                    this.Botoes();
                    this.LimparCampo();
                    this.Mostra();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataLista_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdCategoria.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["idcategoria"].Value);
            this.txtNome.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["nome"].Value);
            this.txtDescricao.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["descricao"].Value);
            this.tabControl1.SelectedIndex = 1;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.txtIdCategoria.Text.Equals(""))
            {
                this.MensagemErro("Selecione um registro para inserir");
            }
            else
            {
                this.eEditar = true;
                this.Botoes();
                this.Habilitar(true);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.eNovo = false;
            this.eEditar = false;
            this.Botoes();
            this.Habilitar(false);
            this.LimparCampo();

        }

        private void chkDeletar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDeletar.Checked)
            {
                this.dataLista.Columns[0].Visible = true;
            }
            else
            {
                this.dataLista.Columns[0].Visible = false;
            }
        }

        private void dataLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataLista.Columns["Deletar"].Index)
            {
                DataGridViewCheckBoxCell chkDeletar = (DataGridViewCheckBoxCell)dataLista.Rows[e.RowIndex].Cells["Deletar"];
                chkDeletar.Value = !Convert.ToBoolean(chkDeletar.Value);
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcao;
                opcao = MessageBox.Show("Realmente Deseja apagar os registros ?", "Sistema Comércio", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(opcao == DialogResult.OK)
                {
                    string codigo;
                    string resposta = "";

                    foreach (DataGridViewRow row in dataLista.Rows)
                    {
                        if(Convert.ToBoolean( row.Cells[0].Value))
                        {
                            codigo = Convert.ToString( row.Cells[1].Value);
                            resposta = nCategoria.Excluir(Convert.ToInt32(codigo));

                            if (resposta.Equals("ok"))
                            {
                                this.MensagemOk("Registro excluido com sucesso");
                            }
                            else
                            {
                                this.MensagemErro(resposta);
                            }
                        }
                    }
                    this.Mostra();

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

    }
}
