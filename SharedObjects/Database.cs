using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace CTIS.SIMNAC.Teste.Automatizado.SharedObjects
{
    public class BancodeDados

    {
        #region Declaração de variáveis públicas da classe


        public SqlConnection conexaoSQL;
        public SqlCommand cmd = new SqlCommand();
        public SqlDataReader reader;
        private object driver;

        public BancodeDados(object driver)
        {
            this.driver = driver;
        }

        #endregion

        public void executarComandoSQL(string comandoSQL)
        {
            var caminhoArquivo = Path.Combine(Global.DIRETORIO_APLICACAO, "database.ini");
            var conteudoArquivo = File.ReadAllLines(caminhoArquivo).First();

            conexaoSQL = new SqlConnection(conteudoArquivo);
            cmd.CommandText = (comandoSQL);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conexaoSQL;

            conexaoSQL.Open();

            reader = cmd.ExecuteReader();

            conexaoSQL.Close();
        }
    }
}
