namespace Waes.Assignment.Domain.Commands
{
    /// <summary>
    /// Command object that represents the intention of analyze the diff
    /// </summary>
    public class AnalyzeDiffCommand : Command
    {
        /// <summary>
        /// The correlation id
        /// </summary>
        public string CorrelationId { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="AnalyzeDiffCommand"/> with correlationId
        /// </summary>
        /// <param name="correlationId"></param>
        public AnalyzeDiffCommand(string correlationId)
        {
            CorrelationId = correlationId;
        }
    }
}
