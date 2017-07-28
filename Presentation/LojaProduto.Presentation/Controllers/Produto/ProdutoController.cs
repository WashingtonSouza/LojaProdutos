using LojaProduto.Presentation.Controllers.Base;
using LojaProduto.Presentation.Models;
using LojaProduto.Services.Spec.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaProduto.Presentation.Controllers
{
    public class ProdutoController : CustomController
    {
        // GET: Produto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form()
        {
            return View();
        }

        public ActionResult ListaProdutos(string query)
        {
            ListaProdutos model = new ListaProdutos();

            model.TextoDePesquisa = query;
            if (query != null)
            {
                var result = GetCadastroService().PesquisarProdutos(query);
                model.Produtos = result.ToList();

                return View(model);
            }
            else
            {
                var result = GetCadastroService().ListarProdutos();
                model.Produtos = result.ToList();

                return View(model);// Json(result, JsonRequestBehavior.DenyGet);   
            }
        }

        public ActionResult CadastrarProduto(DTOProduto produto)
        {
            if (ModelState.IsValid)
            {
                produto = GetCadastroService().SalvarProduto(produto);

                return InformationMessage("Informação", "O registro foi gravado com sucesso.", Url.Action("Index"));
            }
            else
                return ErrorMessage("Erro", "Registro Inválido!");
        }

        //public ActionResult PesquisarProdutos(string query)
        //{
        //    var produto = GetCadastroService().PesquisarProdutos(query);

        //    ListaProdutos model = new ListaProdutos();

        //    //var result = GetCadastroService().ListarProdutos();
        //    //model.Produtos = result.ToList();
        //    return View();
        //}

    }
}