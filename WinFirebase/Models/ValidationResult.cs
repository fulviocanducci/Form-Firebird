using System.Collections.Generic;
using System.Linq;

namespace WinFirebase.Models
{
    public class ValidationResult: System.IDisposable
    {
        public bool IsValid()
        {
            return Messages.Any() == false;
        }
        public List<string> Messages { get; set; } = new List<string>();
        public string Content()
        {
            return string.Join("\r\n", Messages);
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
