﻿namespace Functional.Mvc.Helpers.ResultsExtensions
{
    using System.Web.Mvc;
    public abstract class ModalActionResult : ActionResult
    {
        /// <summary>
        /// Inicializa um <see cref="ModalActionResult"/>
        /// </summary>
        /// <param name="message">Uma mensagem</param>
        /// <param name="callbackData">Um resultado a ser serializado em Json</param>
        protected ModalActionResult(string message, object callbackData)
        {
            this.CallbackData = callbackData;
            this.Message = message ?? string.Empty;
        }

        /// <summary>
        /// Inicializa um <see cref="ModalActionResult"/>
        /// </summary>
        /// <param name="message">Uma mensagem</param>
        protected ModalActionResult(string message)
        {
            this.Message = message ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        protected string Message { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        protected object CallbackData { get; set; }

        /// <summary>
        /// Gets or sets the parsed data.
        /// </summary>
        /// <autogeneratedoc />
        protected string ParsedData { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var input = "<input ajax-callback-result type='hidden' value='@Data@'/>";

            if (this.CallbackData != null)
            {
                this.ProcessData(context);
            }

            input = input.Replace("@Data@", ParsedData);
            context.HttpContext.Response.Write(input);
            context.HttpContext.Response.Write($"<div data-dialog-close='true' data-dialog-result='{this.Message}' />");
        }

        /// <summary>
        /// Processa o retorno dos dados para o callback do modal
        /// </summary>
        /// <param name="context">O contexto</param>
        /// <autogeneratedoc />
        protected abstract void ProcessData(ControllerContext context);
    }
}