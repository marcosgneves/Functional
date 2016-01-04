﻿namespace Functional.Domain.Services
{
    using System.Linq;

    using Functional.Domain.Entities.Model;
    using Functional.Domain.Interfaces.Repository;
    using Functional.Domain.Interfaces.Service;
    using Functional.Domain.Services.Common;
    using Functional.Domain.Validation;

    public class RequisitoService : Service<Requisito>, IRequisitoService
    {
        private readonly IRequisitoRepository _requisitoRepository;

        public RequisitoService(IRequisitoRepository repository)
            : base(repository)
        {
            this._requisitoRepository = repository;
        }

        /// <summary>
        /// Salva um projeto
        /// </summary>
        /// <param name="entity">A entidade</param>
        /// <returns>
        /// Um <see cref="ValidationResult" />
        /// </returns>
        /// <autogeneratedoc />
        public override ValidationResult Create(Requisito entity)
        {
            entity.Codigo = this.GetProximoCodigo(entity.Projeto);
            return base.Create(entity);
        }

        /// <summary>
        /// Dado um projeto, recupera o próximo código de requisito para o projeto
        /// </summary>
        /// <param name="projeto">projeto a que pertence o requisito</param>
        /// <returns>
        /// Uma string
        /// </returns>
        /// <autogeneratedoc />
        public string GetProximoCodigo(Projeto projeto)
        {
            var contagem = this._requisitoRepository.Find(x => x.ProjetoId == projeto.ProjetoId).Count();
            return $"{projeto.Codigo}-{"REQ"}-{contagem + 1}";
        }
    }
}