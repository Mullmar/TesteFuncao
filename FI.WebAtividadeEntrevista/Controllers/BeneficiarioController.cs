using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;
using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        // GET: Beneficiario
        [HttpGet]
        [Route("Beneficiario/Index/{id}")]
        public ActionResult Index(long id)
        {
            BoBeneficiario bllBen = new BoBeneficiario();
            List<Beneficiario> beneficiarios = bllBen.Listar(id);
            ClienteBeneficiariosModel cliBenModel = new ClienteBeneficiariosModel();
            cliBenModel.Beneficiarios = new List<BeneficiarioModel>();
            BeneficiarioModel ben = null;

            foreach (Beneficiario beneficiario in beneficiarios)
            {
                ClienteModel cli = new ClienteModel();
                ben = new BeneficiarioModel();
                ben.Id = beneficiario.Id;
                ben.Nome = beneficiario.Nome;
                ben.CPF = beneficiario.CPF;
                ben.IdCliente = beneficiario.IdCliente;
                cliBenModel.Beneficiarios.Add(ben);
                cli.Id = beneficiario.IdCliente;
                cliBenModel.Cliente = cli;
            }

            return PartialView("Index", cliBenModel);
        }
        
        [HttpPut]
        public JsonResult Alterar()
        {
            try
            {
                var jsonString = new StreamReader(HttpContext.Request.InputStream).ReadToEnd();
                var beneficiario = JsonConvert.DeserializeObject<Beneficiario>(jsonString);

                
                
                if (beneficiario == null)
                {
                    return Json(new { Message = "Dados Inválidos", StatusCode = "400", Records = beneficiario });
                } else
                {
                    beneficiario.CPF = beneficiario.CPF.Replace("-", "").Replace(".", "");
                }

                BoBeneficiario bo = new BoBeneficiario();

                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, erros));
                }
                else
                {
                    List<Beneficiario> listBen = bo.Listar(beneficiario.IdCliente);

                    

                    foreach (var item in listBen)
                    {
                        if (item.CPF == beneficiario.CPF && item.Id != beneficiario.Id)
                        {
                            return Json(new { Message = "CPF já cadastrado anteriormente!", StatusCode = "400" });
                        } else if (item.CPF == beneficiario.CPF && item.Nome == beneficiario.Nome)
                        {
                            return Json(new { Message = "Não houveram alterações!", StatusCode = "400" });
                        }
                    }
                   
                    bo.Alterar(beneficiario);
             

                    return Json(new { Message = "Beneficiário alterado com sucesso!", StatusCode = "200" });


                }
            }
            catch (Exception e)
            {
                return Json(new { Message = e.Message, StatusCode = "500" });
            }



        }

        [HttpDelete]
        public JsonResult Excluir(long id)
        {
            try
            {
                BoBeneficiario bo = new BoBeneficiario();



                bo.Excluir(id);


                return Json(new { Message = "Beneficiário excluído com sucesso!", StatusCode = "200" });


            }
            catch (Exception e)
            {
                return Json(new { Message = e.Message, StatusCode = "500" });
            }



        }

        [HttpPost]
        public JsonResult Inserir()
        {
            try
            {
                var jsonString = new StreamReader(HttpContext.Request.InputStream).ReadToEnd();
                var beneficiario = JsonConvert.DeserializeObject<Beneficiario>(jsonString);



                if (beneficiario == null)
                {
                    return Json(new { Message = "Dados Inválidos", StatusCode = "400", Records = beneficiario });
                }
                else
                {
                    beneficiario.CPF = beneficiario.CPF.Replace("-", "").Replace(".", "");
                }

                BoBeneficiario bo = new BoBeneficiario();

                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, erros));
                }
                else
                {
                    List<Beneficiario> listBen = bo.Listar(beneficiario.IdCliente);



                    foreach (var item in listBen)
                    {
                        if (item.CPF == beneficiario.CPF)
                        {
                            return Json(new { Message = "CPF já cadastrado anteriormente!", StatusCode = "400" });
                        }
                    }

                    bo.Inserir(beneficiario);


                    return Json(new { Message = "Beneficiário incluído com sucesso!", StatusCode = "200" });


                }
            }
            catch (Exception e)
            {
                return Json(new { Message = e.Message, StatusCode = "500" });
            }



        }
    }
}