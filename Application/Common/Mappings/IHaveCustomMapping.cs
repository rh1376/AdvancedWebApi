using AutoMapper;

namespace CleanArchitecture.Application.Common.Mappings;

public interface IHaveCustomMapping
{
    void CreateMappings(Profile profile);
}
