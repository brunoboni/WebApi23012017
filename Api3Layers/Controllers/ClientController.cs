using Business;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces;
using Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Api3Layers.Controllers
{

    /// <summary>
    /// Classe de acesso da API, utiliza o prefixo api/Client, seu acesso se da pela url/api/Client + o endpoint selecionado
    /// </summary>
    [RoutePrefix("api/Client")]
    public class ClientController : ApiController
    {
        HttpResponseMessage _response = new HttpResponseMessage();
        TaskCompletionSource<HttpResponseMessage> _CompleteTaskSource = new TaskCompletionSource<HttpResponseMessage>();


        /// <summary>
        /// Lista todos os clientes, é necessario melhorar a performance dessa busca, esta um coco
        /// enpoint getclients
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [Route("getclients")]
        [HttpGet]
        //[DeflateCompression]
        public Task<HttpResponseMessage> getClients(Client client)
        {


            ClientRepository clientrepository = new ClientRepository();
            try
            {
                IEnumerable<Client> clients = clientrepository.getAllDapper();
                var Adress = new AdressRepository();
                foreach (var item in clients)
                {
                    item.Adress = Adress.GetByClientId(item.Adress.ID_Adress);
                }

                _response = Request.CreateResponse(HttpStatusCode.OK, clients);
               


            }
            catch (Exception e)
            {
                _response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }

            _CompleteTaskSource.SetResult(_response);
            return _CompleteTaskSource.Task;

        }


        /// <summary>
        /// Realiza a busca de um cliente pelo ID (PK na tabela)
        /// endpoint GetbyID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getbyID")]
        [HttpGet]
        //[DeflateCompression]
        public Task<HttpResponseMessage> getbyID(int id)
        {
            ClientRepository clientrepository = new ClientRepository();
            try
            {
                Client client = clientrepository.GetById(id);
                _response = Request.CreateResponse(HttpStatusCode.OK, client);
            }
            catch (Exception e)
            {
                _response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }

            _CompleteTaskSource.SetResult(_response);
            return _CompleteTaskSource.Task;
        }


        /// <summary>
        /// Busca um cliente pelo nome
        /// endpoint getbyName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("getbyName")]
        [HttpGet]
        //[DeflateCompression]
        public Task<HttpResponseMessage> getbyName(string name)
        {
            ClientRepository clientrepository = new ClientRepository();
            try
            {
                Client client = clientrepository.getByNameAndActive(name);
                _response = Request.CreateResponse(HttpStatusCode.OK, client);
            }
            catch (Exception e)
            {
                _response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }

            _CompleteTaskSource.SetResult(_response);
            return _CompleteTaskSource.Task;
        }


        /// <summary>
        /// Cria um novo cliente
        /// endpoint PostClient
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [Route("PostClient")]
        [HttpPost]
        [ResponseType(typeof(Client))]
        public  Task<HttpResponseMessage> PostClient(Client client)
        {
            try
            {

               
                   

                    var clientbusiness = new ClientBusiness();
                    client = clientbusiness.ValidationClient(client);
       
                    ClientRepository clientrepository = new ClientRepository();
                    clientrepository.insert(client);
                
                _response = Request.CreateResponse(HttpStatusCode.Created, "Success");
                
            }
            catch (Exception e)
            {
                _response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }

            _CompleteTaskSource.SetResult(_response);
            return _CompleteTaskSource.Task;
        }

        /// <summary>
        /// Atualiza um cliente selecionado
        /// endpoint PutClient
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [Route("PutClient")]
        [HttpPut]
        [ResponseType(typeof(Client))]
        public Task<HttpResponseMessage> PutClient(Client client)
        {
            try
            {
                var clientbusiness = new ClientBusiness();
                client = clientbusiness.ValidationClient(client);
                ClientRepository clientrepository = new ClientRepository();
                clientrepository.Update(client);

                _response = Request.CreateResponse(HttpStatusCode.OK, "Sucess");
            }
            catch (Exception e)
            {
                _response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }

            _CompleteTaskSource.SetResult(_response);
            return _CompleteTaskSource.Task;

        }


        /// <summary>
        /// Realiza a desativação de um cliente 
        /// endpoint DeleteClient
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [Route("DeleteClient")]
        [HttpDelete]
        [ResponseType(typeof(Client))]
        public Task<HttpResponseMessage> DeleteClient(Client client)
        {
            try
            {
                var clientbusiness = new ClientBusiness();
                client = clientbusiness.ValidationClient(client);
                client = clientbusiness.desactiveClient(client);
                ClientRepository clientrepository = new ClientRepository();
                
                clientrepository.Update(client);

                _response = Request.CreateResponse(HttpStatusCode.OK, "Sucess");
            }
            catch (Exception e)
            {
                _response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }

            _CompleteTaskSource.SetResult(_response);
            return _CompleteTaskSource.Task;

        }

    }
}
