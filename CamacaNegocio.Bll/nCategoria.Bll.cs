using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CamadaData;
using System.Data;

namespace CamacaNegocio.Bll
{
    public class nCategoria
    {
        //Medoto Inserir
        public static string Inserir(string nome, string descricao)
        {
            dCategoria obj = new dCategoria();
            obj.Nome = nome;
            obj.Descricao = descricao;
            return obj.Inserir(obj);
        }

        //metodo editar
        public static string Editar(int idCategoria, string nome, string descricao)
        {
            dCategoria obj = new  CamadaData.dCategoria();
            obj.IdCategoria = idCategoria; 
            obj.Nome = nome;
            obj.Descricao = descricao;
            return obj.Editar(obj);
        }

        //metodo deletar
        public static string Excluir(int idCategoria)
        {
            dCategoria obj = new dCategoria();
            obj.IdCategoria = idCategoria;
            return obj.Deletar(obj);
        }

        //metodo mostra
        public static DataTable Mostra()
        {
            return new dCategoria().Mostrar();
        }

        //metodo buscarNome
        public static DataTable BuscarNome(string textoBuscar)
        {
            dCategoria obj = new dCategoria();
            obj.TextoBuscar = textoBuscar;
            return obj.BuscarNome(obj);
        }
    }
}
