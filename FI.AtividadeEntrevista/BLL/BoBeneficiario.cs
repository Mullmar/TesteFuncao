using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(DML.Cliente cliente)
        {

            cliente.CPF = cliente.CPF.Replace(".", "").Replace("-", "");

            if (this.VerificarExistencia(cliente.CPF))
            {
                return -1;
            }

            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Incluir(cliente);
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public DML.Cliente Consultar(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Consultar(id);
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os beneficiários de determinado cliente
        /// </summary>
        public List<DML.Beneficiario> Listar(long id)
        {
            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();
            return ben.Listar(id);
        }


        /// <summary>
        /// Altera um beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        public void Alterar(DML.Beneficiario beneficiario)
        {
            beneficiario.CPF = beneficiario.CPF.Replace("-", "").Replace(".", "");
            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();
            if(!ben.VerificarExistencia(beneficiario.CPF))
            {
                ben.Alterar(beneficiario);
            } 
        }
        public bool VerificarExistencia(string CPF)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.VerificarExistencia(CPF);
        }

        public void Inserir(Beneficiario beneficiario)
        {
            beneficiario.CPF = beneficiario.CPF.Replace("-", "").Replace(".", "");
            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();
            ben.Inserir(beneficiario);
        }

        public void Excluir(Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();
            ben.Excluir(beneficiario);
        }
    }
}
