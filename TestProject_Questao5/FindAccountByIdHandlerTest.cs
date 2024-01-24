using AutoMapper;
using FluentAssertions;
using Microsoft.OpenApi.Any;
using Moq;
using NSubstitute;
using OneOf;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
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
    public class FindAccountByIdHandlerTest
    {
        private readonly Mock<IAccountRepository> _repository;
        private readonly Mock<IMapper> _mapper;

        public FindAccountByIdHandlerTest()
        {
            _repository = new();
            _mapper = new();
        }

        [Fact]
        public async Task Handle_Should_InvalidAccount()
        {
            // Arrange
            var command = new FindAccountByIdRequest();
            command.Id = "";

            var handler = new FindAccountByIdHandler(_repository.Object, _mapper.Object);
            
            // Act
            var result = await handler.Handle(command, default);
            result.IsT0.Should().BeFalse();
            result.IsT1.Should().BeTrue();
            result.AsT1.CodigoErro.Should().Be(TiposRetornoEnum.ConsultaContaInvalida.ToDescriptionString());
        }

        [Fact]
        public async Task Handle_Should_InactiveAccount()
        {
            // Arrange
            var command = new FindAccountByIdRequest();

            var retorno = new AccountDataResponse() { Ativo = UserStatus.Inativo };

            _repository.Setup(
                x => x.GetAccountByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(retorno);

            // Act
            var handler = new FindAccountByIdHandler(_repository.Object, _mapper.Object);

            // Assert
            var result = await handler.Handle(command, default);
            result.IsT0.Should().BeFalse();
            result.IsT1.Should().BeTrue();
            result.AsT1.CodigoErro.Should().Be(TiposRetornoEnum.ConsultaContaInativa.ToDescriptionString());
        }

        [Fact]
        public async Task Handle_Should_ReturnAccount()
        {
            // Arrange
            var command = new FindAccountByIdRequest();

            var retorno = new AccountDataResponse() { Id = Guid.NewGuid().ToString(), Conta = 12456, Titular = "Jose", Ativo = UserStatus.Ativo };

            _repository.Setup(
                x => x.GetAccountByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(retorno);

            // Act
            var handler = new FindAccountByIdHandler(_repository.Object, _mapper.Object);

            // Assert
            var result = await handler.Handle(command, default);
            result.IsT0.Should().BeTrue();
            result.IsT1.Should().BeFalse();
            result.AsT0.Successo.Should().BeTrue();
        }
    }
}
