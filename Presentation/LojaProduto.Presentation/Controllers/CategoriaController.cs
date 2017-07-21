using LojaProduto.Presentation.Controllers.Base;
using LojaProduto.Services.Spec.DataTransferObjects;
using SQFramework.Core;
using SQFramework.Web.Mvc;
using SQFramework.Web.Mvc.Extensions;
using SQFramework.Web.Mvc.Filters;
using SQFramework.Web.Mvc.Report;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LojaProduto.Presentation.Controllers
{
    public class CategoriaController : CustomController
    {
        #region [ Actions ]

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int startIndex = 0, int pageSize = 10, string orderProperty = null, bool orderAscending = true)
        {
            var result = GetCadastroService().ListarCategorias(startIndex, pageSize, orderProperty, orderAscending);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Detalhar(int? id)
        {
            DTOCategoria result = null;

            if (id.HasValue)
            {
                result = GetCadastroService().ObterCategoria(id.Value);

                if (result == null)
                    throw new Exception("Registro não encontrado!");
            }
            else
                result = new DTOCategoria();

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Salvar(DTOCategoria item)
        {
            if (ModelState.IsValid)
            {
                item = GetCadastroService().SalvarCategoria(item);

                return InformationMessage("Informação", "O registro foi gravado com sucesso.", Url.Action("Index"));
            }
            else
                return ErrorMessage("Erro", "Registro Inválido!");
        }

        [HttpPost]
        public ActionResult Excluir(int? id)
        {
            if (id.HasValue)
            {
                GetCadastroService().DeletarCategoria(id.Value);

                return InformationMessage("Informação", "Registro excluído com sucesso.");
            }
            else
                return ErrorMessage("Erro", "Registro Inválido!");
        }

        [HttpGet]
        public ActionResult ExportReport(ReportViewerHelper.ReportType reportType = ReportViewerHelper.ReportType.PDF, string orderProperty = null, bool orderAscending = true)
        {
            byte[] bytes = GetCadastroService().ExportReportCategoria(reportType, orderProperty, orderAscending);

            if (bytes != null)
            {
                string filename = "RelatorioCategoria";

                switch (reportType)
                {
                    case ReportViewerHelper.ReportType.Excel:
                        filename += ".xls";
                        break;
                    case ReportViewerHelper.ReportType.PDF:
                        filename += ".pdf";
                        break;
                    case ReportViewerHelper.ReportType.Word:
                    default:
                        filename += ".doc";
                        break;
                }

                return File(bytes, "application/octet-stream", filename);
            }
            else
                return ErrorMessage("Erro", "Não foi possível gerar o relatório!");
        }

        #endregion
    }
}