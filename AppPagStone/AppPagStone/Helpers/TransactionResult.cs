using AppPagStone.Resources;

namespace AppPagStone.Helpers
{
    public class TransactionResult
    {
        public static string Message(string codMessage)
        {
            return TransactionErrors.ResourceManager.GetString(string.Concat("c", codMessage));
        }
    }
}
