namespace Waes.Assignment.Api.ViewModels
{
    public abstract class CreatePayLoadRequest
    {
        //TODO: Adicionar validações
        public byte[] Content { get; set; }
    }
}
