using AutoMapper;
using FluentAssertions;
using Moq;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Infrastructure.Database.Repositories.Interfaces;
using Questao5.Utils;
using Questao5.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject_Questao5
{
    public class CreateMovimentHandlerTest
    {
        private readonly Mock<IAccountRepository> _repository;
        private readonly Mock<IMapper> _mapper;

        public CreateMovimentHandlerTest()
        {
            _repository = new();
            _mapper = new();
        }

        [Fact]
        public async Task Handle_Should_InvalidTypeTransaction()
        {
            // Arrange
            var command = new CreateMovimentRequest();
            command.Tipo = "f";

            var handler = new CreateMovimentHandler(_repository.Object, _mapper.Object);

            // Act
            var result = await handler.Handle(command, default);
            result.IsT0.Should().BeFalse();
            result.IsT1.Should().BeTrue();
            result.AsT1.CodigoErro.Should().Be(TiposRetornoEnum.TipoInvalido.ToDescriptionString());
        }

        [Fact]
        public async Task Handle_Should_InvalidValue()
        {
            // Arrange
            var command = new CreateMovimentRequest();
            command.Tipo = "D";
            command.Valor = -100;

            var handler = new CreateMovimentHandler(_repository.Object, _mapper.Object);

            // Act
            var result = await handler.Handle(command, default);
            result.IsT0.Should().BeFalse();
            result.IsT1.Should().BeTrue();
            result.AsT1.CodigoErro.Should().Be(TiposRetornoEnum.ValorInvalido.ToDescriptionString());
        }

        [Fact]
        public async Task Handle_Should_InvalidAccount()
        {
            // Arrange
            var command = new CreateMovimentRequest();
            command.Tipo = "D";
            command.Valor = 400;

            var handler = new CreateMovimentHandler(_repository.Object, _mapper.Object);

            // Act
            var result = await handler.Handle(command, default);
            result.IsT0.Should().BeFalse();
            result.IsT1.Should().BeTrue();
            result.AsT1.CodigoErro.Should().Be(TiposRetornoEnum.MovimentoContaInvalida.ToDescriptionString());
        }

        [Fact]
        public async Task Handle_Should_InactiveAccount()
        {
            // Arrange
            var command = new CreateMovimentRequest();
            command.Tipo = "C";
            command.Valor = 300;

            var retorno = new AccountDataResponse() { Ativo = UserStatus.Inativo };

            _repository.Setup(
                x => x.GetAccountByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(retorno);

            // Act
            var handler = new CreateMovimentHandler(_repository.Object, _mapper.Object);

            // Assert
            var result = await handler.Handle(command, default);
            result.IsT0.Should().BeFalse();
            result.IsT1.Should().BeTrue();
            result.AsT1.CodigoErro.Should().Be(TiposRetornoEnum.MovimentoContaInativa.ToDescriptionString());
        }
    }
}
