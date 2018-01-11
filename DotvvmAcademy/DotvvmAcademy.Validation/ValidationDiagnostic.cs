namespace DotvvmAcademy.Validation
{
    /// <summary>
    /// Represents an issue found by an <see cref="IValidationService{TRequest, TResponse}"/>.
    /// </summary>
    public class ValidationDiagnostic
    {
        /// <summary>
        /// Creates a new <see cref="ValidationDiagnostic"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <param name="location"></param>
        public ValidationDiagnostic(string id, string message, ValidationDiagnosticLocation location)
        {
            Id = id;
            Message = message;
            Location = location;
        }

        /// <summary>
        /// The unique identification code of the found issue.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The specific location in a file of the found issue.
        /// </summary>
        public ValidationDiagnosticLocation Location { get; set; }

        /// <summary>
        /// The message describing the found issue to the user.
        /// </summary>
        public string Message { get; set; }
    }
}