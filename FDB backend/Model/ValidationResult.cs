namespace FDB_backend.Model
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }}
