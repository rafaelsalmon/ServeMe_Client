using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SirvaMe.Interfaces;
using SirvaMe.Models;
using SQLite.Net;
using Xamarin.Forms;

namespace SirvaMe.Repositorio
{

    /// <summary>
    /// Encapsulates persistence of data
    /// </summary>
    /// 
    public class AcessoDados<T> : IDisposable
    {
        private readonly SQLiteConnection _conexao;

        public AcessoDados()
        {
            var config = DependencyService.Get<IConfig>();
            _conexao = new SQLiteConnection(config.Plataforma, Path.Combine(config.DiretorioDB, "bancodados.db3"));

            //Tables
            _conexao.CreateTable<Usuario>();
            _conexao.CreateTable<Sistema>();
        }

        public void Insert(T objeto)
        {
            _conexao.Insert(objeto);
        }

        public void Update(T objeto)
        {
            _conexao.Update(objeto);
        }

        public void Delete(T objeto)
        {
            _conexao.Delete(objeto);
        }

        public Sistema GetSistema()
        {
            return _conexao.Table<Sistema>().FirstOrDefault();
        }

        public Usuario GetUsuario()
        {
            return _conexao.Table<Usuario>().FirstOrDefault();
        }

        public Usuario ObterPorId(int id)
        {
            return _conexao.Table<Usuario>().FirstOrDefault(c => c.Id == id);
        }

        public List<Usuario> Listar()
        {
            return _conexao.Table<Usuario>().OrderBy(c => c.Nome).ToList();
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
