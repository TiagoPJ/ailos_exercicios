using AutoMapper;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.Extensao
{
    public class AdapterMapper
    {
        public static IMapper MapperRegister()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountDataResponse, FindAccountByIdResponse>(MemberList.None)
                       .ForMember(fa => fa.NumeroConta, opt => opt.MapFrom(account => account.Conta))
                       .ForMember(fa => fa.Nome, opt => opt.MapFrom(account => account.Titular))
                       .ReverseMap();

                cfg.CreateMap<MovimentDataRequest, CreateMovimentRequest>(MemberList.None)
                       .ForMember(cmv => cmv.Tipo, opt => opt.MapFrom(mdr => mdr.TipoMovimento))
                       .ReverseMap();
                
                cfg.CreateMap<MovimentDataResponse, Moviment>(MemberList.None)
                       .ForMember(cmv => cmv.DataMovimento, opt => opt.MapFrom(mov => mov.Data))
                       .ForMember(cmv => cmv.IdMovimento, opt => opt.MapFrom(mov => mov.Id))
                       .ReverseMap();                 

            });

            configuration.AssertConfigurationIsValid();
            return configuration.CreateMapper();
        }
    }
}
