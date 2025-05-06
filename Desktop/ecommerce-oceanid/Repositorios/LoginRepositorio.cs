using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MySqlX.XDevAPI;
using prototipo1204.Data;
using prototipo1204.Models;
using prototipo1204.Repositorios.Interface;
using System.Configuration;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace prototipo1204.Repositorios
{
    public class LoginRepositorio : ILoginRepositorio
    {
        private readonly oceanidDBContext _context;
        private readonly string _conexaoMySQL;
        public LoginRepositorio(oceanidDBContext context, IConfiguration configuration)
        {
            _context = context;
            _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");
        }

        public object Login(string email, string senha)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                // parte de cleinte
                var cmdCliente = new MySqlCommand("SELECT * FROM tbCliente WHERE emailCliente = @Email AND senhaCliente = @Senha", conexao);
                cmdCliente.Parameters.AddWithValue("@Email", email);
                cmdCliente.Parameters.AddWithValue("@Senha", senha);

                using (var dr = cmdCliente.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        var cliente = new Cliente
                        {
                            idCliente = Convert.ToInt32(dr["idCliente"]),
                            nomeCompleto = dr["nomeCompleto"].ToString(),
                            emailCliente = dr["emailCliente"].ToString(),
                            senhaCliente = dr["senhaCliente"].ToString()
                        };
                        return cliente;
                    }
                }

                // parte de adm
                var cmdAdm = new MySqlCommand("SELECT * FROM tbAdm WHERE emailAdm = @Email AND senhaAdm = @Senha", conexao);
                cmdAdm.Parameters.AddWithValue("@Email", email);
                cmdAdm.Parameters.AddWithValue("@Senha", senha);

                using (var dr = cmdAdm.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        var adm = new Adm
                        {
                            idAdm = Convert.ToInt32(dr["idAdm"]),
                            nomeAdm = dr["nomeAdm"].ToString(),
                            emailAdm = dr["emailAdm"].ToString(),
                            senhaAdm = dr["senhaAdm"].ToString()
                        };
                        return adm;
                    }
                }

                return null;
            }
        }
    
    }

}