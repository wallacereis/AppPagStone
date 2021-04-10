using Acr.UserDialogs;
using AppPagStone.Helpers;
using AppPagStone.Models;
using AppPagStone.Services;
using AppPagStone.Stone;
using AppPagStone.Stone.Split;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Forms;

namespace AppPagStone.ViewModel
{
    public class CartaoCreditoViewModel : BaseViewModel
    {
        public CartaoCreditoViewModel()
        {
            MensagemTransacao = "Resposta da Transação";
            EnviarTransacaoCommand = new Command(ExecuteEnviarTransacaoCommand);
            GerarTokenCommand = new Command(ExecuteGerarTokenCommand);
            CadastrarRecipienteCommand = new Command(ExecuteCadastrarRecipienteCommand);
            ConsultarSplitCommand = new Command(ExecuteConsultarSplitCommand);
            CancelarSplitCommand = new Command(ExecuteCancelarSplitCommand);
            Token = "Token";
            ValorMaximo = 10;
        }

        public Command EnviarTransacaoCommand { get; }
        public Command AlterarCommand { get; }
        public Command GerarTokenCommand { get; set; }
        public Command CadastrarRecipienteCommand { get; set; }
        public Command ConsultarSplitCommand { get; set; }
        public Command CancelarSplitCommand { get; set; }
        public CreateSaleResponse _saleResponse { get; set; }

        private string _numeroCartao;
        public string NumeroCartao
        {
            get { return _numeroCartao; }
            set
            {
                SetProperty(ref _numeroCartao, value);

                if (!string.IsNullOrEmpty(NumeroCartao))
                {
                    Bandeira = NumeroCartao.IdentificarBandeira();
                }
            }
        }

        private string _validade;
        public string Validade
        {
            get { return _validade; }
            set
            {
                SetProperty(ref _validade, value);
            }
        }

        private string _cvv;
        public string CVV
        {
            get { return _cvv; }
            set
            {
                SetProperty(ref _cvv, value);
            }
        }

        private string _titular;
        public string Titular
        {
            get { return _titular; }
            set
            {
                SetProperty(ref _titular, value);
            }
        }

        private string _valorVenda;
        public string ValorVenda
        {
            get { return _valorVenda; }
            set
            {
                SetProperty(ref _valorVenda, value);
            }
        }

        private string _numeroParcelas;
        public string NumeroParcelas
        {
            get { return _numeroParcelas; }
            set
            {
                SetProperty(ref _numeroParcelas, value);
            }
        }

        private string _mensagemTransacao;
        public string MensagemTransacao
        {
            get { return _mensagemTransacao; }
            set
            {
                SetProperty(ref _mensagemTransacao, value);
            }
        }

        private string _bandeira;
        public string Bandeira
        {
            get { return _bandeira; }
            set
            {
                SetProperty(ref _bandeira, value);
            }
        }

        private string _token;
        public string Token
        {
            get { return _token; }
            set
            {
                SetProperty(ref _token, value);
            }
        }

        private string _mensagemSplit;
        public string MensagemSplit
        {
            get { return _mensagemSplit; }
            set
            {
                SetProperty(ref _mensagemSplit, value);
            }
        }

        private int _valorMaximo;
        public int ValorMaximo
        {
            get { return _valorMaximo; }
            set
            {
                SetProperty(ref _valorMaximo, value);
            }
        }


        private async void ExecuteEnviarTransacaoCommand()
        {
            if (string.IsNullOrEmpty(NumeroCartao))
            {
                await Application.Current.MainPage.DisplayAlert("Informação", "Informe o número do cartão!", "OK");
                return;
            }

            if (string.IsNullOrEmpty(Validade) || Validade.Length < 7)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", "Informe a data de validade!", "OK");
                return;
            }

            if (string.IsNullOrEmpty(CVV))
            {
                await Application.Current.MainPage.DisplayAlert("Informação", "Informe o código de segurança (CVV)!", "OK");
                return;
            }

            if (string.IsNullOrEmpty(Titular))
            {
                await Application.Current.MainPage.DisplayAlert("Informação", "Informe o nome do titular!", "OK");
                return;
            }

            if (string.IsNullOrEmpty(ValorVenda) || Convert.ToDecimal(ValorVenda) == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", "Informe o valor da venda!", "OK");
                return;
            }

            if (string.IsNullOrEmpty(NumeroParcelas) || Convert.ToInt32(NumeroParcelas) == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", "Informe o número de parcelas!", "OK");
                return;
            }

            using (UserDialogs.Instance.Loading("Aguarde, enviando transação...", null, null, true, MaskType.Black))
            {
                var cartaoCredito = new CartaoCredito()
                {
                    Numero = NumeroCartao,
                    Validade = Validade,
                    CVV = CVV,
                    ValorVenda = Convert.ToDecimal(ValorVenda),
                    NumeroParcelas = Convert.ToInt32(NumeroParcelas),
                    OrderReference = Guid.NewGuid().ToString(),
                    Bandeira = Bandeira,
                    Titular = Titular
                };

                var identBandeira = cartaoCredito.Numero.ValidarBandeiraCartao();
                if (identBandeira == Stone.EnumTypes.CreditCardBrandEnum.NaoIdentificada)
                {
                    await Application.Current.MainPage.DisplayAlert("Informação", "Bandeira não suportada!", "OK");
                    return;
                }
                else
                {
                    cartaoCredito.CreditCardBrand = identBandeira;
                }

                var _response = await StoneService.EnviarTransacao(cartaoCredito);

                if (_response != null)
                {
                    //---------------------
                    //RESPOSTA DA TRANSAÇÃO
                    //---------------------
                    _saleResponse = _response;
                    //------------------------

                    if (_response.ErrorReport == null)
                    {
                        MensagemTransacao = $"Chave da Autorização\n{_response.CreditCardTransactionResultCollection?[0].TransactionKey.ToString()}\n" + _response.CreditCardTransactionResultCollection?[0].AcquirerMessage;

                        var confirm = await Application.Current.MainPage.DisplayAlert("Confirmação", "Splitar transação?", "SIM", "NÃO");
                        if (confirm)
                        {
                            SplitarPagamentoAsync(_saleResponse.CreditCardTransactionResultCollection[0].TransactionKey);
                        }
                    }
                    else
                    {
                        MensagemTransacao = string.Empty;

                        if (_response.ErrorReport != null && _response.ErrorReport.ErrorItemCollection.Count > 1)
                        {
                            foreach (var error in _response.ErrorReport.ErrorItemCollection)
                            {
                                MensagemTransacao = error.ErrorCode + " - " + error.Description + "\n";
                            }

                            MensagemTransacao = MensagemTransacao.Substring(0, MensagemTransacao.Length - 1);
                        }
                        else
                        {
                            MensagemTransacao = _response.ErrorReport.ErrorItemCollection[0].Description;
                        }
                    }
                }
                else
                {
                    MensagemTransacao = "Não foi possível processar a solicitação. Tente novamente!";
                }
            }
        }

        private async void ExecuteGerarTokenCommand()
        {
            try
            {
                var token = await StoneService.GerarToken();
                if(token != null && token.success)
                {
                    Token = token.data.token;
                    AppSettings.Token = Token;
                }
                else
                {
                    Token = "Erro ao gerar o token!";
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message, "OK");
            }
        }

        private async void ExecuteCadastrarRecipienteCommand()
        {
            try
            {
                var recipient = new Recipient()
                {
                     provider = "Sample Provider",
                     recipient_affiliation_key_in_provider = "4a48baaf-dcdd-40cd-852e-50e3de33d192",
                     recipient_name = "Sample Recipient 2"
                };

                var result = await StoneService.CadastrarRecipiente(recipient);
                if (result != null && result.success)
                {
                    await Application.Current.MainPage.DisplayAlert("Informação", "Recipiente cadastrado!\n" + result.data.recipient_name + "\n" + result.data.recipient_key, "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Informação", "Não foi possível cadastrar o recipiente!", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message, "OK");
            }
        }

        private async void SplitarPagamentoAsync(Guid transactionKey)
        {
            var recebedores = new List<SplitData>();
            var quantRecebedores = 2;
            var valorVenda = ValorVenda.DefaulFloat().ToString("N2");
            var valorPorRecebedor = (valorVenda.DefaulFloat() / quantRecebedores).ToString("N2").DefaulFloat();

            //--------------------
            //RECIPIENTES DE TESTE
            //--------------------
            recebedores.Add(new SplitData()
            {
                recipient_key = "c4286a21-b9c2-45ef-8f4b-c2eb4e89538d", //Guid do lojista na Stone
                amount = valorPorRecebedor
            });

            recebedores.Add(new SplitData()
            {
                recipient_key = "4c036ef6-8302-4fc2-bff2-2ba4263c2114",
                amount = valorPorRecebedor
            });
            //---------------------------------
            //VALIDAÇÃO DO VALOR POR RECIPIENTE
            //O VALOR DISTRIBUÍDO NÃO PODE ULTRAPASSAR O VALOR DA VENDA (Valor da venda / Quantidade de recipientes)
            //------------------------------------------------------------------------------------------------------
            var valorFinal = valorPorRecebedor * quantRecebedores;
            if (valorFinal > valorVenda.DefaulFloat())
            {
                var valorDif = valorFinal - valorVenda.DefaulFloat();
                valorDif = valorDif / quantRecebedores;
                foreach (var item in recebedores)
                {
                    item.amount -= valorDif.ToString("N2").DefaulFloat();
                }
            }
            //-----------------------------------------------------------

            var split = new Split()
            {
                 provider = "Sample Provider",
                 provider_transaction_key = transactionKey.ToString(),
                 transaction_amount = valorVenda.DefaulFloat(),
                 amount_split_mode = Split.AmountSplitMode.absolute,
                 fee_liability = Split.FeeLiability.Merchant,
                 splits = recebedores
            };

            try
            {
                var _response = await StoneService.EnviarSplit(split, Utils.STONE_TOKEN_HOM);

                if (_response != null && _response.success)
                {
                    MensagemSplit = _response.data.split_key;
                }
                else
                {
                    var mensagem = new StringBuilder();
                    foreach (var item in _response.operation_report)
                    {
                        mensagem.Append($"{item.property} - {item.message}\n");
                    }

                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem.ToString(), "OK");
                }
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message, "OK");
            }
        }

        private async void ExecuteConsultarSplitCommand()
        {
            Guid guidSplit;
            MensagemSplit = "c5d9b6a6-eed1-4b5e-82b6-4c503408f0c8";

            if (Guid.TryParse(MensagemSplit, out guidSplit))
            {
                try
                {
                    var _response = await StoneService.ConsultarSplit(guidSplit.ToString());

                    if (_response != null && _response.success)
                    {
                        MensagemSplit = _response.data.split_key;
                    }
                    else
                    {
                        var mensagem = new StringBuilder();
                        foreach (var item in _response.operation_report)
                        {
                            mensagem.Append($"{item.property} - {item.message}\n");
                        }

                        await Application.Current.MainPage.DisplayAlert("Informação", mensagem.ToString(), "OK");
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Informação", ex.Message, "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Informação", "Chave do split é inválida!", "OK");
            }
        }

        private async void ExecuteCancelarSplitCommand()
        {
            var confirm = await Application.Current.MainPage.DisplayAlert("Confirmação", "Confirma cancelamento do split?", "SIM", "NÃO");
            if (confirm)
            {
                Guid guidSplit;
                MensagemSplit = "c5d9b6a6-eed1-4b5e-82b6-4c503408f0c8";

                if (Guid.TryParse(MensagemSplit, out guidSplit))
                {
                    try
                    {
                        var _response = await StoneService.CancelarSplit(guidSplit.ToString());

                        if (_response != null && _response.success)
                        {
                            MensagemSplit = "Split cancelado com sucesso!";
                        }
                        else
                        {
                            var mensagem = new StringBuilder();
                            foreach (var item in _response.operation_report)
                            {
                                mensagem.Append($"{item.property} - {item.message}\n");
                            }

                            await Application.Current.MainPage.DisplayAlert("Informação", mensagem.ToString(), "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Informação", ex.Message, "OK");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Informação", "Chave do split é inválida!", "OK");
                }
            }
        }
    }
}
