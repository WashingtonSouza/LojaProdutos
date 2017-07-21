using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SQFramework.Spring;
using SQFramework.Data.Pagging;
using SQFramework.Web.Mvc.Report;
using LojaProduto.Services.Spec.DataTransferObjects;

namespace LojaProduto.Services.Spec.Services
{
    [ServiceContract]
    [ObjectMap("CadastroService", true)]
    public interface ICadastroService : IServiceBase
    {
        [OperationContract]
        byte[] ExportReportCategoria(ReportViewerHelper.ReportType reportType, string orderProperty, bool orderAscending);

        [OperationContract]
        DTOProduto SalvarProduto(DTOProduto dto);

        [OperationContract]
        DTOProduto ObterProduto(int id);

        [OperationContract]
        void DeletarProduto(int id);

        [OperationContract]
        IList<DTOProduto> PesquisarProdutos(string pesquisa);

        [OperationContract]
        IList<DTOProduto> ListarProdutos();

        [OperationContract(Name = "ListarProdutosPaged")]
        PageMessage<DTOProduto> ListarProdutos(int startIndex, int pageSize, string orderProperty, bool orderAscending);

        [OperationContract]
        DTOPedido SalvarPedido(DTOPedido dto);

        [OperationContract]
        DTOPedido ObterPedido(int id);

        [OperationContract]
        void DeletarPedido(int id);

        [OperationContract]
        IList<DTOPedido> ListarPedidos();

        [OperationContract(Name = "ListarPedidosPaged")]
        PageMessage<DTOPedido> ListarPedidos(int startIndex, int pageSize, string orderProperty, bool orderAscending);

        [OperationContract]
        DTOItensPedido SalvarItensPedido(DTOItensPedido dto);

        [OperationContract]
        DTOItensPedido ObterItensPedido(int id);

        [OperationContract]
        void DeletarItensPedido(int id);

        [OperationContract]
        IList<DTOItensPedido> ListarItensPedidos();

        [OperationContract(Name = "ListarItensPedidosPaged")]
        PageMessage<DTOItensPedido> ListarItensPedidos(int startIndex, int pageSize, string orderProperty, bool orderAscending);

        [OperationContract]
        DTOFornecedor SalvarFornecedor(DTOFornecedor dto);

        [OperationContract]
        DTOFornecedor ObterFornecedor(int id);

        [OperationContract]
        void DeletarFornecedor(int id);

        [OperationContract]
        IList<DTOFornecedor> ListarFornecedores();

        [OperationContract(Name = "ListarFornecedoresPaged")]
        PageMessage<DTOFornecedor> ListarFornecedores(int startIndex, int pageSize, string orderProperty, bool orderAscending);

        [OperationContract]
        DTOEndereco SalvarEndereco(DTOEndereco dto);

        [OperationContract]
        DTOEndereco ObterEndereco(int id);

        [OperationContract]
        void DeletarEndereco(int id);

        [OperationContract]
        IList<DTOEndereco> ListarEnderecos();

        [OperationContract(Name = "ListarEnderecosPaged")]
        PageMessage<DTOEndereco> ListarEnderecos(int startIndex, int pageSize, string orderProperty, bool orderAscending);

        [OperationContract]
        DTOCliente SalvarCliente(DTOCliente dto);

        [OperationContract]
        DTOCliente ObterCliente(int id);

        [OperationContract]
        void DeletarCliente(int id);

        [OperationContract]
        IList<DTOCliente> ListarClientes();

        [OperationContract(Name = "ListarClientesPaged")]
        PageMessage<DTOCliente> ListarClientes(int startIndex, int pageSize, string orderProperty, bool orderAscending);

        [OperationContract]
        DTOCategoria SalvarCategoria(DTOCategoria dto);

        [OperationContract]
        DTOCategoria ObterCategoria(int id);

        [OperationContract]
        void DeletarCategoria(int id);

        [OperationContract]
        IList<DTOCategoria> ListarCategorias();

        [OperationContract(Name = "ListarCategoriasPaged")]
        PageMessage<DTOCategoria> ListarCategorias(int startIndex, int pageSize, string orderProperty, bool orderAscending);
    }
}