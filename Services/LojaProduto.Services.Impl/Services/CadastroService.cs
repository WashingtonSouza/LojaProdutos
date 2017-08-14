using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SQFramework.Core;
using SQFramework.Data.Pagging;
using SQFramework.Web.Mvc.Report;
using Spring.Transaction.Interceptor;
using Spring.Transaction;
using LojaProduto.Services.Spec.DataTransferObjects;
using LojaProduto.Services.Spec.Services;
using LojaProduto.Domain.Entities;

namespace LojaProduto.Services.Impl.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    public class CadastroService : ServiceBase, ICadastroService
    {
        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public byte[] ExportReportCategoria(ReportViewerHelper.ReportType reportType, string orderProperty, bool orderAscending)
        {
            var items = ListarCategorias(0, int.MaxValue, orderProperty, orderAscending);

            return ReportViewerHelper.ExportReport(reportType, @"Reports\Categoria.rdlc", items);
        }

        [Transaction(TransactionPropagation.Required)]
        public DTOProduto SalvarProduto(DTOProduto dto)
        {
            Produto produto = null;

            var categoria = Categoria.GetRepository().Get(dto.Categoria.Id);
            var fornecedor = Fornecedor.GetRepository().Get(dto.Fornecedor.Id);

            if (dto.Id > 0)
            {
                produto = Produto.GetRepository().Get(dto.Id);

                if (produto == null)
                    throw new Exception("Produto não encontrado(a)!");

                dto.Transform<Produto>(produto);

                produto.SetCategoria(categoria);
                produto.SetFornecedor(fornecedor);
            }
            else
            {
                produto = new Produto(categoria, fornecedor);
                dto.Transform<Produto>(produto);
            }

            produto.Save();

            return produto.Transform<DTOProduto>();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public DTOProduto ObterProduto(int id)
        {
            return Produto.GetRepository().Get(id).Transform<DTOProduto>();
        }

        [Transaction(TransactionPropagation.Required)]
        public void DeletarProduto(int id)
        {
            var item = Produto.GetRepository().Get(id);
            item.Delete();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public IList<DTOProduto> ListarProdutos()
        {
            return Produto.GetRepository().ListAll().TransformList<DTOProduto>();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public PageMessage<DTOProduto> ListarProdutos(int startIndex, int pageSize, string orderProperty, bool orderAscending)
        {
            return Produto.GetRepository().ListarProdutos(startIndex, pageSize, orderProperty, orderAscending)
                .Transform<PageMessage<DTOProduto>>();
        }


        [Transaction(TransactionPropagation.Required)]
        public DTOPedido SalvarPedido(DTOPedido dto)
        {
            Pedido pedido = null;

            var cliente = Cliente.GetRepository().Get(dto.Cliente.Id);

            if (dto.Id > 0)
            {
                pedido = Pedido.GetRepository().Get(dto.Id);

                if (pedido == null)
                    throw new Exception("Pedido não encontrado(a)!");

                dto.Transform<Pedido>(pedido);

                pedido.SetCliente(cliente);
            }
            else
            {
                pedido = new Pedido(cliente);
                dto.Transform<Pedido>(pedido);
            }

            pedido.Save();

            return pedido.Transform<DTOPedido>();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public DTOPedido ObterPedido(int id)
        {
            return Pedido.GetRepository().Get(id).Transform<DTOPedido>();
        }

        [Transaction(TransactionPropagation.Required)]
        public void DeletarPedido(int id)
        {
            var item = Pedido.GetRepository().Get(id);
            item.Delete();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public IList<DTOPedido> ListarPedidos()
        {
            return Pedido.GetRepository().ListAll().TransformList<DTOPedido>();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public PageMessage<DTOPedido> ListarPedidos(int startIndex, int pageSize, string orderProperty, bool orderAscending)
        {
            return Pedido.GetRepository().ListarPedidos(startIndex, pageSize, orderProperty, orderAscending)
                .Transform<PageMessage<DTOPedido>>();
        }

        [Transaction(TransactionPropagation.Required)]
        public DTOItensPedido SalvarItensPedido(DTOItensPedido dto)
        {
            ItensPedido itensPedido = null;

            var pedido = Pedido.GetRepository().Get(dto.Pedido.Id);
            var produto = Produto.GetRepository().Get(dto.Produto.Id);

            if (dto.Id > 0)
            {
                itensPedido = ItensPedido.GetRepository().Get(dto.Id);

                if (itensPedido == null)
                    throw new Exception("ItensPedido não encontrado(a)!");

                dto.Transform<ItensPedido>(itensPedido);

                itensPedido.SetPedido(pedido);
                itensPedido.SetProduto(produto);
            }
            else
            {
                itensPedido = new ItensPedido(pedido, produto);
                dto.Transform<ItensPedido>(itensPedido);
            }

            itensPedido.Save();

            return itensPedido.Transform<DTOItensPedido>();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public DTOItensPedido ObterItensPedido(int id)
        {
            return ItensPedido.GetRepository().Get(id).Transform<DTOItensPedido>();
        }

        [Transaction(TransactionPropagation.Required)]
        public void DeletarItensPedido(int id)
        {
            var item = ItensPedido.GetRepository().Get(id);
            item.Delete();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public IList<DTOItensPedido> ListarItensPedidos()
        {
            return ItensPedido.GetRepository().ListAll().TransformList<DTOItensPedido>();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public PageMessage<DTOItensPedido> ListarItensPedidos(int startIndex, int pageSize, string orderProperty, bool orderAscending)
        {
            return ItensPedido.GetRepository().ListarItensPedidos(startIndex, pageSize, orderProperty, orderAscending)
                .Transform<PageMessage<DTOItensPedido>>();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public void TemPedido(int idProduto = 0, int quantidadeProduto = 0)
        {


            //var pedido = ObtemPedido(clienteId.Id);
            //var produto = ObterProduto(idProduto).Transform<Pedido>();
            if (pedido != null)
            {
                
                //AdicionaItemPedido(produto, quantidadeProduto);
            }
            else
            {
                //CriaPedido();
            }
        }

        public Pedido PossuiPedido()
        {
            var clienteId = ObterCliente(5);
            

            return
        }

        [Transaction(TransactionPropagation.Required)]
        public DTOFornecedor SalvarFornecedor(DTOFornecedor dto)
        {
            Fornecedor fornecedor = null;

            var endereco = Endereco.GetRepository().Get(dto.Endereco.Id);

            if (dto.Id > 0)
            {
                fornecedor = Fornecedor.GetRepository().Get(dto.Id);

                if (fornecedor == null)
                    throw new Exception("Fornecedor não encontrado(a)!");

                dto.Transform<Fornecedor>(fornecedor);

                fornecedor.SetEndereco(endereco);
            }
            else
            {
                fornecedor = new Fornecedor(endereco);
                dto.Transform<Fornecedor>(fornecedor);
            }

            fornecedor.Save();

            return fornecedor.Transform<DTOFornecedor>();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public DTOFornecedor ObterFornecedor(int id)
        {
            return Fornecedor.GetRepository().Get(id).Transform<DTOFornecedor>();
        }

        [Transaction(TransactionPropagation.Required)]
        public void DeletarFornecedor(int id)
        {
            var item = Fornecedor.GetRepository().Get(id);
            item.Delete();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public IList<DTOFornecedor> ListarFornecedores()
        {
            return Fornecedor.GetRepository().ListAll().TransformList<DTOFornecedor>();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public PageMessage<DTOFornecedor> ListarFornecedores(int startIndex, int pageSize, string orderProperty, bool orderAscending)
        {
            return Fornecedor.GetRepository().ListarFornecedores(startIndex, pageSize, orderProperty, orderAscending)
                .Transform<PageMessage<DTOFornecedor>>();
        }

        [Transaction(TransactionPropagation.Required)]
        public DTOEndereco SalvarEndereco(DTOEndereco dto)
        {
            Endereco endereco = null;

            if (dto.Id > 0)
            {
                endereco = Endereco.GetRepository().Get(dto.Id);

                if (endereco == null)
                    throw new Exception("Endereco não encontrado(a)!");

                dto.Transform<Endereco>(endereco);
            }
            else
            {
                endereco = dto.Transform<Endereco>();
            }

            endereco.Save();

            return endereco.Transform<DTOEndereco>();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public DTOEndereco ObterEndereco(int id)
        {
            return Endereco.GetRepository().Get(id).Transform<DTOEndereco>();
        }

        [Transaction(TransactionPropagation.Required)]
        public void DeletarEndereco(int id)
        {
            var item = Endereco.GetRepository().Get(id);
            item.Delete();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public IList<DTOEndereco> ListarEnderecos()
        {
            return Endereco.GetRepository().ListAll().TransformList<DTOEndereco>();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public PageMessage<DTOEndereco> ListarEnderecos(int startIndex, int pageSize, string orderProperty, bool orderAscending)
        {
            return Endereco.GetRepository().ListarEnderecos(startIndex, pageSize, orderProperty, orderAscending)
                .Transform<PageMessage<DTOEndereco>>();
        }

        [Transaction(TransactionPropagation.Required)]
        public DTOCliente SalvarCliente(DTOCliente dto)
        {
            Cliente cliente = null;

            var endereco = Endereco.GetRepository().Get(dto.Endereco.Id);

            if (dto.Id > 0)
            {
                cliente = Cliente.GetRepository().Get(dto.Id);

                if (cliente == null)
                    throw new Exception("Cliente não encontrado(a)!");

                dto.Transform<Cliente>(cliente);

                cliente.SetEndereco(endereco);
            }
            else
            {
                cliente = new Cliente(endereco);
                dto.Transform<Cliente>(cliente);
            }

            cliente.Save();

            return cliente.Transform<DTOCliente>();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public DTOCliente ObterCliente(int id)
        {
            return Cliente.GetRepository().Get(id).Transform<DTOCliente>();
        }

        [Transaction(TransactionPropagation.Required)]
        public void DeletarCliente(int id)
        {
            var item = Cliente.GetRepository().Get(id);
            item.Delete();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public IList<DTOCliente> ListarClientes()
        {
            return Cliente.GetRepository().ListAll().TransformList<DTOCliente>();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public PageMessage<DTOCliente> ListarClientes(int startIndex, int pageSize, string orderProperty, bool orderAscending)
        {
            return Cliente.GetRepository().ListarClientes(startIndex, pageSize, orderProperty, orderAscending)
                .Transform<PageMessage<DTOCliente>>();
        }

        [Transaction(TransactionPropagation.Required)]
        public DTOCategoria SalvarCategoria(DTOCategoria dto)
        {
            Categoria categoria = null;

            if (dto.Id > 0)
            {
                categoria = Categoria.GetRepository().Get(dto.Id);

                if (categoria == null)
                    throw new Exception("Categoria não encontrado(a)!");

                dto.Transform<Categoria>(categoria);
            }
            else
            {
                categoria = dto.Transform<Categoria>();
            }

            categoria.Save();

            return categoria.Transform<DTOCategoria>();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public DTOCategoria ObterCategoria(int id)
        {
            return Categoria.GetRepository().Get(id).Transform<DTOCategoria>();
        }

        [Transaction(TransactionPropagation.Required)]
        public void DeletarCategoria(int id)
        {
            var item = Categoria.GetRepository().Get(id);
            item.Delete();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public IList<DTOCategoria> ListarCategorias()
        {
            return Categoria.GetRepository().ListAll().TransformList<DTOCategoria>();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public PageMessage<DTOCategoria> ListarCategorias(int startIndex, int pageSize, string orderProperty, bool orderAscending)
        {
            return Categoria.GetRepository().ListarCategorias(startIndex, pageSize, orderProperty, orderAscending)
                .Transform<PageMessage<DTOCategoria>>();
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public IList<DTOProduto> PesquisarProdutos(string pesquisa)
        {
            return Produto.GetRepository().PesquisarProdutos(pesquisa).TransformList<DTOProduto>();
        }

        public bool ValidaQuantidadeProduto(DTOProduto produto)
        {
            if (produto.QuantidadeEmEstoque <= 0)
            {
                return false;
                throw new System.ArgumentException("Quantidade do Produto é zero ou negativo");
            }
            else
                return true;
        }

        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public DTOCliente PesquisaCliente(string cpf, string codigo)
        {
            return Cliente.GetRepository().PesquisaCliente(cpf, codigo).Transform<DTOCliente>();
        }
    }
}