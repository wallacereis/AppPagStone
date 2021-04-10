namespace AppPagStone.Stone.EnumTypes
{
    public enum CreditCardOperationEnum
    {

        /// <summary>
        /// Somente autorizar
        /// </summary>
        AuthOnly = 1,
        /// <summary>
        /// Autorizar e capturar
        /// </summary>
        AuthAndCapture = 2,
        /// <summary>
        /// Autorizar e capturar com atraso
        /// </summary>
        AuthAndCaptureWithDelay = 3,
    }
}
