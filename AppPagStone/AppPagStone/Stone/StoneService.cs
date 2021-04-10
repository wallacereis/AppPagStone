using AppPagStone.Helpers;
using AppPagStone.Models;
using AppPagStone.Stone;
using AppPagStone.Stone.EnumTypes;
using AppPagStone.Stone.Split;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace AppPagStone.Services
{
    public static class StoneService
    {
        static HttpClient _httpClient = new HttpClient();

        public static async Task<CreateSaleResponse> EnviarTransacao(CartaoCredito cartaoCredito)
        {
            var model = new CreateSaleResponse();

            try
            {
                var transacao = new CreditCardTransaction()
                {
                    AmountInCents = Convert.ToInt64(cartaoCredito.ValorVenda.ToString("N2").Replace(",", "")),
                    CreditCard = new CreditCard()
                    {
                        CreditCardBrand = cartaoCredito.CreditCardBrand,
                        CreditCardNumber = cartaoCredito.Numero,
                        ExpMonth = cartaoCredito.Validade.Substring(0, 2).DefaultInt(),
                        ExpYear = cartaoCredito.Validade.Substring(5, 2).DefaultInt(),
                        HolderName = cartaoCredito.Titular, //O nome do portador deve conter mais de um caracter
                        SecurityCode = cartaoCredito.CVV
                    },
                    InstallmentCount = cartaoCredito.NumeroParcelas,
                    Options = new CreditCardTransactionOptions()
                    {
                        //0 - PRODUÇÃO
                        //1 - HOMOLOGAÇÃO (TESTE)
                        PaymentMethodCode = 1,
                        CurrencyIso = CurrencyIsoEnum.BRL,
                        SoftDescriptorText = "Obraki" //Texto da fatura do cartão
                    },
                    CreditCardOperation = CreditCardOperationEnum.AuthAndCapture //Autorização e Captura Instantânea
                };

                var saleRequest = new CreateSaleRequest()
                {
                    CreditCardTransactionCollection = new Collection<CreditCardTransaction>(new CreditCardTransaction[] { transacao }),
                    Order = new Order()
                    {
                        //Um dos campos que não é obrigatório, mas que é de extrema importância é o OrderReference.
                        //Aconselhamos que sempre envie essa informação, pois assim, facilitara a sua gestão!
                        OrderReference = cartaoCredito.OrderReference.ToString()
                    }
                };

                var data = JsonConvert.SerializeObject(saleRequest);
                //HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
                //content.Headers.Add("Keep-Alive", "true");

                var request = (HttpWebRequest)WebRequest.Create(new Uri($"{Utils.STONE_BASE_URL}/Sale"));
                request.ContentType = Utils.CONTENT_TYPE;
                request.Accept = Utils.CONTENT_TYPE;
                request.Method = "POST";
                request.Headers["MerchantKey"] = Utils.STONE_MERCHANT_KEY;

                using (var writer = new StreamWriter(await request.GetRequestStreamAsync()))
                {
                    writer.Write(data);
                    writer.Flush();
                    writer.Dispose();
                }

                using (var response = await request.GetResponseAsync())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        var streamReader = new StreamReader(stream);
                        var result = streamReader.ReadToEnd();

                        if (!string.IsNullOrEmpty(result))
                        {
                            model = JsonConvert.DeserializeObject<CreateSaleResponse>(result);
                        }
                        else
                        {
                            model.ErrorReport = new ErrorReport()
                            {
                                ErrorItemCollection = new Collection<ErrorItem>()
                                {
                                    new ErrorItem()
                                    {
                                         ErrorCode = 999,
                                         Description = TransactionResult.Message("c999")
                                    }
                                }
                            };
                        }
                    }
                }

                return model;
            }
            catch (WebException ex)
            {
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;

                    model.ErrorReport = new ErrorReport()
                    {
                        ErrorItemCollection = new Collection<ErrorItem>()
                        {
                            new ErrorItem()
                            {
                                ErrorCode = ((int)httpResponse.StatusCode),
                                Description = TransactionResult.Message(((int)httpResponse.StatusCode).ToString())
                            }
                        }
                    };
                }

                return model;

                //await Application.Current.MainPage.DisplayAlert("Informação", ex.Message, "OK");
            }
        }

        public static async Task<Token> GerarToken()
        {
            Token token = null;

            var merchanKeyMerchantSecretKeyBytes = Encoding.UTF8.GetBytes(Utils.STONE_SPLIT_MERCHANT_KEY + ":" + Utils.STONE_SPLIT_MERCHANT_SECRET_KEY);
            var merchanKeyMerchantSecretKeyBase64 = Convert.ToBase64String(merchanKeyMerchantSecretKeyBytes);

            using (var request = new HttpRequestMessage(HttpMethod.Get, Utils.STONE_TOKEN_URL))
            {
                request.Headers.Add("Authorization", $"Basic {merchanKeyMerchantSecretKeyBase64}");

                using (var response = await _httpClient.SendAsync(request))
                {
                    var strContent = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(strContent))
                        token = JsonConvert.DeserializeObject<Token>(strContent);
                }

                return token;
            }
        }

        public static async Task<SplitResponse> EnviarSplit(Split split, string token)
        {
            SplitResponse splitResponse = null;

            using (var request = new HttpRequestMessage(HttpMethod.Post, Utils.STONE_SPLIT_POST_URL))
            {
                var strContent = new StringContent(JsonConvert.SerializeObject(split), Encoding.UTF8, "application/json");

                request.Headers.Add("Authorization", $"Bearer {token}");
                request.Content = strContent;

                using (var response = await _httpClient.SendAsync(request))
                {
                    var strResponse = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(strResponse))
                        splitResponse = JsonConvert.DeserializeObject<SplitResponse>(strResponse);

                    return splitResponse;
                }
            }
        }

        public static async Task<SplitStatus> ConsultarSplit(string splitKey)
        {
            SplitStatus splitStatus = null;

            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{Utils.STONE_SPLIT_GET_URL}{splitKey}"))
            {
                request.Headers.Add("Authorization", $"Bearer {Utils.STONE_TOKEN_HOM}");

                using (var response = await _httpClient.SendAsync(request))
                {
                    var strResponse = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(strResponse))
                        splitStatus = JsonConvert.DeserializeObject<SplitStatus>(strResponse);

                    return splitStatus;
                }
            }
        }

        public static async Task<SplitCancel> CancelarSplit(string splitKey)
        {
            SplitCancel splitCancel = null;

            using (var request = new HttpRequestMessage(HttpMethod.Delete, $"{Utils.STONE_SPLIT_DELETE_URL}{splitKey}"))
            {
                request.Headers.Add("Authorization", $"Bearer {Utils.STONE_TOKEN_HOM}");

                using (var response = await _httpClient.SendAsync(request))
                {
                    var strResponse = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(strResponse))
                        splitCancel = JsonConvert.DeserializeObject<SplitCancel>(strResponse);

                    return splitCancel;
                }
            }
        }

        public static async Task<RecipientResponse> CadastrarRecipiente(Recipient recipient)
        {
            RecipientResponse recipientResponse = null;

            using (var request = new HttpRequestMessage(HttpMethod.Post, Utils.STONE_CREATE_RECIPIENT_URL))
            {
                var strContent = new StringContent(JsonConvert.SerializeObject(recipient), Encoding.UTF8, "application/json");

                request.Headers.Add("Authorization", $"Bearer {Utils.STONE_TOKEN_HOM}");
                request.Content = strContent;

                using (var response = await _httpClient.SendAsync(request))
                {
                    var strResponse = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(strResponse))
                        recipientResponse = JsonConvert.DeserializeObject<RecipientResponse>(strResponse);

                    return recipientResponse;
                }
            }
        }

        //public static async Task<string> PostTest(string strObjJson, string uri)
        //{
        //    var request = (HttpWebRequest)WebRequest.Create(new Uri(uri));
        //    request.ContentType = "application/json";
        //    request.Method = "POST";
        //    request.Headers["MerchantKey"] = STONE_MERCHANT_KEY;
        //    request.Accept = "application/json";

        //    var itemToSend = JsonConvert.SerializeObject(strObjJson);

        //    using (var streamWriter = new StreamWriter(await request.GetRequestStreamAsync()))
        //    {
        //        streamWriter.Write(sampleData);
        //        streamWriter.Flush();
        //        streamWriter.Dispose();
        //    }

        //    // Send the request to the server and wait for the response:  
        //    using (var response = await request.GetResponseAsync())
        //    {
        //        // Get a stream representation of the HTTP web response:  
        //        using (var stream = response.GetResponseStream())
        //        {
        //            var reader = new StreamReader(stream);
        //            var message = reader.ReadToEnd();
        //            return message;
        //        }
        //    }
        //}
    }
}
