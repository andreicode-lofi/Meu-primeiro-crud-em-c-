using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CamadaData
{
    public class dCategoria
    {
        //Atributos privados
        private int _idCategoria;
        private string _nome;
        private string _descricao;
        private string _textoBuscar;

        //encapsulamento get e set properties
        public int IdCategoria 
        {
            get // leitura
            {
                return _idCategoria;
            }
            set //alterar
            {
                _idCategoria = value;
            }
        }
        public string Nome
        {
            get
            {
                return _nome;
            }

            set
            {
                _nome = value;
            }
        }
        public string Descricao
        {
            get
            {
                return _descricao;
            }

            set
            {
                _descricao = value;
            }
        }
        public string TextoBuscar
        {
            get
            {
                return _textoBuscar;
            }
            set
            {
                _textoBuscar = value;
            }
        }

        // construtores
        public dCategoria() { }

        public dCategoria(int idCategoria, string nome, string descricao, string textoBuscar)
        {
            this.IdCategoria = idCategoria;
            this.Nome = nome;
            this.Descricao = descricao;
            this.TextoBuscar = textoBuscar;
        }

        //metodo inserir
        public string Inserir(dCategoria categoria)
        {
            string rsp = "";

            SqlConnection sqlCon = new SqlConnection();
            try 
            {
                sqlCon.ConnectionString = Concxao.Cn;
                sqlCon.Open();

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "spinserir_categoria";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParCategoria = new SqlParameter();
                ParCategoria.ParameterName = "@idcategoria";
                ParCategoria.SqlDbType = SqlDbType.Int;
                ParCategoria.Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add(ParCategoria);

                SqlParameter parNome = new SqlParameter();
                parNome.ParameterName = "@nome";
                parNome.SqlDbType = SqlDbType.VarChar;
                parNome.Size = 50;
                parNome.Value = categoria.Nome;
                sqlCmd.Parameters.Add(parNome);

                SqlParameter parDescricao = new SqlParameter();
                parDescricao.ParameterName = "@descricao";
                parDescricao.SqlDbType = SqlDbType.VarChar;
                parDescricao.Size = 8000;
                parDescricao.Value = categoria.Descricao;
                sqlCmd.Parameters.Add(parDescricao);

                rsp = sqlCmd.ExecuteNonQuery() == 1 ? "ok" : "Registro não foi inserido"; 

            }
            catch(Exception ex)
            {
                rsp = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }

            return rsp;
           
        }

        //metodo editar
        public string Editar(dCategoria categoria)
        {
            string rsp = "";

            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = Concxao.Cn;
                sqlCon.Open();

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "speditar_categoria";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdCategoria = new SqlParameter();
                parIdCategoria.ParameterName = "@idcategoria";
                parIdCategoria.SqlDbType = SqlDbType.Int;
                parIdCategoria.Value = categoria.IdCategoria;
                sqlCmd.Parameters.Add(parIdCategoria);

                SqlParameter parNome = new SqlParameter();
                parNome.ParameterName = "@nome";
                parNome.SqlDbType = SqlDbType.VarChar;
                parNome.Size = 50;
                parNome.Value = categoria.Nome;
                sqlCmd.Parameters.Add(parNome);

                SqlParameter parDescricao = new SqlParameter();
                parDescricao.ParameterName = "@descricao";
                parDescricao.SqlDbType = SqlDbType.VarChar;
                parDescricao.Size = 256;
                parDescricao.Value = categoria.Descricao;
                sqlCmd.Parameters.Add(parDescricao);

                rsp = sqlCmd.ExecuteNonQuery() == 1 ? "ok" : "Edição não foi concluida ";
        
            }
            catch(Exception ex)
            {
                rsp = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }

            return rsp;  
        }

        //metodo excluir
        public string Deletar(dCategoria categoria)
        {
            string rsp = "";
            SqlConnection sqlCom = new SqlConnection();
            try
            {
                sqlCom.ConnectionString = Concxao.Cn;
                sqlCom.Open();

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCom;
                sqlCmd.CommandText = "spdeletar_categoria";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParDeletar = new SqlParameter();
                ParDeletar.ParameterName = "@idcategoria";
                ParDeletar.SqlDbType = SqlDbType.Int;
                ParDeletar.Value = categoria.IdCategoria;
                sqlCmd.Parameters.Add(ParDeletar);

                rsp = sqlCmd.ExecuteNonQuery() == 1 ? "ok" : "A exclusão não foi concluida";
            }
            catch(Exception ex)
            {
                rsp = ex.Message;
            }
            finally
            {
                if (sqlCom.State == ConnectionState.Open) sqlCom.Close();
            }
            return rsp;
        }
       
        //metodo mostrar
        public DataTable Mostrar()
        {
            DataTable dtResultado = new DataTable("categoria");
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = Concxao.Cn;
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "spmostrar_categoria";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDat = new SqlDataAdapter(sqlCmd);
                sqlDat.Fill(dtResultado);
            }
            catch(Exception ex)
            {
                dtResultado = null;
            }
            return dtResultado;
        }

        //metodo buscar nome
        public DataTable BuscarNome(dCategoria categoria)
        {
            DataTable dtResultado = new DataTable("categoria");
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = Concxao.Cn;
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "sbbuscar_nome";
                sqlCmd.CommandType = CommandType.StoredProcedure;
               

                SqlParameter parTextoBuscar = new SqlParameter();
                parTextoBuscar.ParameterName = "@textobuscar";
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;
                parTextoBuscar.Size = 50;
                parTextoBuscar.Value = categoria.TextoBuscar;
                sqlCmd.Parameters.Add(parTextoBuscar);

                SqlDataAdapter sqlData = new SqlDataAdapter(sqlCmd);
                sqlData.Fill(dtResultado);
            }

            catch(Exception ex)
            {
                dtResultado = null;
            }

            return dtResultado;
        }



    }
}
