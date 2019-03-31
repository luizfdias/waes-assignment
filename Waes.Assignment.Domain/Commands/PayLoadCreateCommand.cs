using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Domain.Commands
{
    /// <summary>
    /// Command object that represents the intention of creates a new payload
    /// </summary>
    public class PayLoadCreateCommand : Command
    {
        /// <summary>
        /// The correlation id 
        /// </summary>
        public string CorrelationId { get; }

        /// <summary>
        /// The content 
        /// </summary>
        public byte[] Content { get; }

        /// <summary>
        /// The side 
        /// </summary>
        public SideEnum Side { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="AnalyzeDiffCommand"/> with correlationId, content and side
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="content"></param>
        /// <param name="side"></param>
        public PayLoadCreateCommand(string correlationId, byte[] content, SideEnum side)
        {
            CorrelationId = correlationId;
            Content = content;
            Side = side;
        }
    }
}
