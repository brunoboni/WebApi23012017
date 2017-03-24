using DDD.Domain.Entities;


/// <summary>
/// Camada responsavel por realizar todas as validações de negocios contidas nas entidades
/// </summary>
namespace Business
{
    public class ClientBusiness
    {

        /// <summary>
        /// Realiza as validações para inserção de ususarios
        /// </summary>
        /// <param name="client"></param>
        /// <returns>Client</returns>
        public  Client ValidationClient(Client client)
        {
           return client.ValidationClient(client);
        }

        /// <summary>
        /// Realiza as validações para desativar (excluir) um cliente 
        /// </summary>
        /// <param name="client"></param>
        /// <returns>Client</returns>
        public Client desactiveClient(Client client)
        {
                return client.desactiveClient(client);
        }

    }
}
