using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;

namespace MaratonaBots.Formulario
{
    // FormFlow ignora índices zerados, por isso a necessidade de atribuir valor 1 às primeiras posições dos Enums.
    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "Desculpe, não entendi \"{0}\".")]
    public class Pedido
    {
        public Salgadinhos Salgadinhos { get; set; }

        public Bebidas Bebidas { get; set; }

        public TipoEntrega TipoEntrega { get; set; }

        public CPFNaNota CPFNaNota { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }

        public static IForm<Pedido> BuildForm()
        {
            var form = new FormBuilder<Pedido>();

            // Prioriza botões na tela
            form.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Buttons;
            form.Configuration.Yes = new string[] { "sim", "simm", "yes", "s", "y", "yep" };
            form.Configuration.No = new string[] { "não", "nao", "no", "not", "n" };
            form.Message("Olá, seja bem vindo. Será um prazer atender você!");
            form.OnCompletion(async (context, pedido) =>
            {
                await context.PostAsync("Seu pedido número foi gerado");
            });

            return form.Build();
        }
    }

    [Describe("TipoEntrega")]
    public enum TipoEntrega
    {
        [Terms("Retirar No Local", "Passo aí", "Eu pego", "Retiro aí")]
        [Describe("Retirar No Local")]
        RetirarNoLocal = 1,

        [Terms("Motoboy", "Motoca", "Entrega", "Delivery")]
        [Describe("Motoboy")]
        Motoboy
    }

    [Describe("Salgadinhos")]
    public enum Salgadinhos
    {
        [Terms("Esfiha", "Esfiha", "Isfirra", "Isfira", "i")]
        [Describe("Esfiha")]
        Esfiha = 1,

        [Terms("Quibe", "Kibe", "k", "q")]
        [Describe("Quibe")]
        Quibe,

        [Terms("Coxinha", "Cochinha", "Coxa", "c")]
        [Describe("Coxinha")]
        Coxinha
    }

    [Describe("Bebidas")]
    public enum Bebidas
    {
        [Terms("Água", "agua", "h2o", "a")]
        [Describe("Água")]
        Agua = 1,

        [Terms("Refrigerante", "refri", "r")]
        [Describe("Refrigerante")]
        Refrigerante,

        [Terms("Suco", "tang", "s")]
        [Describe("Suco")]
        Suco
    }

    [Describe("CPF Na Nota")]
    public enum CPFNaNota
    {
        [Terms("Sim", "s", "yep")]
        [Describe("Sim")]
        Sim = 1,

        [Terms("Não", "Nao", "n")]
        [Describe("Não")]
        Nao
    }
}