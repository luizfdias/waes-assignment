namespace Waes.Assignment.Application.ApiModels
{
    /// <summary>
    /// The EqualResponse represents the diff result when payloads are equal
    /// </summary>
    public class EqualResponse : DiffResponse
    {        
        /// <summary>
        /// The result: Equal
        /// </summary>
        public override string Result => "Equal";
    }
}
