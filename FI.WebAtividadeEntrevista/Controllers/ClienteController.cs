using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using Newtonsoft.Json;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
            
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
                
                model.Id = bo.Incluir(new Cliente()
                {                    
                    CEP = model.CEP,
                    CPF = model.CPF,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone
                });

                if (model.Id == -1)
                    return Json(new { titulo = "CPF já cadastrado!", status = "400", mensagem = "Use outro CPF!" });


                return Json(new { titulo = "Sucesso!", status = "200", mensagem = "Cliente cadastrado com sucesso!" });
            }
        }

        [HttpPut]
        public ActionResult AlterarCliente(ClienteModel model)
        {
            try
            {
                BoCliente bo = new BoCliente();
   
                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();
                    var erro = erros.First();
                    Response.StatusCode = 400;
                    var dados = new { titulo = "CPF inválido", status = "400", mensagem = erro };
                    //json = Newtonsoft.Json.JsonConvert.SerializeObject(dados);
                    //Response.ContentType = "application/json";
                    //return Content(json);
                    var json = JsonConvert.SerializeObject(dados);
                    Response.Clear();
                    // Define o Content-Type manualmente
                    Response.ContentType = "application/json"; // Remove charset
                    return Content(json); // Retorna o JSON
                    //var json = JsonConvert.SerializeObject(dados);
                    //var result = new ContentResult
                    //{
                    //    Content = json,
                    //    ContentType = "application/json"
                    //};

                    //return result;
                }
                else
                {
                    model.Id = bo.Alterar(new Cliente()
                    {
                        Id = model.Id,
                        CPF = model.CPF,
                        CEP = model.CEP,
                        Cidade = model.Cidade,
                        Email = model.Email,
                        Estado = model.Estado,
                        Logradouro = model.Logradouro,
                        Nacionalidade = model.Nacionalidade,
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Telefone = model.Telefone
                    });
                    
                    if (model.Id == -1)
                    {
                        Response.StatusCode = 400;
                        var dados1 = new { titulo = "CPF já cadastrado!", status = "400", mensagem = "Use outro CPF!" };
                        //var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(dados1);
                        Response.ContentType = "application/json";
                        var json1 = JsonConvert.SerializeObject(dados1);
                        Response.Clear();
                       // Define o Content-Type manualmente
                        Response.ContentType = "application/json"; // Remove charset
                        return Content(json1); // Retorna o JSON
                        //var json1 = JsonConvert.SerializeObject(dados1);
                        //var result1 = new ContentResult
                        //{
                        //    Content = json1,
                        //    ContentType = "application/json"
                        //};

                        //return result1;
                      //  return Json(dados1, JsonRequestBehavior.AllowGet);
                    }

                    Response.StatusCode = 200;
                    var dados2 = new { titulo = "Sucesso!", status = "200", mensagem = "Cliente alterado com sucesso!" };
                    //var json2 = Newtonsoft.Json.JsonConvert.SerializeObject(dados2);
                    Response.ContentType = "application/json";
                    var json2 = JsonConvert.SerializeObject(dados2);
                    Response.Clear();
                    // Define o Content-Type manualmente
                    Response.ContentType = "application/json"; // Remove charset
                    return Content(json2); // Retorna o JSON
                    //var json2 = JsonConvert.SerializeObject(dados2);
                    //var result2 = new ContentResult
                    //{
                    //    Content = json2,
                    //    ContentType = "application/json"
                    //};
                    //return result2;
                    //return Json(dados2, JsonRequestBehavior.AllowGet);

                }
            
            } catch (Exception e)
            {
                Response.StatusCode = 500;
                var dados3 = new { titulo = "Erro!", status = "500", mensagem = e.Message };
                //var json3 = Newtonsoft.Json.JsonConvert.SerializeObject(dados3);
                Response.ContentType = "application/json";
                var json3 = JsonConvert.SerializeObject(dados3);

                // Define o Content-Type manualmente
                Response.Clear();
                Response.ContentType = "application/json"; // Remove charset
                return Content(json3); // Retorna o JSON
                //var json3 = JsonConvert.SerializeObject(dados3);
                //var result3 = new ContentResult
                //{
                //    Content = json3,
                //    ContentType = "application/json"
                //};
                //return result3;
                //return Json(dados3, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    CPF = cliente.CPF,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone
                };

            
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}